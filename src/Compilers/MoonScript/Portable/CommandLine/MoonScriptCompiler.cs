// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MoonScriptCompiler
{
    /// <summary>Name of executable file without extension.</summary>
    internal const string ExecutableName = "moonc";

    protected override partial void ResolveAnalyzersFromArguments(
        List<DiagnosticInfo> diagnostics,
        CommonMessageProvider messageProvider,
        Microsoft.CodeAnalysis.CompilationOptions compilationOptions,
        bool skipAnalyzers,
        out ImmutableArray<DiagnosticAnalyzer> analyzers,
        out ImmutableArray<ISourceGenerator> generators)
    {
        Arguments.ResolveAnalyzersFromArguments(
            LanguageNames.Lua,
            diagnostics,
            messageProvider,
            AssemblyLoader,
            compilationOptions,
            skipAnalyzers,
            out analyzers,
            out generators);
    }

    public override partial void PrintHelp(TextWriter consoleOutput)
    {
        consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_MOONCHelp, Culture));
    }

    private protected override partial void DiagnoseBadAccesses(
        TextWriter consoleOutput,
        ErrorLogger? errorLogger,
        Compilation compilation,
        ImmutableArray<Diagnostic> diagnostics)
    {
#warning Not implemented
        throw new NotImplementedException();
    }
}
