// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Qtyi.CodeAnalysis.MoonScript;

internal abstract partial class
#if DEBUG
    SymbolAdapter
#else
    Symbol
#endif
    : IReference
{
    // MoonScript does not support attributes.
    IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context) => Enumerable.Empty<ICustomAttribute>();
}
