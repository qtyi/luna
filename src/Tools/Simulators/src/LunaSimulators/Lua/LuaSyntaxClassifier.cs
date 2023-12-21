// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Simulators.Syntax;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua;
using LanguageNames = Qtyi.CodeAnalysis.LanguageNames;

namespace Luna.Compilers.Simulators.Lua;

[Export(LanguageNames.Lua)]
public sealed class LuaSyntaxClassifier : AbstractSyntaxClassifier
{
    public override void Classify(SyntaxClassifierExecutionContext context)
    {
        var root = context.SyntaxInfoProvider.Root;
        new LuaSyntaxInfoVisitor().Visit(root);
    }

    private sealed class LuaSyntaxInfoVisitor : SyntaxInfoVisitor
    {
        public LuaSyntaxInfoVisitor() : base(true) { }

        protected override void VisitToken(SyntaxTokenInfo info)
        {
            var kind = info.Token.Kind();
            if (SyntaxFacts.IsKeywordKind(kind) ||
                kind == SyntaxKind.DotDotDotToken)
            {
                info.TokenClassification = TokenClassification.Keyword;
            }
            else if (kind == SyntaxKind.IdentifierToken)
            {
                info.TokenClassification = TokenClassification.Identifier;
                info.IdentifierTokenClassification = info.Token.IsContextualKeyword() ? IdentifierTokenClassification.ContextualKeyword : IdentifierTokenClassification.Normal;
            }
            else if (kind == SyntaxKind.PlusToken ||
                     kind == SyntaxKind.MinusToken ||
                     kind == SyntaxKind.AsteriskToken ||
                     kind == SyntaxKind.SlashToken ||
                     kind == SyntaxKind.CaretToken ||
                     kind == SyntaxKind.PersentToken ||
                     kind == SyntaxKind.HashToken ||
                     kind == SyntaxKind.AmpersandToken ||
                     kind == SyntaxKind.TildeToken ||
                     kind == SyntaxKind.BarToken ||
                     kind == SyntaxKind.LessThanToken ||
                     kind == SyntaxKind.GreaterThanToken ||
                     kind == SyntaxKind.EqualsToken ||
                     kind == SyntaxKind.LessThanLessThanToken ||
                     kind == SyntaxKind.LessThanEqualsToken ||
                     kind == SyntaxKind.GreaterThanGreaterThanToken ||
                     kind == SyntaxKind.GreaterThanEqualsToken ||
                     kind == SyntaxKind.SlashSlashToken ||
                     kind == SyntaxKind.EqualsEqualsToken ||
                     kind == SyntaxKind.TildeEqualsToken ||
                     kind == SyntaxKind.DotDotToken
                     )
            {
                info.TokenClassification = TokenClassification.Operator;
            }
            else if (kind == SyntaxKind.OpenParenToken ||
                     kind == SyntaxKind.CloseParenToken ||
                     kind == SyntaxKind.OpenBraceToken ||
                     kind == SyntaxKind.CloseBraceToken ||
                     kind == SyntaxKind.OpenBracketToken ||
                     kind == SyntaxKind.CloseBracketToken ||
                     kind == SyntaxKind.ColonToken ||
                     kind == SyntaxKind.SemicolonToken ||
                     kind == SyntaxKind.CommaToken ||
                     kind == SyntaxKind.DotToken ||
                     kind == SyntaxKind.ColonColonToken)
            {
                info.TokenClassification = TokenClassification.Punctuation;
            }
            else if (SyntaxFacts.GetLiteralExpression(kind) != SyntaxKind.None)
            {
                info.TokenClassification = TokenClassification.Literal;
                if (kind == SyntaxKind.NumericLiteralToken)
                    info.LiteralTokenClassification = LiteralTokenClassification.Numeric;
                else if (kind == SyntaxKind.StringLiteralToken ||
                         kind == SyntaxKind.MultiLineRawStringLiteralToken)
                {
                    info.LiteralTokenClassification = LiteralTokenClassification.String;
                }
            }
            else if (SyntaxFacts.IsPreprocessorDirective(kind))
            {
                info.TokenClassification = TokenClassification.Directive;
            }
            else if (kind == SyntaxKind.EndOfFileToken)
            {
                info.TokenClassification = TokenClassification.EndOfFile;
            }
        }

        protected override void VisitTrivia(SyntaxTriviaInfo info)
        {
            var kind = info.Trivia.Kind();
            if (kind == SyntaxKind.EndOfLineTrivia)
            {
                info.TriviaClassification = TriviaClassification.EndOfLine;
            }
            else if (kind == SyntaxKind.SingleLineCommentTrivia ||
                kind == SyntaxKind.MultiLineCommentTrivia)
            {
                info.TriviaClassification = TriviaClassification.Comment;
                info.CommentTriviaClassification = kind == SyntaxKind.SingleLineCommentTrivia ? CommentTriviaClassification.SingleLine : CommentTriviaClassification.MultiLine;
            }
            else if (info.HasStructure)
            {
                info.TriviaClassification = TriviaClassification.Structured;
            }
            else
            {
                info.TriviaClassification = TriviaClassification.Whitespace;
            }
        }
    }
}
