// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CommandLine;
using Microsoft.CodeAnalysis.ErrorReporting;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.CommandLine;

using Thisc = Luac;
using ThisCompiler = LuaCompiler;
using ThisCommandLineParser = LuaCommandLineParser;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.CommandLine;

using Thisc = Moonc;
using ThisCompiler = MoonScriptCompiler;
using ThisCommandLineParser = MoonScriptCommandLineParser;
#endif

/// <summary>
/// 此类表示一个<see cref="ThisCompiler"/>的实现。
/// </summary>
internal sealed class
#if LANG_LUA
    Luac
#elif LANG_MOONSCRIPT
    Moonc
#endif
    : ThisCompiler
{
    /// <summary>
    /// 初始化编译器的新实例。
    /// </summary>
    /// <param name="responseFile">响应文件路径，若不指定则传入<see langword="null"/>。</param>
    /// <param name="buildPaths">生成所需的各个路径。</param>
    /// <param name="args">尚未解析的命令行参数。</param>
    /// <param name="analyzerLoader">分析器程序集加载器，用于控制如何加载分析器程序集。</param>
    internal
#if LANG_LUA
        Luac
#elif LANG_MOONSCRIPT
        Moonc
#endif
    (string? responseFile, BuildPaths buildPaths, string[] args, IAnalyzerAssemblyLoader analyzerLoader)
        : base(ThisCommandLineParser.Default, responseFile, args, buildPaths, Environment.GetEnvironmentVariable("LIB"), analyzerLoader)
    {
    }

    /// <summary>
    /// 编译器的运行入口静态方法。创建编译器的新实例并调用其<see cref="CommonCompiler.Run(TextWriter, CancellationToken)"/>方法。
    /// </summary>
    /// <param name="args">尚未解析的命令行参数。</param>
    /// <param name="buildPaths">生成所需的各个路径。</param>
    /// <param name="textWriter">控制台输出目标。</param>
    /// <param name="analyzerLoader">分析器程序集加载器，用于控制如何加载分析器程序集。</param>
    /// <returns>程序返回码。</returns>
    internal static int Run(string[] args, BuildPaths buildPaths, TextWriter textWriter, IAnalyzerAssemblyLoader analyzerLoader)
    {
        FatalError.Handler = FailFast.Handler;

        var responseFile = Path.Combine(buildPaths.ClientDirectory, ThisCompiler.ResponseFileName);
        var compiler = new Thisc(responseFile, buildPaths, args, analyzerLoader);
        return ConsoleUtil.RunWithUtf8Output(compiler.Arguments.Utf8Output, textWriter, tw => compiler.Run(tw));
    }
}
