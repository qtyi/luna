// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.Syntax.Model;

#pragma warning disable CS8618
public abstract class TreeType : ITreeType<TreeTypeChild>
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
    public List<TreeTypeChild> Children;

    string ITreeType<TreeTypeChild>.Name => this.Name;
    string? ITreeType<TreeTypeChild>.Base => this.Base;
    ImmutableList<TreeTypeChild> ITreeType<TreeTypeChild>.Children => this.Children.ToImmutableList();
}
#pragma warning restore CS8618
