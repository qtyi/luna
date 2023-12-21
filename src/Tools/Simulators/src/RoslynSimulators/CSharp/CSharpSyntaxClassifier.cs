// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Simulators.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Luna.Compilers.Simulators.CSharp;

[Export(LanguageNames.CSharp)]
public sealed class CSharpSyntaxClassifier : AbstractSyntaxClassifier
{
    public override void Classify(SyntaxClassifierExecutionContext context)
    {
        var root = context.SyntaxInfoProvider.Root;
        new CSharpSyntaxInfoVisitor().Visit(root);
    }

    private sealed class CSharpSyntaxInfoVisitor : SyntaxInfoVisitor
    {
        public CSharpSyntaxInfoVisitor() : base(true) { }

        private bool IsPartOfDocumentationTrivia(SyntaxNode node)
        {
            for (var n = node; n != null; n = n.Parent)
            {
                if (n.IsStructuredTrivia)
                    return SyntaxFacts.IsDocumentationCommentTrivia(n.ParentTrivia.Kind());
            }

            return false;
        }

        protected override void VisitToken(SyntaxTokenInfo info)
        {
            var kind = info.Token.Kind();
            if (this.IsPartOfDocumentationTrivia(info.Token.Parent))
            {
                info.TokenClassification = TokenClassification.Documentation;
            }
            else if (SyntaxFacts.IsKeywordKind(kind))
            {
                info.TokenClassification = TokenClassification.Keyword;
            }
            else if (kind == SyntaxKind.IdentifierToken)
            {
                info.TokenClassification = TokenClassification.Identifier;
                info.IdentifierTokenClassification = info.Token.IsContextualKeyword() ? IdentifierTokenClassification.ContextualKeyword : IdentifierTokenClassification.Normal;
            }
            else if (kind == SyntaxKind.TildeToken ||
                     kind == SyntaxKind.ExclamationToken ||
                     kind == SyntaxKind.PercentToken ||
                     kind == SyntaxKind.CaretToken ||
                     kind == SyntaxKind.AmpersandToken ||
                     kind == SyntaxKind.AsteriskToken ||
                     kind == SyntaxKind.MinusToken
                     )
            {
                info.TokenClassification = TokenClassification.Operator;
            }
            else if (kind == SyntaxKind.DollarToken)
            {
                info.TokenClassification = TokenClassification.Punctuation;
            }
            else if (SyntaxFacts.GetLiteralExpression(kind) != SyntaxKind.None)
            {
                info.TokenClassification = TokenClassification.Literal;
                if (kind == SyntaxKind.NumericLiteralToken)
                    info.LiteralTokenClassification = LiteralTokenClassification.Numeric;
                else if (kind == SyntaxKind.CharacterLiteralToken ||
                         kind == SyntaxKind.StringLiteralToken ||
                         kind == SyntaxKind.SingleLineRawStringLiteralToken ||
                         kind == SyntaxKind.MultiLineRawStringLiteralToken ||
                         kind == SyntaxKind.Utf8StringLiteralToken ||
                         kind == SyntaxKind.Utf8SingleLineRawStringLiteralToken ||
                         kind == SyntaxKind.Utf8MultiLineRawStringLiteralToken)
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
            else if (SyntaxFacts.IsDocumentationCommentTrivia(kind) || this.IsPartOfDocumentationTrivia(info.Trivia.Token.Parent))
            {
                info.TriviaClassification = TriviaClassification.Documentation;
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
