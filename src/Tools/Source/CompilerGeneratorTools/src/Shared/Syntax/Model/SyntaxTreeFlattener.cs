// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.Syntax.Model;

public sealed class SyntaxTreeFlattener : TreeFlattener<SyntaxTree, SyntaxTreeType, SyntaxTreeTypeChild>
{
    public static SyntaxTreeFlattener Instance { get; } = new();

    /// <inheritdoc/>
    protected override bool ShouldFlattenType(SyntaxTreeType treeType, SyntaxTree containingTree, CancellationToken cancellationToken) => treeType switch
    {
        AbstractNode or
        Node => true,

        _ => false
    };

    /// <inheritdoc/>
    protected override void FlattenChild(SyntaxTreeTypeChild child, SyntaxTree containingTree, SyntaxTreeType containingTreeType, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        switch (containingTreeType)
        {
            case AbstractNode abstractNode:
                this.FlattenChildAndItsChildren(child, abstractNode.Fields, makeOptional: false);
                break;

            case Node node:
                this.FlattenChildAndItsChildren(child, node.Fields, makeOptional: false);
                break;
        }
    }

    private void FlattenChildAndItsChildren(
        SyntaxTreeTypeChild fieldOrChoice, List<Field> fields, bool makeOptional)
    {
        switch (fieldOrChoice)
        {
            case Field field:
                if (makeOptional && !SyntaxFileWriter.IsAnyNodeList(field.Type))
                    field.Optional = "true";

                fields.Add(field);
                break;
            case Choice choice:
                // Children of choices are always optional (since the point is to
                // chose from one of them and leave out the rest).
                foreach (var child in choice.Children)
                    FlattenChildAndItsChildren(child, fields, makeOptional: true);
                break;
            case Sequence sequence:
                foreach (var child in sequence.Children)
                    FlattenChildAndItsChildren(child, fields, makeOptional);
                break;
            default:
                throw new InvalidOperationException("Unknown child type.");
        }
    }
}
