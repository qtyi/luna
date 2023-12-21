// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using Qtyi.CodeAnalysis.Operations;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Root type for representing the abstract semantics of Luna statements and expressions.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
[InternalImplementationOnly]
public partial interface IOperation : Microsoft.CodeAnalysis.IOperation
{
    /// <summary>
    /// Gets the operation that has this operation as a child.
    /// </summary>
    /// <value>
    /// Returns the parent operation, or <see langword="null"/> if the operation is the root.
    /// </value>
    new IOperation? Parent { get; }

    /// <summary>
    /// Gets the result type of the operation.
    /// </summary>
    /// <value>
    /// Returns the esult type of the operation, or <see langword="null"/> if the operation does not produce a result.
    /// </value>
    new ITypeSymbol? Type { get; }

    /// <summary>
    /// An enumerable of child operations for this operation.
    /// </summary>
    new OperationList ChildOperations { get; }

    void Accept(OperationVisitor visitor);

    TResult? Accept<TArgument, TResult>(OperationVisitor<TArgument, TResult> visitor, TArgument argument);

    /// <summary>
    /// Optional semantic model that was used to generate this operation.
    /// Non-null for operations generated from source with <see cref="SemanticModel.GetOperation(SyntaxNode, CancellationToken)"/> API
    /// and operation callbacks made to analyzers.
    /// Null for operations inside a <see cref="FlowAnalysis.ControlFlowGraph"/>.
    /// </summary>
    new SemanticModel? SemanticModel { get; }
}
