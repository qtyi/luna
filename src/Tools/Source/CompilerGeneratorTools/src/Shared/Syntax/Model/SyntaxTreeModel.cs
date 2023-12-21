// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.Syntax.Model;

#pragma warning disable CS8618
[XmlRoot(ElementName = "Tree")]
public class SyntaxTree : ITree<SyntaxTreeType, SyntaxTreeTypeChild>
{
    [XmlAttribute]
    public string Root;

    [XmlElement(ElementName = "Node", Type = typeof(Node))]
    [XmlElement(ElementName = "AbstractNode", Type = typeof(AbstractNode))]
    [XmlElement(ElementName = "PredefinedNode", Type = typeof(PredefinedNode))]
    public List<SyntaxTreeType> Types;

    string ITree<SyntaxTreeType>.Root => this.Root;
    ImmutableList<SyntaxTreeType> ITree<SyntaxTreeType>.Types => this.Types.ToImmutableList();
}

public abstract class SyntaxTreeType : ITreeType<SyntaxTreeTypeChild>
{
    [XmlAttribute]
    public string Name;

    [XmlAttribute]
    public string? Base;

    [XmlAttribute]
    public string? SkipConvenienceFactories;

    [XmlElement]
    public Comment? TypeComment;

    [XmlElement]
    public Comment? FactoryComment;

    [XmlElement(ElementName = "Field", Type = typeof(Field))]
    [XmlElement(ElementName = "Choice", Type = typeof(Choice))]
    [XmlElement(ElementName = "Sequence", Type = typeof(Sequence))]
    public List<SyntaxTreeTypeChild> Children;

    string ITreeType.Name => this.Name;
    string? ITreeType.Base => this.Base;
    ImmutableList<SyntaxTreeTypeChild> ITreeType<SyntaxTreeTypeChild>.Children => this.Children.ToImmutableList();
}

public abstract class SyntaxTreeTypeChild : ITreeTypeChild { }

public class Choice : SyntaxTreeTypeChild
{
    // Choice节点不应嵌套Choice子节点，如果必要，则应内联子节点。
    [XmlElement(ElementName = "Field", Type = typeof(Field))]
    [XmlElement(ElementName = "Sequence", Type = typeof(Sequence))]
    public List<SyntaxTreeTypeChild> Children;
}

public class Sequence : SyntaxTreeTypeChild
{
    // Sequence节点不应嵌套Sequence子节点，如果必要，则应内联子节点。
    [XmlElement(ElementName = "Field", Type = typeof(Field))]
    [XmlElement(ElementName = "Choice", Type = typeof(Choice))]
    public List<SyntaxTreeTypeChild> Children;
}

public class Field : SyntaxTreeTypeChild
{
    [XmlAttribute]
    public string Name;

    [XmlAttribute]
    public string Type;

    [XmlAttribute]
    public string? Override;

    [XmlAttribute]
    public string? New;

    [XmlAttribute]
    public string? Optional;

    [XmlAttribute]
    public int MinCount;

    [XmlAttribute]
    public bool AllowTrailingSeparator;

    [XmlElement(ElementName = "Kind", Type = typeof(Kind))]
    public List<Kind> Kinds;

    [XmlElement]
    public Comment? PropertyComment;

    internal bool IsToken => this.Type.IsToken();

    internal bool IsNode => this.Type.IsNode();

    internal bool IsNodeList => this.Type.IsNodeList();

    internal bool IsSeparatedNodeList => this.Type.IsSeparatedNodeList();
}
#pragma warning restore CS8618
