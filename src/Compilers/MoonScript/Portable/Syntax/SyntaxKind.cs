// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript;

/// <summary>
/// Represents different parts of MoonScript syntax.
/// </summary>
public enum SyntaxKind : ushort
{
    /// <summary>Represents unknown part of syntax.</summary>
    None = 0,
    /// <summary>Represents node list.</summary>
    List = GreenNode.ListKind,

    #region Punctuation
    /// <summary>Represents <c>+</c> token.</summary>
    PlusToken = 8193,
    /// <summary>Represents <c>-</c> token.</summary>
    MinusToken,
    /// <summary>Represents <c>*</c> token.</summary>
    AsteriskToken,
    /// <summary>Represents <c>/</c> token.</summary>
    SlashToken,
    /// <summary>Represents <c>\</c> token.</summary>
    BackSlashToken,
    /// <summary>Represents <c>^</c> token.</summary>
    CaretToken,
    /// <summary>Represents <c>%</c> token.</summary>
    PercentToken,
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
    /// <summary>Represents <c>!</c> token.</summary>
    ExclamationToken,
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
    /// <summary>Represents <c>,</c> token.</summary>
    CommaToken,
    /// <summary>Represents <c>.</c> token.</summary>
    DotToken,
    /// <summary>Represents <c>@</c> token.</summary>
    CommercialAtToken,

    /// <summary>Represents <c>+=</c> token.</summary>
    PlusEqualsToken = 8241,
    /// <summary>Represents <c>-&gt;</c> token.</summary>
    MinusGreaterThanToken,
    /// <summary>Represents <c>-=</c> token.</summary>
    MinusEqualsToken,
    /// <summary>Represents <c>*=</c> token.</summary>
    AsteriskEqualsToken,
    /// <summary>Represents <c>/=</c> token.</summary>
    SlashEqualsToken,
    /// <summary>Represents <c>^=</c> token.</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    CaretEqualsToken,
    /// <summary>Represents <c>%=</c> token.</summary>
    PersentEqualsToken,
    /// <summary>Represents <c>&amp;=</c> token.</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    AmpersandEqualsToken,
    /// <summary>Represents <c>~=</c> token.</summary>
    TildeEqualsToken,
    /// <summary>Represents <c>|=</c> token.</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    BarEqualsToken,
    /// <summary>Represents <c>&lt;&lt;</c> token.</summary>
    LessThanLessThanToken,
    /// <summary>Represents <c>&lt;&lt;=</c> token.</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    LessThanLessThanEqualsToken,
    /// <summary>Represents <c>&lt;=</c> token.</summary>
    LessThanEqualsToken,
    /// <summary>Represents <c>&gt;&gt;</c> token.</summary>
    GreaterThanGreaterThanToken,
    /// <summary>Represents <c>&gt;&gt;=</c> token.</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    GreaterThanGreaterThanEqualsToken,
    /// <summary>Represents <c>&gt;=</c> token.</summary>
    GreaterThanEqualsToken,
    /// <summary>Represents <c>//</c> token.</summary>
    SlashSlashToken,
    /// <summary>Represents <c>//=</c> token.</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    SlashSlashEqualsToken,
    /// <summary>Represents <c>=&gt;</c> token.</summary>
    EqualsGreaterThanToken,
    /// <summary>Represents <c>==</c> token.</summary>
    EqualsEqualsToken,
    /// <summary>Represents <c>!=</c> token.</summary>
    ExclamationEqualsToken,
    /// <summary>Represents <c>..</c> token.</summary>
    DotDotToken,
    /// <summary>Represents <c>..=</c> token.</summary>
    DotDotEqualsToken,
    /// <summary>Represents <c>...</c> token.</summary>
    DotDotDotToken,
    /// <summary>Represents <c>@@</c> token.</summary>
    CommercialAtCommercialAtToken,

    /// <summary>Represents <c>and=</c> token.</summary>
    AndEqualsToken,
    /// <summary>Represents <c>or=</c> token.</summary>
    OrEqualsToken,

    /// <summary>Represents <c>#!</c> token.</summary>
    HashExclamationToken,
    #endregion

