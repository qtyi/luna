// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.Syntax.Model;

#pragma warning disable CS8618
[XmlRoot]
public class Tree : ITree<TreeType, TreeTypeChild>
{
    [XmlAttribute]
    public string Root;

    [XmlElement(ElementName = "Node", Type = typeof(Node))]
    [XmlElement(ElementName = "AbstractNode", Type = typeof(AbstractNode))]
    [XmlElement(ElementName = "PredefinedNode", Type = typeof(PredefinedNode))]
    public List<TreeType> Types;

    string ITree<TreeType, TreeTypeChild>.Root => this.Root;
    ImmutableList<TreeType> ITree<TreeType, TreeTypeChild>.Types => this.Types.ToImmutableList();
}
#pragma warning restore CS8618
