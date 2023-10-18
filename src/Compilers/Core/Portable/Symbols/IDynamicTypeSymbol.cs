// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents the 'dynamic' type.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface IDynamicTypeSymbol : ITypeSymbol,
    Microsoft.CodeAnalysis.IDynamicTypeSymbol
{
}
