// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua;

/// <summary>
/// Represents different parts of Lua syntax.
/// </summary>
public enum SyntaxKind : ushort
{
    /// <summary>Represents unknown part of syntax.</summary>
    None = 0,
    /// <summary>Represents node list.</summary>
    List = GreenNode.ListKind,

    #region Tokens
    #region Punctuations
    /// <summary>Represents <c>+</c> token.</summary>
    PlusToken = 8193,
    /// <summary>Represents <c>-</c> token.</summary>
    MinusToken,
    /// <summary>Represents <c>*</c> token.</summary>
    AsteriskToken,
    /// <summary>Represents <c>/</c> token.</summary>
    SlashToken,
    /// <summary>Represents <c>^</c> token.</summary>
    CaretToken,
    /// <summary>Represents <c>%</c> token.</summary>
    PercentToken,
    /// <summary>Represents <c>@</c> token.</summary>
    CommercialAtToken,
    /// <summary>Represents <c>#</c> token.</summary>
    HashToken,
    /// <summary>Represents <c>&amp;</c> token.</summary>
    AmpersandToken,
    /// <summary>Represents <c>~</c> token.</summary>
    TildeToken,
    /// <summary>Represents <c>|</c> token.</summary>
    BarToken,
    /// <summary>Represents <c>&lt;</c> token.</summary>
    LessThanToken,
    /// <summary>Represents <c>&gt;</c> token.</summary>
    GreaterThanToken,
    /// <summary>Represents <c>=</c> token.</summary>
    EqualsToken,
    /// <summary>Represents <c>(</c> token.</summary>
    OpenParenToken,
    /// <summary>Represents <c>)</c> token.</summary>
    CloseParenToken,
    /// <summary>Represents <c>{</c> token.</summary>
    OpenBraceToken,
    /// <summary>Represents <c>}</c> token.</summary>
    CloseBraceToken,
    /// <summary>Represents <c>[</c> token.</summary>
    OpenBracketToken,
    /// <summary>Represents <c>]</c> token.</summary>
    CloseBracketToken,
    /// <summary>Represents <c>:</c> token.</summary>
    ColonToken,
    /// <summary>Represents <c>;</c> token.</summary>
    SemicolonToken,
    /// <summary>Represents <c>,</c> token.</summary>
    CommaToken,
    /// <summary>Represents <c>.</c> token.</summary>
    DotToken,

    /// <summary>Represents <c>$</c> token.</summary>
    DollarToken = 8256,

    /// <summary>Represents <c>&lt;&lt;</c> token.</summary>
    LessThanLessThanToken = 8257,
    /// <summary>Represents <c>&lt;=</c> token.</summary>
    LessThanEqualsToken,
    /// <summary>Represents <c>&gt;&gt;</c> token.</summary>
    GreaterThanGreaterThanToken,
    /// <summary>Represents <c>&gt;=</c> token.</summary>
    GreaterThanEqualsToken,
    /// <summary>Represents <c>//</c> token.</summary>
    SlashSlashToken,
    /// <summary>Represents <c>==</c> token.</summary>
    EqualsEqualsToken,
    /// <summary>Represents <c>~=</c> token.</summary>
    TildeEqualsToken,
    /// <summary>Represents <c>::</c> token.</summary>
    ColonColonToken,
    /// <summary>Represents <c>..</c> token.</summary>
    DotDotToken,
    /// <summary>Represents <c>...</c> token.</summary>
    DotDotDotToken,

    /// <summary>Represents <c>#!</c> token.</summary>
    HashExclamationToken,
    #endregion

    #region Keywords
    // Reserved Keywords
    /// <summary>Represents <c>and</c> keyword.</summary>
    AndKeyword = 8321,
    /// <summary>Represents <c>break</c> keyword.</summary>
    BreakKeyword,
    /// <summary>Represents <c>do</c> keyword.</summary>
    DoKeyword,
    /// <summary>Represents <c>else</c> keyword.</summary>
    ElseKeyword,
    /// <summary>Represents <c>elseif</c> keyword.</summary>
    ElseIfKeyword,
    /// <summary>Represents <c>end</c> keyword.</summary>
    EndKeyword,
    /// <summary>Represents <c>false</c> keyword.</summary>
    FalseKeyword,
    /// <summary>Represents <c>for</c> keyword.</summary>
    ForKeyword,
    /// <summary>Represents <c>function</c> keyword.</summary>
    FunctionKeyword,
    /// <summary>Represents <c>goto</c> keyword.</summary>
    GotoKeyword,
    /// <summary>Represents <c>if</c> keyword.</summary>
    IfKeyword,
    /// <summary>Represents <c>in</c> keyword.</summary>
    InKeyword,
    /// <summary>Represents <c>local</c> keyword.</summary>
    LocalKeyword,
    /// <summary>Represents <c>nil</c> keyword.</summary>
    NilKeyword,
    /// <summary>Represents <c>not</c> keyword.</summary>
    NotKeyword,
    /// <summary>Represents <c>or</c> keyword.</summary>
    OrKeyword,
    /// <summary>Represents <c>repeat</c> keyword.</summary>
    RepeatKeyword,
    /// <summary>Represents <c>return</c> keyword.</summary>
    ReturnKeyword,
    /// <summary>Represents <c>then</c> keyword.</summary>
    ThenKeyword,
    /// <summary>Represents <c>true</c> keyword.</summary>
    TrueKeyword,
    /// <summary>Represents <c>until</c> keyword.</summary>
    UntilKeyword,
    /// <summary>Represents <c>while</c> keyword.</summary>
    WhileKeyword,

