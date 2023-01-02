// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics;
using MSCA::Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua.Symbols;

partial class SourceAssemblySymbol
{
    internal override partial void ForceComplete(SourceLocation? location, CancellationToken cancellationToken)
    {
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var incompletePart = this._state.NextIncompletePart;
            switch (incompletePart)
            {
                case CompletionPart.Module:
                    this.SourceNetModule.ForceComplete(location, cancellationToken);
                    if (this.SourceNetModule.HasComplete(CompletionPart.MembersCompleted))
                    {
                        this._state.NotePartComplete(CompletionPart.Module);
                        break;
                    }
                    else
                    {
                        Debug.Assert(location is not null, "若不指定位置，则.NET模块的成员应全部完成。");
                        return;
                    }

                case CompletionPart.StartValidatingAddedModules:
                case CompletionPart.FinishValidatingAddedModules:
                    if (this._state.NotePartComplete(CompletionPart.StartValidatingAddedModules))
                    {
                        this.ReportDiagnosticsForAddedNetModules();
                        var thisThreadCompleted = this._state.NotePartComplete(CompletionPart.FinishValidatingAddedModules);
                        Debug.Assert(thisThreadCompleted);
                    }
                    break;

                case CompletionPart.None: return;

                default:
                    this._state.NotePartComplete(CompletionPart.All & ~CompletionPart.AssemblySymbolAll);
                    break;
            }

            this._state.SpinWaitComplete(incompletePart, cancellationToken);
        }
    }
}
