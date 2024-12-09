// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;

using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

internal sealed class UnsupportedMetadataTypeSymbol : ErrorTypeSymbol
{
    private readonly BadImageFormatException? _mrEx;

    internal UnsupportedMetadataTypeSymbol(BadImageFormatException? mrEx = null) => this._mrEx = mrEx;

    #region 未完成
#warning 未完成
    public override int Arity => throw new NotImplementedException();

    internal override DiagnosticInfo? ErrorInfo => throw new NotImplementedException();
    #endregion
}