    #region 关键词
    /// <summary>表示<see langword="and"/>关键词。</summary>
    AndKeyword = 8321,
    /// <summary>表示<see langword="break"/>关键词。</summary>
    BreakKeyword,
    /// <summary>表示<see langword="class"/>关键词。</summary>
    ClassKeyword,
    /// <summary>表示<see langword="continue"/>关键词。</summary>
    ContinueKeyword,
    /// <summary>表示<see langword="do"/>关键词。</summary>
    DoKeyword,
    /// <summary>表示<see langword="else"/>关键词。</summary>
    ElseKeyword,
    /// <summary>表示<see langword="elseif"/>关键词。</summary>
    ElseIfKeyword,
    /// <summary>表示<see langword="end"/>关键词。</summary>
    EndKeyword,
    /// <summary>表示<see langword="export"/>关键词。</summary>
    ExportKeyword,
    /// <summary>表示<see langword="extends"/>关键词。</summary>
    ExtendsKeyword,
    /// <summary>表示<see langword="false"/>关键词。</summary>
    FalseKeyword,
    /// <summary>表示<see langword="for"/>关键词。</summary>
    ForKeyword,
    /// <summary>表示<see langword="from"/>关键词。</summary>
    FromKeyword,
    /// <summary>表示<see langword="if"/>关键词。</summary>
    IfKeyword,
    /// <summary>表示<see langword="import"/>关键词。</summary>
    ImportKeyword,
    /// <summary>表示<see langword="in"/>关键词。</summary>
    InKeyword,
    /// <summary>表示<see langword="local"/>关键词。</summary>
    LocalKeyword,
    /// <summary>表示<see langword="nil"/>关键词。</summary>
    NilKeyword,
    /// <summary>表示<see langword="not"/>关键词。</summary>
    NotKeyword,
    /// <summary>表示<see langword="or"/>关键词。</summary>
    OrKeyword,
    /// <summary>表示<see langword="return"/>关键词。</summary>
    ReturnKeyword,
    /// <summary>表示<see langword="switch"/>关键词。</summary>
    SwitchKeyword,
    /// <summary>表示<see langword="then"/>关键词。</summary>
    ThenKeyword,
    /// <summary>表示<see langword="true"/>关键词。</summary>
    TrueKeyword,
    /// <summary>表示<see langword="unless"/>关键词。</summary>
    UnlessKeyword,
    /// <summary>表示<see langword="using"/>关键词。</summary>
    UsingKeyword,
    /// <summary>表示<see langword="when"/>关键词。</summary>
    WhenKeyword,
    /// <summary>表示<see langword="while"/>关键词。</summary>
    WhileKeyword,
    /// <summary>表示<see langword="with"/>关键词。</summary>
    WithKeyword,

    // 上下文关键词
    /// <summary>表示<c>_G</c>关键词。</summary>
    GlobalEnvironmentKeyword = 8385,
    /// <summary>表示<c>_ENV</c>关键词。编译器在编译期间将这个关键词作为所有游离的变量的作用环境，它的值不是固定的。（自Lua 5.2版本添加。）</summary>
    EnvironmentKeyword,
    /// <summary>表示<see langword="new"/>上下文关键词。</summary>
    NewKeyword,
    /// <summary>表示<see langword="self"/>上下文关键词。</summary>
    SelfKeyword,
    /// <summary>表示<see langword="super"/>上下文关键词。</summary>
    SuperKeyword,

    // 元字段和元方法
    /// <summary>表示元表（<c>()</c>）元字段<c>__metatable</c>。</summary>
    MetatableMetafield = 8449,
    /// <summary>表示元表（<c>()</c>）元字段<c>__class</c>。</summary>
    ClassMetafield,
    /// <summary>表示元表（<c>()</c>）元字段<c>__name</c>。</summary>
    NameMetafield,
    /// <summary>表示元表（<c>+</c>）元方法<c>__inherited</c>。</summary>
    InheritedMetamethod,
    /// <summary>表示元表（<c>+</c>）元字段<c>__base</c>。</summary>
    BaseMetafield,
    /// <summary>表示元表（<c>+</c>）元字段<c>__parent</c>。</summary>
    ParentMetafield,
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
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    MultiLineRawStringLiteralToken,

    /// <summary>表示整个插值字符串字面量（" ... #{ 表达式 } ..."）标记。</summary>
    InterpolatedStringLiteralToken,
    /// <summary>表示插值字符串字面量中普通文本的标记。</summary>
    /// <remarks>只应在解析过程中临时使用。</remarks>
    InterpolatedStringTextToken,
    /// <summary>表示插值字符串字面量中表达式的标记。</summary>
    /// <remarks>只应在解析过程中临时使用。</remarks>
    InterpolationToken,
    /// <summary>表示插值字符串字面量中表达式的起始标记（“#{”）。</summary>
    InterpolationStartToken,
    /// <summary>表示插值字符串字面量中表达式的起始标记（“}”）。</summary>
    InterpolationEndToken,
    #endregion

    #region 琐碎内容
    /// <summary>表示换行。</summary>
    EndOfLineTrivia = 9249,
    /// <summary>表示空白字符。</summary>
    WhitespaceTrivia,
    /// <summary>表示单行注释。</summary>
    SingleLineCommentTrivia,
    /// <summary>表示多行注释。</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    MultiLineCommentTrivia,
    DisabledTextTrivia,
    PreprocessingMessageTrivia,
    BadDirectiveTrivia,
    /// <summary>Represents <c>#!</c> interpreter directive.</summary>
    ShebangDirectiveTrivia,
    /// <summary>表示被跳过的多个语法标记。</summary>
    SkippedTokensTrivia,
    #endregion

