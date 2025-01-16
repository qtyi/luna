// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using System.Xml;
using Luna.Tools.Syntax;
using Luna.Tools.Syntax.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Tools;

[Generator(LanguageNames.CSharp)]
public class SyntaxGenerator : AbstractSyntaxGenerator
{
    protected override void ProduceSource(SourceProductionContext context, string? thisLanguageName, ImmutableArray<AdditionalText> inputs)
    {
        if (string.IsNullOrWhiteSpace(thisLanguageName))
            return;

        var tree = SerializeInputs(context, inputs);
        if (tree is null)
            return;

        var syntaxContext = new SyntaxSourceProductionContext(thisLanguageName!, tree);
        WriteAndAddSource(context, SyntaxSourceWriter.WriteMain, syntaxContext, Syntax_xml + ".Main.Generated.cs");
        WriteAndAddSource(context, SyntaxSourceWriter.WriteInternal, syntaxContext, Syntax_xml + ".Internal.Generated.cs");
        WriteAndAddSource(context, SyntaxSourceWriter.WriteSyntax, syntaxContext, Syntax_xml + ".Syntax.Generated.cs");
    }
}

/// <summary>
/// Context passed to an <see cref="SyntaxSourceWriter"/> to start source production.
/// </summary>
internal readonly struct SyntaxSourceProductionContext
{
    public readonly string ThisLanguageName;
    public readonly Syntax.Model.SyntaxTree SyntaxTree;

    public SyntaxSourceProductionContext(string thisLanguageName, Syntax.Model.SyntaxTree syntaxTree)
    {
        ThisLanguageName = thisLanguageName;
        SyntaxTree = syntaxTree;
    }
}
