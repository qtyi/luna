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

partial class FieldSymbol : IFieldSymbol
{
    private readonly InternalModel.Symbols.FieldSymbol _underlying;

    internal override InternalModel.Symbols.ModuleSymbol UnderlyingModuleSymbol => this._underlying;

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitField(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitField(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitField(this, argument);
    #endregion

    #region 未完成
#warning 未完成
    public ISymbol? AssociatedSymbol => throw new NotImplementedException();

    public ITypeSymbol Type => throw new NotImplementedException();

    public IFieldSymbol OriginalDefinition => throw new NotImplementedException();

    public IFieldSymbol? CorrespondingTupleField => throw new NotImplementedException();

    public FieldSymbol(InternalModel.Symbols.FieldSymbol underlying)
    {
        Debug.Assert(underlying is not null);
        this._underlying = underlying;
    }
    #endregion
}
