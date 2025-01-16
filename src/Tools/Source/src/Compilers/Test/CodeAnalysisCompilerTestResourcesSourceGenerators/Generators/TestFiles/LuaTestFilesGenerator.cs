// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Luna.Tools;

[Generator]
public sealed partial class LuaTestFilesGenerator : AbstractTestResourcesGenerator
{
    protected override string GeneratorName { get; } = nameof(LuaTestFilesGenerator);

    protected override void ProduceSource(SourceProductionContext context, string? thisLanguageName, ImmutableArray<AdditionalText> inputs)
    {
        var luaTestFilesContext = new TestFilesSourceProductionContext(inputs.SelectAsArray(static text => Path.GetFileName(text.Path)));
        WriteAndAddSource(context, LuaTestFilesSourceWriter.WriteSource, luaTestFilesContext, "TestResources.LuaTestFiles.g.cs");
    }
}
