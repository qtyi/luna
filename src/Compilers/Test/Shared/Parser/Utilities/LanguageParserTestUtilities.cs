using System.Diagnostics;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

#if LANG_LUA
using Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxNode;

namespace Qtyi.CodeAnalysis.Lua.Parser.UnitTests.Utilities;
#elif LANG_MOONSCRIPT
using Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.LuaSyntaxNode;

namespace Qtyi.CodeAnalysis.MoonScript.Parser.UnitTests.Utilities;
#endif

public static class LanguageParserTestUtilities
{
    #region AtEndOfFile
    internal static void AtEndOfFile(this Assert assert, LanguageParser parser) => Assert.IsTrue(parser.IsAtEndOfFile, "语言解析器并未抵达文件结尾。");
    internal static void NotAtEndOfFile(this Assert assert, LanguageParser parser) => Assert.IsFalse(parser.IsAtEndOfFile, "语言解析器已抵达文件结尾。");
    #endregion

    #region ContainsDiagnostics
    internal static void ContainsDiagnostics(this Assert assert, ThisInternalSyntaxNode node, params ErrorCode[] codes) =>
        assert.ContainsDiagnostics(new[] { node }, codes);

    internal static void ContainsDiagnostics<TNode>(this Assert assert, SyntaxList<TNode> list, params ErrorCode[] codes) where TNode : ThisInternalSyntaxNode =>
        assert.ContainsDiagnostics(list.Nodes, codes);

    internal static void ContainsDiagnostics<TNode>(this Assert assert, SeparatedSyntaxList<TNode> list, params ErrorCode[] codes) where TNode : ThisInternalSyntaxNode =>
        assert.ContainsDiagnostics(list.GetWithSeparators().Nodes.Cast<ThisInternalSyntaxNode>(), codes);

    private static void ContainsDiagnostics(this Assert assert, IEnumerable<ThisInternalSyntaxNode> nodes, params ErrorCode[] codes)
    {
        if (codes.Length == 0)
            Assert.IsTrue(nodes.Any(node => node.ContainsDiagnostics), "未报告语法错误。");
        else
        {
            var diagnostics = nodes.SelectMany(node => node.GetDiagnostics());
            var unraisedCodes = codes.Where(code => !diagnostics.Any(diag => diag.Code == (int)code)).ToArray();
            if (unraisedCodes.Length != 0)
                Assert.Fail("未报告语法错误：{0}。", string.Join("、", unraisedCodes));
        }
    }

    internal static void NotContainsDiagnostics(this Assert assert, ThisInternalSyntaxNode node, params ErrorCode[] codes) =>
        assert.NotContainsDiagnostics(new[] { node }, codes);

    internal static void NotContainsDiagnostics<TNode>(this Assert assert, SyntaxList<TNode> list, params ErrorCode[] codes) where TNode : ThisInternalSyntaxNode =>
        assert.NotContainsDiagnostics(list.Nodes, codes);

    internal static void NotContainsDiagnostics<TNode>(this Assert assert, SeparatedSyntaxList<TNode> list, params ErrorCode[] codes) where TNode : ThisInternalSyntaxNode =>
        assert.NotContainsDiagnostics(list.GetWithSeparators().Nodes.Cast<ThisInternalSyntaxNode>(), codes);

    private static void NotContainsDiagnostics(this Assert assert, IEnumerable<ThisInternalSyntaxNode> nodes, params ErrorCode[] codes)
    {
        if (codes.Length == 0)
            Assert.IsTrue(nodes.All(node => !node.ContainsDiagnostics), "报告语法错误。");
        else
        {
            var diagnostics = nodes.SelectMany(node => node.GetDiagnostics());
            var raisedCodes = codes.Where(code => diagnostics.Any(diag => diag.Code == (int)code)).ToArray();
            if (raisedCodes.Length != 0)
                Assert.Fail("报告语法错误：{0}。", string.Join("、", raisedCodes));
        }
    }

    #endregion

    #region IsIdentifierName
    internal static void IsIdentifierName(this Assert assert, NameSyntax name, string value)
    {
        Assert.IsInstanceOfType(name, typeof(IdentifierNameSyntax));
        assert.IsIdentifierName((IdentifierNameSyntax)name, value);
    }

