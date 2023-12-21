// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Operations;

/// <summary>
/// Represents a <see cref="IOperation"/> visitor that visits only the single IOperation
/// passed into its Visit method.
/// </summary>
public abstract partial class OperationVisitor : Microsoft.CodeAnalysis.Operations.OperationVisitor
{
}

/// <summary>
/// Represents a <see cref="IOperation"/> visitor that visits only the single IOperation
/// passed into its Visit method with an additional argument of the type specified by the
/// <typeparamref name="TArgument"/> parameter and produces a value of the type specified by
/// the <typeparamref name="TResult"/> parameter.
/// </summary>
/// <inheritdoc/>
public abstract partial class OperationVisitor<TArgument, TResult> : Microsoft.CodeAnalysis.Operations.OperationVisitor<TArgument, TResult>
{
}
