// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics;
using System.Runtime.CompilerServices;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using MSCA::Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract partial class
#if LANG_LUA
    LuaSyntaxNode
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxNode
#endif
    : GreenNode
{
    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind) : base((ushort)kind) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, int fullWidth) : base((ushort)kind, fullWidth) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base((ushort)kind, diagnostics) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics, int fullWidth) : base((ushort)kind, diagnostics, fullWidth) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base((ushort)kind, diagnostics, annotations) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations, int fullWidth) : base((ushort)kind, diagnostics, annotations, fullWidth) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (ObjectReader reader) : base(reader) { }

    public sealed override string Language =>
#if LANG_LUA
        LanguageNames.Lua;
#elif LANG_MOONSCRIPT
        LanguageNames.MoonScript;
#endif

    public SyntaxKind Kind => (SyntaxKind)this.RawKind;

    public override string KindText => this.Kind.ToString();

    public override int RawContextualKind => this.RawKind;

    public override bool IsStructuredTrivia => this is StructuredTriviaSyntax;

    public override bool IsDirective => false;

    public sealed override bool IsDocumentationCommentTrivia => false;

    public SyntaxToken? GetFirstToken() => (SyntaxToken?)this.GetFirstTerminal();

    public SyntaxToken? GetLastToken() => (SyntaxToken?)this.GetLastTerminal();

    public SyntaxToken? GetLastNonmissingToken() => (SyntaxToken?)this.GetLastNonmissingTerminal();

    public virtual GreenNode? GetLeadingTrivia() => null;
    public sealed override GreenNode? GetLeadingTriviaCore() => this.GetLeadingTrivia();

    public virtual GreenNode? GetTrailingTrivia() => null;
    public sealed override GreenNode? GetTrailingTriviaCore() => this.GetTrailingTrivia();

    public abstract TResult? Accept<TResult>(
#if LANG_LUA
        LuaSyntaxVisitor<TResult>
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor<TResult>
#endif
        visitor);

    public abstract void Accept(
#if LANG_LUA
        LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor
#endif
        visitor);

    /// <summary>
    /// 设置语法工厂上下文。
    /// </summary>
    /// <param name="context">包含语法工厂上下文信息的对象。</param>
    /// <remarks>
    /// 此方法仅应在构造语法节点时调用。
    /// </remarks>
    protected void SetFactoryContext(SyntaxFactoryContext context) =>
        this.flags = ThisInternalSyntaxNode.SetFactoryContext(this.flags, context);

    /// <summary>
    /// 给指定的节点标示设置语法工厂上下文。
    /// </summary>
    /// <param name="flags">要修改的节点标示。</param>
    /// <param name="context">包含语法工厂上下文信息的对象。</param>
    /// <returns>修改后的<paramref name="flags"/>。</returns>
    internal static partial NodeFlags SetFactoryContext(NodeFlags flags, SyntaxFactoryContext context);

    public override partial MSCA::Microsoft.CodeAnalysis.SyntaxToken CreateSeparator<TNode>(SyntaxNode element);

    public virtual bool IsTokenAtEndOfLine() =>
        this is SyntaxToken token &&
            token.TrailingTrivia.Count > 0 &&
                token.TrailingTrivia.Last?.IsTriviaWithEndOfLine() == true;

    public override partial bool IsTriviaWithEndOfLine();

    /// <summary>
    /// 使用条件弱表来保存与语法节点相对应的结构语法琐碎内容的唯一实例。
    /// </summary>
    private static readonly ConditionalWeakTable<SyntaxNode, Dictionary<MSCA::Microsoft.CodeAnalysis.SyntaxTrivia, WeakReference<SyntaxNode>>> s_structuresTable = new();

    public override SyntaxNode GetStructure(MSCA::Microsoft.CodeAnalysis.SyntaxTrivia trivia)
    {
        if (!trivia.HasStructure) throw new ArgumentException(string.Format(LunaResources.ArgIsNotStructuredTrivia), nameof(trivia));

        var parent = trivia.Token.Parent;
        if (parent is null)
            return this.GetNewStructure(trivia);
        else
        {
            SyntaxNode? structure;
            var structsInParent = s_structuresTable.GetOrCreateValue(parent);
            lock (structsInParent)
            {
                if (!structsInParent.TryGetValue(trivia, out var weekStructure))
                {
                    structure = this.GetNewStructure(trivia);
                    structsInParent.Add(trivia, new(structure));
                }
                else if (!weekStructure.TryGetTarget(out structure))
                {
                    structure = this.GetNewStructure(trivia);
                    structsInParent.Add(trivia, new(structure));
                }
            }

            return structure;
        }
    }

    /// <summary>
    /// 使用指定语法琐碎内容创建新的<see cref="Syntax.StructuredTriviaSyntax"/>实例。
    /// </summary>
    /// <param name="trivia">现有的语法琐碎内容。</param>
    /// <returns>根据现有的语法琐碎内容创建的新的表示结构语法琐碎内容的语法节点。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual SyntaxNode GetNewStructure(MSCA::Microsoft.CodeAnalysis.SyntaxTrivia trivia) =>
        Syntax.StructuredTriviaSyntax.Create(trivia);
}
