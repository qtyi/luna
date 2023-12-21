// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Operations;

/// <summary>
/// Use this to create <see cref="IOperation"/> when the operation is invalid.
/// </summary>
internal sealed partial class InvalidOperation : Operation, IInvalidOperation
{
    /// <inheritdoc/>
    public override OperationKind Kind => OperationKind.Invalid;

    /// <inheritdoc/>
    public override ITypeSymbol? Type { get; }

    /// <inheritdoc/>
    internal ImmutableArray<IOperation> Children { get; }

    /// <inheritdoc/>
    internal override ConstantValue? OperationConstantValue { get; }

    /// <inheritdoc/>
    internal override int ChildOperationsCount => this.Children.Length;

    public InvalidOperation(ImmutableArray<IOperation> children, SemanticModel? semanticModel, SyntaxNode syntax, ITypeSymbol? type, ConstantValue? constantValue, bool isImplicit) :
        base(semanticModel, syntax, isImplicit)
    {
        // we don't allow null children.
        Debug.Assert(children.All(static o => o is not null));
        this.Type = type;
        this.Children = SetParentOperation(children, this);
        this.OperationConstantValue = constantValue;
    }

    /// <inheritdoc/>
    internal override IOperation GetCurrent(int slot, int index)
        => slot switch
        {
            0 when index < this.Children.Length
                => this.Children[index],
            _ => throw ExceptionUtilities.UnexpectedValue((slot, index))
        };

    /// <inheritdoc/>
    internal override (bool hasNext, int nextSlot, int nextIndex) MoveNext(int previousSlot, int previousIndex)
    {
        switch (previousSlot)
        {
            case -1:
                if (!this.Children.IsEmpty) return (true, 0, 0);
                else goto case 0;
            case 0 when previousIndex + 1 < this.Children.Length:
                return (true, 0, previousIndex + 1);
            case 0:
            case 1:
                return (false, 1, 0);
            default:
                throw ExceptionUtilities.UnexpectedValue((previousSlot, previousIndex));
        }
    }

    /// <inheritdoc/>
    internal override (bool hasNext, int nextSlot, int nextIndex) MoveNextReversed(int previousSlot, int previousIndex)
        => previousSlot switch
        {
            int.MaxValue when !this.Children.IsEmpty => (true, 0, this.Children.Length - 1),
            0 when previousIndex > 0 => (true, 0, previousIndex - 1),
            int.MaxValue or 0 or -1 => (false, -1, 0),
            _ => throw ExceptionUtilities.UnexpectedValue((previousSlot, previousIndex))
        };

    /// <inheritdoc/>
    public override void Accept(OperationVisitor visitor) => visitor.VisitInvalid(this);

    /// <inheritdoc/>
    public override TResult? Accept<TArgument, TResult>(OperationVisitor<TArgument, TResult> visitor, TArgument argument) where TResult : default => visitor.VisitInvalid(this, argument);
}