    internal static void IsMissingIdentifierName(this Assert assert, NameSyntax name)
    {
        Assert.IsInstanceOfType(name, typeof(IdentifierNameSyntax));
        assert.IsMissingIdentifierName((IdentifierNameSyntax)name);
    }

    internal static void IsIdentifierName(this Assert assert, IdentifierNameSyntax identifierName, string value)
    {
        Assert.IsFalse(identifierName.Identifier.IsMissing, $"{nameof(identifierName)}包含的标识符名称标志缺失。");
        Assert.AreEqual(value, identifierName.Identifier.Text, $"{nameof(identifierName)}包含的标识符名称应为“{value}”，实为“{identifierName.Identifier.Text}”。");
    }

    internal static void IsMissingIdentifierName(this Assert assert, IdentifierNameSyntax identifierName)
    {
        Assert.IsTrue(identifierName.Identifier.IsMissing, $"{nameof(identifierName)}包含的标识符名称标志并未缺失。");
    }
    #endregion

    #region IsQualifiedName
    internal static void IsQualifiedName(this Assert assert, NameSyntax name, string value)
    {
        Assert.IsInstanceOfType(name, typeof(QualifiedNameSyntax));
        assert.IsQualifiedName((QualifiedNameSyntax)name, value);
    }

    internal static void IsMissingQualifiedName(this Assert assert, NameSyntax name)
    {
        Assert.IsInstanceOfType(name, typeof(QualifiedNameSyntax));
        assert.IsMissingIdentifierName((QualifiedNameSyntax)name);
    }

    internal static void IsQualifiedName(this Assert assert, NameSyntax name, Stack<string?> values)
    {
        Assert.IsInstanceOfType(name, typeof(QualifiedNameSyntax));
        assert.IsQualifiedName((QualifiedNameSyntax)name, values);
    }

    internal static void IsQualifiedName(this Assert assert, QualifiedNameSyntax qualifiedName, string value)
    {
        assert.IsIdentifierName(qualifiedName.right, value);
    }

    internal static void IsMissingQualifiedName(this Assert assert, QualifiedNameSyntax qualifiedName)
    {
        assert.IsMissingIdentifierName(qualifiedName.right);
    }

    internal static void IsQualifiedName(this Assert assert, QualifiedNameSyntax qualifiedName, Stack<string?> values)
    {
        if (values.Count < 2) Assert.Fail($"限定名称语法必须含有不少于2个标识符名称。{nameof(values)}中只含有{values.Count}个元素。");

        var value = values.Pop();
        if (value is not null)
            assert.IsQualifiedName(qualifiedName, value);
        else
            assert.IsMissingQualifiedName(qualifiedName);

        if (values.Count == 1)
        {
            value = values.Pop();
            if (value is not null)
                assert.IsIdentifierName(qualifiedName.Left, value);
            else
                assert.IsMissingIdentifierName(qualifiedName.Left);
        }
        else
            assert.IsQualifiedName(qualifiedName.Left, values);
    }
    #endregion

    #region IsImplicitSelfParameterName
    internal static void IsImplicitSelfParameterName(this Assert assert, NameSyntax name, string value)
    {
        Assert.IsInstanceOfType(name, typeof(ImplicitSelfParameterNameSyntax));
        assert.IsImplicitSelfParameterName((ImplicitSelfParameterNameSyntax)name, value);
    }

    internal static void IsMissingImplicitSelfParameterName(this Assert assert, NameSyntax name)
    {
        Assert.IsInstanceOfType(name, typeof(ImplicitSelfParameterNameSyntax));
        assert.IsMissingIdentifierName((ImplicitSelfParameterNameSyntax)name);
    }

    internal static void IsImplicitSelfParameterName(this Assert assert, NameSyntax name, Stack<string?> values)
    {
        Assert.IsInstanceOfType(name, typeof(ImplicitSelfParameterNameSyntax));
        assert.IsImplicitSelfParameterName((ImplicitSelfParameterNameSyntax)name, values);
    }

