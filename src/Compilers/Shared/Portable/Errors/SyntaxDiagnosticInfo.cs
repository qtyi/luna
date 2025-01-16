// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// A DiagnosticInfo object about a syntax diagnostic that contains position and width in source text.
/// </summary>
internal class SyntaxDiagnosticInfo : DiagnosticInfo
{
    /// <summary>
    /// Start position of the syntax diagnostic to report.
    /// </summary>
    internal readonly int Offset;
    /// <summary>
    /// Number of characters of the syntax diagnostic to report.
    /// </summary>
    internal readonly int Width;

    internal SyntaxDiagnosticInfo(int offset, int width, ErrorCode code, params object[] args)
        : base(ThisMessageProvider.Instance, (int)code, args)
    {
        Debug.Assert(width >= 0);
        Offset = offset;
        Width = width;
    }

    internal SyntaxDiagnosticInfo(int offset, int width, ErrorCode code)
        : this(offset, width, code, args: []) { }

    internal SyntaxDiagnosticInfo(ErrorCode code, params object[] args)
        : this(offset: 0, width: 0, code, args) { }

    internal SyntaxDiagnosticInfo(ErrorCode code)
        : this(offset: 0, width: 0, code, args: []) { }

    /// <summary>
    /// Create a new instance with specified diagnostic severity.
    /// </summary>
    /// <param name="original">The original syntax diagnostic info.</param>
    /// <param name="severity">New diagnostic severity.</param>
    protected SyntaxDiagnosticInfo(SyntaxDiagnosticInfo original, DiagnosticSeverity severity) : base(original, severity)
    {
        Offset = original.Offset;
        Width = original.Width;
    }

    protected override DiagnosticInfo GetInstanceWithSeverityCore(DiagnosticSeverity severity) => new SyntaxDiagnosticInfo(this, severity);

    /// <summary>
    /// Create a new <see cref="SyntaxDiagnosticInfo"/> with specified offset.
    /// </summary>
    /// <param name="offset">The new offset.</param>
    public SyntaxDiagnosticInfo WithOffset(int offset) => new(offset, Width, (ErrorCode)Code, Arguments);
}
