// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Luna.Compilers.Generators;

[Generator]
public sealed partial class MoonScriptTestFilesGenerator : TestResourcesGenerator
{
    protected override string GeneratorName { get; } = nameof(MoonScriptTestFilesGenerator);

    protected override void GenerateOutput(
        SourceProductionContext context,
        ImmutableArray<AdditionalText> texts)
    {
        WriteAndAddSource(context, MoonScriptTestFilesSourceWriter.WriteSource, texts.Select(static text => Path.GetFileName(text.Path)).ToImmutableArray(), "TestResources.LuaTestFiles.g.cs");
    }
}
