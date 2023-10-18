// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxHelper = LuaSyntaxHelper;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxHelper = MoonScriptSyntaxHelper;
#endif

using Syntax;

internal sealed partial class
#if LANG_LUA
    LuaSyntaxHelper
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxHelper
#endif
    : AbstractSyntaxHelper
{
    /// <summary>
    /// <see cref="ThisSyntaxHelper"/>的唯一实例。
    /// </summary>
    public static readonly ThisSyntaxHelper Instance = new();

    private
#if LANG_LUA
        LuaSyntaxHelper
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxHelper
#endif
    ()
    { }

    /// <summary>
    /// 获取一个值，指示语言是否大小写敏感。
    /// </summary>
    /// <value>
    /// 返回<see langword="true"/>。
    /// </value>
    public override bool IsCaseSensitive => true;

    protected override int AttributeListKind => -1;

    public override bool IsValidIdentifier(string name) => SyntaxFacts.IsValidIdentifier(name);

    public override bool IsAnyNamespaceBlock(SyntaxNode node) => false;

    public override bool IsAttribute(SyntaxNode node) => false;

    public override SyntaxNode GetNameOfAttribute(SyntaxNode node) => throw ExceptionUtilities.Unreachable();

    public override bool IsAttributeList(SyntaxNode node) => false;

    public override void AddAttributeTargets(SyntaxNode node, ArrayBuilder<SyntaxNode> targets) => throw ExceptionUtilities.Unreachable();

    public override SeparatedSyntaxList<SyntaxNode> GetAttributesOfAttributeList(SyntaxNode node) => throw ExceptionUtilities.Unreachable();

    public override partial bool IsLambdaExpression(SyntaxNode node);

    public override string GetUnqualifiedIdentifierOfName(SyntaxNode node) => ((NameSyntax)node).GetUnqualifiedName().Identifier.ValueText;

    public override void AddAliases(GreenNode node, ArrayBuilder<(string aliasName, string symbolName)> aliases, bool global) => throw ExceptionUtilities.Unreachable();

    public override void AddAliases(CompilationOptions options, ArrayBuilder<(string aliasName, string symbolName)> aliases) => throw ExceptionUtilities.Unreachable();

    public override bool ContainsGlobalAliases(SyntaxNode root) => throw ExceptionUtilities.Unreachable();
}
