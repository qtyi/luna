// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;

using ThisAttributeData = LuaAttributeData;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;

using ThisAttributeData = MoonScriptAttributeData;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

internal sealed partial class PEAssemblySymbol : MetadataOrSourceAssemblySymbol
{
}