    #region 注释文档节点
    // = 9345
    #endregion

    #region 名称和类型名称
    /// <summary>表示标识符名称。</summary>
    IdentifierName = 9473,
    #endregion

    #region 表达式
    // 基础表达式
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
    /// <summary>表示类本身（<c>@</c>）上下文表达式（仅在类内部有效）。</summary>
    SelfExpression,
    /// <summary>表示类的父类（<see langword="super"/>）上下文关键字表达式（仅在类内部有效）。</summary>
    SuperExpression,
    /// <summary>表示类的类型（<c>@@</c>）上下文表达式（仅在类内部有效）。</summary>
    TypeExpression,
    /// <summary>表示参数列表表达式。</summary>
    ArgumentListExpression,
    /// <summary>表示表初始化表达式。</summary>
    TableConstructorExpression,

    /// <summary>表示带括号表达式。</summary>
    ParenthesizedExpression,
    /// <summary>表示类定义表达式。</summary>
    ClassExpression,
    /// <summary>表示匿名类定义表达式。</summary>
    AnomymousClassExpression,
    /// <summary>表示<see langword="do"/>表达式。</summary>
    DoExpression,
    /// <summary>表示逐量<see langword="for"/>循环表达式。</summary>
    ForExpression,
    /// <summary>表示迭代<see langword="for"/>循环表达式。</summary>
    ForInExpression,
    /// <summary>表示<see langword="if"/>条件表达式。</summary>
    IfExpression,
    /// <summary>表示<see langword="switch"/>切换表达式。</summary>
    SwitchExpression,
    /// <summary>表示<see langword="unless"/>条件表达式。</summary>
    UnlessExpression,
    /// <summary>表示<see langword="while"/>循环表达式。</summary>
    WhileExpression,
    /// <summary>表示<see langword="with"/>限定表达式。</summary>
    WithExpression,
    /// <summary>表示列表推导式表达式。</summary>
    ListComprehensionExpression,
    /// <summary>表示表推导式表达式。</summary>
    TableComprehensionExpression,
    /// <summary>表示逐量<see langword="for"/>推导式从句。</summary>
    ForComprehensionClause,
    /// <summary>表示迭代<see langword="for"/>推导式从句。</summary>
    ForInComprehensionClause,
    /// <summary>表示<see langword="when"/>推导式从句。</summary>
    WhenComprehensionClause,
    /// <summary>表示迭代数字索引表达式。</summary>
    NumericallyIterateExpression, // 标识符前缀“*”
    /// <summary>表示切片表达式。</summary>
    SlicingExpression,
    /// <summary>表示调用表达式。</summary>
    InvocationExpression,
    /// <summary>表示Lambda表达式。</summary>
    LambdaExpression,

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

    // 赋值表达式
    /// <summary>表示一般赋值表达式。</summary>
    SimpleAssignmentExpression = 9697,

    // 更新赋值表达式
    /// <summary>表示加法更新赋值表达式。</summary>
    AdditionAssignmentExpression,
    /// <summary>表示减法更新赋值表达式。</summary>
    SubtractionAssignmentExpression,
    /// <summary>表示乘法更新赋值表达式。</summary>
    MultiplicationAssignmentExpression,
    /// <summary>表示除法更新赋值表达式。</summary>
    DivisionAssignmentExpression,
    /// <summary>表示向下取整除法更新赋值表达式。</summary>
    FloorDivisionAssignmentExpression,
    /// <summary>表示取幂更新赋值表达式。</summary>
    ExponentiationAssignmentExpression,
    /// <summary>表示取模更新赋值表达式。</summary>
    ModuloAssignmentExpression,
    /// <summary>表示按位与更新赋值表达式。</summary>
    BitwiseAndAssignmentExpression,
    /// <summary>表示按位异或更新赋值表达式。</summary>
    BitwiseExclusiveOrAssignmentExpression,
    /// <summary>表示按位或更新赋值表达式。</summary>
    BitwiseOrAssignmentExpression,
    /// <summary>表示按位右移更新赋值表达式。</summary>
    BitwiseRightShiftAssignmentExpression,
    /// <summary>表示按位左移更新赋值表达式。</summary>
    BitwiseLeftShiftAssignmentExpression,
    /// <summary>表示连接更新赋值表达式。</summary>
    ConcatenationAssignmentExpression,
    /// <summary>表示逻辑与更新赋值表达式。</summary>
    AndAssignmentExpression,
    /// <summary>表示逻辑或更新赋值表达式。</summary>
    OrAssignmentExpression,

