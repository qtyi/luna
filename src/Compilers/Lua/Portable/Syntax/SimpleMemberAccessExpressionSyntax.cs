// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class SimpleMemberAccessExpressionSyntax
{
    public override ExpressionSyntax Member => this.MemberName;

    internal override MemberAccessExpressionSyntax WithMemberCore(ExpressionSyntax member)
    {
        if (member is IdentifierNameSyntax memberName)
            return this.WithMemberName(memberName);
        else
            return SyntaxFactory.IndexMemberAccessExpression(this.Self, member);
    }
}
