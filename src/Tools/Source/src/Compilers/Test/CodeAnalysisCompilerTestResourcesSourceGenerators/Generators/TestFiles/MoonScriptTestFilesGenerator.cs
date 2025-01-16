// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Luna.Tools;

[Generator]
public sealed partial class MoonScriptTestFilesGenerator : AbstractTestResourcesGenerator
{
    protected override string GeneratorName { get; } = nameof(MoonScriptTestFilesGenerator);

    protected override void ProduceSource(SourceProductionContext context, string? thisLanguageName, ImmutableArray<AdditionalText> inputs)
    {
        var luaTestFilesContext = new TestFilesSourceProductionContext(inputs.SelectAsArray(static text => Path.GetFileName(text.Path)));
        WriteAndAddSource(context, MoonScriptTestFilesSourceWriter.WriteSource, luaTestFilesContext, "TestResources.MoonScriptTestFiles.g.cs");
    }
}