    // Environment Keywords
    /// <summary>Represents <c>_ENV</c> keyword.</summary>
    EnvironmentKeyword = 8491,

    // Variable Attribute Keywords
    /// <summary>Represents <c>close</c> keyword.</summary>
    CloseKeyword = 8501,
    /// <summary>Represents <c>const</c> keyword.</summary>
    ConstKeyword,

    // Dotnet Keywords
    /// <summary>Represents <c>abstract</c> keyword.</summary>
    AbstractKeyword = 8601,
    /// <summary>Represents <c>annotatedwith</c> keyword.</summary>
    AnnotatedWithKeyword,
    /// <summary>Represents <c>assembly</c> keyword.</summary>
    AssemblyKeyword,
    /// <summary>Represents <c>class</c> keyword.</summary>
    ClassKeyword,
    /// <summary>Represents <c>constainas</c> keyword.</summary>
    ConstrainAsKeyword,
    /// <summary>Represents <c>event</c> keyword.</summary>
    EventKeyword,
    /// <summary>Represents <c>extends</c> keyword.</summary>
    ExtendsKeyword,
    /// <summary>Represents <c>field</c> keyword.</summary>
    FieldKeyword,
    /// <summary>Represents <c>final</c> keyword.</summary>
    FinalKeyword,
    /// <summary>Represents <c>implements</c> keyword.</summary>
    ImplementsKeyword,
    /// <summary>Represents <c>interface</c> keyword.</summary>
    InterfaceKeyword,
    /// <summary>Represents <c>method</c> keyword.</summary>
    MethodKeyword,
    /// <summary>Represents <c>module</c> keyword.</summary>
    ModuleKeyword,
    /// <summary>Represents <c>namespace</c> keyword.</summary>
    NamespaceKeyword,
    /// <summary>Represents <c>new</c> keyword.</summary>
    NewKeyword,
    /// <summary>Represents <c>out</c> keyword.</summary>
    OutKeyword,
    /// <summary>Represents <c>parameter</c> keyword.</summary>
    ParameterKeyword,
    /// <summary>Represents <c>private</c> keyword.</summary>
    PrivateKeyword,
    /// <summary>Represents <c>property</c> keyword.</summary>
    PropertyKeyword,
    /// <summary>Represents <c>protected</c> keyword.</summary>
    ProtectedKeyword,
    /// <summary>Represents <c>public</c> keyword.</summary>
    PublicKeyword,
    /// <summary>Represents <c>readonly</c> keyword.</summary>
    ReadonlyKeyword,
    /// <summary>Represents <c>ref</c> keyword.</summary>
    RefKeyword,
    /// <summary>Represents <c>typeparameter</c> keyword.</summary>
    TypeParameterKeyword,
    /// <summary>Represents <c>static</c> keyword.</summary>
    StaticKeyword,

    // Preprocessor Keywords
    /// <summary>Represents <c>debug</c> keyword.</summary>
    DebugKeyword = 8650,
    /// <summary>Represents <c>nodebug</c> keyword.</summary>
    NoDebugKeyword,
    /// <summary>Represents <c>ifnot</c> keyword.</summary>
    IfNotKeyword,
    /// <summary>Represents <c>endinput</c> keyword.</summary>
    EndInputKeyword,
    #endregion

    #region Other Tokens
    // Tokens without text
    /// <summary>Represents a token that comes after the end of a directive.</summary>
    EndOfDirectiveToken = 9001,
    /// <summary>Represents the end of a triple-slash documentation comment.</summary>
    EndOfDocumentationCommentToken,
    /// <summary>Represents the end of a file.</summary>
    EndOfFileToken, // This is assumed to be the last textless token.

