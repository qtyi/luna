// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators.Syntax;

public sealed partial class SyntaxTriviaInfo : SyntaxNodeOrTokenOrTriviaInfo
{
    public SyntaxTrivia Trivia { get; }
    public SyntaxNodeInfo? Structure { get; }

    [ObservableProperty]
    private TriviaClassification _triviaClassification;
    [ObservableProperty]
    private CommentTriviaClassification _commentTriviaClassification;
    [ObservableProperty]
    private StructuredTriviaClassification _structuredTriviaClassification;

    public override bool IsNode => false;
    public override bool IsToken => false;
    public override bool IsTrivia => true;

    [MemberNotNullWhen(true, nameof(Structure))]
    public bool HasStructure => this.Structure is not null;

    private SyntaxTriviaInfo(SyntaxTrivia trivia)
    {
        this.Trivia = trivia;
        this.Structure = trivia.HasStructure ? (SyntaxNodeInfo)trivia.GetStructure()! : null;
    }

    public static implicit operator SyntaxTriviaInfo(SyntaxTrivia trivia) => new(trivia);
}

public enum TriviaClassification : byte
{
    Whitespace,
    EndOfLine,
    Comment,
    Documentation,
    Structured
}

public enum CommentTriviaClassification : byte
{
    SingleLine,
    MultiLine
}

public enum StructuredTriviaClassification : byte
{
    Directive
}
