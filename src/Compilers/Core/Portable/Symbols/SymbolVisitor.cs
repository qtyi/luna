// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public abstract class SymbolVisitor : Microsoft.CodeAnalysis.SymbolVisitor
{
    public virtual void Visit(ISymbol? symbol) => symbol?.Accept(this);

    public virtual void DefaultVisit(ISymbol symbol) { }

    public virtual void VisitArrayType(IArrayTypeSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitAssembly(IAssemblySymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitDynamicType(IDynamicTypeSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitField(IFieldSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitLabel(ILabelSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitLocal(ILocalSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitModule(IModuleSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitNamedType(INamedTypeSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitNetmodule(INetmoduleSymbol symbol) => this.DefaultVisit(symbol);

    public virtual void VisitParameter(IParameterSymbol symbol) => this.DefaultVisit(symbol);

    #region Microsoft.CodeAnalysis.SymbolVisitor
#pragma warning disable CS0809
    public sealed override void Visit(Microsoft.CodeAnalysis.ISymbol? symbol) => this.Visit((ISymbol?)symbol);

    public sealed override void DefaultVisit(Microsoft.CodeAnalysis.ISymbol symbol) => this.DefaultVisit((ISymbol)symbol);

    [Obsolete("VisitAlias is not supported.", error: true)]
    public sealed override void VisitAlias(Microsoft.CodeAnalysis.IAliasSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitArrayType(Microsoft.CodeAnalysis.IArrayTypeSymbol symbol) => this.VisitArrayType((IArrayTypeSymbol)symbol);

    public sealed override void VisitAssembly(Microsoft.CodeAnalysis.IAssemblySymbol symbol) => this.VisitAssembly((IAssemblySymbol)symbol);

    [Obsolete("VisitDiscard is not supported.", error: true)]
    public sealed override void VisitDiscard(Microsoft.CodeAnalysis.IDiscardSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitDynamicType(Microsoft.CodeAnalysis.IDynamicTypeSymbol symbol) => this.VisitDynamicType((IDynamicTypeSymbol)symbol);

    public sealed override void VisitEvent(Microsoft.CodeAnalysis.IEventSymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    public sealed override void VisitField(Microsoft.CodeAnalysis.IFieldSymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    public sealed override void VisitLabel(Microsoft.CodeAnalysis.ILabelSymbol symbol) => this.VisitLabel((ILabelSymbol)symbol);

    public sealed override void VisitLocal(Microsoft.CodeAnalysis.ILocalSymbol symbol) => this.VisitLocal((ILocalSymbol)symbol);

    public sealed override void VisitMethod(Microsoft.CodeAnalysis.IMethodSymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    public sealed override void VisitModule(Microsoft.CodeAnalysis.IModuleSymbol symbol) => this.VisitNetmodule((INetmoduleSymbol)symbol);

    public sealed override void VisitNamedType(Microsoft.CodeAnalysis.INamedTypeSymbol symbol) => this.VisitNamedType((INamedTypeSymbol)symbol);

    public sealed override void VisitNamespace(Microsoft.CodeAnalysis.INamespaceSymbol symbol) => this.VisitModule((IModuleSymbol)symbol);

    public sealed override void VisitParameter(Microsoft.CodeAnalysis.IParameterSymbol symbol) => this.VisitParameter((IParameterSymbol)symbol);

    [Obsolete("VisitPointerType is not supported.", error: true)]
    public sealed override void VisitPointerType(Microsoft.CodeAnalysis.IPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [Obsolete("VisitFunctionPointerType is not supported.", error: true)]
    public sealed override void VisitFunctionPointerType(Microsoft.CodeAnalysis.IFunctionPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitProperty(Microsoft.CodeAnalysis.IPropertySymbol symbol) => this.VisitField((IFieldSymbol)symbol);

    [Obsolete("VisitRangeVariable is not supported.", error: true)]
    public sealed override void VisitRangeVariable(Microsoft.CodeAnalysis.IRangeVariableSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [Obsolete("VisitTypeParameter is not supported.", error: true)]
    public sealed override void VisitTypeParameter(Microsoft.CodeAnalysis.ITypeParameterSymbol symbol) => throw ExceptionUtilities.Unreachable();
#pragma warning restore CS0809
    #endregion
}
