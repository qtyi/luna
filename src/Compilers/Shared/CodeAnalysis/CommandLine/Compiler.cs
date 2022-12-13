// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCommandLineArguments = LuaCommandLineArguments;
using ThisCommandLineParser = LuaCommandLineParser;
using ThisCompiler = LuaCompiler;
using ThisCompilation = LuaCompilation;
using ThisParseOptions = LuaParseOptions;
using ThisSyntaxTree = LuaSyntaxTree;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCommandLineArguments = MoonScriptCommandLineArguments;
using ThisCommandLineParser = MoonScriptCommandLineParser;
using ThisCompiler = MoonScriptCompiler;
using ThisCompilation = MoonScriptCompilation;
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
    internal const string ResponseFileName = ExecutableName + ".rsp";

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
    /// <param name="consoleOutput">控制台输出。</param>
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
            return ThisCompiler.ParseFile(parseOptions, scriptParseOptions, content, file);
        }
    }

    private static ThisSyntaxTree ParseFile(
        ThisParseOptions parseOptions,
        ThisParseOptions scriptParseOptions,
        SourceText content,
        CommandLineSourceFile file)
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
