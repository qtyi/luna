// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_MOONSCRIPT
#define LANG_THIS
#endif

#if LANG_THIS
using System.Diagnostics;

#if LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class LabelSymbol : ILabelSymbol
{
    private readonly InternalModel.Symbols.LabelSymbol _underlying;

    internal override InternalModel.Symbol UnderlyingSymbol => this._underlying;

    public LabelSymbol(InternalModel.Symbols.LabelSymbol underlying)
    {
        Debug.Assert(underlying is not null);
        this._underlying = underlying;
    }

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitLabel(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitLabel(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitLabel(this, argument);
    #endregion

    #region 未完成
    IModuleSymbol ILabelSymbol.ContainingMethod => throw new NotImplementedException();

    Microsoft.CodeAnalysis.IMethodSymbol Microsoft.CodeAnalysis.ILabelSymbol.ContainingMethod => throw new NotImplementedException();
    #endregion
}
#endif
