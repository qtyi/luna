// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

internal partial class MoonScriptSyntaxNode
{
    public virtual object? Value => this.Kind switch
    {
        SyntaxKind.TrueKeyword => Boxes.BoxedTrue,
        SyntaxKind.FalseKeyword => Boxes.BoxedFalse,
        SyntaxKind.NilKeyword => null,
        _ => this.KindText
    };

    internal static partial NodeFlags SetFactoryContext(NodeFlags flags, SyntaxFactoryContext context) => flags;

    public override partial Microsoft.CodeAnalysis.SyntaxToken CreateSeparator<TNode>(SyntaxNode element) => MoonScript.SyntaxFactory.Token(SyntaxKind.CommaToken);

    public override partial bool IsTriviaWithEndOfLine() =>
        this.Kind switch
        {
            SyntaxKind.EndOfLineTrivia or
            SyntaxKind.SingleLineCommentTrivia => true,
            _ => false
        };
}
