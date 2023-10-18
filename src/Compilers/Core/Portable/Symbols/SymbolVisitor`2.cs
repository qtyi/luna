// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public abstract class SymbolVisitor<TArgument, TResult> : Microsoft.CodeAnalysis.SymbolVisitor<TArgument, TResult>
{
    public virtual TResult Visit(ISymbol? symbol, TArgument argument)
    {
        if (symbol is null) return this.DefaultResult;

        return symbol.Accept(this, argument);
    }

    public virtual TResult DefaultVisit(ISymbol symbol, TArgument argument) => this.DefaultResult;

    public virtual TResult VisitArrayType(IArrayTypeSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitAssembly(IAssemblySymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitDynamicType(IDynamicTypeSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitField(IFieldSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitLabel(ILabelSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitLocal(ILocalSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitModule(IModuleSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitNamedType(INamedTypeSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitNetmodule(INetmoduleSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    public virtual TResult VisitParameter(IParameterSymbol symbol, TArgument argument) => this.DefaultVisit(symbol, argument);

    #region Microsoft.CodeAnalysis.SymbolVisitor<TArgument, TResult>
#pragma warning disable CS0809
    public sealed override TResult Visit(Microsoft.CodeAnalysis.ISymbol? symbol, TArgument argument) => this.Visit((ISymbol?)symbol, argument);

    public sealed override TResult DefaultVisit(Microsoft.CodeAnalysis.ISymbol symbol, TArgument argument) => this.DefaultVisit((ISymbol)symbol, argument);

    [Obsolete("VisitAlias is not supported.", error: true)]
    public sealed override TResult VisitAlias(Microsoft.CodeAnalysis.IAliasSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    public sealed override TResult VisitArrayType(Microsoft.CodeAnalysis.IArrayTypeSymbol symbol, TArgument argument) => this.VisitArrayType((IArrayTypeSymbol)symbol, argument);

    public sealed override TResult VisitAssembly(Microsoft.CodeAnalysis.IAssemblySymbol symbol, TArgument argument) => this.VisitAssembly((IAssemblySymbol)symbol, argument);

    [Obsolete("VisitDiscard is not supported.", error: true)]
    public sealed override TResult VisitDiscard(Microsoft.CodeAnalysis.IDiscardSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    public sealed override TResult VisitDynamicType(Microsoft.CodeAnalysis.IDynamicTypeSymbol symbol, TArgument argument) => this.VisitDynamicType((IDynamicTypeSymbol)symbol, argument);

    public sealed override TResult VisitEvent(Microsoft.CodeAnalysis.IEventSymbol symbol, TArgument argument) => this.VisitField((IFieldSymbol)symbol, argument);

    public sealed override TResult VisitField(Microsoft.CodeAnalysis.IFieldSymbol symbol, TArgument argument) => this.VisitField((IFieldSymbol)symbol, argument);

    public sealed override TResult VisitLabel(Microsoft.CodeAnalysis.ILabelSymbol symbol, TArgument argument) => this.VisitLabel((ILabelSymbol)symbol, argument);

    public sealed override TResult VisitLocal(Microsoft.CodeAnalysis.ILocalSymbol symbol, TArgument argument) => this.VisitLocal((ILocalSymbol)symbol, argument);

    public sealed override TResult VisitMethod(Microsoft.CodeAnalysis.IMethodSymbol symbol, TArgument argument) => this.VisitField((IFieldSymbol)symbol, argument);

    public sealed override TResult VisitModule(Microsoft.CodeAnalysis.IModuleSymbol symbol, TArgument argument) => this.VisitNetmodule((INetmoduleSymbol)symbol, argument);

    public sealed override TResult VisitNamedType(Microsoft.CodeAnalysis.INamedTypeSymbol symbol, TArgument argument) => this.VisitNamedType((INamedTypeSymbol)symbol, argument);

    public sealed override TResult VisitNamespace(Microsoft.CodeAnalysis.INamespaceSymbol symbol, TArgument argument) => this.VisitModule((IModuleSymbol)symbol, argument);

    public sealed override TResult VisitParameter(Microsoft.CodeAnalysis.IParameterSymbol symbol, TArgument argument) => this.VisitParameter((IParameterSymbol)symbol, argument);

    [Obsolete("VisitPointerType is not supported.", error: true)]
    public sealed override TResult VisitPointerType(Microsoft.CodeAnalysis.IPointerTypeSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    [Obsolete("VisitFunctionPointerType is not supported.", error: true)]
    public sealed override TResult VisitFunctionPointerType(Microsoft.CodeAnalysis.IFunctionPointerTypeSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    public sealed override TResult VisitProperty(Microsoft.CodeAnalysis.IPropertySymbol symbol, TArgument argument) => this.VisitField((IFieldSymbol)symbol, argument);

    [Obsolete("VisitRangeVariable is not supported.", error: true)]
    public sealed override TResult VisitRangeVariable(Microsoft.CodeAnalysis.IRangeVariableSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    [Obsolete("VisitTypeParameter is not supported.", error: true)]
    public sealed override TResult VisitTypeParameter(Microsoft.CodeAnalysis.ITypeParameterSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();
#pragma warning restore CS0809
    #endregion
}
