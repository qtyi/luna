// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using RoslynTypeKind = Microsoft.CodeAnalysis.TypeKind;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Enumeration for possible kinds of type symbols.
/// </summary>
public enum TypeKind : byte
{
    /// <summary>
    /// Type's kind is undefined.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Type is an array type.
    /// </summary>
    Array = 1,

    /// <summary>
    /// Type is an array dynamic type.
    /// </summary>
    Dynamic = 2,

    /// <summary>
    /// Type is an array error type.
    /// </summary>
    Error = 3,

    /// <summary>
    /// Type is an array named type.
    /// </summary>
    NamedType = 4,
}
