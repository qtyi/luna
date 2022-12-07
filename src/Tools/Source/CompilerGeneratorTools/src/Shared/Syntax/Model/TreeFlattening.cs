// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Syntax.Model;

public static class TreeFlattening
{
    public static void FlattenChildren(Tree tree)
    {
        foreach (var type in tree.Types)
        {
            switch (type)
            {
                case AbstractNode node:
                    FlattenChildren(node.Children, node.Fields, makeOptional: false);
                    break;
                case Node node:
                    FlattenChildren(node.Children, node.Fields, makeOptional: false);
                    break;
            }
        }
    }

    private static void FlattenChildren(
        List<TreeTypeChild> fieldsAndChoices, List<Field> fields, bool makeOptional)
    {
        foreach (var fieldOrChoice in fieldsAndChoices)
        {
            switch (fieldOrChoice)
            {
                case Field field:
                    if (makeOptional && !SyntaxFileWriter.IsAnyNodeList(field.Type))
                    {
                        field.Optional = "true";
                    }

                    fields.Add(field);
                    break;
                case Choice choice:
                    // Children of choices are always optional (since the point is to
                    // chose from one of them and leave out the rest).
                    FlattenChildren(choice.Children, fields, makeOptional: true);
                    break;
                case Sequence sequence:
                    FlattenChildren(sequence.Children, fields, makeOptional);
                    break;
                default:
                    throw new InvalidOperationException("Unknown child type.");
            }
        }
    }
}
