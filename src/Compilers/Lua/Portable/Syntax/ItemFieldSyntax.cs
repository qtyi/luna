// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class ItemFieldSyntax
{
    public override ExpressionSyntax? FieldKey => null;

    internal override FieldSyntax WithFieldKeyCore(ExpressionSyntax? fieldKey)
    {
        if (fieldKey is null)
            return SyntaxFactory.ItemField(this.FieldValue);
        else
            return SyntaxFactory.KeyValueField(fieldKey, this.FieldValue);
    }
}
