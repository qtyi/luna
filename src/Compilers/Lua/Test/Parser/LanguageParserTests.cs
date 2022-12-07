using Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

namespace Qtyi.CodeAnalysis.Lua.Parser.UnitTests;

using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Utilities;

[TestClass]
public partial class LanguageParserTests
{
    internal static LanguageParser CreateLanguageParser(string source, LuaParseOptions? options = null) => new(LexerTests.CreateLexer(source, options), null, null);

    [TestMethod]
    public void TestFilesLexTests()
    {
        foreach (var path in Directory.GetFiles("tests"))
        {
            var tree = SyntaxFactory.ParseSyntaxTree(
                text: SourceText.From(File.OpenRead(path)),
                options: LuaParseOptions.Default,
                path: path,
                cancellationToken: default);
        }
    }

    #region 名称
    [TestMethod]
    public void IdentifierNameParseTests()
    {
        { // 西文标识符
            var parser = LanguageParserTests.CreateLanguageParser(" identifier ");
            var identifierName = parser.ParseIdentifierName();
            Assert.That.IsIdentifierName(identifierName, "identifier");
            Assert.That.NotContainsDiagnostics(identifierName);
            Assert.That.AtEndOfFile(parser);
        }
        { // 中文标识符
            var parser = LanguageParserTests.CreateLanguageParser(" 标识符 ");
            var identifierName = parser.ParseIdentifierName();
            Assert.That.IsIdentifierName(identifierName, "标识符");
            Assert.That.NotContainsDiagnostics(identifierName);
            Assert.That.AtEndOfFile(parser);
        }

        { // 非标识符
            var parser = LanguageParserTests.CreateLanguageParser(" 'string' ");
            var identifierName = parser.ParseIdentifierName();
            Assert.That.IsMissingIdentifierName(identifierName);
            Assert.That.ContainsDiagnostics(identifierName);
            Assert.That.NotAtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void NameParseTests()
    {
        { // 合法的限定名称
            var parser = LanguageParserTests.CreateLanguageParser(" name.identifier ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("name");
            values.Push("identifier");
            Assert.That.IsQualifiedName(name, values);
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 合法的隐式self参数名称
            var parser = LanguageParserTests.CreateLanguageParser(" name:identifier ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("name");
            values.Push("identifier");
            Assert.That.IsImplicitSelfParameterName(name, values);
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }

        { // 缺失右侧标识符名称的限定名称
            var parser = LanguageParserTests.CreateLanguageParser(" name. ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("name");
            values.Push(null);
            Assert.That.IsQualifiedName(name, values);
            Assert.That.ContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失左侧标识符名称的限定名称
            var parser = LanguageParserTests.CreateLanguageParser(" .identifier ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push(null);
            values.Push("identifier");
            Assert.That.IsQualifiedName(name, values);
            Assert.That.ContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失右侧标识符名称的限定名称
            var parser = LanguageParserTests.CreateLanguageParser(" name: ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("name");
            values.Push(null);
            Assert.That.IsImplicitSelfParameterName(name, values);
            Assert.That.ContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失左侧标识符名称的限定名称
            var parser = LanguageParserTests.CreateLanguageParser(" :identifier ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push(null);
            values.Push("identifier");
            Assert.That.IsImplicitSelfParameterName(name, values);
            Assert.That.ContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }

        { // 合法的多重限定名称
            var parser = LanguageParserTests.CreateLanguageParser(" a.b.c.d.e.f.g ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("a");
            values.Push("b");
            values.Push("c");
            values.Push("d");
            values.Push("e");
            values.Push("f");
            values.Push("g");
            Assert.That.IsQualifiedName(name, values);
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 合法的多重限定隐式self参数名称
            var parser = LanguageParserTests.CreateLanguageParser(" a.b.c.d.e.f:g ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("a");
            values.Push("b");
            values.Push("c");
            values.Push("d");
            values.Push("e");
            values.Push("f");
            values.Push("g");
            Assert.That.IsImplicitSelfParameterName(name, values);
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 多重限定名称缺少点标志
            // 会被分拆成两个名称语法。
            var parser = LanguageParserTests.CreateLanguageParser(" a.b.c d.e.f ");
            {
                var name = parser.ParseName();
                var values = new Stack<string?>();
                values.Push("a");
                values.Push("b");
                values.Push("c");
                Assert.That.IsQualifiedName(name, values);
                Assert.That.NotContainsDiagnostics(name);
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var name = parser.ParseName();
                var values = new Stack<string?>();
                values.Push("d");
                values.Push("e");
                values.Push("f");
                Assert.That.IsQualifiedName(name, values);
                Assert.That.NotContainsDiagnostics(name);
                Assert.That.AtEndOfFile(parser);
            }
        }
        { // 多重限定隐式self参数名称缺少点标志
            // 会被分拆成两个名称语法。
            var parser = LanguageParserTests.CreateLanguageParser(" a.b.c d.e:f ");
            {
                var name = parser.ParseName();
                var values = new Stack<string?>();
                values.Push("a");
                values.Push("b");
                values.Push("c");
                Assert.That.IsQualifiedName(name, values);
                Assert.That.NotContainsDiagnostics(name);
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var name = parser.ParseName();
                var values = new Stack<string?>();
                values.Push("d");
                values.Push("e");
                values.Push("f");
                Assert.That.IsImplicitSelfParameterName(name, values);
                Assert.That.NotContainsDiagnostics(name);
                Assert.That.AtEndOfFile(parser);
            }
        }

        { // 多重限定名称缺少标识符
            var parser = LanguageParserTests.CreateLanguageParser(" a. .c. .e. .g ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("a");
            values.Push(null);
            values.Push("c");
            values.Push(null);
            values.Push("e");
            values.Push(null);
            values.Push("g");
            Assert.That.IsQualifiedName(name, values);
            Assert.That.ContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }
        { // 多重限定隐式self参数名称缺少标识符
            var parser = LanguageParserTests.CreateLanguageParser(" a. .c. .e.:g ");
            var name = parser.ParseName();
            var values = new Stack<string?>();
            values.Push("a");
            values.Push(null);
            values.Push("c");
            values.Push(null);
            values.Push("e");
            values.Push(null);
            values.Push("g");
            Assert.That.IsImplicitSelfParameterName(name, values);
            Assert.That.ContainsDiagnostics(name);
            Assert.That.AtEndOfFile(parser);
        }

        { // 隐式self参数名称后错误追加限定、隐式self参数名称语法
            // 将跳过第一个合法的隐式self参数语法后的所有限定、隐式self参数语法。
            var parser = LanguageParserTests.CreateLanguageParser(" a:b.c d:e:f ");
            {
                var name = parser.ParseName();
                var values = new Stack<string?>();
                values.Push("a");
                values.Push("b");
                Assert.That.IsImplicitSelfParameterName(name, values);
                Assert.That.ContainsDiagnostics(name);
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var name = parser.ParseName();
                var values = new Stack<string?>();
                values.Push("d");
                values.Push("e");
                Assert.That.IsImplicitSelfParameterName(name, values);
                Assert.That.ContainsDiagnostics(name);
                Assert.That.AtEndOfFile(parser);
            }
        }
    }
    #endregion

    #region 表达式
    [TestMethod]
    public void LiteralExpressionParseTests()
    {
        var parser = LanguageParserTests.CreateLanguageParser("""
            nil
            false
            true
            1
            1.0
            'string'
            ""
            """);
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.NilLiteralExpression, SyntaxKind.NilKeyword);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.NilLiteralExpression);
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.NotAtEndOfFile(parser);
        }
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.FalseLiteralExpression, SyntaxKind.FalseKeyword);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.FalseLiteralExpression);
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.NotAtEndOfFile(parser);
        }
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxKind.TrueKeyword);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.TrueLiteralExpression);
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.NotAtEndOfFile(parser);
        }
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.NotAtEndOfFile(parser);
        }
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.NumericLiteralExpression, 1D);
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.NotAtEndOfFile(parser);
        }
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxKind.StringLiteralToken);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.StringLiteralExpression, "string");
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.NotAtEndOfFile(parser);
        }
        {
            var literal = parser.ParseLiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxKind.StringLiteralToken);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.StringLiteralExpression, string.Empty);
            Assert.That.NotContainsDiagnostics(literal);
            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void ParenthesizedExpressionParseTests()
    {
        var tree = new Tree<SyntaxKind>();
        { // 合法的括号
            var parser = LanguageParserTests.CreateLanguageParser(" (a) ");
            var expr = parser.ParseParenthesizedExpression();
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.ParenthesizedExpression) { SyntaxKind.IdentifierName };
            Assert.That.IsParenthesizedExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 不合法的空的括号
            var parser = LanguageParserTests.CreateLanguageParser(" () ");
            var expr = parser.ParseParenthesizedExpression();
            Assert.That.IsParenthesizedExpression(expr);
            Assert.That.ContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 不合法的非空的括号，右括号缺失
            var parser = LanguageParserTests.CreateLanguageParser(" (a 1.0");
            {
                var expr = parser.ParseParenthesizedExpression();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.ParenthesizedExpression) { SyntaxKind.IdentifierName };
                Assert.That.IsParenthesizedExpression(expr, root);
                Assert.That.ContainsDiagnostics(expr);
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var expr = parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken);
                Assert.That.IsLiteralExpression(expr, SyntaxKind.NumericLiteralExpression, 1D);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }
        }
    }

    [TestMethod]
    public void ExpressionWithOperatorParseTests()
    {
        #region 基础运算式
        #region 一元运算式
        { // 取负
            var parser = LanguageParserTests.CreateLanguageParser(" -1 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsUnaryExpression(expr, SyntaxKind.UnaryMinusExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 逻辑非
            var parser = LanguageParserTests.CreateLanguageParser(" not true ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsUnaryExpression(expr, SyntaxKind.LogicalNotExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 取长度
            var parser = LanguageParserTests.CreateLanguageParser(" #t ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsUnaryExpression(expr, SyntaxKind.LengthExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 按位非
            var parser = LanguageParserTests.CreateLanguageParser(" ~1 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsUnaryExpression(expr, SyntaxKind.BitwiseNotExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        #endregion
        #region 二元运算式
        { // 加法
            var parser = LanguageParserTests.CreateLanguageParser(" 1 + 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.AdditionExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 减法
            var parser = LanguageParserTests.CreateLanguageParser(" 1 - 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.SubtractionExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 乘法
            var parser = LanguageParserTests.CreateLanguageParser(" 1 * 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.MultiplicationExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 除法
            var parser = LanguageParserTests.CreateLanguageParser(" 1 / 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.DivisionExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 向下取整除法
            var parser = LanguageParserTests.CreateLanguageParser(" 1 // 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.FloorDivisionExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 取幂
            var parser = LanguageParserTests.CreateLanguageParser(" 1 ^ 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.ExponentiationExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 取模
            var parser = LanguageParserTests.CreateLanguageParser(" 1 % 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.ModuloExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 按位与
            var parser = LanguageParserTests.CreateLanguageParser(" 1 & 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.BitwiseAndExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 按位异或
            var parser = LanguageParserTests.CreateLanguageParser(" 1 ~ 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.BitwiseExclusiveOrExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 按位或
            var parser = LanguageParserTests.CreateLanguageParser(" 1 | 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.BitwiseOrExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 按位左移
            var parser = LanguageParserTests.CreateLanguageParser(" 1 << 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.BitwiseLeftShiftExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 按位右移
            var parser = LanguageParserTests.CreateLanguageParser(" 1 >> 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.BitwiseRightShiftExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 连接
            var parser = LanguageParserTests.CreateLanguageParser(" '1' .. '2' ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.ConcatenationExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 小于
            var parser = LanguageParserTests.CreateLanguageParser(" 1 < 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.LessThanExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 小于等于
            var parser = LanguageParserTests.CreateLanguageParser(" 1 <= 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.LessThanOrEqualExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 大于
            var parser = LanguageParserTests.CreateLanguageParser(" 1 > 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.GreaterThanExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 大于等于
            var parser = LanguageParserTests.CreateLanguageParser(" 1 >= 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.GreaterThanOrEqualExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 相等
            var parser = LanguageParserTests.CreateLanguageParser(" 1 == 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.EqualExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 不等
            var parser = LanguageParserTests.CreateLanguageParser(" 1 ~= 2 ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.NotEqualExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 逻辑与
            var parser = LanguageParserTests.CreateLanguageParser(" true and false ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.AndExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 逻辑或
            var parser = LanguageParserTests.CreateLanguageParser(" true or false ");
            var expr = parser.ParseExpressionWithOperator();
            Assert.That.IsBinaryExpression(expr, SyntaxKind.OrExpression);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        #endregion
        #endregion

        #region 组合运算式
        var unaryExpressionOperatorTokens = (from SyntaxKind kind in Enum.GetValues(typeof(SyntaxKind))
                                             where SyntaxFacts.IsUnaryExpressionOperatorToken(kind)
                                             select kind)
                                             .ToArray();
        var binaryExpressionOperatorTokens = (from SyntaxKind kind in Enum.GetValues(typeof(SyntaxKind))
                                              where SyntaxFacts.IsBinaryExpressionOperatorToken(kind)
                                              select kind)
                                              .ToArray();
        var leftAssociativeBinaryExpressionOperatorTokens = (from kind in binaryExpressionOperatorTokens
                                                             where SyntaxFacts.IsLeftAssociativeBinaryExpressionOperatorToken(kind)
                                                             select kind)
                                                             .ToArray();
        var rightAssociativeBinaryExpressionOperatorTokens = (from kind in binaryExpressionOperatorTokens
                                                              where SyntaxFacts.IsRightAssociativeBinaryExpressionOperatorToken(kind)
                                                              select kind)
                                                              .ToArray();
        { // 两个二元运算式
            var tree = new Tree<SyntaxKind>();

            static void FirstAssociativeBinaryExpressionParseTest(SyntaxKind first, SyntaxKind second, Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" 1 {SyntaxFacts.GetText(first)} a {SyntaxFacts.GetText(second)} 'string' ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(second))
                {
                    new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(first))
                    {
                        SyntaxKind.NumericLiteralExpression,
                        SyntaxKind.IdentifierName
                    },
                    SyntaxKind.StringLiteralExpression
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }
            static void SecondAssociativeBinaryExpressionParseTest(SyntaxKind first, SyntaxKind second, Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" 1 {SyntaxFacts.GetText(first)} a {SyntaxFacts.GetText(second)} 'string' ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(first))
                {
                    SyntaxKind.NumericLiteralExpression,
                    new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(second))
                    {
                        SyntaxKind.IdentifierName,
                        SyntaxKind.StringLiteralExpression
                    }
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }

            { // 左结合运算式，两个运算符相同
                var tokens = leftAssociativeBinaryExpressionOperatorTokens;
                foreach (var token in tokens)
                    FirstAssociativeBinaryExpressionParseTest(token, token, tree);
            }
            { // 右结合运算式，两个运算符相同
                var tokens = rightAssociativeBinaryExpressionOperatorTokens;
                foreach (var token in tokens)
                    SecondAssociativeBinaryExpressionParseTest(token, token, tree);
            }
            { // 左结合运算式，两个运算符优先级相同
                foreach (var first in leftAssociativeBinaryExpressionOperatorTokens)
                    foreach (var second in leftAssociativeBinaryExpressionOperatorTokens)
                    {
                        if (SyntaxFacts.GetOperatorPrecedence(first, false) != SyntaxFacts.GetOperatorPrecedence(second, false)) continue;
                        FirstAssociativeBinaryExpressionParseTest(first, second, tree);
                    }
            }
            { // 右结合运算式，两个运算符优先级相同
                foreach (var first in rightAssociativeBinaryExpressionOperatorTokens)
                    foreach (var second in rightAssociativeBinaryExpressionOperatorTokens)
                    {
                        if (SyntaxFacts.GetOperatorPrecedence(first, false) != SyntaxFacts.GetOperatorPrecedence(second, false)) continue;
                        SecondAssociativeBinaryExpressionParseTest(first, second, tree);
                    }
            }
            { // 两个运算符优先级不相同
                foreach (var first in binaryExpressionOperatorTokens)
                    foreach (var second in binaryExpressionOperatorTokens)
                    {
                        var firstPrecedence = SyntaxFacts.GetOperatorPrecedence(first, false);
                        var secondPrecedence = SyntaxFacts.GetOperatorPrecedence(second, false);
                        if (firstPrecedence < secondPrecedence)
                            SecondAssociativeBinaryExpressionParseTest(first, second, tree);
                        else if (firstPrecedence > secondPrecedence)
                            FirstAssociativeBinaryExpressionParseTest(first, second, tree);
                    }
            }
            { // 第一个为左结合运算符，第二个为右结合运算符，两个运算符优先级相同
                foreach (var first in leftAssociativeBinaryExpressionOperatorTokens)
                    foreach (var second in rightAssociativeBinaryExpressionOperatorTokens)
                    {
                        if (SyntaxFacts.GetOperatorPrecedence(first, false) != SyntaxFacts.GetOperatorPrecedence(second, false)) continue;
                        SecondAssociativeBinaryExpressionParseTest(first, second, tree);
                    }
            }
            { // 第一个为右结合运算符，第二个为左结合运算符，两个运算符优先级相同
                foreach (var first in rightAssociativeBinaryExpressionOperatorTokens)
                    foreach (var second in leftAssociativeBinaryExpressionOperatorTokens)
                    {
                        if (SyntaxFacts.GetOperatorPrecedence(first, false) != SyntaxFacts.GetOperatorPrecedence(second, false)) continue;
                        FirstAssociativeBinaryExpressionParseTest(first, second, tree);
                    }
            }
        }

        { // 两个一元运算式
            var tree = new Tree<SyntaxKind>();

            static void UnaryExpressionParseTest(SyntaxKind first, SyntaxKind second, Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" {SyntaxFacts.GetText(first)} {SyntaxFacts.GetText(second)} a ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetUnaryExpression(first))
                {
                    new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetUnaryExpression(second))
                    {
                        SyntaxKind.IdentifierName
                    }
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }

            foreach (var first in unaryExpressionOperatorTokens)
                foreach (var second in unaryExpressionOperatorTokens)
                {
                    UnaryExpressionParseTest(first, second, tree);
                }
        }

        { // 第一个为二元运算符，第二个为一元运算符
            var tree = new Tree<SyntaxKind>();

            static void UnaryAssociativeExpressionParseTest(SyntaxKind first, SyntaxKind second, Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" 1 {SyntaxFacts.GetText(first)} {SyntaxFacts.GetText(second)} a ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(first))
                {
                    SyntaxKind.NumericLiteralExpression,
                    new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetUnaryExpression(second))
                    {
                        SyntaxKind.IdentifierName
                    }
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }

            foreach (var first in binaryExpressionOperatorTokens)
                foreach (var second in unaryExpressionOperatorTokens)
                {
                    UnaryAssociativeExpressionParseTest(first, second, tree);
                }
        }

        { // 第一个为一元运算符，第二个为二元运算符
            var tree = new Tree<SyntaxKind>();

            static void UnaryAssociativeExpressionParseTest(SyntaxKind first, SyntaxKind second, Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" {SyntaxFacts.GetText(first)} 1 {SyntaxFacts.GetText(second)} a ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(second))
                {
                    new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetUnaryExpression(first))
                    {
                        SyntaxKind.NumericLiteralExpression
                    },
                    SyntaxKind.IdentifierName
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }
            static void BinaryAssociativeExpressionParseTest(SyntaxKind first, SyntaxKind second, Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" {SyntaxFacts.GetText(first)} 1 {SyntaxFacts.GetText(second)} a ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetUnaryExpression(first))
                {
                    new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(second))
                    {
                        SyntaxKind.NumericLiteralExpression,
                        SyntaxKind.IdentifierName
                    }
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }

            foreach (var first in unaryExpressionOperatorTokens)
                foreach (var second in binaryExpressionOperatorTokens)
                {
                    var firstPrecedence = SyntaxFacts.GetOperatorPrecedence(first, true);
                    var secondPrecedence = SyntaxFacts.GetOperatorPrecedence(second, false);
                    if (firstPrecedence < secondPrecedence)
                        BinaryAssociativeExpressionParseTest(first, second, tree);
                    else if (firstPrecedence > secondPrecedence)
                        UnaryAssociativeExpressionParseTest(first, second, tree);
                }
        }
        #endregion

        #region 括号表达式
        {
            var tree = new Tree<SyntaxKind>();

            static void ParenthesizedExpressionParseTest(
                SyntaxKind first,
                SyntaxKind second,
                SyntaxKind third,
                SyntaxKind forth,
                Tree<SyntaxKind> tree)
            {
                var parser = LanguageParserTests.CreateLanguageParser($" {SyntaxFacts.GetText(first)}((1 {SyntaxFacts.GetText(second)} a){SyntaxFacts.GetText(third)}('string' {SyntaxFacts.GetText(forth)} false)) ");
                var expr = parser.ParseExpressionWithOperator();
                var root = new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetUnaryExpression(first))
                {
                    new TreeNode<SyntaxKind>(tree, SyntaxKind.ParenthesizedExpression)
                    {
                        new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(third))
                        {
                            new TreeNode<SyntaxKind>(tree, SyntaxKind.ParenthesizedExpression)
                            {
                                new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(second))
                                {
                                    SyntaxKind.NumericLiteralExpression,
                                    SyntaxKind.IdentifierName
                                }
                            },
                            new TreeNode<SyntaxKind>(tree, SyntaxKind.ParenthesizedExpression)
                            {
                                new TreeNode<SyntaxKind>(tree, SyntaxFacts.GetBinaryExpression(forth))
                                {
                                    SyntaxKind.StringLiteralExpression,
                                    SyntaxKind.FalseLiteralExpression
                                }
                            }
                        }
                    }
                };
                Assert.That.IsExpression(expr, root);
                Assert.That.NotContainsDiagnostics(expr);
                Assert.That.AtEndOfFile(parser);
            }

            foreach (var first in unaryExpressionOperatorTokens)
                foreach (var second in binaryExpressionOperatorTokens)
                    foreach (var third in binaryExpressionOperatorTokens)
                        foreach (var forth in binaryExpressionOperatorTokens)
                        {
                            ParenthesizedExpressionParseTest(first, second, third, forth, tree);
                        }
        }
        #endregion
    }

    [TestMethod]
    public void MemberAccessExpressionParseTests()
    {
        var tree = new Tree<SyntaxKind>();
        { // 通过普通成员操作语法获取标识符的成员
            var parser = LanguageParserTests.CreateLanguageParser("a.b");
            var expr = parser.ParseSimpleMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsSimpleMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");

            Assert.That.IsIdentifierName(expr.MemberName, "b");

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过普通成员操作语法获取整型数字常量的成员
            var parser = LanguageParserTests.CreateLanguageParser("1.GetType");
            var expr = parser.ParseSimpleMemberAccessExpressionSyntax(parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken));
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                SyntaxKind.NumericLiteralExpression,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsSimpleMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)expr.Self, SyntaxKind.NumericLiteralExpression, 1L);

            Assert.That.IsIdentifierName(expr.MemberName, "GetType");

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过普通成员操作语法获取浮点型数字常量的成员
            var parser = LanguageParserTests.CreateLanguageParser("1.0.ToString");
            var expr = parser.ParseSimpleMemberAccessExpressionSyntax(parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken));
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                SyntaxKind.NumericLiteralExpression,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsSimpleMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)expr.Self, SyntaxKind.NumericLiteralExpression, 1D);

            Assert.That.IsIdentifierName(expr.MemberName, "ToString");

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过普通成员操作语法获取标识符的成员，但成员错误使用了整型数字常量
            // 解析器会将其解析成一个实际为“a”的部分缺失的普通成员操作表达式，和一个实际为“.1”的浮点型数字常量表达式。
            var parser = LanguageParserTests.CreateLanguageParser("a.1");
            var expr = parser.ParseSimpleMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsSimpleMemberAccessExpression(expr, root);
            Assert.That.ContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");
            Assert.That.NotContainsDiagnostics(expr.Self);

            Assert.That.IsMissing(expr.OperatorToken);

            Assert.That.IsMissingIdentifierName(expr.MemberName);
            Assert.That.ContainsDiagnostics(expr.MemberName);

            Assert.That.NotAtEndOfFile(parser);

            var literal = parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.NumericLiteralExpression, 0.1D);
            Assert.That.NotContainsDiagnostics(expr.Self);

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过普通成员操作语法获取标识符的成员，但成员错误使用了浮点型数字常量
            // 解析器会将其解析成一个实际为“a”的部分缺失的普通成员操作表达式，后为一个实际为“.1”的浮点型数字常量表达式，最后为一个实际为“.0”的浮点型数字常量表达式。
            var parser = LanguageParserTests.CreateLanguageParser("a.1.0");
            var expr = parser.ParseSimpleMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsSimpleMemberAccessExpression(expr, root);
            Assert.That.ContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");
            Assert.That.ContainsDiagnostics(expr);

            Assert.That.IsMissing(expr.OperatorToken);

            Assert.That.IsMissingIdentifierName(expr.MemberName);
            Assert.That.ContainsDiagnostics(expr);

            Assert.That.NotAtEndOfFile(parser);

            {
                var literal = parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken);
                Assert.That.IsLiteralExpression(literal, SyntaxKind.NumericLiteralExpression, 0.1D);
                Assert.That.NotContainsDiagnostics(expr.Self);
            }
            {
                var literal = parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken);
                Assert.That.IsLiteralExpression(literal, SyntaxKind.NumericLiteralExpression, 0D);
                Assert.That.NotContainsDiagnostics(expr.Self);
            }

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过普通成员操作语法获取标识符的成员，但成员错误使用了字符串常量
            // 解析器会将其解析成一个实际为“a.”部分缺失的普通成员操作表达式，后为一个字符串常量表达式。
            var parser = LanguageParserTests.CreateLanguageParser("a.'string'");
            var expr = parser.ParseSimpleMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsSimpleMemberAccessExpression(expr, root);
            Assert.That.ContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");
            Assert.That.ContainsDiagnostics(expr);

            Assert.That.IsNotMissing(expr.OperatorToken);

            Assert.That.IsMissingIdentifierName(expr.MemberName);
            Assert.That.ContainsDiagnostics(expr);

            Assert.That.NotAtEndOfFile(parser);

            var literal = parser.ParseLiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxKind.StringLiteralToken);
            Assert.That.IsLiteralExpression(literal, SyntaxKind.StringLiteralExpression, "string");
            Assert.That.NotContainsDiagnostics(expr.Self);

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过索引成员操作语法获取标识符的成员，索引是标识符
            var parser = LanguageParserTests.CreateLanguageParser("a[b]");
            var expr = parser.ParseIndexMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.IndexMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                SyntaxKind.IdentifierName
            };
            Assert.That.IsIndexMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");

            Assert.IsInstanceOfType(expr.Member, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Member, "b");

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过索引成员操作语法获取标识符的成员，索引是常量
            var parser = LanguageParserTests.CreateLanguageParser("a[\"b\"]");
            var expr = parser.ParseIndexMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.IndexMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                SyntaxKind.StringLiteralExpression
            };
            Assert.That.IsIndexMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");

            Assert.IsInstanceOfType(expr.Member, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)expr.Member, SyntaxKind.StringLiteralExpression, "b");

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过索引成员操作语法获取常量表达式的成员，索引是常量
            var parser = LanguageParserTests.CreateLanguageParser("1['ToString']");
            var expr = parser.ParseIndexMemberAccessExpressionSyntax(parser.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxKind.NumericLiteralToken));
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.IndexMemberAccessExpression)
            {
                SyntaxKind.NumericLiteralExpression,
                SyntaxKind.StringLiteralExpression
            };
            Assert.That.IsIndexMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)expr.Self, SyntaxKind.NumericLiteralExpression, 1L);

            Assert.IsInstanceOfType(expr.Member, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)expr.Member, SyntaxKind.StringLiteralExpression, "ToString");

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过索引成员操作语法获取标识符的成员，索引是表达式
            var parser = LanguageParserTests.CreateLanguageParser("a[1.0..[[string]]]");
            var expr = parser.ParseIndexMemberAccessExpressionSyntax(parser.ParseIdentifierName());
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.IndexMemberAccessExpression)
            {
                SyntaxKind.IdentifierName,
                new TreeNode<SyntaxKind>(tree, SyntaxKind.ConcatenationExpression)
                {
                    SyntaxKind.NumericLiteralExpression,
                    SyntaxKind.StringLiteralExpression
                }
            };
            Assert.That.IsIndexMemberAccessExpression(expr, root);
            Assert.That.NotContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr.Self, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr.Self, "a");

            Assert.IsInstanceOfType(expr.Member, typeof(BinaryExpressionSyntax));
            var binary = (BinaryExpressionSyntax)expr.Member;
            {
                Assert.IsInstanceOfType(binary.Left, typeof(LiteralExpressionSyntax));
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)binary.Left, SyntaxKind.NumericLiteralExpression, 1D);

                Assert.IsInstanceOfType(binary.Right, typeof(LiteralExpressionSyntax));
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)binary.Right, SyntaxKind.StringLiteralExpression, "string");
            }

            Assert.That.AtEndOfFile(parser);
        }
        { // 通过索引成员操作语法获取标识符的成员，但错误输入普通成员操作语法
            var parser = LanguageParserTests.CreateLanguageParser("a.[b].c");
            var expr = parser.ParseExpression();
            var root = new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
            {
                new TreeNode<SyntaxKind>(tree, SyntaxKind.IndexMemberAccessExpression)
                {
                    new TreeNode<SyntaxKind>(tree, SyntaxKind.SimpleMemberAccessExpression)
                    {
                        SyntaxKind.IdentifierName,
                        SyntaxKind.IdentifierName
                    },
                    SyntaxKind.IdentifierName
                },
                SyntaxKind.IdentifierName
            };
            Assert.That.IsExpression(expr, root);
            Assert.That.ContainsDiagnostics(expr);

            Assert.IsInstanceOfType(expr, typeof(SimpleMemberAccessExpressionSyntax));
            var outerSimple = (SimpleMemberAccessExpressionSyntax)expr;
            {
                Assert.That.ContainsDiagnostics(outerSimple);

                Assert.That.IsIdentifierName(outerSimple.MemberName, "c");
                Assert.That.NotContainsDiagnostics(outerSimple.MemberName);
            }

            Assert.IsInstanceOfType(outerSimple.Self, typeof(IndexMemberAccessExpressionSyntax));
            var innerIndex = (IndexMemberAccessExpressionSyntax)outerSimple.Self;
            {
                Assert.That.ContainsDiagnostics(innerIndex);

                Assert.IsInstanceOfType(innerIndex.Member, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)innerIndex.Member, "b");
                Assert.That.NotContainsDiagnostics(innerIndex.Member);
            }

            Assert.IsInstanceOfType(innerIndex.Self, typeof(SimpleMemberAccessExpressionSyntax));
            var innerSimple = (SimpleMemberAccessExpressionSyntax)innerIndex.Self;
            {
                Assert.That.ContainsDiagnostics(innerSimple);

                Assert.IsInstanceOfType(innerSimple.Self, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)innerSimple.Self, "a");
                Assert.That.NotContainsDiagnostics(innerSimple.Self);

                Assert.That.IsMissingIdentifierName(innerSimple.MemberName);
                Assert.That.ContainsDiagnostics(innerSimple.MemberName);
            }

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void TableConstructorExpressionParseTests()
    {
        { // 空的表构造表达式
            var parser = LanguageParserTests.CreateLanguageParser("{}");
            var table = parser.ParseTableConstructorExpression();
            Assert.That.NotContainsDiagnostics(table);
            Assert.That.IsEmptyList(table.Fields);
            Assert.That.AtEndOfFile(parser);
        }
        { // 非空的表构造表达式
            var parser = LanguageParserTests.CreateLanguageParser($"{{{FieldListSouce}}}");
            var table = parser.ParseTableConstructorExpression();
            Assert.That.NotContainsDiagnostics(table);
            FieldListTest(table.Fields);
            Assert.That.AtEndOfFile(parser);
        }
        { // 以分隔符结尾的非空的表构造表达式
            var parser = LanguageParserTests.CreateLanguageParser($"{{{FieldListSouce},}}");
            var table = parser.ParseTableConstructorExpression();
            Assert.That.NotContainsDiagnostics(table);
            FieldListTest(table.Fields);
            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void InvocationExpressionParseTests()
    {
        var parser = LanguageParserTests.CreateLanguageParser("""
            empty()
            list(a,2)
            table{a=1,2}
            print[[line]]
            a.b()
            a[b][[line]]
            a:b()
            """);
        { // 空的参数列表
            var invocation = parser.ParseInvocationExpressionSyntax(parser.ParseIdentifierName());
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.That.IsIdentifierName((IdentifierNameSyntax)invocation.Expression, "empty");

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentListSyntax));
            Assert.That.IsEmptyList(((ArgumentListSyntax)invocation.Arguments).List);

            Assert.That.NotAtEndOfFile(parser);
        }
        { // 参数列表
            var invocation = parser.ParseInvocationExpressionSyntax(parser.ParseIdentifierName());
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.That.IsIdentifierName((IdentifierNameSyntax)invocation.Expression, "list");

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentListSyntax));
            var arguments = (ArgumentListSyntax)invocation.Arguments;
            Assert.That.IsNotEmptyList(arguments.List, 2);

            {
                Assert.IsInstanceOfType(arguments.List[0]!.Expression, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)arguments.List[0]!.Expression, "a");

                Assert.IsInstanceOfType(arguments.List[1]!.Expression, typeof(LiteralExpressionSyntax));
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)arguments.List[1]!.Expression, SyntaxKind.NumericLiteralExpression, 2L);
            }

            Assert.That.NotAtEndOfFile(parser);
        }
        { // 参数表
            var invocation = parser.ParseInvocationExpressionSyntax(parser.ParseIdentifierName());
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.That.IsIdentifierName((IdentifierNameSyntax)invocation.Expression, "table");

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentTableSyntax));
            var arguments = (ArgumentTableSyntax)invocation.Arguments;
            Assert.That.IsNotEmptyList(arguments.Table.Fields, 2);

            {
                Assert.IsInstanceOfType(arguments.Table.Fields[0], typeof(NameValueFieldSyntax));
                var field = (NameValueFieldSyntax)arguments.Table.Fields[0]!;
                Assert.That.IsIdentifierName(field.FieldName, "a");
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 1L);
            }
            {
                Assert.IsInstanceOfType(arguments.Table.Fields[1], typeof(ItemFieldSyntax));
                var field = (ItemFieldSyntax)arguments.Table.Fields[1]!;
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 2L);
            }

            Assert.That.NotAtEndOfFile(parser);
        }
        { // 参数字符串
            var invocation = parser.ParseInvocationExpressionSyntax(parser.ParseIdentifierName());
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.That.IsIdentifierName((IdentifierNameSyntax)invocation.Expression, "print");

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentStringSyntax));
            var arguments = (ArgumentStringSyntax)invocation.Arguments;

            Assert.That.IsLiteral(arguments.String, "line");
        }
        { // 左侧是普通成员操作表达式，右侧是空的参数列表
            var invocation = parser.ParseExpression() as InvocationExpressionSyntax;
            Assert.IsNotNull(invocation);
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.That.IsSimpleMemberAccessExpression(invocation.Expression);

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentListSyntax));
            Assert.That.IsEmptyList(((ArgumentListSyntax)invocation.Arguments).List);

            Assert.That.NotAtEndOfFile(parser);
        }
        { // 左侧是索引成员操作表达式，右侧是参数字符串
            var invocation = parser.ParseExpression() as InvocationExpressionSyntax;
            Assert.IsNotNull(invocation);
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.That.IsIndexMemberAccessExpression(invocation.Expression);

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentStringSyntax));
            var arguments = (ArgumentStringSyntax)invocation.Arguments;

            Assert.That.IsLiteral(arguments.String, "line");

            Assert.That.NotAtEndOfFile(parser);
        }
        { // 传入隐式self参数的调用表达式，右侧是空的参数列表
            var invocation = parser.ParseExpression() as InvocationExpressionSyntax;
            Assert.IsNotNull(invocation);
            Assert.That.NotContainsDiagnostics(invocation);

            Assert.IsInstanceOfType(invocation.Expression, typeof(ImplicitSelfParameterExpressionSyntax));

            Assert.IsInstanceOfType(invocation.Arguments, typeof(ArgumentListSyntax));
            Assert.That.IsEmptyList(((ArgumentListSyntax)invocation.Arguments).List);

            Assert.That.AtEndOfFile(parser);
        }
    }
    #endregion

    #region 字段
    [TestMethod]
    public void FieldValueParseTests()
    {
        { // 字段值为合法表达式
            var parser = LanguageParserTests.CreateLanguageParser("1");
            var expr = parser.ParseFieldValue();
            Assert.IsInstanceOfType(expr, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)expr, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.NotContainsDiagnostics(expr);
            Assert.That.AtEndOfFile(parser);
        }
        { // 字段值为合法表达式，但后方错误追加了其他标志和表达式
            // 只识别第一个合法表达式，后方的标志和表达式作为前者的被跳过的标志的语法琐碎内容，并报告错误。
            var parser = LanguageParserTests.CreateLanguageParser("a if b + c then return true end");
            var expr = parser.ParseFieldValue();
            Assert.IsInstanceOfType(expr, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)expr, "a");
            Assert.That.ContainsDiagnostics(expr);
            Assert.That.ContainsDiagnostics(expr.GetLastToken()!, ErrorCode.ERR_InvalidFieldValueTerm);
            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void NameValueFieldParseTests()
    {
        { // 合法的名值对字段
            var parser = LanguageParserTests.CreateLanguageParser("a = 1");
            var field = parser.ParseNameValueField();
            Assert.That.NotContainsDiagnostics(field);

            Assert.That.IsIdentifierName(field.FieldName, "a");

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 1L);

            Assert.That.AtEndOfFile(parser);
        }
        { // 不合法的名值对字段，缺少名称
            var parser = LanguageParserTests.CreateLanguageParser(" = 'string'");
            var field = parser.ParseNameValueField();
            Assert.That.ContainsDiagnostics(field);

            Assert.That.IsMissingIdentifierName(field.FieldName);
            Assert.That.ContainsDiagnostics(field);

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.StringLiteralExpression, "string");
            Assert.That.NotContainsDiagnostics(field.FieldValue);

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void KeyValueFieldParseTests()
    {
        { // 合法的键值对字段
            var parser = LanguageParserTests.CreateLanguageParser("[a] = 1");
            var field = parser.ParseKeyValueField();
            Assert.That.NotContainsDiagnostics(field);

            Assert.IsInstanceOfType(field.FieldKey, typeof(IdentifierNameSyntax));
            Assert.That.IsIdentifierName((IdentifierNameSyntax)field.FieldKey, "a");

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 1L);

            Assert.That.AtEndOfFile(parser);
        }
        { // 不合法的键值对字段，缺失键表达式
            var parser = LanguageParserTests.CreateLanguageParser("[] = 1");
            var field = parser.ParseKeyValueField();
            Assert.That.ContainsDiagnostics(field);

            Assert.IsInstanceOfType(field.FieldKey, typeof(IdentifierNameSyntax));
            Assert.That.IsMissingIdentifierName((IdentifierNameSyntax)field.FieldKey);
            Assert.That.ContainsDiagnostics(field.FieldKey);
            Assert.That.ContainsDiagnostics(field.FieldKey, ErrorCode.ERR_InvalidExprTerm);

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.NotContainsDiagnostics(field.FieldValue);

            Assert.That.AtEndOfFile(parser);
        }
        { // 不合法的键值对字段，缺失值表达式
            var parser = LanguageParserTests.CreateLanguageParser("['string'] = ");
            var field = parser.ParseKeyValueField();
            Assert.That.ContainsDiagnostics(field);

            Assert.IsInstanceOfType(field.FieldKey, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldKey, SyntaxKind.StringLiteralExpression, "string");
            Assert.That.NotContainsDiagnostics(field.FieldKey);

            Assert.IsInstanceOfType(field.FieldValue, typeof(IdentifierNameSyntax));
            Assert.That.IsMissingIdentifierName((IdentifierNameSyntax)field.FieldValue);
            Assert.That.ContainsDiagnostics(field);
        }
        { // 不合法的键值对字段，键表达式未使用方括号包裹
            var parser = LanguageParserTests.CreateLanguageParser("1.0 = true");
            var field = parser.ParseKeyValueField();
            Assert.That.ContainsDiagnostics(field);

            Assert.That.IsMissing(field.OpenBracketToken);
            Assert.That.ContainsDiagnostics(field.OpenBracketToken);

            Assert.IsInstanceOfType(field.FieldKey, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldKey, SyntaxKind.NumericLiteralExpression, 1D);
            Assert.That.NotContainsDiagnostics(field.FieldKey);

            Assert.That.IsMissing(field.CloseBracketToken);
            Assert.That.ContainsDiagnostics(field.CloseBracketToken);

            Assert.That.IsNotMissing(field.EqualsToken);
            Assert.That.NotContainsDiagnostics(field.EqualsToken);

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.TrueLiteralExpression);
            Assert.That.NotContainsDiagnostics(field.FieldValue);

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void FieldListParseTests()
    {
        var parser = LanguageParserTests.CreateLanguageParser(FieldListSouce);
        var fieldList = parser.ParseFieldList();
        FieldListTest(fieldList);
    }

    private const string FieldListSouce = """
        integer = 5,
        float = 5.0,
        [a+b]=c^d;
        true,
        'string'
        """;
    private static void FieldListTest(SeparatedSyntaxList<FieldSyntax> fields)
    {
        Assert.That.NotContainsDiagnostics(fields);
        Assert.That.IsNotEmptyList(fields, 5);
        {
            Assert.IsInstanceOfType(fields[0]!, typeof(NameValueFieldSyntax));
            var field = (NameValueFieldSyntax)fields[0]!;

            Assert.That.IsIdentifierName(field.FieldName, "integer");

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 5L);
        }
        {
            Assert.IsInstanceOfType(fields[1]!, typeof(NameValueFieldSyntax));
            var field = (NameValueFieldSyntax)fields[1]!;

            Assert.That.IsIdentifierName(field.FieldName, "float");

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.NumericLiteralExpression, 5D);
        }
        {
            Assert.IsInstanceOfType(fields[2]!, typeof(KeyValueFieldSyntax));
            var field = (KeyValueFieldSyntax)fields[2]!;

            Assert.That.IsBinaryExpression(field.FieldKey, SyntaxKind.AdditionExpression);
            {
                var binary = (BinaryExpressionSyntax)field.FieldKey;

                Assert.IsInstanceOfType(binary.Left, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)binary.Left, "a");

                Assert.IsInstanceOfType(binary.Right, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)binary.Right, "b");
            }

            Assert.That.IsBinaryExpression(field.FieldValue, SyntaxKind.ExponentiationExpression);
            {
                var binary = (BinaryExpressionSyntax)field.FieldValue;

                Assert.IsInstanceOfType(binary.Left, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)binary.Left, "c");

                Assert.IsInstanceOfType(binary.Right, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)binary.Right, "d");
            }
        }
        {
            Assert.IsInstanceOfType(fields[3]!, typeof(ItemFieldSyntax));
            var field = (ItemFieldSyntax)fields[3]!;

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.TrueLiteralExpression);
        }
        {
            Assert.IsInstanceOfType(fields[4]!, typeof(ItemFieldSyntax));
            var field = (ItemFieldSyntax)fields[4]!;

            Assert.IsInstanceOfType(field.FieldValue, typeof(LiteralExpressionSyntax));
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)field.FieldValue, SyntaxKind.StringLiteralExpression, "string");
        }
    }
    #endregion

    #region 语句
    [TestMethod]
    public void BlockParseTests()
    {
        { // 空块
            var parser = LanguageParserTests.CreateLanguageParser("");
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.NotContainsDiagnostics(block);
            Assert.That.IsEmptyList(block.Statements);
            Assert.IsNull(block.Return);
            Assert.That.AtEndOfFile(parser);
        }
        { // 仅有一句返回语句的块
            var parser = LanguageParserTests.CreateLanguageParser("return nil");
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.NotContainsDiagnostics(block);
            Assert.That.IsEmptyList(block.Statements);
            Assert.IsNotNull(block.Return);
            Assert.That.AtEndOfFile(parser);
        }
        { // 仅有一句非返回语句的块
            var parser = LanguageParserTests.CreateLanguageParser("print 'Hello world!'");
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.NotContainsDiagnostics(block);
            Assert.That.IsNotEmptyList(block.Statements, 1);
            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(block.Return);
            Assert.That.AtEndOfFile(parser);
        }
        { // 有一句非返回语句和一句返回语句的块
            var parser = LanguageParserTests.CreateLanguageParser("""
                print 'Hello world!'
                return nil
                """);
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.NotContainsDiagnostics(block);
            Assert.That.IsNotEmptyList(block.Statements, 1);
            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNotNull(block.Return);
            Assert.That.AtEndOfFile(parser);
        }
        { // 返回语句不在最后一行的块
            var parser = LanguageParserTests.CreateLanguageParser("""
                print 'Hello world!'
                return nil
                goto label
                """);
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.ContainsDiagnostics(block);
            Assert.That.IsNotEmptyList(block.Statements, 3);

            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.That.NotContainsDiagnostics(block.Statements[0]!);

            Assert.IsInstanceOfType(block.Statements[1]!, typeof(ReturnStatementSyntax));
            Assert.That.ContainsDiagnostics(block.Statements[1]!, ErrorCode.ERR_MisplacedReturnStat);

            Assert.IsInstanceOfType(block.Statements[2]!, typeof(GotoStatementSyntax));
            Assert.That.NotContainsDiagnostics(block.Statements[2]!);

            Assert.IsNull(block.Return);
            Assert.That.AtEndOfFile(parser);
        }
        { // 返回语句不在最后一行的块
            var parser = LanguageParserTests.CreateLanguageParser("""
                print 'Hello world!'
                return nil
                print 'After return statement'
                return true
                """);
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.ContainsDiagnostics(block);
            Assert.That.IsNotEmptyList(block.Statements, 3);

            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.That.NotContainsDiagnostics(block.Statements[0]!);

            Assert.IsInstanceOfType(block.Statements[1]!, typeof(ReturnStatementSyntax));
            Assert.That.ContainsDiagnostics(block.Statements[1]!, ErrorCode.ERR_MisplacedReturnStat);

            Assert.IsInstanceOfType(block.Statements[2]!, typeof(InvocationStatementSyntax));
            Assert.That.NotContainsDiagnostics(block.Statements[2]!);

            Assert.IsNotNull(block.Return);
            Assert.That.NotContainsDiagnostics(block.Return!);
            Assert.That.AtEndOfFile(parser);
        }
        { // 返回语句后方有不少于一个空语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                return nil;
                ;
                """);
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.NotContainsDiagnostics(block);
            Assert.That.IsEmptyList(block.Statements);
            Assert.IsNotNull(block.Return);
            Assert.That.AtEndOfFile(parser);
        }
        {
            var parser = LanguageParserTests.CreateLanguageParser("""
                local t = { 2, 3, 5, 7, 11 }
                local count = #t
                for i = 1, count do
                    print(t[i])
                end
                """);
            var block = parser.ParseBlock(SyntaxKind.Chunk);
            Assert.That.NotContainsDiagnostics(block);
            Assert.That.IsNotEmptyList(block.Statements, 3);

            Assert.IsInstanceOfType(block.Statements[0]!, typeof(LocalDeclarationStatementSyntax));
            Assert.IsInstanceOfType(block.Statements[1]!, typeof(LocalDeclarationStatementSyntax));
            Assert.IsInstanceOfType(block.Statements[2]!, typeof(ForStatementSyntax));

            Assert.IsNull(block.Return);

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void AssignmentStatementParseTests()
    {
        { // 合法的多值赋值语句。
            var parser = LanguageParserTests.CreateLanguageParser("a, b = true, false");
            var assignment = parser.ParseAssignmentStatement();
            Assert.That.NotContainsDiagnostics(assignment);

            var left = assignment.Left;
            Assert.That.IsNotEmptyList(left, 2);
            {
                Assert.IsInstanceOfType(left[0]!, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)left[0]!, "a");

                Assert.IsInstanceOfType(left[1]!, typeof(IdentifierNameSyntax));
                Assert.That.IsIdentifierName((IdentifierNameSyntax)left[1]!, "b");
            }

            var right = assignment.Right;
            Assert.That.IsNotEmptyList(right, 2);
            {
                Assert.IsInstanceOfType(right[0]!, typeof(LiteralExpressionSyntax));
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)right[0]!, SyntaxKind.TrueLiteralExpression);

                Assert.IsInstanceOfType(right[1]!, typeof(LiteralExpressionSyntax));
                Assert.That.IsLiteralExpression((LiteralExpressionSyntax)right[1]!, SyntaxKind.FalseLiteralExpression);
            }

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失赋值左值的赋值语句。
            var parser = LanguageParserTests.CreateLanguageParser(" = nil");
            var assignment = parser.ParseAssignmentStatement();
            Assert.That.ContainsDiagnostics(assignment);

            var left = assignment.Left;
            Assert.That.IsNotEmptyList(left, 1);
            Assert.That.ContainsDiagnostics(left);
            {
                Assert.IsInstanceOfType(left[0]!, typeof(IdentifierNameSyntax));
                var identifier = (IdentifierNameSyntax)left[0]!;
                Assert.That.IsMissingIdentifierName(identifier);
                Assert.That.ContainsDiagnostics(identifier);
                Assert.That.ContainsDiagnostics(identifier, ErrorCode.ERR_IdentifierExpected);
            }

            Assert.That.IsNotMissing(assignment.EqualsToken);
            Assert.That.NotContainsDiagnostics(assignment.EqualsToken);

            var right = assignment.Right;
            Assert.That.IsNotEmptyList(right, 1);
            {
                Assert.IsInstanceOfType(right[0]!, typeof(LiteralExpressionSyntax));
                var literal = (LiteralExpressionSyntax)right[0]!;
                Assert.That.IsLiteralExpression(literal, SyntaxKind.NilLiteralExpression);
                Assert.That.NotContainsDiagnostics(literal);
            }

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失第一个赋值左值的赋值语句。
            var parser = LanguageParserTests.CreateLanguageParser(" , b = true, false");
            var assignment = parser.ParseAssignmentStatement();
            Assert.That.ContainsDiagnostics(assignment);

            var left = assignment.Left;
            Assert.That.IsNotEmptyList(left, 2);
            Assert.That.ContainsDiagnostics(left);
            {
                {
                    Assert.IsInstanceOfType(left[0]!, typeof(IdentifierNameSyntax));
                    var literal = (IdentifierNameSyntax)left[0]!;
                    Assert.That.IsMissingIdentifierName(literal);
                    Assert.That.ContainsDiagnostics(literal);
                }

                {
                    Assert.IsInstanceOfType(left[1]!, typeof(IdentifierNameSyntax));
                    var literal = (IdentifierNameSyntax)left[1]!;
                    Assert.That.IsIdentifierName(literal, "b");
                    Assert.That.NotContainsDiagnostics(literal);
                }
            }

            Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
            Assert.That.NotContainsDiagnostics(assignment.Right);

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失最后一个赋值左值的赋值语句。
            var parser = LanguageParserTests.CreateLanguageParser(" a, = true, false");
            var assignment = parser.ParseAssignmentStatement();
            Assert.That.ContainsDiagnostics(assignment);

            var left = assignment.Left;
            Assert.That.IsNotEmptyList(left, 2);
            Assert.That.ContainsDiagnostics(left);
            {
                {
                    Assert.IsInstanceOfType(left[0]!, typeof(IdentifierNameSyntax));
                    var literal = (IdentifierNameSyntax)left[0]!;
                    Assert.That.IsIdentifierName(literal, "a");
                    Assert.That.NotContainsDiagnostics(literal);
                }

                {
                    Assert.IsInstanceOfType(left[1]!, typeof(IdentifierNameSyntax));
                    var literal = (IdentifierNameSyntax)left[1]!;
                    Assert.That.IsMissingIdentifierName(literal);
                    Assert.That.ContainsDiagnostics(literal);
                }
            }

            Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
            Assert.That.NotContainsDiagnostics(assignment.Right);

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺失赋值右值的赋值语句
            var parser = LanguageParserTests.CreateLanguageParser("a = ");
            var assignment = parser.ParseAssignmentStatement();
            Assert.That.ContainsDiagnostics(assignment);

            Assert.That.NotContainsDiagnostics(assignment.Left);
            Assert.That.NotContainsDiagnostics(assignment.EqualsToken);

            var right = assignment.Right;
            Assert.That.IsNotEmptyList(right, 1);
            Assert.That.ContainsDiagnostics(right);
            {
                Assert.IsInstanceOfType(right[0]!, typeof(IdentifierNameSyntax));
                var identifier = (IdentifierNameSyntax)right[0]!;
                Assert.That.IsMissingIdentifierName(identifier);
                Assert.That.ContainsDiagnostics(identifier);
                Assert.That.ContainsDiagnostics(identifier, ErrorCode.ERR_ExpressionExpected);
            }

            Assert.That.AtEndOfFile(parser);
        }
        { // 正确和不正确的赋值左值
            var parser = LanguageParserTests.CreateLanguageParser("""
                a = nil
                a.b = nil
                a[b] = nil
                a + b = nil
                ;(a + b) = nil
                ;-a = nil
                nil = nil
                false = false
                true = true
                1 = 1
                1.0 = 1.0
                ;"string" = 'string'
                ... = ...
                function() end = nil
                ;{} = nil
                a() = nil
                a:b() = nil
                """);
            { // a = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.NotContainsDiagnostics(assignment);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // a.b = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.NotContainsDiagnostics(assignment);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // a[b] = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.NotContainsDiagnostics(assignment);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // a + b = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 作为分隔用的空语句，防止产生调用表达式“nil(a + b)”歧义
                var empty = parser.ParseStatement();
                Assert.IsInstanceOfType(empty, typeof(EmptyStatementSyntax));
                Assert.That.NotContainsDiagnostics(empty);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // (a + b) = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 作为分隔用的空语句，防止产生二元运算符表达式“nil-a”歧义
                var empty = parser.ParseStatement();
                Assert.IsInstanceOfType(empty, typeof(EmptyStatementSyntax));
                Assert.That.NotContainsDiagnostics(empty);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // -a = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // nil = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // false = false
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // true = true
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 1 = 1
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 1.0 = 1.0
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 作为分隔用的空语句，防止产生调用表达式“1.0"string"”歧义
                var empty = parser.ParseStatement();
                Assert.IsInstanceOfType(empty, typeof(EmptyStatementSyntax));
                Assert.That.NotContainsDiagnostics(empty);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // "string" = 'string'
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // ... = ...
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // function() end = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 作为分隔用的空语句，防止产生调用表达式“nil{}”歧义
                var empty = parser.ParseStatement();
                Assert.IsInstanceOfType(empty, typeof(EmptyStatementSyntax));
                Assert.That.NotContainsDiagnostics(empty);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // {} = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // a() = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.NotAtEndOfFile(parser);
            }
            { // a:b() = nil
                var assignment = parser.ParseAssignmentStatement();
                Assert.That.ContainsDiagnostics(assignment);
                Assert.That.ContainsDiagnostics(assignment.Left[0]!, ErrorCode.ERR_AssgLvalueExpected);
                Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
                Assert.That.NotContainsDiagnostics(assignment.Right);
                Assert.That.AtEndOfFile(parser);
            }
        }
    }

    [TestMethod]
    public void ControlStatementParseTests()
    {
        {
            var parser = LanguageParserTests.CreateLanguageParser("::label::");
            var label = parser.ParseLabelStatement();
            Assert.That.NotContainsDiagnostics(label);
            Assert.That.IsIdentifierName(label.Name, "label");
            Assert.That.AtEndOfFile(parser);
        }
        {
            var parser = LanguageParserTests.CreateLanguageParser("break");
            var @break = parser.ParseBreakStatement();
            Assert.That.NotContainsDiagnostics(@break);
            Assert.That.AtEndOfFile(parser);
        }
        {
            var parser = LanguageParserTests.CreateLanguageParser("goto label");
            var @goto = parser.ParseGotoStatement();
            Assert.That.NotContainsDiagnostics(@goto);
            Assert.That.IsIdentifierName(@goto.Name, "label");
            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void ReturnStatementParseTests()
    {
        { // 无返回值
            var parser = LanguageParserTests.CreateLanguageParser("return");
            var stat = parser.ParseReturnStatement();
            Assert.That.NotContainsDiagnostics(stat);
            Assert.That.AtEndOfFile(parser);
        }
        { // 一个返回值
            var parser = LanguageParserTests.CreateLanguageParser("return a");
            var stat = parser.ParseReturnStatement();
            Assert.That.NotContainsDiagnostics(stat);
            Assert.That.IsNotEmptyList(stat.Expressions, 1);

            Assert.That.IsIdentifierName((IdentifierNameSyntax)stat.Expressions[0]!, "a");

            Assert.That.AtEndOfFile(parser);
        }
        { // 多个返回值
            var parser = LanguageParserTests.CreateLanguageParser("return true, nil, false");
            var stat = parser.ParseReturnStatement();
            Assert.That.NotContainsDiagnostics(stat);
            Assert.That.IsNotEmptyList(stat.Expressions, 3);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)stat.Expressions[0]!, SyntaxKind.TrueLiteralExpression);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)stat.Expressions[1]!, SyntaxKind.NilLiteralExpression);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)stat.Expressions[2]!, SyntaxKind.FalseLiteralExpression);

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void DoStatementParseTests()
    {
        var parser = LanguageParserTests.CreateLanguageParser("do return nil end");
        var doStat = parser.ParseDoStatement();
        Assert.That.NotContainsDiagnostics(doStat);

        var block = doStat.Block;
        Assert.AreEqual(0, block.Statements.Count);
        Assert.IsNotNull(block.Return);

        Assert.That.AtEndOfFile(parser);
    }

    [TestMethod]
    public void WhileStatementParseTests()
    {
        var parser = LanguageParserTests.CreateLanguageParser("while i < 100 do print(i + 1) ; i = i + 1 end");
        var whileStat = parser.ParseWhileStatement();
        Assert.That.NotContainsDiagnostics(whileStat);

        Assert.IsInstanceOfType(whileStat.Condition, typeof(BinaryExpressionSyntax));

        var block = whileStat.block;
        Assert.AreEqual(3, block.Statements.Count);
        Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
        Assert.IsInstanceOfType(block.Statements[1]!, typeof(EmptyStatementSyntax));
        Assert.IsInstanceOfType(block.Statements[2]!, typeof(AssignmentStatementSyntax));
        Assert.IsNull(block.Return);

        Assert.That.AtEndOfFile(parser);
    }

    [TestMethod]
    public void RepeatStatementParseTests()
    {
        var parser = LanguageParserTests.CreateLanguageParser("repeat print(i + 1) ; i = i + 1 until i >= 100");
        var repeatStat = parser.ParseRepeatStatement();
        Assert.That.NotContainsDiagnostics(repeatStat);

        Assert.IsInstanceOfType(repeatStat.Condition, typeof(BinaryExpressionSyntax));

        var block = repeatStat.block;
        Assert.AreEqual(3, block.Statements.Count);
        Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
        Assert.IsInstanceOfType(block.Statements[1]!, typeof(EmptyStatementSyntax));
        Assert.IsInstanceOfType(block.Statements[2]!, typeof(AssignmentStatementSyntax));
        Assert.IsNull(block.Return);

        Assert.That.AtEndOfFile(parser);
    }

    [TestMethod]
    public void IfStatementParseTests()
    {
        { // 仅有if语句
            var parser = LanguageParserTests.CreateLanguageParser("if type(a) == 'number' then output = tostring(a) end");
            var ifStat = parser.ParseIfStatement();
            Assert.That.NotContainsDiagnostics(ifStat);

            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));

            var ifBlock = ifStat.block;
            Assert.AreEqual(1, ifBlock.Statements.Count);
            Assert.IsInstanceOfType(ifBlock.Statements[0]!, typeof(AssignmentStatementSyntax));
            Assert.IsNull(ifBlock.Return);

            Assert.AreEqual(0, ifStat.ElseIfs.Count);

            Assert.IsNull(ifStat.Else);

            Assert.That.AtEndOfFile(parser);
        }
        { // 有else从句的if语句
            var parser = LanguageParserTests.CreateLanguageParser("if type(a) == 'number' then output = tostring(a) else error('not number') end");
            var ifStat = parser.ParseIfStatement();
            Assert.That.NotContainsDiagnostics(ifStat);

            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));

            var ifBlock = ifStat.block;
            Assert.AreEqual(1, ifBlock.Statements.Count);
            Assert.IsInstanceOfType(ifBlock.Statements[0]!, typeof(AssignmentStatementSyntax));
            Assert.IsNull(ifBlock.Return);

            Assert.AreEqual(0, ifStat.ElseIfs.Count);

            Assert.IsNotNull(ifStat.Else);
            var elseBlock = ifStat.Else.Block;
            Assert.AreEqual(1, elseBlock.Statements.Count);
            Assert.IsInstanceOfType(elseBlock.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(elseBlock.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 有elseif从句的if语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                if type(x) ~= 'number' then
                    error('x is not number')
                elseif typeof(y) ~= 'number' then
                    error('y is not number')
                end
                """);
            var ifStat = parser.ParseIfStatement();
            Assert.That.NotContainsDiagnostics(ifStat);

            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));

            var ifBlock = ifStat.Block;
            Assert.AreEqual(1, ifBlock.Statements.Count);
            Assert.IsInstanceOfType(ifBlock.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(ifBlock.Return);

            Assert.AreEqual(1, ifStat.ElseIfs.Count);

            var elseIfClause = ifStat.ElseIfs[0]!;
            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));

            var elseIfBlock = elseIfClause.Block;
            Assert.AreEqual(1, elseIfBlock.Statements.Count);
            Assert.IsInstanceOfType(elseIfBlock.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(elseIfBlock.Return);

            Assert.IsNull(ifStat.Else);

            Assert.That.AtEndOfFile(parser);
        }
        { // 有elseif和else从句的if语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                if x < y then return -1
                elseif x > y then return 1
                else return 0
                end
                """);
            var ifStat = parser.ParseIfStatement();
            Assert.That.NotContainsDiagnostics(ifStat);

            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));

            var ifBlock = ifStat.Block;
            Assert.AreEqual(0, ifBlock.Statements.Count);
            Assert.IsNotNull(ifBlock.Return);

            Assert.AreEqual(1, ifStat.ElseIfs.Count);

            var elseIfClause = ifStat.ElseIfs[0]!;
            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));

            var elseIfBlock = elseIfClause.Block;
            Assert.AreEqual(0, elseIfBlock.Statements.Count);
            Assert.IsNotNull(elseIfBlock.Return);

            Assert.IsNotNull(ifStat.Else);

            var elseBlock = ifStat.Else.Block;
            Assert.AreEqual(0, elseBlock.Statements.Count);
            Assert.IsNotNull(elseBlock.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 错误以elseif从句开头的if语句
            // 第一个elseif从句会被作为if解析。
            var parser = LanguageParserTests.CreateLanguageParser("""
                elseif x < y then return -1
                elseif x > y then return 1
                else return 0
                end
                """);
            var ifStat = parser.ParseMisplaceElseIf();
            Assert.That.ContainsDiagnostics(ifStat);

            Assert.That.IsMissing(ifStat.IfKeyword);
            Assert.That.ContainsDiagnostics(ifStat.IfKeyword, ErrorCode.ERR_ElseIfCannotStartStatement);

            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));
            Assert.That.NotContainsDiagnostics(ifStat.Condition);

            var ifBlock = ifStat.Block;
            Assert.AreEqual(0, ifBlock.Statements.Count);
            Assert.IsNotNull(ifBlock.Return);
            Assert.That.NotContainsDiagnostics(ifBlock);

            Assert.AreEqual(1, ifStat.ElseIfs.Count);

            var elseIfClause = ifStat.ElseIfs[0]!;
            Assert.IsInstanceOfType(ifStat.Condition, typeof(BinaryExpressionSyntax));
            Assert.That.NotContainsDiagnostics(elseIfClause);

            var elseIfBlock = elseIfClause.Block;
            Assert.AreEqual(0, elseIfBlock.Statements.Count);
            Assert.IsNotNull(elseIfBlock.Return);

            Assert.IsNotNull(ifStat.Else);
            Assert.That.NotContainsDiagnostics(ifStat.Else);

            var elseBlock = ifStat.Else.Block;
            Assert.AreEqual(0, elseBlock.Statements.Count);
            Assert.IsNotNull(elseBlock.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 嵌套if语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                if a == 1 then
                    if b == 1 then
                    elseif b == 2 then
                    else
                    end
                elseif a == 2 then
                    if c == 1 then
                    elseif c == 2 then
                    else
                    end
                else
                    if d == 1 then
                    elseif d == 2 then
                    else
                    end
                end
                """);
            var ifStat = parser.ParseIfStatement();
            Assert.That.NotContainsDiagnostics(ifStat);
            Assert.That.AtEndOfFile(parser);
        }
        { // 嵌套if语句，但间隔非if语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                if a == 1 then
                    do
                        if b == 1 then
                        elseif b == 2 then
                        else
                        end
                    end
                elseif a == 2 then
                    do
                        if c == 1 then
                        elseif c == 2 then
                        else
                        end
                    end
                else
                    do
                        if d == 1 then
                        elseif d == 2 then
                        else
                        end
                    end
                end
                """);
            var ifStat = parser.ParseIfStatement();
            Assert.That.NotContainsDiagnostics(ifStat);
            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void ForStatementParseTests()
    {
        { // 合法的算术for循环语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                for i = 1, 10 do
                    print(i)
                end
                """);
            var forStat = parser.ParseForStatement() as ForStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.NotContainsDiagnostics(forStat);

            Assert.That.IsIdentifierName(forStat.Name, "i");
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Initial, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Limit, SyntaxKind.NumericLiteralExpression, 10L);
            Assert.IsNull(forStat.Step);

            var block = forStat.Block;
            Assert.AreEqual(1, block.Statements.Count);
            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(block.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 合法的算术for循环语句，指定增量
            var parser = LanguageParserTests.CreateLanguageParser("""
                for i = 1, 10, 2 do
                    print(i)
                end
                """);
            var forStat = parser.ParseForStatement() as ForStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.NotContainsDiagnostics(forStat);

            Assert.That.IsIdentifierName(forStat.Name, "i");
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Initial, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Limit, SyntaxKind.NumericLiteralExpression, 10L);
            Assert.IsNotNull(forStat.Step);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Step, SyntaxKind.NumericLiteralExpression, 2L);

            var block = forStat.Block;
            Assert.AreEqual(1, block.Statements.Count);
            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(block.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺少赋值等号的算术for循环语句
            // 识别到只声明了一个标识符，判断为算术for循环语句。
            var parser = LanguageParserTests.CreateLanguageParser("""
                for i 1, 10, 2 do
                    print(i)
                end
                """);
            var forStat = parser.ParseForStatement() as ForStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.ContainsDiagnostics(forStat);

            Assert.That.IsNotMissing(forStat.ForKeyword);
            Assert.That.NotContainsDiagnostics(forStat.ForKeyword);

            Assert.That.IsIdentifierName(forStat.Name, "i");
            Assert.That.NotContainsDiagnostics(forStat.Name);

            Assert.That.IsMissing(forStat.EqualsToken);
            Assert.That.ContainsDiagnostics(forStat.EqualsToken);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Initial, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.NotContainsDiagnostics(forStat.Initial);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Limit, SyntaxKind.NumericLiteralExpression, 10L);
            Assert.That.NotContainsDiagnostics(forStat.Limit);

            Assert.IsNotNull(forStat.Step);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Step, SyntaxKind.NumericLiteralExpression, 2L);
            Assert.That.NotContainsDiagnostics(forStat.Step);

            Assert.That.IsNotMissing(forStat.DoKeyword);
            Assert.That.NotContainsDiagnostics(forStat.DoKeyword);

            Assert.That.NotContainsDiagnostics(forStat.Block);

            Assert.That.IsNotMissing(forStat.EndKeyword);
            Assert.That.NotContainsDiagnostics(forStat.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺少标识符和赋值等号的算术for循环语句
            // 标识符缺失（声明了一个缺失的标识符），判断为算术for循环语句。
            var parser = LanguageParserTests.CreateLanguageParser("""
                for 1, 10, 2 do
                    print(i)
                end
                """);
            var forStat = parser.ParseForStatement() as ForStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.ContainsDiagnostics(forStat);

            Assert.That.IsNotMissing(forStat.ForKeyword);
            Assert.That.NotContainsDiagnostics(forStat.ForKeyword);

            Assert.That.IsMissingIdentifierName(forStat.Name);
            Assert.That.ContainsDiagnostics(forStat.Name);

            Assert.That.IsMissing(forStat.EqualsToken);
            Assert.That.ContainsDiagnostics(forStat.EqualsToken);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Initial, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.NotContainsDiagnostics(forStat.Initial);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Limit, SyntaxKind.NumericLiteralExpression, 10L);
            Assert.That.NotContainsDiagnostics(forStat.Limit);

            Assert.IsNotNull(forStat.Step);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Step, SyntaxKind.NumericLiteralExpression, 2L);
            Assert.That.NotContainsDiagnostics(forStat.Step);

            Assert.That.IsNotMissing(forStat.DoKeyword);
            Assert.That.NotContainsDiagnostics(forStat.DoKeyword);

            Assert.That.NotContainsDiagnostics(forStat.Block);

            Assert.That.IsNotMissing(forStat.EndKeyword);
            Assert.That.NotContainsDiagnostics(forStat.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
        { // 声明了过多的标识符
            var parser = LanguageParserTests.CreateLanguageParser("""
                for index, item = 1, 10, 2 do
                    print(i)
                end
                """);
            var forStat = parser.ParseForStatement() as ForStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.ContainsDiagnostics(forStat);

            Assert.That.IsNotMissing(forStat.ForKeyword);
            Assert.That.NotContainsDiagnostics(forStat.ForKeyword);

            Assert.That.IsMissingIdentifierName(forStat.Name);
            Assert.That.ContainsDiagnostics(forStat.Name);
            Assert.That.ContainsDiagnostics(forStat.Name, ErrorCode.ERR_TooManyIdentifiers);

            Assert.That.IsNotMissing(forStat.EqualsToken);
            Assert.That.NotContainsDiagnostics(forStat.EqualsToken);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Initial, SyntaxKind.NumericLiteralExpression, 1L);
            Assert.That.NotContainsDiagnostics(forStat.Initial);

            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Limit, SyntaxKind.NumericLiteralExpression, 10L);
            Assert.That.NotContainsDiagnostics(forStat.Limit);

            Assert.IsNotNull(forStat.Step);
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Step, SyntaxKind.NumericLiteralExpression, 2L);
            Assert.That.NotContainsDiagnostics(forStat.Step);

            Assert.That.IsNotMissing(forStat.DoKeyword);
            Assert.That.NotContainsDiagnostics(forStat.DoKeyword);

            Assert.That.NotContainsDiagnostics(forStat.Block);

            Assert.That.IsNotMissing(forStat.EndKeyword);
            Assert.That.NotContainsDiagnostics(forStat.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
        { // 合法的泛型for循环语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                for k, v in pairs(t) do
                    print(k, v)
                end
                """);
            var forStat = parser.ParseForStatement() as ForInStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.NotContainsDiagnostics(forStat);

            Assert.AreEqual(2, forStat.Names.Count);
            Assert.That.IsIdentifierName(forStat.Names[0]!, "k");
            Assert.That.IsIdentifierName(forStat.Names[1]!, "v");

            Assert.That.IsNotEmptyList(forStat.Expressions, 1);
            Assert.IsInstanceOfType(forStat.Expressions[0], typeof(InvocationExpressionSyntax));

            var block = forStat.Block;
            Assert.AreEqual(1, block.Statements.Count);
            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(block.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 合法的泛型for循环语句，使用自定义迭代逻辑
            var parser = LanguageParserTests.CreateLanguageParser("""
                for k, v in next, t, nil do
                    print(k, v)
                end
                """);
            var forStat = parser.ParseForStatement() as ForInStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.NotContainsDiagnostics(forStat);

            Assert.AreEqual(2, forStat.Names.Count);
            Assert.That.IsIdentifierName(forStat.Names[0]!, "k");
            Assert.That.IsIdentifierName(forStat.Names[1]!, "v");

            Assert.That.IsNotEmptyList(forStat.Expressions, 3);
            Assert.That.IsIdentifierName((IdentifierNameSyntax)forStat.Expressions[0]!, "next");
            Assert.That.IsIdentifierName((IdentifierNameSyntax)forStat.Expressions[1]!, "t");
            Assert.That.IsLiteralExpression((LiteralExpressionSyntax)forStat.Expressions[2]!, SyntaxKind.NilLiteralExpression);

            var block = forStat.Block;
            Assert.AreEqual(1, block.Statements.Count);
            Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));
            Assert.IsNull(block.Return);

            Assert.That.AtEndOfFile(parser);
        }
        { // 缺少in关键词的泛型for循环语句
            // 识别到声明了多个标识符，判断为泛型for循环语句。
            var parser = LanguageParserTests.CreateLanguageParser("""
                for k, v pairs(t) do
                    print(k, v)
                end
                """);
            var forStat = parser.ParseForStatement() as ForInStatementSyntax;
            Assert.IsNotNull(forStat);
            Assert.That.ContainsDiagnostics(forStat);

            Assert.That.IsNotMissing(forStat.ForKeyword);
            Assert.That.NotContainsDiagnostics(forStat.ForKeyword);

            Assert.AreEqual(2, forStat.Names.Count);
            Assert.That.IsIdentifierName(forStat.Names[0]!, "k");
            Assert.That.NotContainsDiagnostics(forStat.Names[0]!);
            Assert.That.IsIdentifierName(forStat.Names[1]!, "v");
            Assert.That.NotContainsDiagnostics(forStat.Names[1]!);

            Assert.That.IsMissing(forStat.InKeyword);
            Assert.That.ContainsDiagnostics(forStat.InKeyword);

            Assert.That.IsNotEmptyList(forStat.Expressions, 1);
            Assert.IsInstanceOfType(forStat.Expressions[0], typeof(InvocationExpressionSyntax));

            Assert.That.IsNotMissing(forStat.DoKeyword);
            Assert.That.NotContainsDiagnostics(forStat.DoKeyword);

            Assert.That.NotContainsDiagnostics(forStat.Block);

            Assert.That.IsNotMissing(forStat.EndKeyword);
            Assert.That.NotContainsDiagnostics(forStat.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void FunctionDefinitionParseTests()
    {
        { // 函数定义，函数名称为标识符名称
            var parser = LanguageParserTests.CreateLanguageParser("function func" + FunctionBodySource);
            var func = parser.ParseFunctionDefinitionStatement();
            Assert.That.NotContainsDiagnostics(func);

            Assert.That.IsIdentifierName(func.Name, "func");

            FunctionBodyTest(func.ParameterList, func.Block, func.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
        { // 函数定义，函数名称为限定名称
            var parser = LanguageParserTests.CreateLanguageParser("function a.b.c" + FunctionBodySource);
            var func = parser.ParseFunctionDefinitionStatement();
            Assert.That.NotContainsDiagnostics(func);

            var values = new Stack<string?>();
            values.Push("a");
            values.Push("b");
            values.Push("c");
            Assert.That.IsQualifiedName(func.Name, values);

            FunctionBodyTest(func.ParameterList, func.Block, func.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
        { // 函数定义，函数名称为隐式self参数名称
            var parser = LanguageParserTests.CreateLanguageParser("function a.b:c" + FunctionBodySource);
            var func = parser.ParseFunctionDefinitionStatement();
            Assert.That.NotContainsDiagnostics(func);

            var values = new Stack<string?>();
            values.Push("a");
            values.Push("b");
            values.Push("c");
            Assert.That.IsImplicitSelfParameterName(func.Name, values);

            FunctionBodyTest(func.ParameterList, func.Block, func.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
        { // 临时函数定义
            var parser = LanguageParserTests.CreateLanguageParser("local function func" + FunctionBodySource);
            var func = parser.ParseLocalFunctionDefinitionStatement();
            Assert.That.NotContainsDiagnostics(func);

            Assert.That.IsIdentifierName(func.Name, "func");

            FunctionBodyTest(func.ParameterList, func.Block, func.EndKeyword);

            Assert.That.AtEndOfFile(parser);
        }
    }

    private const string FunctionBodySource = """
        (...)
            print(...)
            return ...
        end
        """;

    private static void FunctionBodyTest(ParameterListSyntax parameterList, BlockSyntax block, SyntaxToken endKeyword)
    {
        Assert.That.IsNotEmptyList(parameterList.Parameters, 1);
        Assert.That.NotContainsDiagnostics(parameterList);
        Assert.AreEqual(SyntaxKind.DotDotDotToken, parameterList.Parameters[0]!.Identifier.Kind);

        Assert.That.IsNotEmptyList(block.Statements, 1);
        Assert.That.NotContainsDiagnostics(block.Statements);
        Assert.IsInstanceOfType(block.Statements[0]!, typeof(InvocationStatementSyntax));

        Assert.IsNotNull(block.Return);
        Assert.That.IsNotEmptyList(block.Return.Expressions, 1);
        Assert.That.NotContainsDiagnostics(block.Return);
        Assert.That.IsLiteralExpression((LiteralExpressionSyntax)block.Return.Expressions[0]!, SyntaxKind.VariousArgumentsExpression);

        Assert.That.IsNotMissing(endKeyword);
        Assert.That.NotContainsDiagnostics(endKeyword);
    }

    [TestMethod]
    public void LocalDeclarationParseTests()
    {
        { // 声明单个变量，不指定初始值
            var parser = LanguageParserTests.CreateLanguageParser("local a");
            var local = parser.ParseLocalDeclarationStatement();
            Assert.That.NotContainsDiagnostics(local);

            Assert.That.IsNotEmptyList(local.NameAttributeLists, 1);
            Assert.That.IsNameAttributeList(local.NameAttributeLists[0]!, "a");

            Assert.That.AtEndOfFile(parser);
        }
    }

    [TestMethod]
    public void StatementStartsWithExpressionParseTests()
    {
        { // 解析为赋值语句
            var parser = LanguageParserTests.CreateLanguageParser("a, b = true, false");
            var stat = parser.ParseStatement();
            Assert.That.NotContainsDiagnostics(stat);
            Assert.IsInstanceOfType(stat, typeof(AssignmentStatementSyntax));

            Assert.That.AtEndOfFile(parser);
        }
        { // 解析为赋值语句
            var parser = LanguageParserTests.CreateLanguageParser(", b = true, false");
            var stat = parser.ParseStatement();
            Assert.That.ContainsDiagnostics(stat);
            Assert.IsInstanceOfType(stat, typeof(AssignmentStatementSyntax));
            var assignment = (AssignmentStatementSyntax)stat;

            Assert.That.ContainsDiagnostics(assignment.Left);
            Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
            Assert.That.NotContainsDiagnostics(assignment.Right);

            Assert.That.AtEndOfFile(parser);
        }
        { // 解析为赋值语句
            var parser = LanguageParserTests.CreateLanguageParser("= nil");
            var stat = parser.ParseStatement();
            Assert.That.ContainsDiagnostics(stat);
            Assert.IsInstanceOfType(stat, typeof(AssignmentStatementSyntax));
            var assignment = (AssignmentStatementSyntax)stat;

            Assert.That.ContainsDiagnostics(assignment.Left);
            Assert.That.NotContainsDiagnostics(assignment.EqualsToken);
            Assert.That.NotContainsDiagnostics(assignment.Right);

            Assert.That.AtEndOfFile(parser);
        }
        { // 解析为调用语句
            var parser = LanguageParserTests.CreateLanguageParser("""
                print'Hello world!'
                a.b[1 + 2](1, 2, 3)
                c:d(1, 2, 3)
                """);
            {
                var stat = parser.ParseStatement();
                Assert.That.NotContainsDiagnostics(stat);
                Assert.IsInstanceOfType(stat, typeof(InvocationStatementSyntax));
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var stat = parser.ParseStatement();
                Assert.That.NotContainsDiagnostics(stat);
                Assert.IsInstanceOfType(stat, typeof(InvocationStatementSyntax));
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var stat = parser.ParseStatement();
                Assert.That.NotContainsDiagnostics(stat);
                Assert.IsInstanceOfType(stat, typeof(InvocationStatementSyntax));
                Assert.That.AtEndOfFile(parser);
            }
        }
        { // 表达式列表解析为包含错误的空语句
            var parser = LanguageParserTests.CreateLanguageParser("1, 2, 3");
            var stat = parser.ParseStatement();
            Assert.That.ContainsDiagnostics(stat);
            Assert.IsInstanceOfType(stat, typeof(EmptyStatementSyntax));
            Assert.That.AtEndOfFile(parser);
        }
        { // 运算符优先级低于成员操作及调用
            var parser = LanguageParserTests.CreateLanguageParser("""
                c.d{ 1, 2, 3 }
                ;-c.d{ 1, 2, 3 }
                """);
            {
                var stat = parser.ParseStatement();
                Assert.That.NotContainsDiagnostics(stat);
                Assert.IsInstanceOfType(stat, typeof(InvocationStatementSyntax));
                Assert.That.NotAtEndOfFile(parser);
            }
            { // 间隔用空语句
                var stat = parser.ParseStatement();
                Assert.That.NotContainsDiagnostics(stat);
                Assert.IsInstanceOfType(stat, typeof(EmptyStatementSyntax));
                Assert.That.NotAtEndOfFile(parser);
            }
            {
                var stat = parser.ParseStatement();
                Assert.That.ContainsDiagnostics(stat);
                Assert.IsInstanceOfType(stat, typeof(EmptyStatementSyntax));
                Assert.That.AtEndOfFile(parser);
            }
        }
    }
    #endregion

    #region 声明
    [TestMethod]
    public void NameAttributeListParseTests()
    {
        { // 无特性
            var parser = LanguageParserTests.CreateLanguageParser("a");
            var name = parser.ParseNameAttributeList();
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.IsNameAttributeList(name, "a");
            Assert.That.AtEndOfFile(parser);
        }
        { // 常量特性
            var parser = LanguageParserTests.CreateLanguageParser("a<const>");
            var name = parser.ParseNameAttributeList();
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.IsNameAttributeList(name, "a", isConst: true);
            Assert.That.AtEndOfFile(parser);
        }
        { // 终结特性
            var parser = LanguageParserTests.CreateLanguageParser("a<close>");
            var name = parser.ParseNameAttributeList();
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.IsNameAttributeList(name, "a", isClose: true);
            Assert.That.AtEndOfFile(parser);
        }
        { // 常量及终结特性
            var parser = LanguageParserTests.CreateLanguageParser("a<const, close>");
            var name = parser.ParseNameAttributeList();
            Assert.That.NotContainsDiagnostics(name);
            Assert.That.IsNameAttributeList(name, "a", isConst: true, isClose: true);
            Assert.That.AtEndOfFile(parser);
        }
        { // 特性列表第一项合法，其他项缺失
            var parser = LanguageParserTests.CreateLanguageParser("a<const, , close>");
            var name = parser.ParseNameAttributeList();
            Assert.That.ContainsDiagnostics(name);
            Assert.IsNotNull(name.AttributeList);
            Assert.That.IsNotEmptyList(name.AttributeList.Attributes, 1);
            Assert.That.IsNameAttributeList(name, "a", isConst: true);
            Assert.That.NotAtEndOfFile(parser);
        }
        { // 特性列表第一项合缺失
            var parser = LanguageParserTests.CreateLanguageParser("a<, close>");
            var name = parser.ParseNameAttributeList();
            Assert.That.ContainsDiagnostics(name);
            Assert.IsNull(name.AttributeList);
            Assert.That.IsNameAttributeList(name, "a");
            Assert.That.NotAtEndOfFile(parser);
        }
    }
    #endregion
}
