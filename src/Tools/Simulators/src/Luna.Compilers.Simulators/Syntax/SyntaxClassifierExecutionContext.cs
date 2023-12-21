// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

public struct SyntaxClassifierExecutionContext
{
    public ISyntaxInfoProvider SyntaxInfoProvider { get; }
    public CancellationToken CancellationToken { get; }

    internal SyntaxClassifierExecutionContext(ISyntaxInfoProvider syntaxInfoProvider, CancellationToken cancellationToken)
    {
        this.SyntaxInfoProvider = syntaxInfoProvider;
        this.CancellationToken = cancellationToken;
    }
}
