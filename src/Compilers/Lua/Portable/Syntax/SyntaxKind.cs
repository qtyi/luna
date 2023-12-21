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
    PersentToken,
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

    // EnviromentKeywords
    /// <summary>Represents <c>_G</c> keyword.</summary>
    GlobalEnvironmentKeyword = 8385,
    /// <summary>Represents <c>_ENV</c> keyword.编译器在编译期间将这个关键词作为所有游离的变量的作用环境，它的值不是固定的。（自Lua 5.2版本添加。）</summary>
    EnvironmentKeyword,

    // Metafields and Metamethods
    /// <summary>表示元表（<c>()</c>）元字段<c>__metatable</c>。</summary>
    MetatableMetafield = 8449,
    /// <summary>表示加法（<c>+</c>）元方法<c>__add</c>。</summary>
    AdditionMetamethod,
    /// <summary>表示减法（二元<c>-</c>）元方法<c>__sub</c>。</summary>
    SubtractionMetamethod,
    /// <summary>表示乘法（<c>*</c>）元方法<c>__mul</c>。</summary>
    MultiplicationMetamethod,
    /// <summary>表示除法（<c>/</c>）元方法<c>__div</c>。</summary>
    DivisionMetamethod,
    /// <summary>表示取模（<c>%</c>）元方法<c>__mod</c>。</summary>
    ModuloMetamethod,
    /// <summary>表示取幂（<c>^</c>）元方法<c>__pow</c>。</summary>
    ExponentiationMetamethod,
    /// <summary>表示取负（一元<c>-</c>）元方法<c>__unm</c>。</summary>
    NegationMetamethod,
    /// <summary>表示向下取整除法（<c>+</c>）元方法<c>__idiv</c>。</summary>
    FloorDivisionMetamethod,
    /// <summary>表示按位与（<c>&amp;</c>）元方法<c>__band</c>。</summary>
    BitwiseAndMetamethod,
    /// <summary>表示按位或（<c>|</c>）元方法<c>__bor</c>。</summary>
    BitwiseOrMetamethod,
    /// <summary>表示按位异或（二元<c>~</c>）元方法<c>__bxor</c>。</summary>
    BitwiseExclusiveOrMetamethod,
    /// <summary>表示按位取反（一元<c>~</c>）元方法<c>__bnot</c>。</summary>
    BitwiseNotMetamethod,
    /// <summary>表示按位向左位移（<c>&lt;&lt;</c>）元方法<c>__shl</c>。</summary>
    BitwiseLeftShiftMetamethod,
    /// <summary>表示按位向右位移（<c>&gt;&gt;</c>）元方法<c>__shr</c>。</summary>
    BitwiseRightShiftMetamethod,
    /// <summary>表示连接（<c>..</c>）元方法<c>__concat</c>。</summary>
    ConcatenationMetamethod,
    /// <summary>表示长度（<c>#</c>）元方法<c>__len</c>。</summary>
    LengthMetamethod,
    /// <summary>表示相等（<c>==</c>）元方法<c>__eq</c>。</summary>
    EqualMetamethod,
    /// <summary>表示小于（<c>&lt;</c>）元方法<c>__lt</c>。</summary>
    LessThanMetamethod,
    /// <summary>表示小于等于（<c>&lt;=</c>）元方法<c>__le</c>。</summary>
    LessEqualMetamethod,
    /// <summary>表示操作索引（<c>[]</c>）元方法<c>__index</c>。</summary>
    IndexingAccessMetamethod,
    /// <summary>表示调用（<c>()</c>）元方法<c>__call</c>。</summary>
    CallMetamethod,
    /// <summary>表示字典访问（<c>()</c>）元方法<c>__pairs</c>。</summary>
    PairsMetamethod,
    /// <summary>表示转换为字符串（<c>()</c>）元方法<c>__tostring</c>。</summary>
    ToStringMetamethod,
    /// <summary>表示垃圾收集（<c>+</c>）元方法<c>__gc</c>。</summary>
    GarbageCollectionMetamethod,
    /// <summary>表示标记要被关闭（<c>+</c>）元方法<c>__close</c>。（自Lua 5.4版本添加。）</summary>
    ToBeClosedMetamethod,
    /// <summary>表示弱表模式（<c>+</c>）元字段<c>__mode</c>。</summary>
    WeakModeMetafield,
    /// <summary>表示名称（<c>+</c>）元字段<c>__name</c>。</summary>
    NameMetafield,

    // Attributes
    /// <summary>Represents <c>close</c> keyword.</summary>
    CloseKeyword,
    /// <summary>Represents <c>const</c> keyword.</summary>
    ConstKeyword,
    #endregion

    EndOfDirectiveToken = 9216,
    /// <summary>表示文件的结尾。</summary>
    EndOfFileToken = 9217, // 假定此类型为最后一个无文本标记。

    #region 文本标记
    /// <summary>表示不应出现在此位置的错误标记。</summary>
    BadToken = 9218,
    /// <summary>表示标识符标记。</summary>
    IdentifierToken,
    /// <summary>表示数字字面量标记。</summary>
    NumericLiteralToken,
    /// <summary>表示字符串字面量标记。</summary>
    StringLiteralToken,
    /// <summary>表示多行原始字符串字面量标记。</summary>
    MultiLineRawStringLiteralToken,
    #endregion

    #region Trivia
    /// <summary>表示换行。</summary>
    EndOfLineTrivia = 9249,
    /// <summary>表示空白字符。</summary>
    WhiteSpaceTrivia,
    /// <summary>表示单行注释。</summary>
    SingleLineCommentTrivia,
    /// <summary>表示多行注释。</summary>
    MultiLineCommentTrivia,
    PreprocessingMessageTrivia,
    BadDirectiveTrivia,
    /// <summary>Represents <c>#!</c> interpreter directive.</summary>
    ShebangDirectiveTrivia,
    CommentDirectiveTrivia,
    /// <summary>表示被跳过的多个语法标记。</summary>
    SkippedTokensTrivia,
    #endregion

    #region 注释文档节点
    // = 9345
    #endregion

    #region 名称
    /// <summary>表示标识符名称。</summary>
    IdentifierName = 9473,
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
    #endregion

    #region 语句
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
    #endregion

    #region 声明
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
    ParameterList,
    /// <summary>表示函数参数。</summary>
    Parameter,
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
    AttributeList,
    /// <summary>表示特性。</summary>
    Attribute,
    #endregion
}
