// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public abstract class SymbolVisitor<TResult> : Microsoft.CodeAnalysis.SymbolVisitor<TResult>
{
    public virtual TResult? Visit(ISymbol? symbol)
    {
        if (symbol is null) return default;

        return symbol.Accept(this);
    }

    public virtual TResult? DefaultVisit(ISymbol symbol) => default;

    public virtual TResult? VisitArrayType(IArrayTypeSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitAssembly(IAssemblySymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitDynamicType(IDynamicTypeSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitField(IFieldSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitLabel(ILabelSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitLocal(ILocalSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitModule(IModuleSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitNamedType(INamedTypeSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitNetmodule(INetmoduleSymbol symbol) => this.DefaultVisit(symbol);

    public virtual TResult? VisitParameter(IParameterSymbol symbol) => this.DefaultVisit(symbol);

    #region Microsoft.CodeAnalysis.SymbolVisitor<TResult>
#pragma warning disable CS0809
    public sealed override TResult? Visit(Microsoft.CodeAnalysis.ISymbol? symbol) => this.Visit((ISymbol?)symbol);

    public sealed override TResult? DefaultVisit(Microsoft.CodeAnalysis.ISymbol symbol) => this.DefaultVisit((ISymbol)symbol);

    [Obsolete("VisitAlias is not supported.", error: true)]
    public sealed override TResult? VisitAlias(Microsoft.CodeAnalysis.IAliasSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override TResult? VisitArrayType(Microsoft.CodeAnalysis.IArrayTypeSymbol symbol) => this.VisitArrayType((IArrayTypeSymbol)symbol);

    public sealed override TResult? VisitAssembly(Microsoft.CodeAnalysis.IAssemblySymbol symbol) => this.VisitAssembly((IAssemblySymbol)symbol);

    [Obsolete("VisitDiscard is not supported.", error: true)]
    public sealed override TResult? VisitDiscard(Microsoft.CodeAnalysis.IDiscardSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override TResult? VisitDynamicType(Microsoft.CodeAnalysis.IDynamicTypeSymbol symbol) => this.VisitDynamicType((IDynamicTypeSymbol)symbol);

    public sealed override TResult? VisitEvent(Microsoft.CodeAnalysis.IEventSymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    public sealed override TResult? VisitField(Microsoft.CodeAnalysis.IFieldSymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    public sealed override TResult? VisitLabel(Microsoft.CodeAnalysis.ILabelSymbol symbol) => this.VisitLabel((ILabelSymbol)symbol);

    public sealed override TResult? VisitLocal(Microsoft.CodeAnalysis.ILocalSymbol symbol) => this.VisitLocal((ILocalSymbol)symbol);

    public sealed override TResult? VisitMethod(Microsoft.CodeAnalysis.IMethodSymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    public sealed override TResult? VisitModule(Microsoft.CodeAnalysis.IModuleSymbol symbol) => this.VisitNetmodule((INetmoduleSymbol)symbol);

    public sealed override TResult? VisitNamedType(Microsoft.CodeAnalysis.INamedTypeSymbol symbol) => this.VisitNamedType((INamedTypeSymbol)symbol);

    public sealed override TResult? VisitNamespace(Microsoft.CodeAnalysis.INamespaceSymbol symbol) => this.VisitModule((IModuleSymbol)symbol);

    public sealed override TResult? VisitParameter(Microsoft.CodeAnalysis.IParameterSymbol symbol) => this.VisitParameter((IParameterSymbol)symbol);

    [Obsolete("VisitPointerType is not supported.", error: true)]
    public sealed override TResult? VisitPointerType(Microsoft.CodeAnalysis.IPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [Obsolete("VisitFunctionPointerType is not supported.", error: true)]
    public sealed override TResult? VisitFunctionPointerType(Microsoft.CodeAnalysis.IFunctionPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override TResult? VisitProperty(Microsoft.CodeAnalysis.IPropertySymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    [Obsolete("VisitRangeVariable is not supported.", error: true)]
    public sealed override TResult? VisitRangeVariable(Microsoft.CodeAnalysis.IRangeVariableSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [Obsolete("VisitTypeParameter is not supported.", error: true)]
    public sealed override TResult? VisitTypeParameter(Microsoft.CodeAnalysis.ITypeParameterSymbol symbol) => throw ExceptionUtilities.Unreachable();
#pragma warning restore CS0809
    #endregion
}
