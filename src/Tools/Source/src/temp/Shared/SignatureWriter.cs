// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Generators.CSharp;
using Luna.Compilers.Generators.Syntax;
using Luna.Compilers.Generators.Syntax.Model;
using LanguageNames = Qtyi.CodeAnalysis.LanguageNames;

namespace Luna.Compilers.Generators;

internal sealed class SignatureWriter : SyntaxFileWriter
{
    private SignatureWriter(TextWriter writer, SyntaxTree tree, CancellationToken cancellationToken) : base(writer, 2, tree, cancellationToken) { }

    public static void WriteFile(TextWriter writer, SyntaxTree tree, CancellationToken cancellationToken) => new SignatureWriter(writer, tree, cancellationToken).WriteFile();

    private void WriteFile()
    {
        this.WriteLine("using System;");
        this.WriteLine("using System.Collections;");
        this.WriteLine("using System.Collections.Generic;");
        this.WriteLine("using System.Linq;");
        this.WriteLine("using System.Threading;");
        this.WriteLine();
        this.WriteLine($"namespace Qtyi.CodeAnalysis.{LanguageNames.This}");
        this.OpenBlock();

        this.WriteTypes();

        this.CloseBlock();
    }

    private void WriteTypes()
    {
        var nodes = this.Tree.Types.Where(n => n is not PredefinedNode).ToList();
        for (int i = 0, n = nodes.Count; i < n; i++)
        {
            var node = nodes[i];
            this.WriteLine();
            this.WriteType(node);
        }
    }

    private void WriteType(SyntaxTreeType node)
    {
        if (node is AbstractNode)
        {
            var nd = (AbstractNode)node;
            this.WriteLine($"public abstract partial class {node.Name} : {node.Base}");
            this.OpenBlock();
            for (int i = 0, n = nd.Fields.Count; i < n; i++)
            {
                var field = nd.Fields[i];
                if (IsNodeOrNodeList(field.Type))
                    this.WriteLine($"public abstract {field.Type} {field.Name} {{ get; }}");
            }
            this.CloseBlock();
        }
        else if (node is Node)
        {
            var nd = (Node)node;
            this.WriteLine("public partial class {0} : {1}", node.Name, node.Base);
            this.OpenBlock();

            WriteKinds(nd.Kinds);

            var valueFields = nd.Fields.Where(n => !IsNodeOrNodeList(n.Type)).ToList();
            var nodeFields = nd.Fields.Where(n => IsNodeOrNodeList(n.Type)).ToList();

            for (int i = 0, n = nodeFields.Count; i < n; i++)
            {
                var field = nodeFields[i];
                this.WriteLine($"public {field.Type} {field.Name} {{ get; }}");
            }

            for (int i = 0, n = valueFields.Count; i < n; i++)
            {
                var field = valueFields[i];
                this.WriteLine("public {field.Type} {field.Name} {{ get; }}");
            }

            this.CloseBlock();
        }
    }

    private void WriteKinds(List<Kind> kinds)
    {
        if (kinds.Count > 1)
        {
            foreach (var kind in kinds)
                this.WriteLine("// {0}", kind.Name);
        }
    }
}
