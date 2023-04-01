// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisParseOptions = LuaParseOptions;
using ThisSyntaxNode = Lua.LuaSyntaxNode;
using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisParseOptions = MoonScriptParseOptions;
using ThisSyntaxNode = MoonScript.MoonScriptSyntaxNode;
using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// 表示语法解析器，提供语法解析器的必要约束和部分实现，此类必须被继承。
/// </summary>
internal abstract partial class SyntaxParser : IDisposable
{
    /// <summary>语法解析器的词法器。</summary>
    protected readonly Lexer lexer;
    /// <summary>是否进行增量语法分析。</summary>
    private readonly bool _isIncremental;
    /// <summary>是否允许重置词法器模式。</summary>
    private readonly bool _allowModeReset;
    /// <summary>语法解析器的各项操作的取消标志。</summary>
    protected readonly CancellationToken cancellationToken;

    /// <summary>词法器模式。</summary>
    private LexerMode _mode;
    private Blender _firstBlender;
    private BlendedNode _currentNode;
    private BlendedNode[]? _blendedTokens;
    private SyntaxToken? _currentToken;
    private ArrayElement<SyntaxToken>[]? _lexedTokens;
    protected GreenNode? prevTokenTrailingTrivia;
    /// <summary>
    /// <see cref="_lexedTokens"/>或<see cref="_blendedTokens"/>的第一项的位置。
    /// </summary>
    private int _firstToken;
    /// <summary>
    /// 当前处理的标志在<see cref="_lexedTokens"/>或<see cref="_blendedTokens"/>的索引位置。
    /// </summary>
    private int _tokenOffset;
    private int _tokenCount;
    private int _resetCount;
    private int _resetStart;

    private static readonly ObjectPool<BlendedNode[]> s_blendedNodesPool = new(() => new BlendedNode[32], 2);

    /// <summary>
    /// 获取一个值，指示语法解析器是否进行增量语法分析。
    /// </summary>
    protected bool IsIncremental => this._isIncremental;

    /// <summary>
    /// 获取语法解析器的设置。
    /// </summary>
    /// <seealso cref="Lexer.Options"/>
    public ThisParseOptions Options => this.lexer.Options;

    /// <summary>
    /// 获取一个值，指示解析的是脚本类代码文本。
    /// </summary>
    public bool IsScript => this.Options.Kind == SourceCodeKind.Script;

#if TESTING
    protected internal bool IsAtEndOfFile => this.CurrentTokenKind == SyntaxKind.EndOfFileToken;
#endif

    /// <summary>
    /// 获取或设置词法器模式。
    /// </summary>
    protected LexerMode Mode
    {
        get => this._mode;
        set
        {
            if (this._mode != value)
            {
                Debug.Assert(this._allowModeReset);

                // 设置新的词法器模式并重置字段。
                this._mode = value;
                this._currentToken = null;
                this._currentNode = default;
                this._tokenCount = this._tokenOffset;
            }
        }
    }

    /// <summary>
    /// 创建<see cref="SyntaxParser"/>的新实例。
    /// </summary>
    /// <param name="lexer">语法解析器的词法器。</param>
    /// <param name="mode">内部词法器的模式。</param>
    /// <param name="oldTree">旧语法树的根节点。传入不为<see langword="null"/>的值时，将开启增量处理操作。</param>
    /// <param name="changes">被修改的文本范围。</param>
    /// <param name="allowModeReset">是否允许重设词法器的模式。</param>
    /// <param name="preLexIfNotIncremental">当不开启增量处理操作时，是否进行词法预分析。</param>
    /// <param name="cancellationToken">语法解析器的操作的取消标志。</param>
    protected SyntaxParser(
        Lexer lexer,
        LexerMode mode,
        ThisSyntaxNode? oldTree,
        IEnumerable<TextChangeRange>? changes,
        bool allowModeReset,
        bool preLexIfNotIncremental = false,
        CancellationToken cancellationToken = default
    )
    {
        this.lexer = lexer;
        this._mode = mode;
        this._allowModeReset = allowModeReset;
        this.cancellationToken = cancellationToken;
        this._currentNode = default;
        this._isIncremental = oldTree is not null; // 是否开启增量处理操作。

        if (this._isIncremental || allowModeReset) // 协调新旧节点。
        {
            this._firstBlender = new(lexer, oldTree, changes);
            this._blendedTokens = SyntaxParser.s_blendedNodesPool.Allocate();
        }
        else // 均为新节点。
        {
            this._firstBlender = default;
            this._lexedTokens = new ArrayElement<SyntaxToken>[32];
        }

        // 进行词法器预分析，此操作不应被取消。
        // 因为在构造函数中取消操作将会使处置操作变得复杂难懂，所以应排除可取消的情况。
        if (preLexIfNotIncremental && !this._isIncremental && !cancellationToken.CanBeCanceled)
            this.PreLex();
    }

    /// <summary>
    /// 语法解析器是否协调新旧节点。
    /// </summary>
    /// <returns>若语法解析器开启增量处理操作，且允许重设词法器的模式时返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    /// <remarks>
    /// 若返回<see langword="true"/>时，<see cref="SyntaxParser._blendedTokens"/>不为<see langword="null"/>；
    /// 若返回<see langword="false"/>时，<see cref="SyntaxParser._lexedTokens"/>不为<see langword="null"/>。
    /// </remarks>
    [MemberNotNullWhen(true, nameof(SyntaxParser._blendedTokens))]
    [MemberNotNullWhen(false, nameof(SyntaxParser._lexedTokens))]
    protected bool IsBlending() => this._isIncremental || this._allowModeReset;

    /// <summary>
    /// 重新初始化此语法解析器实例。
    /// </summary>
    protected void ReInitialize()
    {
        this._firstToken = 0;
        this._tokenOffset = 0;
        this._tokenCount = 0;
        this._resetCount = 0;
        this._resetStart = 0;
        this._currentToken = null;
        this.prevTokenTrailingTrivia = null;
        if (this.IsBlending())
            this._firstBlender = new(this.lexer, null, null);
    }

