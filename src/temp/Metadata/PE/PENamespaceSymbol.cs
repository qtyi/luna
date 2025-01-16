// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal sealed partial class PENamespaceSymbol : ModuleSymbol
{
#warning 未完成
    internal PENamespaceSymbol(PENetmoduleSymbol module) => throw new NotImplementedException();

    public override bool IsGlobalModule => throw new NotImplementedException();

    public override SymbolKind Kind => throw new NotImplementedException();

    public PENetmoduleSymbol ContainingPEModule => throw new NotImplementedException();

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

    protected override bool IsIteratorCore => throw new NotImplementedException();

    protected override bool IsAsyncCore => throw new NotImplementedException();

    protected override bool IsVolatileCore => throw new NotImplementedException();

    protected override TypeKind TypeKindCore => throw new NotImplementedException();

    protected override bool IsReferenceTypeCore => throw new NotImplementedException();

    protected override bool IsValueTypeCore => throw new NotImplementedException();

    public override void Accept(ThisSymbolVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public override TResult? Accept<TResult>(ThisSymbolVisitor<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<FieldSymbol> GetFieldMembers()
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<FieldSymbol> GetFieldMembers(string name)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<FieldSymbol> GetFieldMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<ModuleSymbol> GetMembers()
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<ModuleSymbol> GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    protected override int CalculateLocalSyntaxOffsetCore(int declaratorPosition, SyntaxTree declaratorTree)
    {
        throw new NotImplementedException();
    }

    protected override IMethodSymbolInternal ConstructCore(params ITypeSymbolInternal[] typeArguments)
    {
        throw new NotImplementedException();
    }

    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }

    protected override ITypeSymbol GetITypeSymbolCore()
    {
        throw new NotImplementedException();
    }

    internal override TResult? Accept<TArgument, TResult>(ThisSymbolVisitor<TArgument, TResult> visitor, TArgument argument) where TResult : default
    {
        throw new NotImplementedException();
    }

    internal override ImmutableArray<ModuleSymbol> GetNamespaceMembers()
    {
        throw new NotImplementedException();
    }

    internal override ImmutableArray<ModuleSymbol> GetNamespaceMembers(string name)
    {
        throw new NotImplementedException();
    }
}
