// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Qtyi.CodeAnalysis.MoonScript.Syntax;

namespace Qtyi.CodeAnalysis.MoonScript;

partial class SyntaxFactory
{
    /// <summary>
    /// Creates an IdentifierNameSyntax node.
    /// </summary>
    /// <param name="name">The identifier name.</param>
    public static IdentifierNameSyntax IdentifierName(string name)
    {
        return IdentifierName(SyntaxFactory.Identifier(name));
    }
}
