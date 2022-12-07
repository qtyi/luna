// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml.Serialization;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.Syntax.Model;

#pragma warning disable CS8618
public abstract class TreeTypeChild : ITreeTypeChild { }

public class Choice : TreeTypeChild
{
    // Choice节点不应嵌套Choice子节点，如果必要，则应内联子节点。
    [XmlElement(ElementName = "Field", Type = typeof(Field))]
    [XmlElement(ElementName = "Sequence", Type = typeof(Sequence))]
    public List<TreeTypeChild> Children;
}

public class Sequence : TreeTypeChild
{
    // Sequence节点不应嵌套Sequence子节点，如果必要，则应内联子节点。
    [XmlElement(ElementName = "Field", Type = typeof(Field))]
    [XmlElement(ElementName = "Choice", Type = typeof(Choice))]
    public List<TreeTypeChild> Children;
}

public class Field : TreeTypeChild
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

    public bool IsToken => this.Type == "SyntaxToken";
}
#pragma warning restore CS8618
