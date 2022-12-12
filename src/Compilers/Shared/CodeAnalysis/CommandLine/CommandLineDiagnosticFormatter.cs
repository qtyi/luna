// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnosticFormatter = LuaDiagnosticFormatter;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnosticFormatter = MoonScriptDiagnosticFormatter;
#endif

internal sealed partial class CommandLineDiagnosticFormatter : ThisDiagnosticFormatter
{
    /// <summary>基地址。</summary>
    private readonly string _baseDirectory;
    /// <summary>标准化的基地址。</summary>
    private readonly Lazy<string?> _lazyNormalizedBaseDirectory;
    /// <summary>是否显示文件完整路径。</summary>
    private readonly bool _displayFullPaths;
    /// <summary>是否显示结尾位置。</summary>
    private readonly bool _displayEndLocations;

    /// <param name="baseDirectory">基地址。</param>
    /// <param name="displayFullPaths">是否显示文件完整路径。</param>
    /// <param name="displayEndLocation">是否显示结尾位置。</param>
    internal CommandLineDiagnosticFormatter(
        string baseDirectory,
        bool displayFullPaths,
        bool displayEndLocation)
    {
        this._baseDirectory = baseDirectory;
        this._displayFullPaths = displayFullPaths;
        this._displayEndLocations = displayEndLocation;
        this._lazyNormalizedBaseDirectory = new(() => FileUtilities.TryNormalizeAbsolutePath(baseDirectory));
    }

    /// <summary>
    /// 格式化一个源代码文本区域。
    /// </summary>
    /// <param name="span">要格式化的源代码文本区域。</param>
    /// <param name="formatter">格式化使用的格式提供器。传入<see langword="null"/>时使用默认的格式提供器。</param>
    /// <returns>格式化后信息。</returns>
    internal override string FormatSourceSpan(LinePositionSpan span, IFormatProvider? formatter)
    {
        if (this._displayEndLocations)
            return string.Format(formatter, "({0},{1},{2},{3})",
                span.Start.Line + 1,        // 起始行号
                span.Start.Character + 1,   // 起始列号
                span.End.Line + 1,          // 结尾行号
                span.End.Character + 1      // 结尾列号
            );
        else
            return string.Format(formatter, "({0},{1})",
                span.Start.Line + 1,        // 起始行号
                span.Start.Character + 1   // 起始列号
            );
    }

    /// <summary>
    /// 格式化一个源代码文件路径。
    /// </summary>
    /// <param name="path">要格式化的源代码文件路径。</param>
    /// <param name="basePath">相对路径的基地址。</param>
    /// <param name="formatter">格式化使用的格式提供器。传入<see langword="null"/>时使用默认的格式提供器。</param>
    /// <returns>格式化后信息。</returns>
    internal override string FormatSourcePath(string path, string? basePath, IFormatProvider? formatter)
    {
        var normalizedPath = FileUtilities.NormalizeRelativePath(path, basePath, this._baseDirectory);
        if (normalizedPath is null) return path;

        return this._displayFullPaths ? normalizedPath : this.RelativizeNormalizedPath(normalizedPath);
    }

    /// <summary>
    /// 生成以<see cref="_baseDirectory"/>为基准的，到<paramref name="normalizedPath"/>的相对路径。
    /// </summary>
    /// <param name="normalizedPath">要生成的目标路径。</param>
    /// <returns>以<see cref="_baseDirectory"/>为基准的，到<paramref name="normalizedPath"/>的相对路径。</returns>
    internal string RelativizeNormalizedPath(string normalizedPath)
    {
        var normalizedBaseDirectory = this._lazyNormalizedBaseDirectory.Value;
        if (normalizedBaseDirectory is null) return normalizedPath;

        var normalizedDirectory = PathUtilities.GetDirectoryName(normalizedPath);
        if (normalizedDirectory is not null && PathUtilities.IsSameDirectoryOrChildOf(normalizedDirectory, normalizedBaseDirectory))
            return normalizedPath.Substring(
                PathUtilities.IsDirectorySeparator(normalizedBaseDirectory.Last())
                    ? normalizedBaseDirectory.Length
                    : normalizedBaseDirectory.Length + 1);

        return normalizedPath;
    }
}