    /// <summary>
    /// 词法器预分析。
    /// </summary>
    [MemberNotNull(nameof(SyntaxParser._lexedTokens))]
    private void PreLex()
    {
        // 不应在这个方法内部处理取消标志。
        var size = Math.Min(4096, Math.Max(32, this.lexer.TextWindow.Text.Length / 2));
        this._lexedTokens ??= new ArrayElement<SyntaxToken>[size];

        for (int i = 0; i < size; i++)
        {
            var token = this.lexer.Lex(this._mode); // 词法器分析一个标志。
            this.AddLexedToken(token);

            // 遇到文件结尾。
            if (token.Kind == SyntaxKind.EndOfFileToken) break;
        }
    }

    /// <summary>
    /// 获取当前的已协调节点。
    /// </summary>
    [MemberNotNull(nameof(SyntaxParser._blendedTokens))]
    protected ThisSyntaxNode? CurrentNode
    {
        get
        {
            Debug.Assert(this.IsBlending());

            var node = this._currentNode.Node;
            if (node is not null) return node;

            this.ReadCurrentNode();
            return this._currentNode.Node;
        }
    }

    /// <summary>
    /// 获取当前的已协调节点的语法部分种类。
    /// </summary>
    protected SyntaxKind CurrentNodeKind => this.CurrentNode?.Kind() ?? SyntaxKind.None;

    /// <summary>
    /// 读取当前的节点到<see cref="SyntaxParser._currentNode"/>。
    /// </summary>
    [MemberNotNull(nameof(SyntaxParser._blendedTokens))]
    private void ReadCurrentNode()
    {
        Debug.Assert(this.IsBlending());

        // 获取用来读取节点的协调器。
        Blender blender;
        if (this._tokenOffset == 0)
            blender = this._firstBlender;
        else
            blender = this._blendedTokens[this._tokenOffset - 1].Blender;
        // 使用协调器读取节点。
        this._currentNode = blender.ReadNode(this._mode);
    }

    /// <summary>
    /// 语法解析器接受当前的语法节点，并返回其对应的绿树节点。
    /// </summary>
    /// <returns>接受的语法节点对应的绿树节点。</returns>
    [MemberNotNull(nameof(SyntaxParser._blendedTokens))]
    protected GreenNode? EatNode()
    {
        Debug.Assert(this.IsBlending());

        // 语法解析器吃下（当前节点）的绿树节点。
        var result = this.CurrentNode?.Green;

        // 必要时扩充已协调标志数组的容量。
        if (this._tokenOffset >= this._blendedTokens.Length)
            this.AddTokenSlot();

        // 添加节点到已处理的标志数组中。
        this._blendedTokens[this._tokenOffset++] = this._currentNode;
        this._tokenCount = this._tokenOffset;

        // 恢复当前处理的变量为初始状态。
        this._currentNode = default;
        this._currentToken = null;

        return result;
    }

    /// <summary>
    /// 语法解析器接受当前的语法节点，并返回其对应的指定类型的绿树节点。
    /// </summary>
    /// <typeparam name="T">要返回的绿树节点的类型。</typeparam>
    /// <returns>接受的语法节点对应的绿树节点。</returns>
    [MemberNotNull(nameof(SyntaxParser._blendedTokens))]
    protected T? EatNode<T>() where T : ThisInternalSyntaxNode => this.EatNode() as T;

    /// <summary>
    /// 获取当前的已词法分析的标志。
    /// </summary>
    protected SyntaxToken CurrentToken => this._currentToken ??= this.FetchCurrentToken();

    /// <summary>
    /// 获取当前的已词法分析的语法部分种类。
    /// </summary>
    protected SyntaxKind CurrentTokenKind => this.CurrentToken.Kind;

    /// <summary>
    /// 取得当前的已词法分析的标志。
    /// </summary>
    private SyntaxToken FetchCurrentToken()
    {
        if (this._tokenOffset >= this._tokenCount)
            this.AddNewToken();

        if (this.IsBlending())
            return this._blendedTokens[this._tokenOffset].Token;
        else
            return this._lexedTokens[this._tokenOffset];
    }

    /// <summary>
    /// 读取并添加下一个新的标志。
    /// </summary>
    private void AddNewToken()
    {
        if (this.IsBlending())
        {
            if (this._tokenCount == 0)
            {
                if (this._currentNode.Token is not null)
                    this.AddToken(this._currentNode);
                else
                    this.AddToken(this._firstBlender.ReadToken(this._mode));
            }
            else
                this.AddToken(this._blendedTokens[this._tokenCount - 1].Blender.ReadToken(this._mode));
        }
        else
            this.AddLexedToken(this.lexer.Lex(this._mode));
    }

    /// <summary>
    /// 添加指定标志。
    /// </summary>
    /// <param name="tokenResult">要添加的已协调节点。</param>
    [MemberNotNull(nameof(SyntaxParser._blendedTokens))]
    private void AddToken(in BlendedNode tokenResult)
    {
        Debug.Assert(tokenResult.Token is not null);
        Debug.Assert(this.IsBlending());

        // 必要时扩充已协调标志数组的容量。
        if (this._tokenCount >= this._blendedTokens.Length)
            this.AddTokenSlot();

        this._blendedTokens[this._tokenCount] = tokenResult;
        this._tokenCount++;
    }

    /// <summary>
    /// 添加指定已词法分析的标志。
    /// </summary>
    /// <param name="token">要添加的已词法分析的标志。</param>
    [MemberNotNull(nameof(SyntaxParser._lexedTokens))]
    private void AddLexedToken(SyntaxToken token)
    {
        Debug.Assert(!this.IsBlending());

        // 必要时扩充已词法分析的标志数组的容量。
        if (this._tokenCount >= this._lexedTokens.Length)
            this.AddLexedTokenSlot();

        this._lexedTokens[this._tokenCount].Value = token;
        this._tokenCount++;
    }

