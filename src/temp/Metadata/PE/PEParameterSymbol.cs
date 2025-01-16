// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal sealed class PEParameterSymbol : ParameterSymbol
{
#warning 未完成
    public override SymbolKind Kind => throw new NotImplementedException();

    public override Symbol? ContainingSymbol => throw new NotImplementedException();

    public override Accessibility DeclaredAccessibility => throw new NotImplementedException();

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => throw new NotImplementedException();

    public override ImmutableArray<Location> Locations => throw new NotImplementedException();

    public override bool IsStatic => throw new NotImplementedException();

    public override bool IsVirtual => throw new NotImplementedException();

    public override bool IsOverride => throw new NotImplementedException();

    public override bool IsAbstract => throw new NotImplementedException();

    public override bool IsSealed => throw new NotImplementedException();

    public override bool IsExtern => throw new NotImplementedException();

    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }
}
