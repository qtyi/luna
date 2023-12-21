// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators.Syntax;

public sealed partial class SyntaxTokenInfo : SyntaxNodeOrTokenInfo
{
    public SyntaxToken Token { get; }
    public readonly IReadOnlyList<SyntaxTriviaInfo> LeadingTrivia;
    public readonly IReadOnlyList<SyntaxTriviaInfo> TrailingTrivia;

    [ObservableProperty]
    private TokenClassification _tokenClassification;
    [ObservableProperty]
    private IdentifierTokenClassification _identifierTokenClassification;
    [ObservableProperty]
    private LiteralTokenClassification _literalTokenClassification;
    [ObservableProperty]
    private DirectiveTokenClassification _directiveTokenClassification;

    public override bool IsNode => false;
    public override bool IsToken => true;

    private SyntaxTokenInfo(SyntaxToken token)
    {
        this.Token = token;
        this.LeadingTrivia = new SyntaxTriviaInfoList(token.LeadingTrivia);
        this.TrailingTrivia = new SyntaxTriviaInfoList(token.TrailingTrivia);
    }

    public static implicit operator SyntaxTokenInfo(SyntaxToken token) => new(token);
}

public enum TokenClassification : byte
{
    Bad,
    Keyword,
    Identifier,
    Operator,
    Punctuation,
    Literal,
    Directive,
    EndOfFile,
    Documentation
}

public enum IdentifierTokenClassification : byte
{
    Normal,
    ContextualKeyword,
}

public enum LiteralTokenClassification : byte
{
    Numeric,
    String,
}

public enum DirectiveTokenClassification : byte
{
    Leading,
    MessageText,
    Keyword,
    Identifier,
    Operator,
    Punctuation,
    Literal,
    EndOfDirective,
    DisabledText
}