    // Tokens with text
    /// <summary>Represents a token that cannot be consumed.</summary>
    BadToken = 9218,
    /// <summary>Represents identifier token.</summary>
    IdentifierToken,
    /// <summary>Represents numeric literal token.</summary>
    NumericLiteralToken,
    /// <summary>Represents single-line string literal token.</summary>
    StringLiteralToken,
    /// <summary>Represents multi-line raw string literal token.</summary>
    MultiLineRawStringLiteralToken,
    #endregion
    #endregion

    #region Trivia
    /// <summary>表示换行。</summary>
    EndOfLineTrivia = 9249,
    /// <summary>表示空白字符。</summary>
    WhitespaceTrivia,
    /// <summary>表示单行注释。</summary>
    SingleLineCommentTrivia,
    /// <summary>表示多行注释。</summary>
    MultiLineCommentTrivia,
    DisabledTextTrivia,
    PreprocessingMessageTrivia,
    BadDirectiveTrivia,
    /// <summary>Represents <c>#!</c> interpreter directive.</summary>
    ShebangDirectiveTrivia,
    /// <summary>Represents <c>$debug</c> preprocessor directive.</summary>
    DebugDirectiveTrivia,
    /// <summary>Represents <c>$nodebug</c> preprocessor directive.</summary>
    NoDebugDirectiveTrivia,
    /// <summary>Represents <c>$if</c> preprocessor directive.</summary>
    IfDirectiveTrivia,
    /// <summary>Represents <c>$ifnot</c> preprocessor directive.</summary>
    IfNotDirectiveTrivia,
    /// <summary>Represents <c>$else</c> preprocessor directive.</summary>
    ElseDirectiveTrivia,
    /// <summary>Represents <c>$end</c> preprocessor directive.</summary>
    EndDirectiveTrivia,
    /// <summary>Represents <c>$endinput</c> preprocessor directive.</summary>
    EndInputDirectiveTrivia,
    /// <summary>表示被跳过的多个语法标记。</summary>
    SkippedTokensTrivia,
    #endregion

    #region 注释文档节点
    // = 9345
    #endregion

    #region Name
    /// <summary>表示标识符名称。</summary>
    IdentifierName = 9473,
    GenericName,
    /// <summary>表示限定的名称。</summary>
    QualifiedName,
    /// <summary>表示传入隐式self参数的名称。</summary>
    ImplicitSelfParameterName,
    #endregion

    #region 表达式
    // 基本表达式
    /// <summary>表示<see langword="nil"/>关键词字面量表达式。</summary>
    NilLiteralExpression = 9537,
    /// <summary>表示<see langword="false"/>关键词字面量表达式。</summary>
    FalseLiteralExpression,
    /// <summary>表示<see langword="true"/>关键词字面量表达式。</summary>
    TrueLiteralExpression,
    /// <summary>表示数字字面量表达式。</summary>
    NumericLiteralExpression,
    /// <summary>表示字符串字面量表达式。</summary>
    StringLiteralExpression,
    /// <summary>表示可变参数列表表达式。</summary>
    VariousArgumentsExpression,
    /// <summary>表示带圆括号的表达式。</summary>
    ParenthesizedExpression,
    /// <summary>表示函数定义表达式。</summary>
    FunctionDefinitionExpression,
    /// <summary>表示表初始化表达式。</summary>
    TableConstructorExpression,
    /// <summary>表示调用表达式。</summary>
    InvocationExpression,
    /// <summary>表示传入隐式self参数的调用表达式左侧。</summary>
    ImplicitSelfParameterExpression,

    // 二元运算符表达式
    /// <summary>表示加法表达式。</summary>
    AdditionExpression = 9601,
    /// <summary>表示减法表达式。</summary>
    SubtractionExpression,
    /// <summary>表示乘法表达式。</summary>
    MultiplicationExpression,
    /// <summary>表示除法表达式。</summary>
    DivisionExpression,
    /// <summary>表示向下取整除法表达式。</summary>
    FloorDivisionExpression,
    /// <summary>表示取幂表达式。</summary>
    ExponentiationExpression,
    /// <summary>表示取模表达式。</summary>
    ModuloExpression,
    /// <summary>表示按位与表达式。</summary>
    BitwiseAndExpression,
    /// <summary>表示按位异或表达式。</summary>
    BitwiseExclusiveOrExpression,
    /// <summary>表示按位或表达式。</summary>
    BitwiseOrExpression,
    /// <summary>表示按位左移表达式。</summary>
    BitwiseLeftShiftExpression,
    /// <summary>表示按位右移表达式。</summary>
    BitwiseRightShiftExpression,
    /// <summary>表示连接表达式。</summary>
    ConcatenationExpression,
    /// <summary>表示小于表达式。</summary>
    LessThanExpression,
    /// <summary>表示小于等于表达式。</summary>
    LessThanOrEqualExpression,
    /// <summary>表示大于表达式。</summary>
    GreaterThanExpression,
    /// <summary>表示大于等于表达式。</summary>
    GreaterThanOrEqualExpression,
    /// <summary>表示相等表达式。</summary>
    EqualExpression,
    /// <summary>表示不等表达式。</summary>
    NotEqualExpression,
    /// <summary>表示逻辑与表达式。</summary>
    AndExpression,
    /// <summary>表示逻辑或表达式。</summary>
    OrExpression,

