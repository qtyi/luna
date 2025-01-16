// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <summary>
/// Represents a base type of syntax visitor that visit nodes, tokens and trivia one by one and produce result.
/// </summary>
/// <typeparam name="TResult">Type of result that visitor produce.</typeparam>
internal abstract partial class
#if LANG_LUA
    LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxVisitor
#endif
    <TResult>
{
    /// <summary>
    /// Visit a syntax node and produce result.
    /// </summary>
    /// <param name="node">The syntax node to be visited.</param>
    /// <returns>Result produced.</returns>
    public virtual TResult? Visit(ThisInternalSyntaxNode? node)
    {
        if (node is null) return default;

        return node.Accept(this);
    }

    /// <summary>
    /// Visit a syntax token and produce result.
    /// </summary>
    /// <param name="token">The syntax token to be visited.</param>
    /// <returns>Result produced.</returns>
    public virtual TResult? VisitToken(SyntaxToken token) => DefaultVisit(token);

    /// <summary>
    /// Visit a syntax trivia and produce result.
    /// </summary>
    /// <param name="trivia">The syntax trivia to be visited.</param>
    /// <returns>Result produced.</returns>
    public virtual TResult? VisitTrivia(SyntaxTrivia trivia) => DefaultVisit(trivia);

    /// <summary>
    /// Visit a syntax node and produce a default result.
    /// </summary>
    /// <param name="node">The syntax node to be visited.</param>
    /// <returns>Result produced.</returns>
    protected virtual TResult? DefaultVisit(ThisInternalSyntaxNode node) => default;
}

/// <summary>
/// Represents a base type of syntax visitor that visit nodes, tokens and trivia one by one.
/// </summary>
internal abstract partial class
#if LANG_LUA
    LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxVisitor
#endif
{
    /// <summary>
    /// Visit a syntax node.
    /// </summary>
    /// <param name="node">The syntax node to be visited.</param>
    public virtual void Visit(ThisInternalSyntaxNode? node)
    {
        if (node is null) return;

        node.Accept(this);
    }

    /// <summary>
    /// Visit a syntax token.
    /// </summary>
    /// <param name="token">The syntax token to be visited.</param>
    public virtual void VisitToken(SyntaxToken token) => DefaultVisit(token);

    /// <summary>
    /// Visit a syntax trivia.
    /// </summary>
    /// <param name="trivia">The syntax trivia to be visited.</param>
    public virtual void VisitTrivia(SyntaxTrivia trivia) => DefaultVisit(trivia);

    /// <summary>
    /// Visit a syntax node and do default operations.
    /// </summary>
    /// <param name="node">The syntax node to be visited.</param>
    public virtual void DefaultVisit(ThisInternalSyntaxNode node) { }
}
