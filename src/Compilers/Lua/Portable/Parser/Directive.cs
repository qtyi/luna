// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial struct Directive
{
    internal partial bool BranchTaken => _node is BranchingDirectiveTriviaSyntax branching && branching.BranchTaken;

    internal partial string? GetIdentifier() => Kind switch
    {
        _ => null
    };

    private partial bool IncrementallyEquivalentWithoutActiveCheck(Directive other) => Kind switch
    {
        SyntaxKind.IfDirectiveTrivia or
        SyntaxKind.IfNotDirectiveTrivia or
        SyntaxKind.ElseDirectiveTrivia or
        SyntaxKind.EndDirectiveTrivia => this.BranchTaken == other.BranchTaken,

        _ => true
    };
}
