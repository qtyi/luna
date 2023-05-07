// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

/// <summary>
/// 表示访问者基类。访问者每次访问和处理一个<see cref="ThisInternalSyntaxNode"/>节点并产生类型为<typeparamref name="TResult"/>的结果。
/// </summary>
/// <typeparam name="TResult">访问者的处理方法的返回结果的类型。</typeparam>
internal abstract partial class
#if LANG_LUA
    LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxVisitor
#endif
    <TResult>
{
    /// <summary>
    /// 处理这个节点并产生结果。
    /// </summary>
    /// <param name="node">要进行处理的节点。</param>
    /// <returns>产生的结果。</returns>
    public virtual TResult? Visit(ThisInternalSyntaxNode? node)
    {
        if (node is null) return default;

        return node.Accept(this);
    }

    /// <summary>
    /// 处理这个标记并产生结果。
    /// </summary>
    /// <param name="token">要进行处理的标记。</param>
    /// <returns>产生的结果。</returns>
    public virtual TResult? VisitToken(SyntaxToken token) => this.DefaultVisit(token);

    /// <summary>
    /// 处理这个琐碎内容并产生结果。
    /// </summary>
    /// <param name="trivia">要进行处理的琐碎内容。</param>
    /// <returns>产生的结果。</returns>
    public virtual TResult? VisitTrivia(SyntaxTrivia trivia) => this.DefaultVisit(trivia);

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个节点并产生默认结果。
    /// </summary>
    /// <param name="node">要进行处理的节点。</param>
    /// <returns>产生的结果。</returns>
    protected virtual TResult? DefaultVisit(ThisInternalSyntaxNode node) => default;
}

/// <summary>
/// 表示访问者基类。访问者每次访问和处理一个<see cref="ThisInternalSyntaxNode"/>节点。
/// </summary>
internal abstract partial class
#if LANG_LUA
    LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxVisitor
#endif
{
    /// <summary>
    /// 处理这个节点。
    /// </summary>
    /// <param name="node">要进行处理的节点。</param>
    public virtual void Visit(ThisInternalSyntaxNode? node)
    {
        if (node is null) return;

        node.Accept(this);
    }

    /// <summary>
    /// 处理这个标记。
    /// </summary>
    /// <param name="token">要进行处理的标记。</param>
    public virtual void VisitToken(SyntaxToken token) => this.DefaultVisit(token);

    /// <summary>
    /// 处理这个琐碎内容。
    /// </summary>
    /// <param name="trivia">要进行处理的琐碎内容。</param>
    public virtual void VisitTrivia(SyntaxTrivia trivia) => this.DefaultVisit(trivia);

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个节点。
    /// </summary>
    /// <param name="node">要进行处理的节点。</param>
    public virtual void DefaultVisit(ThisInternalSyntaxNode node) { }
}
