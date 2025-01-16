// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// The formatter to format a diagnostic reported from command line.
/// </summary>
internal sealed class CommandLineDiagnosticFormatter : ThisDiagnosticFormatter
{
    /// <summary>The base directory path.</summary>
    private readonly string _baseDirectory;
    /// <summary>Normalized base directory path.</summary>
    private readonly Lazy<string?> _lazyNormalizedBaseDirectory;
    /// <summary>Should display the full path of source files.</summary>
    private readonly bool _displayFullPaths;
    /// <summary>Should display the end location.</summary>
    private readonly bool _displayEndLocations;

    /// <param name="baseDirectory">The base directory path.</param>
    /// <param name="displayFullPaths">Should display the full path of source files.</param>
    /// <param name="displayEndLocation">Should display the end location.</param>
    internal CommandLineDiagnosticFormatter(
        string baseDirectory,
        bool displayFullPaths,
        bool displayEndLocation)
    {
        _baseDirectory = baseDirectory;
        _displayFullPaths = displayFullPaths;
        _displayEndLocations = displayEndLocation;
        _lazyNormalizedBaseDirectory = new(() => FileUtilities.TryNormalizeAbsolutePath(baseDirectory));
    }

    /// <summary>
    /// Format a source text span.
    /// </summary>
    /// <param name="span">The source text span.</param>
    /// <param name="formatter">The formatter, or <see langword="null"/> to use the default.</param>
    /// <returns>The formatted message.</returns>
    internal override string FormatSourceSpan(LinePositionSpan span, IFormatProvider? formatter)
    {
        if (_displayEndLocations)
            return string.Format(formatter, "({0},{1},{2},{3})",
                span.Start.Line + 1,        // start line num.
                span.Start.Character + 1,   // start column num.
                span.End.Line + 1,          // end line num.
                span.End.Character + 1      // end column num.
            );
        else
            return string.Format(formatter, "({0},{1})",
                span.Start.Line + 1,        // start line num.
                span.Start.Character + 1    // start column num.
            );
    }

    /// <summary>
    /// Format the path of a source file.
    /// </summary>
    /// <param name="path">The source file path.</param>
    /// <param name="basePath">The source path the result should be relative to. This path is always considered to be a directory.</param>
    /// <param name="formatter">The formatter, or <see langword="null"/> to use the default.</param>
    /// <returns>The formatted message.</returns>
    internal override string FormatSourcePath(string path, string? basePath, IFormatProvider? formatter)
    {
        var normalizedPath = FileUtilities.NormalizeRelativePath(path, basePath, _baseDirectory);
        if (normalizedPath is null) return path;

        // By default, specify the name of the file in which an error was found.
        // When The /fullpaths option is present, specify the full path to the file.
        return _displayFullPaths ? normalizedPath : RelativizeNormalizedPath(normalizedPath);
    }

    /// <summary>
    /// Get the path name starting from the <see cref="_baseDirectory"/> to <paramref name="normalizedPath"/>.
    /// </summary>
    /// <param name="normalizedPath">The destination path.</param>
    /// <returns>The path name starting from the <see cref="_baseDirectory"/> to <paramref name="normalizedPath"/>.</returns>
    internal string RelativizeNormalizedPath(string normalizedPath)
    {
        var normalizedBaseDirectory = _lazyNormalizedBaseDirectory.Value;
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
