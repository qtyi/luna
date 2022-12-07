// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class SyntaxTrivia
{
    public int WhiteSpaceIndent => this.IsWhiteSpace ? 0 :
        this.Text.Sum(SyntaxFacts.WhiteSpaceIndent);
}
