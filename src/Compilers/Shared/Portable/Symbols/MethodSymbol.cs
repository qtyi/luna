// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Symbols;
using System.Reflection;
using System.Reflection.Metadata;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

abstract partial class MethodSymbol : Symbol, IMethodSymbolInternal
{
    bool IMethodSymbolInternal.IsIterator => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsAsync => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsGenericMethod => throw new NotImplementedException();

    bool IMethodSymbolInternal.ReturnsVoid => throw new NotImplementedException();

    int IMethodSymbolInternal.ParameterCount => throw new NotImplementedException();

    ImmutableArray<IParameterSymbolInternal> IMethodSymbolInternal.Parameters => throw new NotImplementedException();

    bool IMethodSymbolInternal.HasDeclarativeSecurity => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsAccessCheckedOnOverride => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsExternal => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsHiddenBySignature => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsMetadataNewSlot => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsPlatformInvoke => throw new NotImplementedException();

    bool IMethodSymbolInternal.IsMetadataFinal => throw new NotImplementedException();

    bool IMethodSymbolInternal.HasSpecialName => throw new NotImplementedException();

    bool IMethodSymbolInternal.HasRuntimeSpecialName => throw new NotImplementedException();

    bool IMethodSymbolInternal.RequiresSecurityObject => throw new NotImplementedException();

    MethodImplAttributes IMethodSymbolInternal.ImplementationAttributes => throw new NotImplementedException();

    ISymbolInternal? IMethodSymbolInternal.AssociatedSymbol => throw new NotImplementedException();

    IMethodSymbolInternal? IMethodSymbolInternal.PartialImplementationPart => throw new NotImplementedException();

    IMethodSymbolInternal? IMethodSymbolInternal.PartialDefinitionPart => throw new NotImplementedException();

    BlobHandle IMethodSymbolInternal.MetadataSignatureHandle => throw new NotImplementedException();

    int IMethodSymbolInternal.CalculateLocalSyntaxOffset(int declaratorPosition, SyntaxTree declaratorTree)
    {
        throw new NotImplementedException();
    }

    IMethodSymbolInternal IMethodSymbolInternal.Construct(params ITypeSymbolInternal[] typeArguments)
    {
        throw new NotImplementedException();
    }
}
