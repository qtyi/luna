// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators;

public abstract class AbstractSyntaxParser : ISyntaxParser
{
    public virtual void Initialize(SyntaxParserInitializationContext context)
    {
        context.RegisterRadioParseOption(nameof(ParseOptions.Kind), new[] { SourceCodeKind.Regular, SourceCodeKind.Script });
        context.RegisterRadioParseOption(nameof(ParseOptions.DocumentationMode), Enum.GetValues(typeof(DocumentationMode)));
    }

    public abstract SyntaxTree Parse(SyntaxParserExecutionContext context);
}