    // 一元运算符表达式
    /// <summary>表示取负表达式。</summary>
    UnaryMinusExpression = 9665,
    /// <summary>表示逻辑非表达式。</summary>
    LogicalNotExpression,
    /// <summary>表示长度表达式。</summary>
    LengthExpression,
    /// <summary>表示按位非表达式。</summary>
    BitwiseNotExpression,

    // 成员操作表达式
    /// <summary>表示一般成员操作表达式。</summary>
    SimpleMemberAccessExpression = 9729, // 使用“.”操作
    /// <summary>表示索引成员操作表达式。</summary>
    IndexMemberAccessExpression, // 使用“[]”操作

    ObjectCreationExpression,
    #endregion

    #region Statement
    /// <summary>表示仅有分隔符（<c>;</c>）的空语句。</summary>
    EmptyStatement = 9761,
    /// <summary>表示赋值语句。</summary>
    AssignmentStatement,
    /// <summary>表示调用语句。</summary>
    InvocationStatement,
    /// <summary>表示标签语句。</summary>
    LabelStatement,
    /// <summary>表示中断流程（<see langword="break"/>）语句。</summary>
    BreakStatement,
    /// <summary>表示跳转标签（<see langword="goto"/>）语句。</summary>
    GotoStatement,
    /// <summary>表示返回（<see langword="return"/>）语句。</summary>
    ReturnStatement,
    /// <summary>表示执行代码块语句。</summary>
    DoStatement,
    /// <summary>表示<see langword="while"/>循环语句。</summary>
    WhileStatement,
    /// <summary>表示<see langword="repeat"/>循环语句。</summary>
    RepeatStatement,
    /// <summary>表示<see langword="if"/>条件语句。</summary>
    IfStatement,
    /// <summary>表示<see langword="elseif"/>条件从句。</summary>
    ElseIfClause,
    /// <summary>表示<see langword="else"/>条件从句。</summary>
    ElseClause,
    /// <summary>表示算术<see langword="for"/>循环语句。</summary>
    ForStatement,
    /// <summary>表示泛型<see langword="for"/>循环语句。</summary>
    ForInStatement,
    /// <summary>表示函数定义语句。</summary>
    FunctionDefinitionStatement,
    /// <summary>表示本地函数定义语句。</summary>
    LocalFunctionDefinitionStatement,
    /// <summary>表示本地变量定义语句。</summary>
    LocalDeclarationStatement,
    /// <summary>表示本地变量定义的赋值从句。</summary>
    EqualsValuesClause,

    DeclarationStatement,
    #endregion

    #region Declaration
    /// <summary>表示程序块（编译单元）。</summary>
    Chunk = 9889,
    /// <summary>表示代码块。</summary>
    Block,
    /// <summary>表示表字段。</summary>
    Field,
    /// <summary>表示列表项表字段。</summary>
    ItemField,
    /// <summary>表示名键对表字段。</summary>
    NameValueField,
    /// <summary>表示值键对表字段。</summary>
    KeyValueField,
    /// <summary>表示函数参数列表。</summary>
    ParameterDeclarationList,
    /// <summary>表示函数参数。</summary>
    ParameterDeclaration,
    /// <summary>表示调用参数列表。</summary>
    ArgumentList,
    /// <summary>表示调用参数表。</summary>
    ArgumentTable,
    /// <summary>表示调用参数字符串常量。</summary>
    ArgumentString,
    /// <summary>表示调用参数。</summary>
    Argument,
    /// <summary>表示标识符后接特性列表。</summary>
    NameAttributeList,
    /// <summary>表示特性列表。</summary>
    VariableAttributeList,
    /// <summary>表示特性。</summary>
    VariableAttribute,
    ModifierList,
    Modifier,
    AnnotatedWithClause,
    AssemblyDeclaration,
    ModuleDeclaration,
    NamespaceDeclaration,
    ConstrainAsClause,
    ExtendsClause,
    ImplementsClause,
    InterfaceDeclaration,
    ClassDeclaration,
    TypeParameterDeclarationList,
    TypeParameterDeclaration,
    FieldDeclaration,
    EventDeclaration,
    PropertyDeclaration,
    MethodDeclaration,
    TypeArgumentList,
    BaseTypeConstraint,
    ConstructorConstraint,
    #endregion
}
