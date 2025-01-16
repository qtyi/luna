// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// The command line arguments to a(n) <see cref="ThisCompiler"/>.
/// </summary>
public sealed class
#if LANG_LUA
    LuaCommandLineArguments
#elif LANG_MOONSCRIPT
    MoonScriptCommandLineArguments
#endif
    : CommandLineArguments
{
    /// <summary>
    /// Gets the compilation options for the <see cref="ThisCompilation"/>
    /// created from the <see cref="ThisCompiler"/>.
    /// </summary>
    /// <value>
    /// The compilation options for the <see cref="ThisCompilation"/>.
    /// </value>
    public new ThisCompilationOptions CompilationOptions { get; internal set; }

    /// <summary>
    /// Gets the parse options for the <see cref="ThisCompilation"/>.
    /// </summary>
    /// <value>
    /// The parse options for the <see cref="ThisCompilation"/>.
    /// </value>
    public new ThisParseOptions ParseOptions { get; internal set; }

    /// <summary>
    /// Should the format of error messages include the line and column of
    /// the end of the offending text.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if the line and column of the end of the
    /// offending text should be included; otherwise, <see langword="false"/>.
    /// </value>
    internal bool ShouldIncludeErrorEndLocation { get; set; }

    /// <remarks>
    /// Always initialized by <see cref="ThisCommandLineParser.Parse(IEnumerable{string}, string?, string?, string?)"/>.
    /// </remarks>
#pragma warning disable CS8618
    internal
#if LANG_LUA
        LuaCommandLineArguments
#elif LANG_MOONSCRIPT
        MoonScriptCommandLineArguments
#endif
    ()
    { }
#pragma warning restore CS8618

    #region CommandLineArguments
    protected override Microsoft.CodeAnalysis.CompilationOptions CompilationOptionsCore => CompilationOptions;
    protected override Microsoft.CodeAnalysis.ParseOptions ParseOptionsCore => ParseOptions;
    #endregion
}
