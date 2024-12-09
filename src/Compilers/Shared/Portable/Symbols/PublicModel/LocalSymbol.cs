// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class LocalSymbol : ILocalSymbol
{
    private readonly InternalModel.Symbols.LocalSymbol _underlying;
    internal override InternalModel.Symbol UnderlyingSymbol => this._underlying;

    public LocalSymbol(InternalModel.Symbols.LocalSymbol underlying)
    {
        Debug.Assert(underlying is not null);
        this._underlying = underlying;
    }

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitLocal(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitLocal(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitLocal(this, argument);
    #endregion

    #region 未完成
#warning 未完成
    ITypeSymbol ILocalSymbol.Type => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.ILocalSymbol.Type => throw new NotImplementedException();

    Microsoft.CodeAnalysis.NullableAnnotation Microsoft.CodeAnalysis.ILocalSymbol.NullableAnnotation => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.IsConst => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.IsRef => throw new NotImplementedException();

    Microsoft.CodeAnalysis.RefKind Microsoft.CodeAnalysis.ILocalSymbol.RefKind => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ScopedKind Microsoft.CodeAnalysis.ILocalSymbol.ScopedKind => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.HasConstantValue => throw new NotImplementedException();

    object? Microsoft.CodeAnalysis.ILocalSymbol.ConstantValue => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.IsFunctionValue => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.IsFixed => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.IsForEach => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ILocalSymbol.IsUsing => throw new NotImplementedException();
    #endregion
}
