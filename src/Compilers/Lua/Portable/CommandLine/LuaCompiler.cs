// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaCompiler
{
    /// <summary>可执行应用名称。</summary>
    internal const string ExecutableName = "luac";

    protected override partial void ResolveAnalyzersFromArguments(
        List<DiagnosticInfo> diagnostics,
        CommonMessageProvider messageProvider,
        bool skipAnalyzers,
        out ImmutableArray<DiagnosticAnalyzer> analyzers,
        out ImmutableArray<ISourceGenerator> generators)
    {
        this.Arguments.ResolveAnalyzersFromArguments(
            LanguageNames.Lua,
            diagnostics,
            messageProvider,
            this.AssemblyLoader,
            skipAnalyzers,
            out analyzers,
            out generators);
    }
}
