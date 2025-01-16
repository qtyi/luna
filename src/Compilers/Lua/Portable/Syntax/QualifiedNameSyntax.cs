// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class QualifiedNameSyntax
{
    public override int Arity => 0;

    internal override SimpleNameSyntax GetUnqualifiedName() => Right;

    internal override string ErrorDisplayName() => Left.ErrorDisplayName() + "." + Right.ErrorDisplayName();
}
