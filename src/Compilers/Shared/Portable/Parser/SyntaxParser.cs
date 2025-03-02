﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
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
    /// <summary>语法解析器的各项操作的取消标记。</summary>
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
    /// 当前处理的标记在<see cref="_lexedTokens"/>或<see cref="_blendedTokens"/>的索引位置。
    /// </summary>
    private int _tokenOffset;
    private int _tokenCount;
    private int _resetCount;
    private int _resetStart;

    private static readonly ObjectPool<BlendedNode[]> s_blendedNodesPool = new(() => new BlendedNode[32], 2);

    /// <summary>
    /// 获取一个值，指示语法解析器是否进行增量语法分析。
    /// </summary>
    protected bool IsIncremental => _isIncremental;

    /// <summary>
    /// 获取语法解析器的设置。
    /// </summary>
    /// <seealso cref="Lexer.Options"/>
    public ThisParseOptions Options => lexer.Options;

    /// <summary>
    /// 获取一个值，指示解析的是脚本类代码文本。
    /// </summary>
    public bool IsScript => Options.Kind == SourceCodeKind.Script;

    // Only for test.
    protected internal bool IsAtEndOfFile => CurrentTokenKind == SyntaxKind.EndOfFileToken;

    /// <summary>
    /// 获取或设置词法器模式。
    /// </summary>
    protected LexerMode Mode
    {
        get => _mode;
        set
        {
            if (_mode != value)
            {
                Debug.Assert(_allowModeReset);

                // 设置新的词法器模式并重置字段。
                _mode = value;
                _currentToken = null;
                _currentNode = default;
                _tokenCount = _tokenOffset;
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
    /// <param name="cancellationToken">语法解析器的操作的取消标记。</param>
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
        _mode = mode;
        _allowModeReset = allowModeReset;
        this.cancellationToken = cancellationToken;
        _currentNode = default;
        _isIncremental = oldTree is not null; // 是否开启增量处理操作。

        if (_isIncremental || allowModeReset) // 协调新旧节点。
        {
            _firstBlender = new(lexer, oldTree, changes);
            _blendedTokens = s_blendedNodesPool.Allocate();
        }
        else // 均为新节点。
        {
            _firstBlender = default;
            _lexedTokens = new ArrayElement<SyntaxToken>[32];
        }

        // 进行词法器预分析，此操作不应被取消。
        // 因为在构造函数中取消操作将会使处置操作变得复杂难懂，所以应排除可取消的情况。
        if (preLexIfNotIncremental && !_isIncremental && !cancellationToken.CanBeCanceled)
            PreLex();
    }

    /// <summary>
    /// 语法解析器是否协调新旧节点。
    /// </summary>
    /// <returns>若语法解析器开启增量处理操作，且允许重设词法器的模式时返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    /// <remarks>
    /// 若返回<see langword="true"/>时，<see cref="_blendedTokens"/>不为<see langword="null"/>；
    /// 若返回<see langword="false"/>时，<see cref="_lexedTokens"/>不为<see langword="null"/>。
    /// </remarks>
    [MemberNotNullWhen(true, nameof(_blendedTokens))]
    [MemberNotNullWhen(false, nameof(_lexedTokens))]
    protected bool IsBlending() => _isIncremental || _allowModeReset;

    /// <summary>
    /// 重新初始化此语法解析器实例。
    /// </summary>
    protected void ReInitialize()
    {
        _firstToken = 0;
        _tokenOffset = 0;
        _tokenCount = 0;
        _resetCount = 0;
        _resetStart = 0;
        _currentToken = null;
        prevTokenTrailingTrivia = null;
        if (IsBlending())
            _firstBlender = new(lexer, null, null);
    }

    /// <summary>
    /// 词法器预分析。
    /// </summary>
    [MemberNotNull(nameof(_lexedTokens))]
    private void PreLex()
    {
        // 不应在这个方法内部处理取消标记。
        var size = Math.Min(4096, Math.Max(32, lexer.TextWindow.Text.Length / 2));
        _lexedTokens ??= new ArrayElement<SyntaxToken>[size];

        for (var i = 0; i < size; i++)
        {
            var token = lexer.Lex(_mode); // 词法器分析一个标记。
            AddLexedToken(token);

            // 遇到文件结尾。
            if (token.Kind == SyntaxKind.EndOfFileToken) break;
        }
    }

    /// <summary>
    /// 获取当前的已协调节点。
    /// </summary>
    [MemberNotNull(nameof(_blendedTokens))]
    protected ThisSyntaxNode? CurrentNode
    {
        get
        {
            Debug.Assert(IsBlending());

            var node = _currentNode.Node;
            if (node is not null) return node;

            ReadCurrentNode();
            return _currentNode.Node;
        }
    }

    /// <summary>
    /// 获取当前的已协调节点的语法部分种类。
    /// </summary>
    protected SyntaxKind CurrentNodeKind => CurrentNode?.Kind() ?? SyntaxKind.None;

    /// <summary>
    /// 读取当前的节点到<see cref="_currentNode"/>。
    /// </summary>
    [MemberNotNull(nameof(_blendedTokens))]
    private void ReadCurrentNode()
    {
        Debug.Assert(IsBlending());

        // 获取用来读取节点的协调器。
        Blender blender;
        if (_tokenOffset == 0)
            blender = _firstBlender;
        else
            blender = _blendedTokens[_tokenOffset - 1].Blender;
        // 使用协调器读取节点。
        _currentNode = blender.ReadNode(_mode);
    }

    /// <summary>
    /// 语法解析器接受当前的语法节点，并返回其对应的绿树节点。
    /// </summary>
    /// <returns>接受的语法节点对应的绿树节点。</returns>
    [MemberNotNull(nameof(_blendedTokens))]
    protected GreenNode? EatNode()
    {
        Debug.Assert(IsBlending());

        // 语法解析器吃下（当前节点）的绿树节点。
        var result = CurrentNode?.Green;

        // 必要时扩充已协调标记数组的容量。
        if (_tokenOffset >= _blendedTokens.Length)
            AddTokenSlot();

        // 添加节点到已处理的标记数组中。
        _blendedTokens[_tokenOffset++] = _currentNode;
        _tokenCount = _tokenOffset;

        // 恢复当前处理的变量为初始状态。
        _currentNode = default;
        _currentToken = null;

        return result;
    }

    /// <summary>
    /// 语法解析器接受当前的语法节点，并返回其对应的指定类型的绿树节点。
    /// </summary>
    /// <typeparam name="T">要返回的绿树节点的类型。</typeparam>
    /// <returns>接受的语法节点对应的绿树节点。</returns>
    [MemberNotNull(nameof(_blendedTokens))]
    protected T? EatNode<T>() where T : ThisInternalSyntaxNode => EatNode() as T;

    /// <summary>
    /// 获取当前的已词法分析的标记。
    /// </summary>
    protected SyntaxToken CurrentToken => _currentToken ??= FetchCurrentToken();

    /// <summary>
    /// 获取当前的已词法分析的语法部分种类。
    /// </summary>
    protected SyntaxKind CurrentTokenKind => CurrentToken.Kind;

    /// <summary>
    /// 取得当前的已词法分析的标记。
    /// </summary>
    private SyntaxToken FetchCurrentToken()
    {
        if (_tokenOffset >= _tokenCount)
            AddNewToken();

        if (IsBlending())
            return _blendedTokens[_tokenOffset].Token;
        else
            return _lexedTokens[_tokenOffset];
    }

    /// <summary>
    /// 读取并添加下一个新的标记。
    /// </summary>
    private void AddNewToken()
    {
        if (IsBlending())
        {
            if (_tokenCount == 0)
            {
                if (_currentNode.Token is not null)
                    AddToken(_currentNode);
                else
                    AddToken(_firstBlender.ReadToken(_mode));
            }
            else
                AddToken(_blendedTokens[_tokenCount - 1].Blender.ReadToken(_mode));
        }
        else
            AddLexedToken(lexer.Lex(_mode));
    }

    /// <summary>
    /// 添加指定标记。
    /// </summary>
    /// <param name="tokenResult">要添加的已协调节点。</param>
    [MemberNotNull(nameof(_blendedTokens))]
    private void AddToken(in BlendedNode tokenResult)
    {
        Debug.Assert(tokenResult.Token is not null);
        Debug.Assert(IsBlending());

        // 必要时扩充已协调标记数组的容量。
        if (_tokenCount >= _blendedTokens.Length)
            AddTokenSlot();

        _blendedTokens[_tokenCount] = tokenResult;
        _tokenCount++;
    }

    /// <summary>
    /// 添加指定已词法分析的标记。
    /// </summary>
    /// <param name="token">要添加的已词法分析的标记。</param>
    [MemberNotNull(nameof(_lexedTokens))]
    private void AddLexedToken(SyntaxToken token)
    {
        Debug.Assert(!IsBlending());

        // 必要时扩充已词法分析的标记数组的容量。
        if (_tokenCount >= _lexedTokens.Length)
            AddLexedTokenSlot();

        _lexedTokens[_tokenCount].Value = token;
        _tokenCount++;
    }

    /// <summary>
    /// 扩充已协调标记数组的容量。
    /// </summary>
    [MemberNotNull(nameof(_blendedTokens))]
    private void AddTokenSlot()
    {
        Debug.Assert(IsBlending());

        if (_tokenOffset > (_blendedTokens.Length >> 1) && (_resetStart == -1 || _resetStart > _firstToken))
        {
            var shiftOffset = (_resetStart == -1) ? _tokenOffset : _resetStart - _firstToken;
            var shiftCount = _tokenCount - shiftOffset;
            Debug.Assert(shiftOffset > 0);
            _firstBlender = _blendedTokens[shiftOffset - 1].Blender;
            if (shiftCount > 0)
                Array.Copy(_blendedTokens, shiftOffset, _blendedTokens, 0, shiftCount);

            _firstToken += shiftOffset;
            _tokenCount -= shiftOffset;
            _tokenOffset -= shiftOffset;
        }
        else
        {
            var old = _blendedTokens;
            Array.Resize(ref _blendedTokens, _blendedTokens.Length * 2);
            s_blendedNodesPool.ForgetTrackedObject(old, replacement: _blendedTokens);
        }
    }

    /// <summary>
    /// 扩充已词法分析的标记数组的容量。
    /// </summary>
    [MemberNotNull(nameof(_lexedTokens))]
    private void AddLexedTokenSlot()
    {
        Debug.Assert(!IsBlending());

        if (_tokenOffset > (_lexedTokens.Length >> 1) && (_resetStart == -1 || _resetStart > _firstToken))
        {
            var shiftOffset = (_resetStart == -1) ? _tokenOffset : _resetStart - _firstToken;
            var shiftCount = _tokenCount - shiftOffset;
            Debug.Assert(shiftOffset > 0);
            if (shiftCount > 0)
                Array.Copy(_lexedTokens, shiftOffset, _lexedTokens, 0, shiftCount);

            _firstToken += shiftOffset;
            _tokenCount -= shiftOffset;
            _tokenOffset -= shiftOffset;
        }
        else
        {
            var old = new ArrayElement<SyntaxToken>[_lexedTokens.Length * 2];
            Array.Copy(_lexedTokens, old, _lexedTokens.Length);
            _lexedTokens = old;
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
        if (n == 0) return CurrentToken;

        // 补充不足的标记。
        while (_tokenOffset + n >= _tokenCount)
            AddNewToken();

        if (IsBlending())
            return _blendedTokens[_tokenOffset + n].Token;
        else
            return _lexedTokens[_tokenOffset + n];
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标记。
    /// </summary>
    /// <returns>当前的已词法分析的语法标记。</returns>
    protected SyntaxToken EatToken()
    {
        var token = CurrentToken;
        MoveToNextToken();
        return token;
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标记，这个语法标记必须符合指定语法部分种类。
    /// </summary>
    /// <param name="kind">要返回的语法标记须符合的语法部分种类，枚举值必须标记语法标记。</param>
    /// <returns>若当前的已词法分析的语法标记符合指定语法部分种类，则返回这个语法标记；否则返回表示缺失标记的语法标记，并报告错误。</returns>
    protected SyntaxToken EatToken(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));

        var token = CurrentToken;
        if (token.Kind == kind)
        {
            MoveToNextToken();
            return token;
        }
        else
            return CreateMissingToken(kind, token.Kind, reportError: true);
    }

    /// <param name="reportError">是否报告错误。</param>
    /// <returns>若当前的已词法分析的语法标记符合指定语法部分种类，则返回这个语法标记；否则返回表示缺失标记的语法标记。</returns>
    /// <inheritdoc cref="EatToken(SyntaxKind)"/>
    protected SyntaxToken EatToken(SyntaxKind kind, bool reportError)
    {
        if (reportError) return EatToken(kind);

        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));

        if (CurrentToken.Kind == kind)
            return EatToken();
        else
            return ThisInternalSyntaxFactory.MissingToken(kind);
    }

    /// <param name="code">要报告的错误码。</param>
    /// <param name="reportError">是否报告错误。</param>
    /// <returns>若当前的已词法分析的语法标记符合指定语法部分种类，则返回这个语法标记；否则返回表示缺失标记的语法标记。</returns>
    /// <inheritdoc cref="EatToken(SyntaxKind)"/>
    protected SyntaxToken EatToken(SyntaxKind kind, ErrorCode code, bool reportError = true)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));
        if (CurrentToken.Kind == kind)
            return EatToken();
        else
            return CreateMissingToken(kind, code, reportError);
    }

    /// <summary>
    /// 尝试接受并返回当前的已词法分析的语法标记，这个语法标记必须符合指定语法部分种类。
    /// </summary>
    /// <returns>若当前的已词法分析的语法标记符合指定语法部分种类，则返回这个语法标记；否则返回<see langword="null"/>。</returns>
    /// <inheritdoc cref="EatToken(SyntaxKind)"/>
    protected SyntaxToken? TryEatToken(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));

        return CurrentToken.Kind == kind ? EatToken() : null;
    }

    /// <summary>
    /// 移动到下一个标记。
    /// </summary>
    private void MoveToNextToken()
    {
        // 设置上一个标记的后方琐碎内容。
        prevTokenTrailingTrivia = CurrentToken.GetTrailingTrivia();

        // 初始化部分变量。
        _currentToken = null;
        if (IsBlending())
            _currentNode = default;

        // 向后移动一个位置。
        _tokenOffset++;
    }

    /// <summary>
    /// 强制使得当前标记为文件结束标记。
    /// </summary>
    protected void ForceEndOfFile() =>
        _currentToken = ThisInternalSyntaxFactory.Token(SyntaxKind.EndOfFileToken);

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标记，若这个语法标记不符合指定语法部分种类，则跳过并替换为正确的语法标记。
    /// </summary>
    /// <param name="expected">要返回的语法标记应符合的语法部分种类，枚举值必须标记语法标记。</param>
    /// <returns>若当前的已词法分析的语法标记符合指定语法部分种类，则返回这个语法标记；否则返回一个结尾跳过语法，使用表示缺失标记的语法标记替换这个语法标记，并报告错误。</returns>
    protected SyntaxToken EatTokenAsKind(SyntaxKind expected)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(expected, Options.LanguageVersion));

        var token = CurrentToken;
        if (token.Kind == expected)
        {
            MoveToNextToken();
            return token;
        }

        var replacement = CreateMissingToken(expected, token.Kind, reportError: true);
        return AddTrailingSkippedSyntax(replacement, EatToken());
    }

    /// <summary>
    /// 创建一个表示缺少指定标记类型的语法标记。
    /// </summary>
    /// <param name="expected">期望的标记类型。</param>
    /// <param name="actual">实际的标记类型。</param>
    /// <param name="reportError">是否报告错误。</param>
    private SyntaxToken CreateMissingToken(SyntaxKind expected, SyntaxKind actual, bool reportError)
    {
        var token = ThisInternalSyntaxFactory.MissingToken(expected);
        if (reportError)
            token = WithAdditionalDiagnostics(token, GetExpectedTokenError(expected, actual));

        return token;
    }

    /// <summary>
    /// 创建一个表示缺少指定标记类型的语法标记。
    /// </summary>
    /// <param name="expected">期望的标记类型。</param>
    /// <param name="code">要报告的错误码。</param>
    /// <param name="reportError">是否报告错误。</param>
    private SyntaxToken CreateMissingToken(SyntaxKind expected, ErrorCode code, bool reportError)
    {
        var token = ThisInternalSyntaxFactory.MissingToken(expected);
        if (reportError)
            token = AddError(token, code);

        return token;
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标记，若这个语法标记不符合指定语法部分种类，则对其报告错误。
    /// </summary>
    /// <param name="kind">要返回的语法标记须符合的语法部分种类，枚举值必须标记语法标记。</param>
    /// <returns>返回当前的已词法分析的语法标记，若这个语法标记不符合指定语法部分种类，则报告错误。</returns>
    protected SyntaxToken EatTokenWithPrejudice(SyntaxKind kind)
    {
        var token = CurrentToken;
        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));
        if (token.Kind != kind)
            token = WithAdditionalDiagnostics(token, GetExpectedTokenError(kind, token.Kind));

        MoveToNextToken();
        return token;
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标记，并对其报告错误。
    /// </summary>
    /// <param name="code">要报告的错误码。</param>
    /// <param name="args">诊断信息的参数。</param>
    /// <returns>返回当前的已词法分析的语法标记，并报告指定错误码和诊断消参数的错误。</returns>
    protected SyntaxToken EatTokenWithPrejudice(ErrorCode code, params object[] args)
    {
        var token = EatToken();
        token = WithAdditionalDiagnostics(token, MakeError(token.GetLeadingTriviaWidth(), token.Width, code, args));
        return token;
    }

    /// <summary>
    /// 语法解析器接受当前的已词法分析的语法标记，转化为关键字语法标记并且返回。
    /// </summary>
    /// <returns>转化当前的已词法分析的语法标记为关键字语法标记并且返回。</returns>
    protected SyntaxToken EatContextualToken() => ConvertToKeyword(EatToken());

    /// <summary>
    /// 语法解析器接受当前的已词法分析的语法标记，转化为关键字语法标记并且返回，这个语法标记的上下文种类必须符合指定语法部分种类。
    /// </summary>
    /// <param name="kind">要返回的语法标记须符合的语法部分种类，枚举值必须标记语法标记。</param>
    /// <returns>若当前的已词法分析的语法标记的上下文种类符合指定语法部分种类，则转化这个语法标记为关键字语法标记并且返回；否则返回表示缺失标记的语法标记，并报告错误。</returns>
    protected SyntaxToken EatContextualToken(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));

        var contextualKind = CurrentToken.ContextualKind;
        if (contextualKind == kind)
            return EatContextualToken();
        else
            return CreateMissingToken(kind, contextualKind, reportError: true);
    }

    /// <summary>
    /// 语法解析器接受并且返回当前的已词法分析的语法标记，这个语法标记的上下文种类必须符合指定语法部分种类。
    /// </summary>
    /// <param name="kind">要返回的语法标记须符合的语法部分种类，枚举值必须标记语法标记。</param>
    /// <param name="reportError">是否报告错误。</param>
    /// <returns>若当前的已词法分析的语法标记的上下文种类符合指定语法部分种类，则返回这个语法标记；否则返回表示缺失标记的语法标记，并报告错误。</returns>
    protected SyntaxToken EatContextualToken(SyntaxKind kind, bool reportError)
    {
        if (reportError) return EatContextualToken(kind);

        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));

        var contextualKind = CurrentToken.ContextualKind;
        if (contextualKind == kind)
            return EatContextualToken();
        else
            return ThisInternalSyntaxFactory.MissingToken(kind);
    }

    /// <param name="code">要报告的错误码。</param>
    /// <returns>若当前的已词法分析的语法标记的上下文种类符合指定语法部分种类，则返回这个语法标记；否则返回表示缺失标记的语法标记，并报告指定错误码的错误。</returns>
    /// <inheritdoc cref="EatContextualToken(SyntaxKind, bool)"/>
    protected SyntaxToken EatContextualToken(SyntaxKind kind, ErrorCode code, bool reportError = true)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind, Options.LanguageVersion));

        if (CurrentToken.ContextualKind == kind)
            return EatContextualToken();
        else
            return CreateMissingToken(kind, code, reportError);
    }

    /// <summary>
    /// 获取一个诊断信息，表示未得到期望的标记。
    /// </summary>
    /// <param name="expected">期望的标记类型。</param>
    /// <param name="actual">实际的标记类型。</param>
    /// <returns>表示未得到期望的标记的诊断信息。</returns>
    protected virtual SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual)
    {
        // 获取缺失标记的诊断文本范围。
        GetDiagnosticSpanForMissingToken(out var offset, out var width);

        return GetExpectedTokenError(expected, actual, offset, width);
    }

    /// <param name="offset">实际的标记的文本范围的偏移量。</param>
    /// <param name="width">实际的标记的文本范围的宽度。</param>
    /// <inheritdoc cref="GetExpectedTokenError(SyntaxKind, SyntaxKind)"/>
    protected virtual partial SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual, int offset, int width);

    /// <summary>
    /// 获取一个错误码，对应未得到期望的标记。
    /// </summary>
    /// <returns>对应未得到期望的标记的错误码。</returns>
    /// <inheritdoc cref="GetExpectedTokenError(SyntaxKind, SyntaxKind)"/>
    protected static partial ErrorCode GetExpectedTokenErrorCode(SyntaxKind expected, SyntaxKind actual, ThisParseOptions options);

    /// <summary>
    /// 获取缺失标记的诊断文本范围。
    /// </summary>
    /// <param name="offset">文本范围的偏移量。</param>
    /// <param name="width">文本范围的宽度。</param>
    /// <remarks>
    /// 若上一个标记后方跟着的琐碎内容中包含行尾琐碎内容，则缺失标记的诊断位置将置于包含上一个标记的行尾，并且宽度为零；
    /// 否则诊断的偏移量和宽度与当前标记的位置和宽度一致。
    /// </remarks>
    protected void GetDiagnosticSpanForMissingToken(out int offset, out int width)
    {
        // 若上一个标记后方跟着的琐碎内容中包含行尾琐碎内容，则缺失标记的诊断位置将置于包含上一个标记的行尾，并且宽度为零。
        var trivia = prevTokenTrailingTrivia;
        if (trivia is not null)
        {
            var triviaList = new SyntaxList<ThisInternalSyntaxNode>(trivia);
            var prevTokenHasEndOfLineTrivia = triviaList.Any((int)SyntaxKind.EndOfLineTrivia);
            if (prevTokenHasEndOfLineTrivia)
            { // 包含行尾琐碎内容。
                offset = -trivia.FullWidth; // 向前跳过上一个标记后方跟着的琐碎内容的宽度。
                width = 0;
                return;
            }
        }

        // 否则诊断的偏移量和宽度与当前标记的位置和宽度一致。
        var token = CurrentToken;
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
        var existingLength = existingDiagnostics.Length;
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
        AddError(node, code, []);

    /// <param name="args">诊断信息的参数。</param>
    /// <inheritdoc cref="AddError{TNode}(TNode, ErrorCode)"/>
    protected TNode AddError<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : GreenNode
    {
        // 不是缺失节点，直接添加附加诊断信息。
        if (!node.IsMissing)
            return WithAdditionalDiagnostics(node, MakeError(node, code, args));

        // 对指点范围添加诊断错误信息。
        int offset, width;

        if (node is SyntaxToken token && token.ContainsSkippedText)
        { // 对连续的被跳过文本添加。
            offset = token.GetLeadingTriviaWidth();
            Debug.Assert(offset == 0);

            width = 0;
            var seenSkipped = false;
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
        else // 对缺失标记的文本添加。
            GetDiagnosticSpanForMissingToken(out offset, out width);

        return WithAdditionalDiagnostics(node, MakeError(offset, width, code, args));
    }

    /// <param name="offset">文本范围的偏移量。</param>
    /// <param name="length">文本范围的长度。</param>
    /// <inheritdoc cref="AddError{TNode}(TNode, ErrorCode, object[])"/>
    protected TNode AddError<TNode>(TNode node, int offset, int length, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode =>
        WithAdditionalDiagnostics(node, MakeError(offset, length, code, args));

    /// <summary>
    /// 向指定根节点下的指定语法节点添加诊断错误信息。
    /// </summary>
    /// <param name="node">包含<paramref name="location"/>的节点。</param>
    /// <param name="location">要添加附加诊断信息的语法节点，此节点在<paramref name="node"/>下。</param>
    /// <inheritdoc cref="AddError{TNode}(TNode, ErrorCode, object[])"/>
    protected TNode AddError<TNode>(TNode node, ThisInternalSyntaxNode location, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode
    {
        // 查找偏移量。
        FindOffset(node, location, out var offset);
        return WithAdditionalDiagnostics(node, MakeError(offset, location.Width, code, args));
    }

    /// <summary>
    /// 向语法节点的第一个标记添加诊断错误信息。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="node">要添加附加诊断信息的标记所在的语法节点。</param>
    /// <param name="code">要添加的错误码。</param>
    /// <returns>其第一个标记添加诊断错误信息后的<paramref name="node"/>。</returns>
    protected TNode AddErrorToFirstToken<TNode>(TNode node, ErrorCode code) where TNode : ThisInternalSyntaxNode
    {
        GetOffsetAndWidthForFirstToken(node, out var offset, out var width);
        return WithAdditionalDiagnostics(node, MakeError(offset, width, code));
    }

    /// <param name="args">诊断信息的参数。</param>
    /// <inheritdoc cref="AddErrorToFirstToken{TNode}(TNode, ErrorCode)"/>
    protected TNode AddErrorToFirstToken<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode
    {
        GetOffsetAndWidthForFirstToken(node, out var offset, out var width);
        return WithAdditionalDiagnostics(node, MakeError(offset, width, code, args));
    }

    /// <summary>
    /// 向语法节点的最后一个标记添加诊断错误信息。
    /// </summary>
    /// <returns>其最后一个标记添加诊断错误信息后的<paramref name="node"/>。</returns>
    /// <inheritdoc cref="AddErrorToFirstToken{TNode}(TNode, ErrorCode)"/>
    protected TNode AddErrorToLastToken<TNode>(TNode node, ErrorCode code) where TNode : ThisInternalSyntaxNode
    {
        GetOffsetAndWidthForLastToken(node, out var offset, out var width);
        return WithAdditionalDiagnostics(node, MakeError(offset, width, code));
    }

    /// <inheritdoc cref="AddErrorToLastToken{TNode}(TNode, ErrorCode)"/>
    /// <inheritdoc cref="AddErrorToFirstToken{TNode}(TNode, ErrorCode, object[])"/>
    protected TNode AddErrorToLastToken<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : ThisInternalSyntaxNode
    {
        GetOffsetAndWidthForLastToken(node, out var offset, out var width);
        return WithAdditionalDiagnostics(node, MakeError(offset, width, code, args));
    }

    /// <summary>
    /// 获取指定语法节点的第一个标记的偏移量和宽度。
    /// </summary>
    private static void GetOffsetAndWidthForFirstToken<TNode>(TNode node, out int offset, out int width) where TNode : ThisInternalSyntaxNode
    {
        var firstToken = node.GetFirstToken();
        Debug.Assert(firstToken is not null);

        offset = firstToken.GetLeadingTriviaWidth();
        width = firstToken.Width;
    }

    /// <summary>
    /// 获取指定语法节点的最后一个标记的偏移量和宽度。
    /// </summary>
    private static void GetOffsetAndWidthForLastToken<TNode>(TNode node, out int offset, out int width) where TNode : ThisInternalSyntaxNode
    {
        var lastToken = node.GetLastNonmissingToken();

        offset = node.FullWidth; // 向后移动到节点尾部。
        if (lastToken is null) // 当所有节点均缺失时。
            width = 0;
        else
        {
            offset -= lastToken.FullWidth; // 向前回退到标记的头部。
            offset += lastToken.GetLeadingTriviaWidth(); // 向后移动到标记前方琐碎内容之后。
            width = lastToken.Width;
        }
    }

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定偏移量、宽度和错误码。
    /// </summary>
    /// <returns>语法诊断错误消息的实例。</returns>
    /// <inheritdoc cref="SyntaxDiagnosticInfo(int, int, ErrorCode)"/>
    protected static SyntaxDiagnosticInfo MakeError(int offset, int width, ErrorCode code) => new(offset, width, code);

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定偏移量、宽度、错误码和消息参数。
    /// </summary>
    /// <inheritdoc cref="MakeError(int, int, ErrorCode)"/>
    /// <inheritdoc cref="SyntaxDiagnosticInfo(int, int, ErrorCode)"/>
    protected static SyntaxDiagnosticInfo MakeError(int offset, int width, ErrorCode code, params object[] args) => new(offset, width, code, args);

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定绿树节点的偏移量和宽度，以及指定的错误码和消息参数。
    /// </summary>
    /// <param name="node">这个绿树节点的范围即创建的实例表示的范围。</param>
    /// <inheritdoc cref="MakeError(int, int, ErrorCode)"/>
    protected static SyntaxDiagnosticInfo MakeError(GreenNode node, ErrorCode code, params object[] args) => new(node.GetLeadingTriviaWidth(), node.Width, code, args);

    /// <summary>
    /// 创建语法诊断错误消息的实例。实例指定错误码和消息参数。
    /// </summary>
    /// <inheritdoc cref="MakeError(int, int, ErrorCode, object[])"/>
    protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args) => new(code, args);

    /// <summary>
    /// 给指定的语法列表构造器添加指定的前方的跳过语法。
    /// </summary>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddLeadingSkippedSyntax(SyntaxListBuilder list, GreenNode skippedSyntax) =>
        list[0] = AddLeadingSkippedSyntax((ThisInternalSyntaxNode)list[0]!, skippedSyntax);

    /// <summary>
    /// 给指定的语法列表构造器添加指定的前方的跳过语法。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddLeadingSkippedSyntax<TNode>(SyntaxListBuilder<TNode> list, GreenNode skippedSyntax) where TNode : ThisInternalSyntaxNode =>
        list[0] = AddTrailingSkippedSyntax(list[0]!, skippedSyntax);

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
        var newToken = AddSkippedSyntax(oldToken, skippedSyntax, trailing: false);
        return SyntaxFirstTokenReplacer.Replace(node, oldToken, newToken, skippedSyntax.FullWidth);
    }

    /// <summary>
    /// 给指定的语法列表构造器添加指定的后方的跳过语法。
    /// </summary>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddTrailingSkippedSyntax(SyntaxListBuilder list, GreenNode skippedSyntax) =>
        list[^1] = AddTrailingSkippedSyntax((ThisInternalSyntaxNode)list[^1]!, skippedSyntax);

    /// <summary>
    /// 给指定的语法列表构造器添加指定的后方的跳过语法。
    /// </summary>
    /// <typeparam name="TNode">语法节点的类型。</typeparam>
    /// <param name="list">要添加<paramref name="skippedSyntax"/>的语法列表构造器。</param>
    /// <param name="skippedSyntax">要添加的跳过语法。</param>
    /// <returns>添加<paramref name="skippedSyntax"/>后的<paramref name="list"/>。</returns>
    protected void AddTrailingSkippedSyntax<TNode>(SyntaxListBuilder<TNode> list, GreenNode skippedSyntax) where TNode : ThisInternalSyntaxNode =>
        list[^1] = AddTrailingSkippedSyntax(list[^1]!, skippedSyntax);

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
        var newToken = AddSkippedSyntax(oldToken, skippedSyntax, trailing: true);
        return SyntaxLastTokenReplacer.Replace(node, oldToken, newToken);
    }

    /// <summary>
    /// 给指定的语法标记添加指定的表示跳过语法的绿树节点。
    /// </summary>
    /// <param name="target">要添加<paramref name="skippedSyntax"/>的语法标记。</param>
    /// <param name="skippedSyntax">要添加的表示跳过语法的绿树节点。</param>
    /// <param name="trailing">若为<see langword="true"/>，则将<paramref name="skippedSyntax"/>添加到后方语法琐碎内容中；否则添加到前方语法琐碎内容中。</param>
    /// <returns></returns>
    internal SyntaxToken AddSkippedSyntax(SyntaxToken target, GreenNode skippedSyntax, bool trailing)
    {
        var builder = new SyntaxListBuilder(4);

        SyntaxDiagnosticInfo? diagnostic = null;

        var diagnosticOffset = 0;

        var currentOffset = 0;
        foreach (var node in skippedSyntax.EnumerateNodes())
        {
            if (node is SyntaxToken token)
            {
                builder.Add(token.GetLeadingTrivia());

                if (token.Width > 0)
                {
                    var tk = token.TokenWithLeadingTrivia(null).TokenWithTrailingTrivia(null);

                    var leadingWidth = token.GetLeadingTriviaWidth();
                    if (leadingWidth > 0)
                    {
                        var tokenDiagnostics = tk.GetDiagnostics();
                        for (var i = 0; i < tokenDiagnostics.Length; i++)
                        {
                            var d = (SyntaxDiagnosticInfo)tokenDiagnostics[i];
                            tokenDiagnostics[i] = new SyntaxDiagnosticInfo(d.Offset - leadingWidth, d.Width, (ErrorCode)d.Code, d.Arguments);
                        }
                    }

                    builder.Add(ThisInternalSyntaxFactory.SkippedTokensTrivia(tk));
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

        var triviaWidth = currentOffset;
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
                for (var i = 0; i < targetDiagnostics.Length; i++)
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
            var newOffset = triviaOffset + diagnosticOffset + diagnostic.Offset;

            target = WithAdditionalDiagnostics(target, new SyntaxDiagnosticInfo(newOffset, diagnostic.Width, (ErrorCode)diagnostic.Code, diagnostic.Arguments)
            );
        }

        return target;
    }

    protected void SkipTokens(
        SyntaxListBuilder<SyntaxToken> builder,
        Func<SyntaxToken, bool> predicate,
        ThisInternalSyntaxVisitor<SyntaxToken>? visitor = null)
    {
        while (predicate(CurrentToken))
        {
            var token = EatToken();
            builder.Add(visitor is null ? token : visitor.Visit(token));
        }
    }

    protected void SkipTokensAndNodes(
        SyntaxListBuilder<ThisInternalSyntaxNode> builder,
        Func<SyntaxToken, bool> tokenPredicate,
        Func<bool> nodePredicate,
        Func<ThisInternalSyntaxNode> nodeProvider,
        ThisInternalSyntaxVisitor<ThisInternalSyntaxNode>? visitor = null)
    {
        while (true)
        {
            if (nodePredicate())
            {
                var node = nodeProvider();
                builder.Add(visitor is null ? node : visitor.Visit(node));
            }
            else if (tokenPredicate(CurrentToken))
            {
                var token = EatToken();
                builder.Add(visitor is null ? token : visitor.Visit(token));
            }
            else break;
        }
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

        var currentOffset = 0;
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
            else if (FindOffset(child, location, out offset))
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
    /// 将非关键字标记转换为包含相同信息的关键字标记。
    /// </summary>
    /// <param name="token">要转化的非关键字标记。</param>
    /// <returns>一个关键字标记，包含非关键字标记<paramref name="token"/>的所有信息。</returns>
    protected static SyntaxToken ConvertToKeyword(SyntaxToken token)
    {
        if (token.Kind != token.ContextualKind)
        {
            // 分两种情况：是否为缺失标记。
            var kw = token.IsMissing
                ? ThisInternalSyntaxFactory.MissingToken(
                    token.LeadingTrivia.Node,
                    token.ContextualKind,
                    token.TrailingTrivia.Node
                )
                : ThisInternalSyntaxFactory.Token(
                    token.LeadingTrivia.Node,
                    token.ContextualKind,
                    token.TrailingTrivia.Node
                );
            var d = token.GetDiagnostics();
            // 如果有诊断信息，则附加到标记上。
            if (d is not null && d.Length > 0)
                kw = kw.WithDiagnosticsGreen(d);

            return kw;
        }

        return token;
    }

    /// <summary>
    /// 将非标识符标记转换为包含相同信息的标识符标记。
    /// </summary>
    /// <param name="token">要转化的非标识符标记。</param>
    /// <returns>一个标识符标记，包含非标识符标记<paramref name="token"/>的所有信息。</returns>
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
    protected bool IsFeatureEnabled(MessageID feature) => Options.IsFeatureEnabled(feature);

    /// <summary>获取当前解析的标记的位置。</summary>
    private int CurrentTokenPosition => _firstToken + _tokenOffset;

    /// <summary>
    /// 当解析进入循环流程中时，为防止因意外的错误导致解析器无法向后分析而出现死循环，此方法应作为保险措施而非实现功能的方式。
    /// </summary>
    /// <param name="lastTokenPosition">上一次更新的标记位置。</param>
    /// <param name="assertIfFalse">当解析器无法向后分析时是否使用断言中断。</param>
    /// <returns>若为<see langword="true"/>时，表示解析器正常向后分析；若为<see langword="false"/>时，表示解析器无法向后分析。</returns>
    protected bool IsMakingProgress(ref int lastTokenPosition, bool assertIfFalse = true)
    {
        var pos = CurrentTokenPosition;
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
            _blendedTokens = null!;
            if (blendedTokens.Length < 4096)
            {
                Array.Clear(blendedTokens, 0, blendedTokens.Length);
                s_blendedNodesPool.Free(blendedTokens);
            }
            else
            {
                s_blendedNodesPool.ForgetTrackedObject(blendedTokens);
            }
        }
    }
    #endregion
}
