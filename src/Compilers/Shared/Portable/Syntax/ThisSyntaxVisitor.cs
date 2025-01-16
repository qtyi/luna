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
/// Represents a <see cref="ThisSyntaxNode"/> visitor that visits only the single <see cref="ThisSyntaxNode"/> passed into its Visit method and produces a value of the type specified by the <typeparamref name="TResult"/> parameter.
/// </summary>
/// <typeparam name="TResult">
/// The type of the return value this visitor's Visit method.
/// </typeparam>
public abstract partial class
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
    /// <param name="node">The syntax node visited.</param>
    /// <returns>The visiting result.</returns>
    public virtual TResult? Visit(SyntaxNode? node)
    {
        if (node is null) return default;

        return ((ThisSyntaxNode)node).Accept(this);
    }

    /// <summary>
    /// Gets the default result after visiting a syntax node.
    /// </summary>
    /// <param name="node">The syntax node visited.</param>
    /// <returns>The visiting result.</returns>
    protected virtual TResult? DefaultVisit(SyntaxNode node) => default;
}

/// <summary>
/// Represents a <see cref="ThisSyntaxNode"/> visitor that visits only the single <see cref="ThisSyntaxNode"/> passed into its Visit method.
/// </summary>
public abstract partial class
#if LANG_LUA
     LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
     MoonScriptSyntaxVisitor
#endif
{
    /// <summary>
    /// Visit a syntax node.
    /// </summary>
    /// <param name="node">The syntax node visited.</param>
    public virtual void Visit(SyntaxNode? node)
    {
        if (node is null) return;

        ((ThisSyntaxNode)node).Accept(this);
    }

    /// <summary>
    /// Do the default visit.
    /// </summary>
    /// <param name="node">The syntax node visited.</param>
    protected virtual void DefaultVisit(SyntaxNode node) { }
}
