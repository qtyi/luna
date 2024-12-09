// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal static partial class TypeSymbolExtensions
{
    public static bool IsErrorType(this TypeSymbol type)
    {
        Debug.Assert(type is not null);
        return type.TypeKind == TypeKind.Error;
    }
}
