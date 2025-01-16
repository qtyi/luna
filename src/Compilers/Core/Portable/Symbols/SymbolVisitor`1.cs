// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public abstract class SymbolVisitor<TResult> : Microsoft.CodeAnalysis.SymbolVisitor<TResult>
{
    [DoesNotReturn]
    public sealed override TResult? VisitAlias(IAliasSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult? VisitPointerType(IPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult? VisitRangeVariable(IRangeVariableSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult? VisitDiscard(IDiscardSymbol symbol) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult? VisitFunctionPointerType(IFunctionPointerTypeSymbol symbol) => throw ExceptionUtilities.Unreachable();
}