    /// <summary>
    /// 扩充已协调标志数组的容量。
    /// </summary>
    [MemberNotNull(nameof(SyntaxParser._blendedTokens))]
    private void AddTokenSlot()
    {
        Debug.Assert(this.IsBlending());

        if (this._tokenOffset > (this._blendedTokens.Length >> 1) && (this._resetStart == -1 || this._resetStart > this._firstToken))
        {
            int shiftOffset = (this._resetStart == -1) ? this._tokenOffset : this._resetStart - this._firstToken;
            int shiftCount = this._tokenCount - shiftOffset;
            Debug.Assert(shiftOffset > 0);
            this._firstBlender = this._blendedTokens[shiftOffset - 1].Blender;
            if (shiftCount > 0)
                Array.Copy(this._blendedTokens, shiftOffset, this._blendedTokens, 0, shiftCount);

            this._firstToken += shiftOffset;
            this._tokenCount -= shiftOffset;
            this._tokenOffset -= shiftOffset;
        }
        else
        {
            var old = this._blendedTokens;
            Array.Resize(ref this._blendedTokens, this._blendedTokens.Length * 2);
            SyntaxParser.s_blendedNodesPool.ForgetTrackedObject(old, replacement: this._blendedTokens);
        }
    }

    /// <summary>
    /// 扩充已词法分析的标志数组的容量。
    /// </summary>
    [MemberNotNull(nameof(SyntaxParser._lexedTokens))]
    private void AddLexedTokenSlot()
    {
        Debug.Assert(!this.IsBlending());

        if (this._tokenOffset > (this._lexedTokens.Length >> 1) && (this._resetStart == -1 || this._resetStart > this._firstToken))
        {
            int shiftOffset = (this._resetStart == -1) ? this._tokenOffset : this._resetStart - this._firstToken;
            int shiftCount = this._tokenCount - shiftOffset;
            Debug.Assert(shiftOffset > 0);
            if (shiftCount > 0)
                Array.Copy(this._lexedTokens, shiftOffset, this._lexedTokens, 0, shiftCount);

            this._firstToken += shiftOffset;
            this._tokenCount -= shiftOffset;
            this._tokenOffset -= shiftOffset;
        }
        else
        {
            var old = new ArrayElement<SyntaxToken>[this._lexedTokens.Length * 2];
            Array.Copy(this._lexedTokens, old, this._lexedTokens.Length);
            this._lexedTokens = old;
        }
    }

    /// <summary>
    /// 查看当前后方第<paramref name="n"/>个位置的语法节点。
    /// </summary>
    /// <param name="n">表示要查看的语法节点在当前后方的位置。</param>
    /// <returns>当前后方第<paramref name="n"/>个位置的语法节点。</returns>
    /// <remarks>此操作不会改变此语法解析器的内容和状态。</remarks>
    protected SyntaxToken PeekToken(int n = 0)
    {
        Debug.Assert(n >= 0);
        if (n == 0) return this.CurrentToken;

        // 补充不足的标志。
        while (this._tokenOffset + n >= this._tokenCount)
            this.AddNewToken();

        if (this.IsBlending())
            return this._blendedTokens[this._tokenOffset + n].Token;
        else
            return this._lexedTokens[this._tokenOffset + n];
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标志。
    /// </summary>
    /// <returns>当前的已词法分析的语法标志。</returns>
    protected SyntaxToken EatToken()
    {
        var token = this.CurrentToken;
        this.MoveToNextToken();
        return token;
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标志，这个语法标志必须符合指定语法部分种类。
    /// </summary>
    /// <param name="kind">要返回的语法标志须符合的语法部分种类，枚举值必须标志语法标志。</param>
    /// <returns>若当前的已词法分析的语法标志符合指定语法部分种类，则返回这个语法标志；否则返回表示缺失标志的语法标志，并报告错误。</returns>
    protected SyntaxToken EatToken(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));

        var token = this.CurrentToken;
        if (token.Kind == kind)
        {
            this.MoveToNextToken();
            return token;
        }
        else
            return this.CreateMissingToken(kind, token.Kind, reportError: true);
    }

    /// <param name="reportError">是否报告错误。</param>
    /// <returns>若当前的已词法分析的语法标志符合指定语法部分种类，则返回这个语法标志；否则返回表示缺失标志的语法标志。</returns>
    /// <inheritdoc cref="SyntaxParser.EatToken(SyntaxKind)"/>
    protected SyntaxToken EatToken(SyntaxKind kind, bool reportError)
    {
        if (reportError) return this.EatToken(kind);

        Debug.Assert(SyntaxFacts.IsAnyToken(kind));

        if (this.CurrentToken.Kind == kind)
            return this.EatToken();
        else
            return SyntaxFactory.MissingToken(kind);
    }

