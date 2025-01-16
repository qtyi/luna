// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class DynamicTypeSymbol : IDynamicTypeSymbol
{
    private readonly Symbols.DynamicTypeSymbol _underlying;

    internal override Symbols.TypeSymbol UnderlyingTypeSymbol => _underlying;

    public DynamicTypeSymbol(Symbols.DynamicTypeSymbol underlying, NullableAnnotation nullableAnnotation) : base()
    {
        Debug.Assert(underlying is not null);
        _underlying = underlying;
    }

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitDynamicType(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitDynamicType(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitDynamicType(this, argument);
    #endregion
}
