// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Symbols;

internal interface IModuleSymbolInternal : ISymbolInternal,
    Microsoft.CodeAnalysis.Symbols.INamespaceSymbolInternal,
    Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal,
    Microsoft.CodeAnalysis.Symbols.IFieldSymbolInternal,
    Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal
{
    /// <summary>
    /// An enumerated value that identifies whether this module is a namespace, type, field
    /// and so on.
    /// </summary>
    ModuleKind ModuleKind { get; }

    /// <summary>
    /// Returns whether this module is the unnamed, global module that is 
    /// at the root of all modules.
    /// </summary>
    bool IsGlobalModule { get; }
}
