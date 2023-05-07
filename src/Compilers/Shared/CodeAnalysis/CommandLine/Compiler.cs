// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCommandLineArguments = LuaCommandLineArguments;
using ThisCommandLineParser = LuaCommandLineParser;
using ThisCompiler = LuaCompiler;
using ThisCompilation = LuaCompilation;
using ThisGeneratorDriver = LuaGeneratorDriver;
using ThisMessageProvider = MessageProvider;
using ThisParseOptions = LuaParseOptions;
using ThisSyntaxTree = LuaSyntaxTree;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCommandLineArguments = MoonScriptCommandLineArguments;
using ThisCommandLineParser = MoonScriptCommandLineParser;
using ThisCompiler = MoonScriptCompiler;
using ThisCompilation = MoonScriptCompilation;
using ThisGeneratorDriver = MoonScriptGeneratorDriver;
using ThisMessageProvider = MessageProvider;
using ThisParseOptions = MoonScriptParseOptions;
using ThisSyntaxTree = MoonScriptSyntaxTree;
#endif

#if LANG_LUA
/// <summary>
/// Provide a base class for Lua compiler.
/// </summary>
#elif LANG_MOONSCRIPT
/// <summary>
/// Provide a base class for MoonScript compiler.
/// </summary>
#endif
internal abstract partial class
#if LANG_LUA
    LuaCompiler
#elif LANG_MOONSCRIPT
    MoonScriptCompiler
