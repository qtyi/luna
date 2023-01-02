// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Diagnostics;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Collections;
using MSCA::Microsoft.CodeAnalysis.Diagnostics;
using MSCA::Microsoft.CodeAnalysis.Text;
using MSCA::Roslyn.Utilities;

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
/// 此类表示Lua编译器的基类。此类必须被继承。
/// </summary>
#elif LANG_MOONSCRIPT
/// <summary>
/// 此类表示MoonScript编译器的基类。此类必须被继承。
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
    /// <summary>响应文件名称。</summary>
    internal const string ResponseFileName = ThisCompiler.ExecutableName + ".rsp";

    /// <summary>命令行诊断格式化器。</summary>
    private readonly CommandLineDiagnosticFormatter _diagnosticFormatter;
    /// <summary>临时文件夹。</summary>
    private readonly string? _tempDirectory;

    /// <summary>
    /// 获取命令行诊断格式化器。
    /// </summary>
    /// <value>
    /// 命令行诊断格式化器。
    /// </value>
    public override DiagnosticFormatter DiagnosticFormatter => this._diagnosticFormatter;

    /// <summary>
    /// 获取命令行参数。
    /// </summary>
    /// <value>
    /// 命令行参数。
    /// </value>
    protected internal new ThisCommandLineArguments Arguments => (ThisCommandLineArguments)base.Arguments;

    /// <summary>
    /// 获取用于在<c>/help</c>和<c>/version</c>选项中提供版本信息的编译器类的类型。
    /// </summary>
    /// <value>
    /// 编译器类的类型。
    /// </value>
    /// <remarks>
    /// 我们并不使用<see cref="object.GetType"/>的返回值，因为这样会使模拟的子类的功能失效。
    /// </remarks>
    internal override Type Type => typeof(ThisCompiler);

    /// <param name="parser">命令行解析器，用于解析命令行参数。</param>
    /// <param name="responseFile">响应文件路径，若不指定则传入<see langword="null"/>。</param>
    /// <param name="args">尚未解析的命令行参数，可能仍保留一些供<paramref name="parser"/>识别的语法信息。</param>
    /// <param name="buildPaths">生成所需的各个路径。</param>
    /// <param name="additionalReferenceDirectories">所有附加引用文件夹，当有多个文件夹时使用能被<paramref name="parser"/>识别的格式组合。</param>
    /// <param name="assemblyLoader">分析器程序集加载器，用于控制如何加载分析器程序集。</param>
    /// <param name="driverCache">生成器启动器缓存，用于获取或设置<see cref="GeneratorDriver"/>。</param>
    /// <param name="fileSystem">编译器文件系统，用于编译期间的模拟或真实文件操作。</param>
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
    /// 创建编译内容。
    /// </summary>
    /// <param name="consoleOutput">控制台输出目标。</param>
    /// <param name="touchedFilesLogger">记录编译期间所有使用过的文件的记录器。</param>
    /// <param name="errorLogger">记录编译期间所有报告的错误的记录器。</param>
    /// <param name="analyzerConfigOptions">所有的分析器配置选项。</param>
    /// <param name="globalConfigOptions">全局的分析器配置选项。</param>
    /// <returns>本次的编译内容。</returns>
    public override Compilation? CreateCompilation(
        TextWriter consoleOutput,
        TouchedFileLogger? touchedFilesLogger,
        ErrorLogger? errorLogger,
        ImmutableArray<AnalyzerConfigOptionsResult> analyzerConfigOptions,
        AnalyzerConfigOptionsResult globalConfigOptions)
    {
        var parseOptions = this.Arguments.ParseOptions;
        var scriptParseOptions = parseOptions.WithKind(SourceCodeKind.Script);

        var hasErrors = false;

        var sourceFiles = this.Arguments.SourceFiles;
        var trees = new ThisSyntaxTree?[sourceFiles.Length];
        var normalizedFilePaths = new string?[sourceFiles.Length];
        var diagnosticBag = DiagnosticBag.GetInstance();

        if (this.Arguments.CompilationOptions.ConcurrentBuild) // 并发编译
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

        // 报告在ParseFile中可能产生的诊断错误。
        if (this.ReportDiagnostics(diagnosticBag.ToReadOnlyAndFree(), consoleOutput, errorLogger, compilation: null))
        {
            // 诊断错误报告成功，在此退出不再继续后续操作。
            Debug.Assert(hasErrors);
            return null;
        }

        var diagnostics = new List<DiagnosticInfo>();
        var uniqueFilePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // 检查重复包含源文件。
        for (var i = 0; i < sourceFiles.Length; i++)
        {
            var normalizedFilePath = normalizedFilePaths[i];
            Debug.Assert(normalizedFilePath is not null);
            Debug.Assert(sourceFiles[i].IsInputRedirected || PathUtilities.IsAbsolute(normalizedFilePath));

            if (!uniqueFilePaths.Add(normalizedFilePath))
            {
                // 重复包含源文件警告。
                diagnostics.Add(new DiagnosticInfo(
                    this.MessageProvider,
                    (int)ErrorCode.WRN_FileAlreadyIncluded,
                    this.Arguments.PrintFullPaths ? normalizedFilePath : this._diagnosticFormatter.RelativizeNormalizedPath(normalizedFilePath)));
            }
        }

        // 记录已使用的源文件。
        if (this.Arguments.TouchedFilesPath is not null)
        {
            Debug.Assert(touchedFilesLogger is not null);
            foreach (var path in uniqueFilePaths)
                touchedFilesLogger.AddRead(path);
        }

        var assemblyIdentityComparer = DesktopAssemblyIdentityComparer.Default;
        // 从app.config文件中获取（可能存在的）信息来创建程序集身份比较器。
        var appConfigPath = this.Arguments.AppConfigPath;
        if (appConfigPath is not null)
        {
            try
            {
                using var appConfigStream = new FileStream(appConfigPath, FileMode.Open, FileAccess.Read);
                assemblyIdentityComparer = DesktopAssemblyIdentityComparer.LoadFromXml(appConfigStream);

                // 记录已使用的app.config文件。
                if (touchedFilesLogger is not null)
                    touchedFilesLogger.AddRead(appConfigPath);
            }
            catch (Exception ex)
            {
                // 无法读取app.config文件。
                diagnostics.Add(new DiagnosticInfo(this.MessageProvider, (int)ErrorCode.ERR_CantReadConfigFile, appConfigPath, ex.Message));
            }
        }

        var xmlFileResolver = new LoggingXmlFileResolver(this.Arguments.BaseDirectory, touchedFilesLogger);
        var sourceFileResolver = new LoggingSourceFileResolver(ImmutableArray<string>.Empty, this.Arguments.BaseDirectory, this.Arguments.PathMap, touchedFilesLogger);

        // 决定元数据引用。
        var resolvedReferences = this.ResolveMetadataReferences(diagnostics, touchedFilesLogger, out var referenceDirectiveResolver);

        // 到此进行最后一次诊断错误报告。
        if (this.ReportDiagnostics(diagnostics, consoleOutput, errorLogger, compilation: null))
            // 诊断错误报告成功，在此退出不再继续后续操作。
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

        void ParseFileAt(int index)
        {
            // 注：无需注意语法树的先后顺序！
            trees[index] = this.ParseFile(
                parseOptions,
                scriptParseOptions,
                ref hasErrors,
                sourceFiles[index],
                diagnosticBag,
                out normalizedFilePaths[index]);
        }
    }

    private ThisSyntaxTree? ParseFile(
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

        ThisSyntaxTree ParseSyntaxTree()
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
    /// 获取输出文件名称。
    /// </summary>
    /// <param name="compilation">若命令行参数中未指定输出文件名称，则根据编译内容的相关符号信息决定。</param>
    /// <param name="cancellationToken">取消操作的标志。</param>
    /// <returns>编译输出的文件名称，文件后缀名为<c>.exe</c>；若失败，则报告一个错误。</returns>
    /// <remarks>
    /// <para>若<see cref="CommandLineArguments.OutputFileName"/>已明确指定，则返回此文件名；</para>
    /// <para>若编译内容为脚本，则返回脚本所在的文件名；</para>
    /// <para>若编译内容包含程序入口点，则返回入口点所在的文件名。</para>
    /// </remarks>
    protected override string GetOutputFileName(Compilation compilation, CancellationToken cancellationToken)
    {
        // 若已明确指定输出文件名。
        if (this.Arguments.OutputFileName is not null) return this.Arguments.OutputFileName;

        Debug.Assert(this.Arguments.CompilationOptions.OutputKind.IsApplication());

        var comp = (ThisCompilation)compilation;
        // 先查看表示脚本的类是否存在。
        Symbol? entryPoint = comp.ScriptClass;
        if (entryPoint is null)
        {
            var method = comp.GetEntryPoint(cancellationToken);
            if (method is null)
                // 未找到入口点，报告一个错误，同时此编译内容不会被发射。
                return "error";

            entryPoint = method;
        }

        // 获取入口点符号所在的源文件路径。
        var entryPointFileName = PathUtilities.GetFileName(entryPoint.Locations.First().SourceTree!.FilePath);
        return Path.ChangeExtension(entryPointFileName, ".exe");
    }

    /// <summary>
    /// 命令行中是否有抑制默认的回响文件的选项。
    /// </summary>
    /// <param name="args">命令行选项。</param>
    /// <returns>若<paramref name="args"/>中有抑制默认的回响文件的选项则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal override bool SuppressDefaultResponseFile(IEnumerable<string> args)
    {
        return args.Any(arg => new[] { "/noconfig", "-noconfig" }.Contains(arg.ToLowerInvariant()));
    }

    /// <summary>
    /// 打印编译器徽标。
    /// </summary>
    /// <param name="consoleOutput">控制台输出目标。</param>
    public override void PrintLogo(TextWriter consoleOutput)
    {
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LogoLine1, this.Culture), this.GetToolName(), this.GetCompilerVersion());
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LogoLine2, this.Culture));
        consoleOutput.WriteLine();
    }

    /// <summary>
    /// 打印支持的语言版本。
    /// </summary>
    /// <param name="consoleOutput">控制台输出目标。</param>
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
    /// 获取工具名称。
    /// </summary>
    /// <returns>此编译器工具的名称。</returns>
    internal override string GetToolName()
    {
        return ErrorFacts.GetMessage(MessageID.IDS_ToolName, this.Culture);
    }

    /// <summary>
    /// 打印命令行帮助信息（一行80个西文字符）。
    /// </summary>
    /// <param name="consoleOutput">控制台输出目标。</param>
    public override void PrintHelp(TextWriter consoleOutput)
    {
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_CSCHelp, this.Culture));
    }

    /// <summary>
    /// 尝试获取指定文本表示的编译器诊断码。
    /// </summary>
    /// <param name="diagnosticId">编译器诊断码的文本表示。</param>
    /// <param name="code">与<paramref name="diagnosticId"/>对应的值。</param>
    /// <returns>若获取成功则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    protected override bool TryGetCompilerDiagnosticCode(string diagnosticId, out uint code)
    {
        return CommonCompiler.TryGetCompilerDiagnosticCode(diagnosticId, ThisMessageProvider.ErrorCodePrefix, out code);
    }

    /// <summary>
    /// 从命令行参数中决定分析器。
    /// </summary>
    /// <param name="diagnostics">诊断消息列表。</param>
    /// <param name="messageProvider">信息提供器。</param>
    /// <param name="skipAnalyzers">是否跳过分析器。</param>
    /// <param name="analyzers">决定的分析器。</param>
    /// <param name="generators">决定的生成器。</param>
    protected override partial void ResolveAnalyzersFromArguments(List<DiagnosticInfo> diagnostics, CommonMessageProvider messageProvider, bool skipAnalyzers, out ImmutableArray<DiagnosticAnalyzer> analyzers, out ImmutableArray<ISourceGenerator> generators);

    /// <summary>
    /// 从外部源指令中决定内置的文件。
    /// </summary>
    /// <remarks>
    /// 由于基于Lua的语言均不支持指令，因此此方法不做任何事。
    /// </remarks>
    protected sealed override void ResolveEmbeddedFilesFromExternalSourceDirectives(SyntaxTree tree, SourceReferenceResolver resolver, OrderedSet<string> embeddedFiles, DiagnosticBag diagnostics)
    {
        return;
    }

    /// <summary>
    /// 创建生成器启动器的新实例。
    /// </summary>
    /// <param name="parseOptions">解析器选项。</param>
    /// <param name="generators">所有的源生成器。</param>
    /// <param name="analyzerConfigOptionsProvider">分析器配置选项提供器。</param>
    /// <param name="additionalTexts">创建实例操作所需的附加文本（非源文件）。</param>
    /// <returns></returns>
    private protected override GeneratorDriver CreateGeneratorDriver(ParseOptions parseOptions, ImmutableArray<ISourceGenerator> generators, AnalyzerConfigOptionsProvider analyzerConfigOptionsProvider, ImmutableArray<AdditionalText> additionalTexts)
    {
        return ThisGeneratorDriver.Create(generators, additionalTexts, (ThisParseOptions)parseOptions, analyzerConfigOptionsProvider);
    }
}