    internal static void IsImplicitSelfParameterName(this Assert assert, ImplicitSelfParameterNameSyntax implicitSelfParameterName, string value)
    {
        assert.IsIdentifierName(implicitSelfParameterName.right, value);
    }

    internal static void IsMissingImplicitSelfParameterName(this Assert assert, ImplicitSelfParameterNameSyntax implicitSelfParameterName)
    {
        assert.IsMissingIdentifierName(implicitSelfParameterName.right);
    }

    internal static void IsImplicitSelfParameterName(this Assert assert, ImplicitSelfParameterNameSyntax implicitSelfParameterName, Stack<string?> values)
    {
        if (values.Count < 2) Assert.Fail($"隐式self参数名称语法必须含有不少于2个标识符名称。{nameof(values)}中只含有{values.Count}个元素。");

        var value = values.Pop();
        if (value is not null)
            assert.IsImplicitSelfParameterName(implicitSelfParameterName, value);
        else
            assert.IsMissingImplicitSelfParameterName(implicitSelfParameterName);

        if (values.Count == 1)
        {
            value = values.Pop();
            if (value is not null)
                assert.IsIdentifierName(implicitSelfParameterName.Left, value);
            else
                assert.IsMissingIdentifierName(implicitSelfParameterName.Left);
        }
        else
            assert.IsQualifiedName(implicitSelfParameterName.Left, values);
    }
    #endregion

    #region IsLiteralExpression
    internal static void IsLiteralExpression(this Assert assert, LiteralExpressionSyntax literalExpression, SyntaxKind kind)
    {
        Assert.AreEqual(kind, literalExpression.Kind);
        switch (kind)
        {
            case SyntaxKind.VariousArgumentsExpression: break;
            case SyntaxKind.NilLiteralExpression:
                Assert.IsNull(literalExpression.token.Value);
                break;
            case SyntaxKind.FalseLiteralExpression:
                Assert.AreEqual(false, literalExpression.token.Value);
                break;
            case SyntaxKind.TrueLiteralExpression:
                Assert.AreEqual(true, literalExpression.token.Value);
                break;
        }
    }

    internal static void IsLiteralExpression<T>(this Assert assert, LiteralExpressionSyntax literalExpression, SyntaxKind kind, T? value)
    {
        Assert.AreEqual(kind, literalExpression.Kind);
        assert.IsLiteral(literalExpression.Token, value);
        Assert.AreEqual(value, literalExpression.token.Value);
    }
    #endregion

    #region IsUnaryExpression
    internal static void IsUnaryExpression(this Assert assert, ExpressionSyntax expression, SyntaxKind kind)
    {
        Assert.IsInstanceOfType(expression, typeof(UnaryExpressionSyntax));
        Assert.AreEqual(kind, expression.Kind);
    }

    internal static void IsUnaryExpression(this Assert assert, UnaryExpressionSyntax unaryExpression, TreeNode<SyntaxKind> kinds)
    {
        Debug.Assert(kinds.Count == 1);
        Assert.AreEqual(kinds.Value, unaryExpression.Kind);

        assert.IsExpression(unaryExpression.Operand, kinds.Children[0]);
    }
    #endregion

    #region IsBinaryExpression
    internal static void IsBinaryExpression(this Assert assert, ExpressionSyntax expression, SyntaxKind kind)
    {
        Assert.IsInstanceOfType(expression, typeof(BinaryExpressionSyntax));
        Assert.AreEqual(kind, expression.Kind);
    }

    internal static void IsBinaryExpression(this Assert assert, BinaryExpressionSyntax binaryExpression, TreeNode<SyntaxKind> kinds)
    {
        Debug.Assert(kinds.Count == 2);
        Assert.AreEqual(kinds.Value, binaryExpression.Kind);

        assert.IsExpression(binaryExpression.Left, kinds.Children[0]);
        assert.IsExpression(binaryExpression.Right, kinds.Children[1]);
    }
    #endregion

    #region IsParenthesizedExpression
    internal static void IsParenthesizedExpression(this Assert assert, ExpressionSyntax expression)
    {
        Assert.IsInstanceOfType(expression, typeof(ParenthesizedExpressionSyntax));
    }

