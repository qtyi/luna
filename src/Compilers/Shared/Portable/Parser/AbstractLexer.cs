// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <summary>
/// Provides essential members and method implentations for building a lexer.
/// </summary>
internal abstract partial class AbstractLexer : IDisposable
{
    /// <summary>A sliding character buffer.</summary>
    internal readonly SlidingTextWindow TextWindow;
    /// <summary>Syntax diagnostics collected during lexing.</summary>
    private readonly ArrayBuilder<SyntaxDiagnosticInfo> _errors;

    /// <summary>
    /// Gets a value indicates whether we collect any error during lexing.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if we collect any error during lexing; otherwise, <see langword="false"/>.
    /// </value>
    protected bool HasErrors => _errors.Count != 0;

    /// <summary>
    /// Create a new instance of <see cref="AbstractLexer"/> type with specified source text.
    /// </summary>
    /// <param name="text">Source text to lex.</param>
    protected AbstractLexer(SourceText text)
    {
        TextWindow = new(text);
        _errors = ArrayBuilder<SyntaxDiagnosticInfo>.GetInstance(8);
    }

    /// <inheritdoc/>
    public virtual void Dispose()
    {
        TextWindow.Dispose();
        _errors.Free();
    }

    /// <summary>
    /// Starts lexing work.
    /// </summary>
    protected void Start()
    {
        TextWindow.Start();
        _errors.Clear();
    }

    /// <summary>
    /// Gets all errors we collect during lexing.
    /// </summary>
    /// <param name="leadingTriviaWidth">Width of leading trivia to fixup error positioning.</param>
    /// <returns>All errors we collect during lexing.</returns>
    /// <remarks>
    /// If <paramref name="leadingTriviaWidth"/> is not zero, position of every diagnostic will be update by <see cref="SyntaxDiagnosticInfo.WithOffset(int)"/> with offset of <paramref name="leadingTriviaWidth"/>.
    /// </remarks>
    protected SyntaxDiagnosticInfo[]? GetErrors(int leadingTriviaWidth)
    {
        if (!HasErrors)
            return null;

        if (leadingTriviaWidth > 0)
        {
            // fixup error positioning to account for leading trivia
            var array = new SyntaxDiagnosticInfo[_errors.Count];
            for (var i = 0; i < _errors.Count; i++)
                array[i] = _errors[i].WithOffset(_errors[i].Offset + leadingTriviaWidth);

            return array;
        }

        return _errors.ToArray();
    }

    /// <summary>
    /// Adds to report a syntax diagnostic with specified location and error code id.
    /// </summary>
    /// <param name="position">Start position of the syntax that diagnostic reported for (relative to source text start).</param>
    /// <param name="width">Number of characters in the syntax that diagnostic reported for.</param>
    /// <param name="code">Error code id for the diagnostic.</param>
    protected void AddError(int position, int width, ErrorCode code) => AddError(MakeError(position, width, code));

    /// <summary>
    /// Adds to report a syntax diagnostic with specified location, error code id and format arguments.
    /// </summary>
    /// <param name="position">Start position of the syntax that diagnostic reported for (relative to source text start).</param>
    /// <param name="width">Number of characters in the syntax that diagnostic reported for.</param>
    /// <param name="code">Error code id for the diagnostic.</param>
    /// <param name="args">Format arguments to make up diagnostic description.</param>
    protected void AddError(int position, int width, ErrorCode code, params object[] args) => AddError(MakeError(position, width, code, args));

    /// <summary>
    /// Adds to report a syntax diagnostic with specified error code id.
    /// </summary>
    /// <param name="code">Error code id for the diagnostic.</param>
    protected void AddError(ErrorCode code) => AddError(MakeError(code));

    /// <summary>
    /// Adds to report a syntax diagnostic with specified error code id and format arguments.
    /// </summary>
    /// <param name="code">Error code id for the diagnostic.</param>
    /// <param name="args">Format arguments to make up diagnostic description.</param>
    protected void AddError(ErrorCode code, params object[] args) => AddError(MakeError(code, args));

    /// <summary>
    /// Adds to report a syntax diagnostic with specified <see cref="SyntaxDiagnosticInfo"/> instance.
    /// </summary>
    /// <param name="error">The diagnostic to add if not <see langword="null"/>.</param>
    protected void AddError(SyntaxDiagnosticInfo? error)
    {
        if (error is null)
            return;

        _errors.Add(error);
    }

    /// <summary>
    /// Makes a syntax diagnostic with specified location and error code id.
    /// </summary>
    /// <param name="position">Start position of the syntax that diagnostic reported for (relative to source text start).</param>
    /// <param name="width">Number of characters in the syntax that diagnostic reported for.</param>
    /// <param name="code">Error code id for the diagnostic.</param>
    protected SyntaxDiagnosticInfo MakeError(int position, int width, ErrorCode code) => new(GetLexemeOffsetFromPosition(position), width, code);

    /// <summary>
    /// Makes a syntax diagnostic with specified location, error code id and format arguments.
    /// </summary>
    /// <param name="position">Start position of the syntax that diagnostic reported for (relative to source text start).</param>
    /// <param name="width">Number of characters in the syntax that diagnostic reported for.</param>
    /// <param name="code">Error code id for the diagnostic.</param>
    /// <param name="args">Format arguments to make up diagnostic description.</param>
    protected SyntaxDiagnosticInfo MakeError(int position, int width, ErrorCode code, params object[] args) => new(GetLexemeOffsetFromPosition(position), width, code, args);

    /// <summary>
    /// Gets the position relative to the start of the current lexeme.
    /// </summary>
    /// <returns>
    /// Returns the position relative to the start of the current lexeme if <paramref name="position"/> is after the start of the current lexeme; otherwise, <paramref name="position"/> itself.
    /// </returns>
    private int GetLexemeOffsetFromPosition(int position) => position >= TextWindow.LexemeStartPosition ? position - TextWindow.LexemeStartPosition : position;

    /// <summary>
    /// Makes a syntax diagnostic with specified error code id.
    /// </summary>
    /// <param name="code">Error code id for the diagnostic.</param>
    protected static SyntaxDiagnosticInfo MakeError(ErrorCode code) => new(code);

    /// <summary>
    /// Makes a syntax diagnostic with specified error code id and format arguments.
    /// </summary>
    /// <param name="code">Error code id for the diagnostic.</param>
    /// <param name="args">Format arguments to make up diagnostic description.</param>
    protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args) => new(code, args);
}
