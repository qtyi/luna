// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Operations;

namespace Qtyi.CodeAnalysis;

internal abstract partial class Operation : OperationBase, IOperation
{
    /// <inheritdoc/>
    public new IOperation? Parent => (IOperation?)base.Parent;

    /// <summary>
    /// Gets the owning semantic model for this operation node.
    /// Note that this may be different than <see cref="IOperation.SemanticModel"/>, which
    /// is the semantic model on which <see cref="SemanticModel.GetOperation(SyntaxNode, CancellationToken)"/> was invoked
    /// to create this node.
    /// </summary>
    internal new SemanticModel? OwningSemanticModel => (SemanticModel?)base.OwningSemanticModel;

    /// <inheritdoc/>
    public new abstract ITypeSymbol? Type { get; }
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected sealed override Microsoft.CodeAnalysis.ITypeSymbol? TypeCore => this.Type;

    /// <inheritdoc/>
    public new IOperation.OperationList ChildOperations => new(this);

    protected Operation(SemanticModel? semanticModel, SyntaxNode syntax, bool isImplicit) : base(semanticModel, syntax, isImplicit) { }

    internal new abstract IOperation GetCurrent(int slot, int index);
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected sealed override IOperation GetCurrentCore(int slot, int index) => this.GetCurrent(slot, index);

    public sealed override void Accept(Microsoft.CodeAnalysis.Operations.OperationVisitor visitor) => this.Accept((OperationVisitor)visitor);
    public abstract void Accept(OperationVisitor visitor);

    public sealed override TResult? Accept<TArgument, TResult>(Microsoft.CodeAnalysis.Operations.OperationVisitor<TArgument, TResult> visitor, TArgument argument) where TResult : default => this.Accept((OperationVisitor<TArgument, TResult>)visitor, argument);
    public abstract TResult? Accept<TArgument, TResult>(OperationVisitor<TArgument, TResult> visitor, TArgument argument);

    SemanticModel? IOperation.SemanticModel => (SemanticModel?)((Microsoft.CodeAnalysis.IOperation)this).SemanticModel;
}

[EditorBrowsable(EditorBrowsableState.Never)]
internal abstract class OperationBase : Microsoft.CodeAnalysis.Operation
{
    public sealed override Microsoft.CodeAnalysis.ITypeSymbol? Type => this.TypeCore;
    protected abstract Microsoft.CodeAnalysis.ITypeSymbol? TypeCore { get; }

    protected OperationBase(SemanticModel? semanticModel, SyntaxNode syntax, bool isImplicit) : base(semanticModel, syntax, isImplicit) { }

    internal sealed override Microsoft.CodeAnalysis.IOperation GetCurrent(int slot, int index) => this.GetCurrent(slot, index);
    protected abstract IOperation GetCurrentCore(int slot, int index);
}
