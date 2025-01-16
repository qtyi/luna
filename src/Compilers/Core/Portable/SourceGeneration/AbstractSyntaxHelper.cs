// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis;

internal abstract class AbstractSyntaxHelper : Microsoft.CodeAnalysis.AbstractSyntaxHelper
{
    public sealed override void AddAliases(GreenNode node, ArrayBuilder<(string aliasName, string symbolName)> aliases, bool global) { }

    public sealed override void AddAliases(Microsoft.CodeAnalysis.CompilationOptions options, ArrayBuilder<(string aliasName, string symbolName)> aliases) { }

    public sealed override bool ContainsGlobalAliases(SyntaxNode root) => false;
}
