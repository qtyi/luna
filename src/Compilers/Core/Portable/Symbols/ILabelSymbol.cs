// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis;

public interface ILabelSymbol : ISymbol,
    Microsoft.CodeAnalysis.ILabelSymbol
{
    /// <summary>
    /// Gets the immediately containing <see cref="IModuleSymbol"/> of this <see cref="ILocalSymbol"/>.
    /// </summary>
    new IModuleSymbol ContainingMethod { get; }
}
