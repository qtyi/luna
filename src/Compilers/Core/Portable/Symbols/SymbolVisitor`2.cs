// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public abstract class SymbolVisitor<TArgument, TResult> : Microsoft.CodeAnalysis.SymbolVisitor<TArgument, TResult>
{
    [DoesNotReturn]
    public sealed override TResult VisitAlias(IAliasSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult VisitPointerType(IPointerTypeSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult VisitRangeVariable(IRangeVariableSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult VisitDiscard(IDiscardSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    public sealed override TResult VisitFunctionPointerType(IFunctionPointerTypeSymbol symbol, TArgument argument) => throw ExceptionUtilities.Unreachable();
}
