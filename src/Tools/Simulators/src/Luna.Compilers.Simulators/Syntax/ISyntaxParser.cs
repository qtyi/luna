// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

public interface ISyntaxParser
{
    void Initialize(SyntaxParserInitializationContext context);

    SyntaxTree Parse(SyntaxParserExecutionContext context);
}
