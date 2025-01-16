﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

internal partial class MoonScriptSyntaxNode
{
    protected partial void SetFactoryContext(SyntaxFactoryContext context)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }

    public sealed override partial Microsoft.CodeAnalysis.SyntaxToken CreateSeparator(SyntaxNode element)
        => MoonScript.SyntaxFactory.Token(SyntaxKind.CommaToken);

    public override partial bool IsTriviaWithEndOfLine() =>
        Kind switch
        {
            SyntaxKind.EndOfLineTrivia or
            SyntaxKind.SingleLineCommentTrivia => true,
            _ => false
        };
}