    // 成员操作表达式
    /// <summary>表示一般成员操作表达式。</summary>
    SimpleMemberAccessExpression = 9729, // 使用“.”操作
    /// <summary>表示索引成员操作表达式。</summary>
    IndexMemberAccessExpression, // 使用“[]”操作
    /// <summary>表示自身成员操作表达式。</summary>
    SelfMemberAccessExpression, // 带有“@”前缀
    /// <summary>表示类型成员操作表达式。</summary>
    TypeMemberAccessExpression, // 带有“@@”前缀

    // 插值表达式
    /// <summary>表示插值表达式。</summary>
    Interpolation,
    InterpolatedStringText,
    /// <summary>表示差值字符串表达式。</summary>
    InterpolatedStringExpression,
    #endregion

    #region 语句
    /// <summary>表示包含表达式的语句。</summary>
    ExpressionStatement = 9761,
    /// <summary>表示中断流程（<see langword="break"/>）语句。</summary>
    BreakStatement,
    /// <summary>表示类声明语句。</summary>
    ClassStatement,
    /// <summary>表示类成员声明语句。</summary>
    MemberStatement,
    /// <summary>表示跳过流程（<see langword="continue"/>）语句。</summary>
    ContinueStatement,
    /// <summary>表示执行代码块（<see langword="do"/>）语句。</summary>
    DoStatement,
    /// <summary>表示导出（<see langword="export"/>）语句。</summary>
    ExportStatement,
    /// <summary>表示逐量<see langword="for"/>循环语句。</summary>
    ForStatement,
    /// <summary>表示迭代<see langword="for"/>循环语句。</summary>
    ForInStatement,
    /// <summary>表示<see langword="if"/>条件语句。</summary>
    IfStatement,
    /// <summary>表示<see langword="elseif"/>条件从句。</summary>
    ElseIfClause,
    /// <summary>表示<see langword="else"/>条件从句。</summary>
    ElseClause,
    /// <summary>表示导入（<see langword="import"/>）语句。</summary>
    ImportStatement,
    /// <summary>表示本地变量定义（<see langword="local"/>）语句。</summary>
    LocalStatement,
    /// <summary>表示<see langword="switch"/>切换语句。</summary>
    SwitchStatement,
    /// <summary>表示<see langword="unless"/>条件语句。</summary>
    UnlessStatement,
    /// <summary>表示函数覆写外部变量列表（<see langword="using"/>）从句。</summary>
    UsingClause,
    /// <summary>表示<see langword="when"/>条件从句。</summary>
    WhenClause,
    /// <summary>表示<see langword="while"/>循环语句。</summary>
    WhileStatement,
    /// <summary>表示<see langword="with"/>限定语句。</summary>
    WithStatement,

    // 解构
    /// <summary>表示解构赋值语句。</summary>
    DestructuringAssignmentStatement,
    /// <summary>表示解构标识符名称结构。</summary>
    DestructuringIdentifierName,
    /// <summary>表示解构列表结构。</summary>
    DestructuringList,
    /// <summary>表示解构表的成员结构。</summary>
    DestructuringField,
    /// <summary>表示解构表结构。</summary>
    DestructuringTable,

    // 行装饰器
    /// <summary>表示逐量<see langword="for"/>循环行装饰器。</summary>
    ForLineDecorator,
    /// <summary>表示迭代<see langword="for"/>循环行装饰器。</summary>
    ForInLineDecorator,
    /// <summary>表示<see langword="if"/>条件行装饰器。</summary>
    IfLineDecorator,
    /// <summary>表示<see langword="unless"/>条件行装饰器。</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    UnlessLineDecorator,
    /// <summary>表示<see langword="while"/>循环行装饰器。</summary>
    /// <remarks>仅在MoonScript预览版本中使用。</remarks>
    WhileLineDecorator,
    #endregion

    #region 声明
    /// <summary>表示程序块（编译单元）。</summary>
    Chunk = 9889,
    /// <summary>表示代码块。</summary>
    Block,
    /// <summary>表示表达式列表。</summary>
    ExpressionList,
    /// <summary>表示表字段列表。</summary>
    FieldList,
    /// <summary>表示表字段。</summary>
    Field,
    /// <summary>表示函数参数列表。</summary>
    ParameterList,
    /// <summary>表示函数参数。</summary>
    Parameter,
    /// <summary>表示调用参数列表。</summary>
    ArgumentList,
    /// <summary>表示调用参数。</summary>
    Argument,
    /// <summary>表示标识符后接冒号。</summary>
    NameColon,
    /// <summary>表示冒号后接标识符。</summary>
    ColonName,
    /// <summary>表示反斜杠后接标识符。</summary>
    BackSlashName,
    /// <summary>表示传入隐式self参数的调用头。</summary>
    ImplicitSelfCall,
    /// <summary>表示<see langword="import"/>导入的名称。</summary>
    ImportName,
    DestructingHolder
    #endregion
}
