// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public abstract class SymbolVisitor : Microsoft.CodeAnalysis.SymbolVisitor
{
    public sealed override void VisitAlias(IAliasSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitPointerType(IPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitRangeVariable(IRangeVariableSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitDiscard(IDiscardSymbol symbol) => throw ExceptionUtilities.Unreachable();

    public sealed override void VisitFunctionPointerType(IFunctionPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();
}