#endif
    : CommonCompiler
{
    /// <summary>Compiler response file name.</summary>
    internal const string ResponseFileName = ThisCompiler.ExecutableName + ".rsp";

    /// <summary>A command line diagnostic formatter.</summary>
    private readonly CommandLineDiagnosticFormatter _diagnosticFormatter;
    /// <summary>Directory path for storing temporary files.</summary>
    private readonly string? _tempDirectory;

    /// <summary>
    /// Gets the diagnostic formatter for the <see cref="ThisCompiler"/>.
    /// </summary>
    /// <value>
    /// An object that formats <see cref="Diagnostic"/> messages.
    /// </value>
    public override DiagnosticFormatter DiagnosticFormatter => this._diagnosticFormatter;

    /// <summary>
    /// Gets the command line arguments to the <see cref="ThisCompiler"/>.
    /// </summary>
    /// <value>
    /// An object that represents command line arguments.
    /// </value>
    protected internal new ThisCommandLineArguments Arguments => (ThisCommandLineArguments)base.Arguments;

    /// <summary>
    /// Gets the <see cref="System.Type"/> of the compiler for providing information in <c>/help</c> and <c>/version</c> switches.
    /// </summary>
    /// <value>
    /// An <see cref="System.Type"/> object of <see cref="ThisCompiler"/>.
    /// </value>
    /// <remarks>
    /// We do not use <see cref="object.GetType"/> so that we don't break mock subtypes
    /// </remarks>
    internal override Type Type => typeof(ThisCompiler);

    /// <param name="parser">A command line parser for providing command line arguments.</param>
    /// <param name="responseFile">The response file path, or <see langword="null"/> if none.</param>
    /// <param name="args">Raw command line string.</param>
    /// <param name="buildPaths">The paths to be used during build stage.</param>
    /// <param name="additionalReferenceDirectories">Additional reference directories.</param>
    /// <param name="assemblyLoader">The assembly loader that handles loading analyzer assemblies and their dependencies.</param>
    /// <param name="driverCache">The generator driver cache to gets/sets <see cref="GeneratorDriver"/>.</param>
    /// <param name="fileSystem">THe compiler file system.</param>
    protected
#if LANG_LUA
        LuaCompiler
#elif LANG_MOONSCRIPT
        MoonScriptCompiler
#endif
    (
        ThisCommandLineParser parser,
        string? responseFile,
        string[] args,
        BuildPaths buildPaths,
        string? additionalReferenceDirectories,
        IAnalyzerAssemblyLoader assemblyLoader,
        GeneratorDriverCache? driverCache = null,
        ICommonCompilerFileSystem? fileSystem = null)
        : base(parser, responseFile, args, buildPaths, additionalReferenceDirectories, assemblyLoader, driverCache, fileSystem)
    {
        this._diagnosticFormatter = new(buildPaths.WorkingDirectory, this.Arguments.PrintFullPaths, this.Arguments.ShouldIncludeErrorEndLocation);
        this._tempDirectory = buildPaths.TempDirectory;
    }

    /// <summary>
    /// Create a compilation.
    /// </summary>
    /// <param name="consoleOutput">The console output target.</param>
    /// <param name="touchedFilesLogger">A logger for logging all the paths which are "touched" (used).</param>
    /// <param name="errorLogger">A logger for logging compiler diagnostics.</param>
    /// <param name="analyzerConfigOptions">A collection of analyzer config options.</param>
    /// <param name="globalConfigOptions">The global analyzer config options</param>
    /// <returns>本次的编译内容。</returns>
    public override Compilation? CreateCompilation(
        TextWriter consoleOutput,
        TouchedFileLogger? touchedFilesLogger,
        ErrorLogger? errorLogger,
        ImmutableArray<AnalyzerConfigOptionsResult> analyzerConfigOptions,
        AnalyzerConfigOptionsResult globalConfigOptions)
    {
        var parseOptions = this.Arguments.ParseOptions;

        // We compute script parse options once so we don't have to do it repeatedly in
        // case there are many script files.
        var scriptParseOptions = parseOptions.WithKind(SourceCodeKind.Script);

        var hasErrors = false;

        var sourceFiles = this.Arguments.SourceFiles;
        var trees = new SyntaxTree?[sourceFiles.Length];
        var normalizedFilePaths = new string?[sourceFiles.Length];
        var diagnosticBag = DiagnosticBag.GetInstance();

        if (this.Arguments.CompilationOptions.ConcurrentBuild) // concurrent build.
        {
            RoslynParallel.For(
                0,
                sourceFiles.Length,
                UICultureUtilities.WithCurrentUICulture<int>(ParseFileAt),
                CancellationToken.None
            );
        }
        else
        {
            for (var i = 0; i < sourceFiles.Length; i++)
                ParseFileAt(i);
        }

        // If errors had been reported in ParseFile, while trying to read files, then we should simply exit.
        if (this.ReportDiagnostics(diagnosticBag.ToReadOnlyAndFree(), consoleOutput, errorLogger, compilation: null))
        {
            Debug.Assert(hasErrors);
            return null;
        }

        var diagnostics = new List<DiagnosticInfo>();
        var uniqueFilePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Check duplicate source files.
        for (var i = 0; i < sourceFiles.Length; i++)
        {
            var normalizedFilePath = normalizedFilePaths[i];
            Debug.Assert(normalizedFilePath is not null);
            Debug.Assert(sourceFiles[i].IsInputRedirected || PathUtilities.IsAbsolute(normalizedFilePath));

            if (!uniqueFilePaths.Add(normalizedFilePath))
            {
                // Duplicate source files warning: Source file '{0}' specified multiple times
                diagnostics.Add(new DiagnosticInfo(
                    this.MessageProvider,
                    (int)ErrorCode.WRN_FileAlreadyIncluded,
                    this.Arguments.PrintFullPaths ? normalizedFilePath : this._diagnosticFormatter.RelativizeNormalizedPath(normalizedFilePath)));
            }
        }

        // Log files that are touched.
        if (this.Arguments.TouchedFilesPath is not null)
        {
            Debug.Assert(touchedFilesLogger is not null);
            foreach (var path in uniqueFilePaths)
                touchedFilesLogger.AddRead(path);
        }

        var assemblyIdentityComparer = DesktopAssemblyIdentityComparer.Default;
        // Read app.config, if exist, to create assembly identity comparer.
        var appConfigPath = this.Arguments.AppConfigPath;
        if (appConfigPath is not null)
        {
            try
            {
                using var appConfigStream = new FileStream(appConfigPath, FileMode.Open, FileAccess.Read);
                assemblyIdentityComparer = DesktopAssemblyIdentityComparer.LoadFromXml(appConfigStream);

                // Log touched app.config.
                if (touchedFilesLogger is not null)
                    touchedFilesLogger.AddRead(appConfigPath);
            }
            catch (Exception ex)
            {
                // Cannot read app.config.
                diagnostics.Add(new DiagnosticInfo(this.MessageProvider, (int)ErrorCode.ERR_CantReadConfigFile, appConfigPath, ex.Message));
            }
        }

        var xmlFileResolver = new LoggingXmlFileResolver(this.Arguments.BaseDirectory, touchedFilesLogger);
        var sourceFileResolver = new LoggingSourceFileResolver(ImmutableArray<string>.Empty, this.Arguments.BaseDirectory, this.Arguments.PathMap, touchedFilesLogger);

        // Resolve metadata references.
        var resolvedReferences = this.ResolveMetadataReferences(diagnostics, touchedFilesLogger, out var referenceDirectiveResolver);

        // Do the final diagnostics report.
        if (this.ReportDiagnostics(diagnostics, consoleOutput, errorLogger, compilation: null))
            // Report diagnostics success, and then we exit.
            return null;

        var loggingFileSystem = new LoggingStrongNameFileSystem(touchedFilesLogger, this._tempDirectory);
        var optionsProvider = new CompilerSyntaxTreeOptionsProvider(trees, analyzerConfigOptions, globalConfigOptions);

        // 创建编译内容。
        return ThisCompilation.Create(
            this.Arguments.CompilationName,
            trees.WhereNotNull(),
            resolvedReferences,
            this.Arguments.CompilationOptions
                .WithMetadataReferenceResolver(referenceDirectiveResolver)
                .WithAssemblyIdentityComparer(assemblyIdentityComparer)
                .WithXmlReferenceResolver(xmlFileResolver)
                .WithStrongNameProvider(this.Arguments.GetStrongNameProvider(loggingFileSystem))
                .WithSourceReferenceResolver(sourceFileResolver)
                .WithSyntaxTreeOptionsProvider(optionsProvider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void ParseFileAt(int index)
        {
            // NOTE: order of trees is important!!
            trees[index] = this.ParseFile(
                parseOptions,
                scriptParseOptions,
                ref hasErrors,
                sourceFiles[index],
                diagnosticBag,
                out normalizedFilePaths[index]);
        }
    }

    /// <summary>
    /// Parse a single source file as a syntax tree.
    /// </summary>
    /// <param name="parseOptions">The parse options, used when <paramref name="file"/> is not a script source file.</param>
    /// <param name="scriptParseOptions">The parse options, used when <paramref name="file"/> is a script source file.</param>
    /// <param name="addedDiagnostics">Whether more diagnostics are added.</param>
    /// <param name="file">The file to be parsed.</param>
    /// <param name="diagnostics">The diagnostic bag we add diagnostics to.</param>
    /// <param name="normalizedFilePath">Normalized path of <paramref name="file"/>.</param>
    /// <returns>A syntax tree object which represent the content of a source file.</returns>
    private SyntaxTree? ParseFile(
        ThisParseOptions parseOptions,
        ThisParseOptions scriptParseOptions,
        ref bool addedDiagnostics,
        CommandLineSourceFile file,
        DiagnosticBag diagnostics,
        out string? normalizedFilePath)
    {
        var fileDiagnostics = new List<DiagnosticInfo>();
        var content = this.TryReadFileContent(file, fileDiagnostics, out normalizedFilePath);

        if (content == null)
        {
            foreach (var info in fileDiagnostics)
                diagnostics.Add(MessageProvider.CreateDiagnostic(info));

            fileDiagnostics.Clear();
            addedDiagnostics = true;
            return null;
        }
        else
        {
            Debug.Assert(fileDiagnostics.Count == 0);
            return ParseSyntaxTree();
        }

        SyntaxTree ParseSyntaxTree()
        {
            var tree = SyntaxFactory.ParseSyntaxTree(
                content,
                file.IsScript ? scriptParseOptions : parseOptions,
                file.Path);

            // prepopulate line tables.
            // we will need line tables anyways and it is better to not wait until we are in emit
            // where things run sequentially.
            tree.GetMappedLineSpanAndVisibility(default, out _);

            return tree;
        }
    }

    /// <summary>
    /// Given a compilation and a destination directory, determine three names:
    ///   1) The name with which the assembly should be output.
    ///   2) The path of the assembly/module file.
    ///   3) The path of the pdb file.
    /// </summary>
    /// <param name="compilation">Used to determine output file name if not specified by command line argument.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>编译输出的文件名称，文件后缀名为<c>.exe</c>；若失败，则报告一个错误。</returns>
    /// <remarks>
    /// When csc produces an executable, but the name of the resulting assembly
    /// is not specified using the <c>/out</c> switch, the name is taken from the name
    /// of the file (note: file, not class) containing the assembly entrypoint
    /// (as determined by binding and the <c>/main</c> switch).
    /// </remarks>
    protected override string GetOutputFileName(Compilation compilation, CancellationToken cancellationToken)
    {
        // If output file name is determined by command line argument.
        if (this.Arguments.OutputFileName is not null) return this.Arguments.OutputFileName;

        Debug.Assert(this.Arguments.CompilationOptions.OutputKind.IsApplication());

        var comp = (ThisCompilation)compilation;
        // Check if implicit script class is defined in this compilation.
        Symbol? entryPoint = comp.ScriptClass;
        if (entryPoint is null)
        {
            var method = comp.GetEntryPoint(cancellationToken);
            if (method is null)
                // no entrypoint found - an error will be reported and the compilation won't be emitted
                return "error";

            entryPoint = method.PartialImplementationPart ?? method;
        }

        // Get source file name which contains entry point symbol.
        var entryPointFileName = PathUtilities.GetFileName(entryPoint.Locations.First().SourceTree!.FilePath);
        return Path.ChangeExtension(entryPointFileName, ".exe");
    }

    /// <summary>
    /// Whether there is a switch which suppress the default response file。
    /// </summary>
    /// <param name="args">All command line arguments.</param>
    /// <returns><see langword="true"/> if there is a switch in <paramref name="args"/> which suppress the default response file; otherwise, <see langword="false"/>.</returns>
    internal override bool SuppressDefaultResponseFile(IEnumerable<string> args)
    {
        return args.Any(arg => new[] { "/noconfig", "-noconfig" }.Contains(arg.ToLowerInvariant()));
    }

    /// <summary>
    /// Print compiler logo.
    /// </summary>
    /// <param name="consoleOutput">The console output target.</param>
    public override void PrintLogo(TextWriter consoleOutput)
    {
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LogoLine1, this.Culture), this.GetToolName(), this.GetCompilerVersion());
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LogoLine2, this.Culture));
        consoleOutput.WriteLine();
    }

    /// <summary>
    /// Print language versions.
    /// </summary>
    /// <param name="consoleOutput">The console output target.</param>
    public override void PrintLangVersions(TextWriter consoleOutput)
    {
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LangVersions, this.Culture));

        var defaultVersion = LanguageVersion.Default.MapSpecifiedToEffectiveVersion();
        var latestVersion = LanguageVersion.Latest.MapSpecifiedToEffectiveVersion();
        foreach (var v in (LanguageVersion[])Enum.GetValues(typeof(LanguageVersion)))
        {
            if (v == defaultVersion)
                consoleOutput.WriteLine($"{v.ToDisplayString()} (default)");
            else if (v == latestVersion)
                consoleOutput.WriteLine($"{v.ToDisplayString()} (latest)");
            else
                consoleOutput.WriteLine(v.ToDisplayString());
        }
        consoleOutput.WriteLine();
    }

    /// <summary>
    /// Get compiler tool name.
    /// </summary>
    /// <returns>The compiler tool name.</returns>
    internal override string GetToolName()
    {
        return ErrorFacts.GetMessage(MessageID.IDS_ToolName, this.Culture);
    }

    /// <summary>
    /// Print Commandline help message (up to 80 English characters per line)
    /// </summary>
    /// <param name="consoleOutput">The console output target.</param>
    public override partial void PrintHelp(TextWriter consoleOutput);

    /// <summary>
    /// Get the compiler diagnostic code with associated ID string.
    /// </summary>
    /// <param name="diagnosticId">The diagnostic ID.</param>
    /// <param name="code">When this method returns, contains the compiler diagnostic code with associated <paramref name="diagnosticId"/>, if the ID is found.</param>
    /// <returns><see langword="true"/> if the diagnostic ID be found; otherwise, <see langword="false"/>.</returns>
    protected override bool TryGetCompilerDiagnosticCode(string diagnosticId, out uint code)
    {
        return CommonCompiler.TryGetCompilerDiagnosticCode(diagnosticId, ThisMessageProvider.ErrorCodePrefix, out code);
    }

    /// <summary>
    /// Resolve analyzers from command line arguments.
    /// </summary>
    /// <param name="diagnostics">A collection of diagnostics.</param>
    /// <param name="messageProvider">An object that classify and load messages for error codes.</param>
    /// <param name="skipAnalyzers">Whether skip analyzers.</param>
    /// <param name="analyzers">The diagnostic analyzer passed into.</param>
    /// <param name="generators">The source generator passed into.</param>
    protected override partial void ResolveAnalyzersFromArguments(
        List<DiagnosticInfo> diagnostics,
        CommonMessageProvider messageProvider,
        bool skipAnalyzers,
        out ImmutableArray<DiagnosticAnalyzer> analyzers,
        out ImmutableArray<ISourceGenerator> generators);

    /// <summary>
    /// Resolve embedded files from external source directives.
    /// </summary>
    /// <remarks>
    /// Do nothing as language does not support directives.
    /// </remarks>
    protected sealed override void ResolveEmbeddedFilesFromExternalSourceDirectives(
        SyntaxTree tree,
        SourceReferenceResolver resolver,
        OrderedSet<string> embeddedFiles,
        DiagnosticBag diagnostics)
    {
        return;
    }

    /// <summary>
    /// Create a new <see cref="GeneratorDriver"/> instance.
    /// </summary>
    /// <param name="parseOptions">The parse options.</param>
    /// <param name="generators">A collection of source generators to drive.</param>
    /// <param name="analyzerConfigOptionsProvider">An object that provide options from an analyzer config file keyed on a source file.</param>
    /// <param name="additionalTexts">Additional text files (not source files).</param>
    /// <returns>A new <see cref="GeneratorDriver"/> instance.</returns>
    private protected override GeneratorDriver CreateGeneratorDriver(
        ParseOptions parseOptions,
        ImmutableArray<ISourceGenerator> generators,
        AnalyzerConfigOptionsProvider analyzerConfigOptionsProvider,
        ImmutableArray<AdditionalText> additionalTexts)
    {
        return ThisGeneratorDriver.Create(generators, additionalTexts, (ThisParseOptions)parseOptions, analyzerConfigOptionsProvider);
    }
}