    /// <param name="code">要报告的错误码。</param>
    /// <param name="reportError">是否报告错误。</param>
    /// <returns>若当前的已词法分析的语法标志符合指定语法部分种类，则返回这个语法标志；否则返回表示缺失标志的语法标志。</returns>
    /// <inheritdoc cref="SyntaxParser.EatToken(SyntaxKind)"/>
    protected SyntaxToken EatToken(SyntaxKind kind, ErrorCode code, bool reportError = true)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));
        if (this.CurrentToken.Kind == kind)
            return this.EatToken();
        else
            return this.CreateMissingToken(kind, code, reportError);
    }

    /// <summary>
    /// 尝试接受并返回当前的已词法分析的语法标志，这个语法标志必须符合指定语法部分种类。
    /// </summary>
    /// <returns>若当前的已词法分析的语法标志符合指定语法部分种类，则返回这个语法标志；否则返回<see langword="null"/>。</returns>
    /// <inheritdoc cref="SyntaxParser.EatToken(SyntaxKind)"/>
    protected SyntaxToken? TryEatToken(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));

        return this.CurrentToken.Kind == kind ? this.EatToken() : null;
    }

    /// <summary>
    /// 移动到下一个标志。
    /// </summary>
    private void MoveToNextToken()
    {
        // 设置上一个标志的后方琐碎内容。
        this.prevTokenTrailingTrivia = this.CurrentToken.GetTrailingTrivia();

        // 初始化部分变量。
        this._currentToken = null;
        if (this.IsBlending())
            this._currentNode = default;

        // 向后移动一个位置。
        this._tokenOffset++;
    }

    /// <summary>
    /// 强制使得当前标志为文件结束标志。
    /// </summary>
    protected void ForceEndOfFile() =>
        this._currentToken = SyntaxFactory.Token(SyntaxKind.EndOfFileToken);

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标志，若这个语法标志不符合指定语法部分种类，则跳过并替换为正确的语法标志。
    /// </summary>
    /// <param name="expected">要返回的语法标志应符合的语法部分种类，枚举值必须标志语法标志。</param>
    /// <returns>若当前的已词法分析的语法标志符合指定语法部分种类，则返回这个语法标志；否则返回一个结尾跳过语法，使用表示缺失标志的语法标志替换这个语法标志，并报告错误。</returns>
    protected SyntaxToken EatTokenAsKind(SyntaxKind expected)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(expected));

        var token = this.CurrentToken;
        if (token.Kind == expected)
        {
            this.MoveToNextToken();
            return token;
        }

        var replacement = this.CreateMissingToken(expected, token.Kind, reportError: true);
        return this.AddTrailingSkippedSyntax(replacement, this.EatToken());
    }

    /// <summary>
    /// 创建一个表示缺少指定标志类型的语法标志。
    /// </summary>
    /// <param name="expected">期望的标志类型。</param>
    /// <param name="actual">实际的标志类型。</param>
    /// <param name="reportError">是否报告错误。</param>
    private SyntaxToken CreateMissingToken(SyntaxKind expected, SyntaxKind actual, bool reportError)
    {
        var token = SyntaxFactory.MissingToken(expected);
        if (reportError)
            token = this.WithAdditionalDiagnostics(token, this.GetExpectedTokenError(expected, actual));

        return token;
    }

    /// <summary>
    /// 创建一个表示缺少指定标志类型的语法标志。
    /// </summary>
    /// <param name="expected">期望的标志类型。</param>
    /// <param name="code">要报告的错误码。</param>
    /// <param name="reportError">是否报告错误。</param>
    private SyntaxToken CreateMissingToken(SyntaxKind expected, ErrorCode code, bool reportError)
    {
        var token = SyntaxFactory.MissingToken(expected);
        if (reportError)
            token = this.AddError(token, code);

        return token;
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标志，若这个语法标志不符合指定语法部分种类，则对其报告错误。
    /// </summary>
    /// <param name="kind">要返回的语法标志须符合的语法部分种类，枚举值必须标志语法标志。</param>
    /// <returns>返回当前的已词法分析的语法标志，若这个语法标志不符合指定语法部分种类，则报告错误。</returns>
    protected SyntaxToken EatTokenWithPrejudice(SyntaxKind kind)
    {
        var token = this.CurrentToken;
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));
        if (token.Kind != kind)
            token = this.WithAdditionalDiagnostics(token, this.GetExpectedTokenError(kind, token.Kind));

        this.MoveToNextToken();
        return token;
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标志，并对其报告错误。
    /// </summary>
    /// <param name="code">要报告的错误码。</param>
    /// <param name="args">诊断信息的参数。</param>
    /// <returns>返回当前的已词法分析的语法标志，并报告指定错误码和诊断消参数的错误。</returns>
    protected SyntaxToken EatTokenWithPrejudice(ErrorCode code, params object[] args)
    {
        var token = this.EatToken();
        token = this.WithAdditionalDiagnostics(token, SyntaxParser.MakeError(token.GetLeadingTriviaWidth(), token.Width, code, args));
        return token;
    }

    /// <summary>
    /// 语法解析器接受当前的已词法分析的语法标志，转化为关键字语法标志并且返回。
    /// </summary>
    /// <returns>转化当前的已词法分析的语法标志为关键字语法标志并且返回。</returns>
    protected SyntaxToken EatContextualToken() => SyntaxParser.ConvertToKeyword(this.EatToken());

    /// <summary>
    /// 语法解析器接受当前的已词法分析的语法标志，转化为关键字语法标志并且返回，这个语法标志的上下文种类必须符合指定语法部分种类。
    /// </summary>
    /// <param name="kind">要返回的语法标志须符合的语法部分种类，枚举值必须标志语法标志。</param>
    /// <returns>若当前的已词法分析的语法标志的上下文种类符合指定语法部分种类，则转化这个语法标志为关键字语法标志并且返回；否则返回表示缺失标志的语法标志，并报告错误。</returns>
    protected SyntaxToken EatContextualToken(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));

        var contextualKind = this.CurrentToken.ContextualKind;
        if (contextualKind == kind)
            return this.EatContextualToken();
        else
            return this.CreateMissingToken(kind, contextualKind, reportError: true);
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标志，这个语法标志的上下文种类必须符合指定语法部分种类。
    /// </summary>
    /// <param name="kind">要返回的语法标志须符合的语法部分种类，枚举值必须标志语法标志。</param>
    /// <param name="reportError">是否报告错误。</param>
    /// <returns>若当前的已词法分析的语法标志的上下文种类符合指定语法部分种类，则返回这个语法标志；否则返回表示缺失标志的语法标志，并报告错误。</returns>
    protected SyntaxToken EatContextualToken(SyntaxKind kind, bool reportError)
    {
        if (reportError) return this.EatContextualToken(kind);

        Debug.Assert(SyntaxFacts.IsAnyToken(kind));

        var contextualKind = this.CurrentToken.ContextualKind;
        if (contextualKind == kind)
            return this.EatContextualToken();
        else
            return SyntaxFactory.MissingToken(kind);
    }

    /// <param name="code">要报告的错误码。</param>
    /// <returns>若当前的已词法分析的语法标志的上下文种类符合指定语法部分种类，则返回这个语法标志；否则返回表示缺失标志的语法标志，并报告指定错误码的错误。</returns>
    /// <inheritdoc cref="SyntaxParser.EatContextualToken(SyntaxKind, bool)"/>
    protected SyntaxToken EatContextualToken(SyntaxKind kind, ErrorCode code, bool reportError = true)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));

        if (this.CurrentToken.ContextualKind == kind)
            return this.EatContextualToken();
        else
            return this.CreateMissingToken(kind, code, reportError);
    }

    /// <summary>
    /// 获取一个诊断信息，表示未得到期望的标志。
    /// </summary>
    /// <param name="expected">期望的标志类型。</param>
    /// <param name="actual">实际的标志类型。</param>
    /// <returns>表示未得到期望的标志的诊断信息。</returns>
    protected virtual SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual)
    {
        // 获取缺失标志的诊断文本范围。
        this.GetDiagnosticSpanForMissingToken(out int offset, out int width);

        return this.GetExpectedTokenError(expected, actual, offset, width);
    }

    /// <param name="offset">实际的标志的文本范围的偏移量。</param>
    /// <param name="width">实际的标志的文本范围的宽度。</param>
    /// <inheritdoc cref="SyntaxParser.GetExpectedTokenError(SyntaxKind, SyntaxKind)"/>
    protected virtual partial SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual, int offset, int width);

    /// <summary>
    /// 获取一个错误码，对应未得到期望的标志。
    /// </summary>
    /// <returns>对应未得到期望的标志的错误码。</returns>
    /// <inheritdoc cref="SyntaxParser.GetExpectedTokenError(SyntaxKind, SyntaxKind)"/>
    protected static partial ErrorCode GetExpectedTokenErrorCode(SyntaxKind expected, SyntaxKind actual);

    /// <summary>
    /// 获取缺失标志的诊断文本范围。
    /// </summary>
    /// <param name="offset">文本范围的偏移量。</param>
    /// <param name="width">文本范围的宽度。</param>
    /// <remarks>
    /// 若上一个标志后方跟着的琐碎内容中包含行尾琐碎内容，则缺失标志的诊断位置将置于包含上一个标志的行尾，并且宽度为零；
    /// 否则诊断的偏移量和宽度与当前标志的位置和宽度一致。
    /// </remarks>
    protected void GetDiagnosticSpanForMissingToken(out int offset, out int width)
    {
        // 若上一个标志后方跟着的琐碎内容中包含行尾琐碎内容，则缺失标志的诊断位置将置于包含上一个标志的行尾，并且宽度为零。
        var trivia = this.prevTokenTrailingTrivia;
        if (trivia is not null)
        {
            var triviaList = new SyntaxList<ThisInternalSyntaxNode>(trivia);
            bool prevTokenHasEndOfLineTrivia = triviaList.Any((int)SyntaxKind.EndOfLineTrivia);
            if (prevTokenHasEndOfLineTrivia)
            { // 包含行尾琐碎内容。
                offset = -trivia.FullWidth; // 向前跳过上一个标志后方跟着的琐碎内容的宽度。
                width = 0;
                return;
            }
        }

        // 否则诊断的偏移量和宽度与当前标志的位置和宽度一致。
        SyntaxToken token = this.CurrentToken;
        offset = token.GetLeadingTriviaWidth(); // 向后跳过前方琐碎内容的宽度。
        width = token.Width;
    }

    /// <summary>
    /// 向语法节点添加附加诊断信息。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">要添加附加诊断信息的语法节点。</param>
    /// <param name="diagnostics">要添加的附加诊断信息。</param>
    /// <returns>添加附加诊断信息后的<paramref name="node"/>。</returns>
    protected virtual TNode WithAdditionalDiagnostics<TNode>(TNode node, params DiagnosticInfo[] diagnostics)
        where TNode : GreenNode
    {
        var existingDiagnostics = node.GetDiagnostics();
        int existingLength = existingDiagnostics.Length;
        if (existingLength == 0)
            return node.WithDiagnosticsGreen(diagnostics);
        else
        {
            var result = new DiagnosticInfo[existingDiagnostics.Length + diagnostics.Length];
            existingDiagnostics.CopyTo(result, 0);
            diagnostics.CopyTo(result, existingLength);
            return node.WithDiagnosticsGreen(result);
        }
    }

    /// <summary>
    /// 向语法节点添加诊断错误信息。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">要添加附加诊断信息的语法节点。</param>
    /// <param name="code">要添加的错误码。</param>
    /// <returns>添加诊断错误信息后的<paramref name="node"/>。</returns>
    protected TNode AddError<TNode>(TNode node, ErrorCode code) where TNode : GreenNode =>
        this.AddError(node, code, Array.Empty<object>());

    /// <param name="args">诊断信息的参数。</param>
    /// <inheritdoc cref="SyntaxParser.AddError{TNode}(TNode, ErrorCode)"/>
    protected TNode AddError<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : GreenNode
    {
        // 不是缺失节点，直接添加附加诊断信息。
        if (!node.IsMissing)
            return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(node, code, args));

        // 对指点范围添加诊断错误信息。
        int offset, width;

        if (node is SyntaxToken token && token.ContainsSkippedText)
        { // 对连续的被跳过文本添加。
            offset = token.GetLeadingTriviaWidth();
            Debug.Assert(offset == 0);

            width = 0;
            bool seenSkipped = false;
            foreach (var trivia in token.TrailingTrivia)
            {
                if (trivia.Kind == SyntaxKind.SkippedTokensTrivia)
                {
                    seenSkipped = true;
                    width += trivia.Width;
                }
                else if (seenSkipped)
                    break;
                else
                    offset += trivia.Width;
            }
        }
        else // 对缺失标志的文本添加。
            this.GetDiagnosticSpanForMissingToken(out offset, out width);

        return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, width, code, args));
    }

    /// <param name="offset">文本范围的偏移量。</param>
    /// <param name="length">文本范围的长度。</param>
    /// <inheritdoc cref="SyntaxParser.AddError{TNode}(TNode, ErrorCode, object[])"/>
    protected TNode AddError<TNode>(TNode node, int offset, int length, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode =>
        this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, length, code, args));

    /// <summary>
    /// 向指定根节点下的指定语法节点添加诊断错误信息。
    /// </summary>
    /// <param name="node">包含<paramref name="location"/>的节点。</param>
    /// <param name="location">要添加附加诊断信息的语法节点，此节点在<paramref name="node"/>下。</param>
    /// <inheritdoc cref="SyntaxParser.AddError{TNode}(TNode, ErrorCode, object[])"/>
    protected TNode AddError<TNode>(TNode node, ThisInternalSyntaxNode location, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode
    {
        // 查找偏移量。
        this.FindOffset(node, location, out int offset);
        return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, location.Width, code, args));
    }

    /// <summary>
    /// 向语法节点的第一个标志添加诊断错误信息。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">要添加附加诊断信息的标志所在的语法节点。</param>
    /// <param name="code">要添加的错误码。</param>
    /// <returns>其第一个标志添加诊断错误信息后的<paramref name="node"/>。</returns>
    protected TNode AddErrorToFirstToken<TNode>(TNode node, ErrorCode code) where TNode : ThisInternalSyntaxNode
    {
        SyntaxParser.GetOffsetAndWidthForFirstToken(node, out int offset, out int width);
        return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, width, code));
    }

    /// <param name="args">诊断信息的参数。</param>
    /// <inheritdoc cref="AddErrorToFirstToken{TNode}(TNode, ErrorCode)"/>
    protected TNode AddErrorToFirstToken<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode
    {
        SyntaxParser.GetOffsetAndWidthForFirstToken(node, out int offset, out int width);
        return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, width, code, args));
    }

    /// <summary>
    /// 向语法节点的最后一个标志添加诊断错误信息。
    /// </summary>
    /// <returns>其最后一个标志添加诊断错误信息后的<paramref name="node"/>。</returns>
    /// <inheritdoc cref="AddErrorToFirstToken{TNode}(TNode, ErrorCode)"/>
    protected TNode AddErrorToLastToken<TNode>(TNode node, ErrorCode code) where TNode : ThisInternalSyntaxNode
    {
        SyntaxParser.GetOffsetAndWidthForLastToken(node, out int offset, out int width);
        return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, width, code));
    }

    /// <inheritdoc cref="AddErrorToLastToken{TNode}(TNode, ErrorCode)"/>
    /// <inheritdoc cref="AddErrorToFirstToken{TNode}(TNode, ErrorCode, object[])"/>
    protected TNode AddErrorToLastToken<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode
    {
        SyntaxParser.GetOffsetAndWidthForLastToken(node, out int offset, out int width);
        return this.WithAdditionalDiagnostics(node, SyntaxParser.MakeError(offset, width, code, args));
    }

    /// <summary>
    /// 获取指定语法节点的第一个标志的偏移量和宽度。
    /// </summary>
    private static void GetOffsetAndWidthForFirstToken<TNode>(TNode node, out int offset, out int width) where TNode : ThisInternalSyntaxNode
    {
        var firstToken = node.GetFirstToken();
        Debug.Assert(firstToken is not null);

        offset = firstToken.GetLeadingTriviaWidth();
        width = firstToken.Width;
    }

    /// <summary>
    /// 获取指定语法节点的最后一个标志的偏移量和宽度。
    /// </summary>
    private static void GetOffsetAndWidthForLastToken<TNode>(TNode node, out int offset, out int width) where TNode : ThisInternalSyntaxNode
    {
        var lastToken = node.GetLastNonmissingToken();

        offset = node.FullWidth; // 向后移动到节点尾部。
        if (lastToken is null) // 当所有节点均缺失时。
            width = 0;
        else
        {
            offset -= lastToken.FullWidth; // 向前回退到标志的头部。
            offset += lastToken.GetLeadingTriviaWidth(); // 向后移动到标志前方琐碎内容之后。
            width = lastToken.Width;
        }
    }

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定偏移量、宽度和错误码。
    /// </summary>
    /// <returns>语法诊断错误消息的实例。</returns>
    /// <inheritdoc cref="SyntaxDiagnosticInfo.SyntaxDiagnosticInfo(int, int, ErrorCode)"/>
    protected static SyntaxDiagnosticInfo MakeError(int offset, int width, ErrorCode code) => new(offset, width, code);

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定偏移量、宽度、错误码和消息参数。
    /// </summary>
    /// <inheritdoc cref="SyntaxParser.MakeError(int, int, ErrorCode)"/>
    /// <inheritdoc cref="SyntaxDiagnosticInfo.SyntaxDiagnosticInfo(int, int, ErrorCode)"/>
    protected static SyntaxDiagnosticInfo MakeError(int offset, int width, ErrorCode code, params object[] args) => new(offset, width, code, args);

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定绿树节点的偏移量和宽度，以及指定的错误码和消息参数。
    /// </summary>
    /// <param name="node">这个绿树节点的范围即创建的实例表示的范围。</param>
    /// <inheritdoc cref="SyntaxParser.MakeError(int, int, ErrorCode)"/>
    protected static SyntaxDiagnosticInfo MakeError(GreenNode node, ErrorCode code, params object[] args) => new(node.GetLeadingTriviaWidth(), node.Width, code, args);

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定错误码和消息参数。
    /// </summary>
    /// <inheritdoc cref="SyntaxParser.MakeError(int, int, ErrorCode, object[])"/>
    protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args) => new(code, args);

    /// <summary>
    /// 给指定的语法列表构造器添加指定的前方的跳过语法。
    /// </summary>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddLeadingSkippedSyntax(SyntaxListBuilder list, GreenNode skippedSyntax) =>
        list[0] = this.AddLeadingSkippedSyntax((ThisInternalSyntaxNode)list[0]!, skippedSyntax);

    /// <summary>
    /// 给指定的语法列表构造器添加指定的前方的跳过语法。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddLeadingSkippedSyntax<TNode>(SyntaxListBuilder<TNode> list, GreenNode skippedSyntax) where TNode : ThisInternalSyntaxNode =>
        list[0] = this.AddTrailingSkippedSyntax(list[0]!, skippedSyntax);

    /// <summary>
    /// 给指定的语法节点添加指定的前方的跳过语法。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">要添加<paramref name="skippedSyntax"/>的语法节点。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="node"/>。</returns>
    protected TNode AddLeadingSkippedSyntax<TNode>(TNode node, GreenNode skippedSyntax) where TNode : ThisInternalSyntaxNode
    {
        var oldToken = node as SyntaxToken ?? node.GetFirstToken()!;
        var newToken = this.AddSkippedSyntax(oldToken, skippedSyntax, trailing: false);
        return SyntaxFirstTokenReplacer.Replace(node, oldToken, newToken, skippedSyntax.FullWidth);
    }

    /// <summary>
    /// 给指定的语法列表构造器添加指定的后方的跳过语法。
    /// </summary>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddTrailingSkippedSyntax(SyntaxListBuilder list, GreenNode skippedSyntax) =>
        list[^1] = this.AddTrailingSkippedSyntax((ThisInternalSyntaxNode)list[^1]!, skippedSyntax);

    /// <summary>
    /// 给指定的语法列表构造器添加指定的后方的跳过语法。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddTrailingSkippedSyntax<TNode>(SyntaxListBuilder<TNode> list, GreenNode skippedSyntax) where TNode : ThisInternalSyntaxNode =>
        list[^1] = this.AddTrailingSkippedSyntax(list[^1]!, skippedSyntax);

    /// <summary>
    /// 给指定的语法节点添加指定的后方的跳过语法。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">要添加<paramref name="skippedSyntax"/>的语法节点。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="node"/>。</returns>
    protected TNode AddTrailingSkippedSyntax<TNode>(TNode node, GreenNode skippedSyntax) where TNode : ThisInternalSyntaxNode
    {
        var oldToken = node as SyntaxToken ?? node.GetLastToken()!;
        var newToken = this.AddSkippedSyntax(oldToken, skippedSyntax, trailing: true);
        return SyntaxLastTokenReplacer.Replace(node, oldToken, newToken);
    }

    /// <summary>
    /// 给指定的语法标志添加指定的表示跳过语法的绿树节点。
    /// </summary>
    /// <param name="target">要添加<paramref name="skippedSyntax"/>的语法标志。</param>
    /// <param name="skippedSyntax">要添加的表示跳过语法的绿树节点。</param>
    /// <param name="trailing">若为<see langword="true"/>，则将<paramref name="skippedSyntax"/>添加到后方语法琐碎内容中；否则添加到前方语法琐碎内容中。</param>
    /// <returns></returns>
    internal SyntaxToken AddSkippedSyntax(SyntaxToken target, GreenNode skippedSyntax, bool trailing)
    {
        var builder = new SyntaxListBuilder(4);

        SyntaxDiagnosticInfo? diagnostic = null;

        int diagnosticOffset = 0;

        int currentOffset = 0;
        foreach (var node in skippedSyntax.EnumerateNodes())
        {
            if (node is SyntaxToken token)
            {
                builder.Add(token.GetLeadingTrivia());

                if (token.Width > 0)
                {
                    var tk = token.TokenWithLeadingTrivia(null).TokenWithTrailingTrivia(null);

                    int leadingWidth = token.GetLeadingTriviaWidth();
                    if (leadingWidth > 0)
                    {
                        var tokenDiagnostics = tk.GetDiagnostics();
                        for (int i = 0; i < tokenDiagnostics.Length; i++)
                        {
                            var d = (SyntaxDiagnosticInfo)tokenDiagnostics[i];
                            tokenDiagnostics[i] = new SyntaxDiagnosticInfo(d.Offset - leadingWidth, d.Width, (ErrorCode)d.Code, d.Arguments);
                        }
                    }

                    builder.Add(SyntaxFactory.SkippedTokensTrivia(tk));
                }
                else
                {
                    var existing = (SyntaxDiagnosticInfo?)token.GetDiagnostics().FirstOrDefault();
                    if (existing is not null)
                    {
                        diagnostic = existing;
                        diagnosticOffset = currentOffset;
                    }
                }
                builder.Add(token.GetTrailingTrivia());

                currentOffset += token.FullWidth;
            }
            else if (node.ContainsDiagnostics && diagnostic == null)
            {
                var existing = (SyntaxDiagnosticInfo?)node.GetDiagnostics().FirstOrDefault();
                if (existing is not null)
                {
                    diagnostic = existing;
                    diagnosticOffset = currentOffset;
                }
            }
        }

        int triviaWidth = currentOffset;
        var trivia = builder.ToListNode();

        int triviaOffset;
        if (trailing)
        {
            var trailingTrivia = target.GetTrailingTrivia();
            triviaOffset = target.FullWidth;
            target = target.TokenWithTrailingTrivia(SyntaxList.Concat(trailingTrivia, trivia));
        }
        else
        {
            if (triviaWidth > 0)
            {
                var targetDiagnostics = target.GetDiagnostics();
                for (int i = 0; i < targetDiagnostics.Length; i++)
                {
                    var d = (SyntaxDiagnosticInfo)targetDiagnostics[i];
                    targetDiagnostics[i] = new SyntaxDiagnosticInfo(d.Offset + triviaWidth, d.Width, (ErrorCode)d.Code, d.Arguments);
                }
            }

            var leadingTrivia = target.GetLeadingTrivia();
            target = target.TokenWithLeadingTrivia(SyntaxList.Concat(trivia, leadingTrivia));
            triviaOffset = 0;
        }

        if (diagnostic is not null)
        {
            int newOffset = triviaOffset + diagnosticOffset + diagnostic.Offset;

            target = this.WithAdditionalDiagnostics(target, new SyntaxDiagnosticInfo(newOffset, diagnostic.Width, (ErrorCode)diagnostic.Code, diagnostic.Arguments)
            );
        }

        return target;
    }

    /// <summary>
    /// 在指定根节点中查找指定语法节点的偏移量。
    /// </summary>
    /// <param name="root">根节点。</param>
    /// <param name="location">要查询的语法节点。</param>
    /// <param name="offset">返回在以<paramref name="root"/>作为根节点的子树中<paramref name="location"/>的偏移量。</param>
    /// <returns>若在<paramref name="root"/>中查询到<paramref name="location"/>，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    private bool FindOffset(GreenNode root, ThisInternalSyntaxNode location, out int offset)
    {
        offset = 0;

        int currentOffset = 0;
        for (int i = 0, n = root.SlotCount; i < n; i++)
        {
            var child = root.GetSlot(i);
            // 忽略子节点为空的情况。
            if (child is null) continue;
            // 检查子节点是否为要查询的节点。
            else if (child == location)
            {
                offset = currentOffset;
                return true;
            }
            // 递归查询子树中的节点是否为要查询的节点。
            else if (this.FindOffset(child, location, out offset))
            {
                // 计算偏移量。
                offset += child.GetLeadingTriviaWidth() + currentOffset;
                return true;
            }
            // 子树中未找到要查询的节点，累加偏移量并进入下一轮循环。
            else
                currentOffset += child.FullWidth;
        }

        // 未找到要查询的节点。
        return false;
    }

    /// <summary>
    /// 将非关键字标志转换为包含相同信息的关键字标志。
    /// </summary>
    /// <param name="token">要转化的非关键字标志。</param>
    /// <returns>一个关键字标志，包含非关键字标志<paramref name="token"/>的所有信息。</returns>
    protected static SyntaxToken ConvertToKeyword(SyntaxToken token)
    {
        if (token.Kind != token.ContextualKind)
        {
            // 分两种情况：是否为缺失标志。
            var kw = token.IsMissing
                ? SyntaxFactory.MissingToken(
                    token.LeadingTrivia.Node,
                    token.ContextualKind,
                    token.TrailingTrivia.Node
                )
                : SyntaxFactory.Token(
                    token.LeadingTrivia.Node,
                    token.ContextualKind,
                    token.TrailingTrivia.Node
                );
            var d = token.GetDiagnostics();
            // 如果有诊断信息，则附加到标志上。
            if (d is not null && d.Length > 0)
                kw = kw.WithDiagnosticsGreen(d);

            return kw;
        }

        return token;
    }

    /// <summary>
    /// 将非标识符标志转换为包含相同信息的标识符标志。
    /// </summary>
    /// <param name="token">要转化的非标识符标志。</param>
    /// <returns>一个标识符标志，包含非标识符标志<paramref name="token"/>的所有信息。</returns>
    protected static SyntaxToken ConvertToIdentifier(SyntaxToken token)
    {
        Debug.Assert(!token.IsMissing);
        return SyntaxToken.Identifier(
            token.Kind,
            token.LeadingTrivia.Node,
            token.Text,
            token.ValueText,
            token.TrailingTrivia.Node
        );
    }

    /// <summary>
    /// 检查特性是否可用，不可用时为语法节点附加错误信息。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">作为载体的语法节点。</param>
    /// <param name="feature">要检查的特性。</param>
    /// <param name="forceWarning">是否强制视为警告。</param>
    /// <returns>检查处理后的语法节点。</returns>
    protected partial TNode CheckFeatureAvailability<TNode>(TNode node, MessageID feature, bool forceWarning = false)
        where TNode : GreenNode;

    /// <inheritdoc cref="ThisParseOptions.IsFeatureEnabled(MessageID)"/>
    protected bool IsFeatureEnabled(MessageID feature) => this.Options.IsFeatureEnabled(feature);

    /// <summary>获取当前解析的标志的位置。</summary>
    private int CurrentTokenPosition => this._firstToken + this._tokenOffset;

    /// <summary>
    /// 当解析进入循环流程中时，为防止因意外的错误导致解析器无法向后分析而出现死循环，此方法应作为保险措施而非实现功能的方式。
    /// </summary>
    /// <param name="lastTokenPosition">上一次更新的标志位置。</param>
    /// <param name="assertIfFalse">当解析器无法向后分析时是否使用断言中断。</param>
    /// <returns>若为<see langword="true"/>时，表示解析器正常向后分析；若为<see langword="false"/>时，表示解析器无法向后分析。</returns>
    protected bool IsMakingProgress(ref int lastTokenPosition, bool assertIfFalse = true)
    {
        var pos = this.CurrentTokenPosition;
        if (pos > lastTokenPosition)
        {
            lastTokenPosition = pos;
            return true;
        }

        Debug.Assert(!assertIfFalse);
        return false;
    }

    #region IDisposable
    public void Dispose()
    {
        var blendedTokens = _blendedTokens;
        if (blendedTokens is not null)
        {
            this._blendedTokens = null!;
            if (blendedTokens.Length < 4096)
            {
                Array.Clear(blendedTokens, 0, blendedTokens.Length);
                SyntaxParser.s_blendedNodesPool.Free(blendedTokens);
            }
            else
            {
                SyntaxParser.s_blendedNodesPool.ForgetTrackedObject(blendedTokens);
            }
        }
    }
    #endregion
}
