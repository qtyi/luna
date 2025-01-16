// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class GenericNameSyntax
{
    public override int Arity => TypeArgumentList.TypeArguments.Count;

    internal override SimpleNameSyntax GetUnqualifiedName() => this;

    internal override string ErrorDisplayName()
    {
        var pb = PooledStringBuilder.GetInstance();
        pb.Builder.Append(Identifier.ValueText).Append('<').Append(',', Arity - 1).Append('>');
        return pb.ToStringAndFree();
    }
}
