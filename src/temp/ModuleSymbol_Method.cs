// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class ModuleSymbol
{
    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.IsIterator"/>.
    /// </summary>
    protected abstract bool IsIteratorCore { get; }

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.IsAsync"/>.
    /// </summary>
    protected abstract bool IsAsyncCore { get; }

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.CalculateLocalSyntaxOffset(int, Microsoft.CodeAnalysis.SyntaxTree)"/>.
    /// </summary>
    protected abstract int CalculateLocalSyntaxOffsetCore(int declaratorPosition, Microsoft.CodeAnalysis.SyntaxTree declaratorTree);

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.Construct(Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal[])"/>.
    /// </summary>
    protected abstract Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal ConstructCore(params Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal[] typeArguments);

    #region Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal
    bool Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.IsIterator => IsIteratorCore;
    bool Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.IsAsync => IsAsyncCore;
    int Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.CalculateLocalSyntaxOffset(int declaratorPosition, Microsoft.CodeAnalysis.SyntaxTree declaratorTree) => CalculateLocalSyntaxOffsetCore(declaratorPosition, declaratorTree);
    Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal.Construct(params Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal[] typeArguments) => ConstructCore(typeArguments);
    #endregion
}
