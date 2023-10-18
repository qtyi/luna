// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class ParameterSymbol : IParameterSymbol
{
    private readonly InternalModel.Symbols.ParameterSymbol _underlying;
    internal override InternalModel.Symbol UnderlyingSymbol => this._underlying;

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitParameter(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitParameter(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitParameter(this, argument);
    #endregion

    #region 未完成
#warning 未完成
    public ITypeSymbol Type => throw new NotImplementedException();

    public IParameterSymbol OriginalDefinition => throw new NotImplementedException();

    public Microsoft.CodeAnalysis.RefKind RefKind => throw new NotImplementedException();

    public Microsoft.CodeAnalysis.ScopedKind ScopedKind => throw new NotImplementedException();

    public bool IsParams => throw new NotImplementedException();

    public bool IsOptional => throw new NotImplementedException();

    public bool IsThis => throw new NotImplementedException();

    public bool IsDiscard => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.IParameterSymbol.Type => throw new NotImplementedException();

    public Microsoft.CodeAnalysis.NullableAnnotation NullableAnnotation => throw new NotImplementedException();

    public ImmutableArray<Microsoft.CodeAnalysis.CustomModifier> CustomModifiers => throw new NotImplementedException();

    public ImmutableArray<Microsoft.CodeAnalysis.CustomModifier> RefCustomModifiers => throw new NotImplementedException();

    public int Ordinal => throw new NotImplementedException();

    public bool HasExplicitDefaultValue => throw new NotImplementedException();

    public object? ExplicitDefaultValue => throw new NotImplementedException();

    Microsoft.CodeAnalysis.IParameterSymbol Microsoft.CodeAnalysis.IParameterSymbol.OriginalDefinition => throw new NotImplementedException();

    public ParameterSymbol(InternalModel.Symbols.ParameterSymbol underlying)
    {
        Debug.Assert(underlying is not null);
        this._underlying = underlying;
    }
    #endregion
}
