// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Tools;

public interface ISourceSyntaxProducer
{
    void Initialize(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken);

    SyntaxTree Produce(params object[] args);
}
