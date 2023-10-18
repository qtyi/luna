// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.Cci;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// A struct that combines a single type with annotations
/// </summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal readonly struct TypeWithAnnotations : IFormattable
{
    [DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
    internal sealed class Boxed
    {
        internal static readonly Boxed Sentinel = new Boxed(default);

        internal readonly TypeWithAnnotations Value;
        internal Boxed(TypeWithAnnotations value)
        {
            Value = value;
        }
        internal string GetDebuggerDisplay() => Value.GetDebuggerDisplay();
    }

    /// <summary>
    /// The underlying type, unless overridden by _extensions.
    /// </summary>
    internal readonly TypeSymbol DefaultType;

    /// <summary>
    /// Additional data or behavior. Such cases should be
    /// uncommon to minimize allocations.
    /// </summary>
    private readonly Extensions _extensions;

    public readonly NullableAnnotation NullableAnnotation;

    private TypeWithAnnotations(TypeSymbol defaultType, NullableAnnotation nullableAnnotation, Extensions extensions)
    {
        throw new NotImplementedException();
    }

    public TypeSymbol Type => throw new NotImplementedException();

    internal static TypeWithAnnotations Create(bool isNullableEnabled, TypeSymbol typeSymbol, bool isAnnotated = false)
    {
        throw new NotImplementedException();
    }

    internal static TypeWithAnnotations Create(TypeSymbol typeSymbol, NullableAnnotation nullableAnnotation = NullableAnnotation.None, ImmutableArray<CustomModifier> customModifiers = default)
    {
        throw new NotImplementedException();
    }

    private string GetDebuggerDisplay()
    {
        throw new NotImplementedException();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        throw new NotImplementedException();
    }

    private abstract class Extensions { }
}
