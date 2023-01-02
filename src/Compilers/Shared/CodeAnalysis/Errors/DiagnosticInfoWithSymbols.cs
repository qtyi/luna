// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using ThisMessageProvider = MessageProvider;

internal class DiagnosticInfoWithSymbols : DiagnosticInfo
{
    internal readonly ImmutableArray<Symbol> Symbols;

    internal DiagnosticInfoWithSymbols(ErrorCode code, object[] arguments, ImmutableArray<Symbol> symbols) : base(ThisMessageProvider.Instance, (int)code, arguments) => this.Symbols = symbols;

    internal DiagnosticInfoWithSymbols(bool isWarningAsError, ErrorCode code, object[] arguments, ImmutableArray<Symbol> symbols) : base(ThisMessageProvider.Instance, isWarningAsError, (int)code, arguments) => this.Symbols = symbols;
}
