// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class NameValueFieldSyntax
{
    public override ExpressionSyntax? FieldKey => FieldName;

    internal override FieldSyntax WithFieldKeyCore(ExpressionSyntax? fieldKey)
    {
        if (fieldKey is null)
            return SyntaxFactory.ItemField(FieldValue);
        else if (fieldKey is IdentifierNameSyntax fieldName)
            return WithFieldName(fieldName);
        else
            return SyntaxFactory.KeyValueField(fieldKey, FieldValue);
    }
}