    internal static void IsParenthesizedExpression(this Assert assert, ParenthesizedExpressionSyntax parenthesizedExpression, TreeNode<SyntaxKind> kinds)
    {
        Debug.Assert(kinds.Count == 1);
        Assert.AreEqual(kinds.Value, parenthesizedExpression.Kind);
        assert.IsExpression(parenthesizedExpression.Expression, kinds.Children[0]);
    }
    #endregion

    #region IsSimpleMemberAccessExpression
    internal static void IsSimpleMemberAccessExpression(this Assert assert, ExpressionSyntax expression)
    {
        Assert.IsInstanceOfType(expression, typeof(SimpleMemberAccessExpressionSyntax));
    }

    internal static void IsSimpleMemberAccessExpression(this Assert assert, SimpleMemberAccessExpressionSyntax simpleMemberAccessExpression, TreeNode<SyntaxKind> kinds)
    {
        Debug.Assert(kinds.Count == 2);
        Assert.AreEqual(kinds.value, simpleMemberAccessExpression.Kind);
        assert.IsExpression(simpleMemberAccessExpression.Self, kinds.Children[0]);
        assert.IsExpression(simpleMemberAccessExpression.MemberName, kinds.Children[1]);
    }
    #endregion

    #region IsIndexMemberAccessExpression
    internal static void IsIndexMemberAccessExpression(this Assert assert, ExpressionSyntax expression)
    {
        Assert.IsInstanceOfType(expression, typeof(IndexMemberAccessExpressionSyntax));
    }

    internal static void IsIndexMemberAccessExpression(this Assert assert, IndexMemberAccessExpressionSyntax indexMemberAccessExpression, TreeNode<SyntaxKind> kinds)
    {
        Debug.Assert(kinds.Count == 2);
        Assert.AreEqual(kinds.value, indexMemberAccessExpression.Kind);
        assert.IsExpression(indexMemberAccessExpression.Self, kinds.Children[0]);
        assert.IsExpression(indexMemberAccessExpression.Member, kinds.Children[1]);
    }
    #endregion

