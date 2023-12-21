// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Diagnostics;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public readonly struct TypeInfo : IEquatable<TypeInfo>
{
    internal static readonly TypeInfo None = default;

    /// <summary>
    /// The type of the expression represented by the syntax node. For expressions that do not
    /// have a type, null is returned. If the type could not be determined due to an error, then
    /// an IErrorTypeSymbol is returned.
    /// </summary>
    public ITypeSymbol? Type { get; }

    /// <summary>
    /// The top-level nullability information of the expression represented by the syntax node.
    /// </summary>
    public NullabilityInfo Nullability { get; }

    /// <summary>
    /// The type of the expression after it has undergone an implicit conversion. If the type
    /// did not undergo an implicit conversion, returns the same as Type.
    /// </summary>
    public ITypeSymbol? ConvertedType { get; }

    /// <summary>
    /// The top-level nullability of the expression after it has undergone an implicit conversion.
    /// For most expressions, this will be the same as the type. It can change in situations such
    /// as implicit user-defined conversions that have a nullable return type.
    /// </summary>
    public NullabilityInfo ConvertedNullability { get; }

    private TypeInfo(ITypeSymbol? type, ITypeSymbol? convertedType, NullabilityInfo nullability, NullabilityInfo convertedNullability)
        : this()
    {
        Debug.Assert(type is null || type.NullableAnnotation == nullability.FlowState.ToAnnotation());
        Debug.Assert(convertedType is null || convertedType.NullableAnnotation == convertedNullability.FlowState.ToAnnotation());
        this.Type = type;
        this.Nullability = nullability;
        this.ConvertedType = convertedType;
        this.ConvertedNullability = convertedNullability;
    }

    public bool Equals(TypeInfo other) =>
        Equals(this.Type, other.Type) &&
        Equals(this.ConvertedType, other.ConvertedType) &&
        this.Nullability.Equals(other.Nullability) &&
        this.ConvertedNullability.Equals(other.ConvertedNullability);

    public override bool Equals(object? obj) =>
        obj is TypeInfo && this.Equals((TypeInfo)obj);

    public override int GetHashCode() =>
        Hash.Combine(this.ConvertedType,
        Hash.Combine(this.Type,
        Hash.Combine(this.Nullability.GetHashCode(),
        this.ConvertedNullability.GetHashCode())));

    public static explicit operator TypeInfo(Microsoft.CodeAnalysis.TypeInfo typeInfo) => new(
        (ITypeSymbol?)typeInfo.Type,
        (ITypeSymbol?)typeInfo.ConvertedType,
        typeInfo.Nullability,
        typeInfo.ConvertedNullability);
}
