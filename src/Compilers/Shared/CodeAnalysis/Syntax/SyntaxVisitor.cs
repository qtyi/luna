// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
#endif

/// <summary>
/// 表示访问者基类。访问者每次访问和处理一个<see cref="ThisSyntaxNode"/>节点并产生类型为<typeparamref name="TResult"/>的结果。
/// </summary>
/// <typeparam name="TResult">访问者的处理方法的返回结果的类型。</typeparam>
public abstract partial class
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
    public virtual TResult? Visit(ThisSyntaxNode? node)
    {
        if (node is null) return default;

        return node.Accept(this);
    }

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个节点并产生默认结果。
    /// </summary>
    /// <param name="node">要进行处理的节点。</param>
    /// <returns>产生的结果。</returns>
    protected virtual TResult? DefaultVisit(ThisSyntaxNode node) => default;
}

/// <summary>
/// 表示访问者基类。访问者每次访问和处理一个<see cref="ThisSyntaxNode"/>节点。
/// </summary>
public abstract partial class
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
    public virtual void Visit(ThisSyntaxNode? node)
    {
        if (node is null) return;

        node.Accept(this);
    }

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个节点。
    /// </summary>
    /// <param name="node">要进行处理的节点。</param>
    protected virtual void DefaultVisit(ThisSyntaxNode node) { }
}