    #region IsEmptyList
    internal static void IsEmptyList<TNode>(this Assert assert, SyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode =>
        Assert.IsTrue(list.Count == 0, "列表不为空。");

    internal static void IsNotEmptyList<TNode>(this Assert assert, SyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode =>
        Assert.IsFalse(list.Count == 0, "列表为空。");
    internal static void IsNotEmptyList<TNode>(this Assert assert, SyntaxList<TNode> list, int count) where TNode : ThisInternalSyntaxNode
    {
        Debug.Assert(count >= 0);
        assert.IsNotEmptyList(list);
        Assert.AreEqual(count, list.Count, $"列表项目个数为{list.Count}个，而非{count}。");
    }

    internal static void IsEmptyList<TNode>(this Assert assert, SeparatedSyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode =>
        Assert.IsTrue(list.Count == 0, "列表不为空。");

    internal static void IsNotEmptyList<TNode>(this Assert assert, SeparatedSyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode =>
        Assert.IsFalse(list.Count == 0, "列表为空。");
    internal static void IsNotEmptyList<TNode>(this Assert assert, SeparatedSyntaxList<TNode> list, int count) where TNode : ThisInternalSyntaxNode
    {
        Debug.Assert(count >= 0);
        assert.IsNotEmptyList(list);
        Assert.AreEqual(count, list.Count, $"列表节点项目个数为{list.Count}个，而非{count}。");
    }
    internal static void IsNotEmptyList<TNode>(this Assert assert, SeparatedSyntaxList<TNode> list, int nodeCount, int separatorCount) where TNode : ThisInternalSyntaxNode
    {
        Debug.Assert(nodeCount >= 0);
        Debug.Assert(separatorCount >= 0);
        assert.IsNotEmptyList(list);
        Assert.AreEqual(nodeCount, list.Count, $"列表节点项目个数为{list.Count}个，而非{nodeCount}。");
        Assert.AreEqual(separatorCount, list.SeparatorCount, $"列表间隔项目个数为{list.SeparatorCount}个，而非{separatorCount}。");
    }
    #endregion

    internal static void IsExpression(this Assert assert, ExpressionSyntax expression, TreeNode<SyntaxKind> kinds)
    {
        if (kinds.Value == SyntaxKind.SimpleMemberAccessExpression)
        {
            Assert.IsInstanceOfType(expression, typeof(SimpleMemberAccessExpressionSyntax));
            assert.IsSimpleMemberAccessExpression((SimpleMemberAccessExpressionSyntax)expression, kinds);
        }
        else if (kinds.Value == SyntaxKind.IndexMemberAccessExpression)
        {
            Assert.IsInstanceOfType(expression, typeof(IndexMemberAccessExpressionSyntax));
            assert.IsIndexMemberAccessExpression((IndexMemberAccessExpressionSyntax)expression, kinds);
        }
        else if (kinds.Value == SyntaxKind.ParenthesizedExpression)
        {
            Assert.IsInstanceOfType(expression, typeof(ParenthesizedExpressionSyntax));
            assert.IsParenthesizedExpression((ParenthesizedExpressionSyntax)expression, kinds);
        }
        else if (SyntaxFacts.IsUnaryExpression(kinds.value))
        {
            Assert.IsInstanceOfType(expression, typeof(UnaryExpressionSyntax));
            assert.IsUnaryExpression((UnaryExpressionSyntax)expression, kinds);
        }
        else if (SyntaxFacts.IsBinaryExpression(kinds.value))
        {
            Assert.IsInstanceOfType(expression, typeof(BinaryExpressionSyntax));
            assert.IsBinaryExpression((BinaryExpressionSyntax)expression, kinds);
        }
        else if (SyntaxFacts.IsLiteralExpression(kinds.value))
        {
            Debug.Assert(kinds.Count == 0);
            Assert.IsInstanceOfType(expression, typeof(LiteralExpressionSyntax));
            assert.IsLiteralExpression((LiteralExpressionSyntax)expression, kinds.value);
        }
        else if (kinds.Value == SyntaxKind.IdentifierName)
        {
            Debug.Assert(kinds.Count == 0);
            Assert.IsInstanceOfType(expression, typeof(IdentifierNameSyntax));
        }
        else if (kinds.Value == SyntaxKind.QualifiedName)
        {
            Debug.Assert(kinds.Count == 0);
            Assert.IsInstanceOfType(expression, typeof(QualifiedNameSyntax));
        }
        else if (kinds.Value == SyntaxKind.ImplicitSelfParameterName)
        {
            Debug.Assert(kinds.Count == 0);
            Assert.IsInstanceOfType(expression, typeof(ImplicitSelfParameterNameSyntax));
        }
        else
            Debug.Fail($"暂不支持测试的表达式语法节点种类：{kinds.value}");
    }

    #region IsNameAttributeList
    internal static void IsNameAttributeList(this Assert assert, NameAttributeListSyntax nameAttributeList, string name, bool isClose = false, bool isConst = false)
    {
        assert.IsIdentifierName(nameAttributeList.Identifier, name);

        var attributeList = nameAttributeList.AttributeList;
        if (attributeList is null)
        {
            Assert.IsFalse(isClose, "包含close特性。");
            Assert.IsFalse(isConst, "包含const特性。");
        }
        else
            assert.IsAttributeList(attributeList, isClose, isConst);
    }

    private static void IsAttributeList(this Assert assert, AttributeListSyntax attributeList, bool isClose = false, bool isConst = false)
    {
        var attributes = Enumerable.Range(0, attributeList.Attributes.Count).Select(index => attributeList.Attributes[index]).OfType<AttributeSyntax>().Select(attr => attr.Token);
        Assert.IsTrue(isConst == attributes.Any(token => token.Kind == SyntaxKind.ConstKeyword), (isClose ? "不" : string.Empty) + "包含close特性。");
        Assert.IsTrue(isClose == attributes.Any(token => token.Kind == SyntaxKind.CloseKeyword), (isConst ? "不" : string.Empty) + "包含const特性。");
    }
    #endregion
}
