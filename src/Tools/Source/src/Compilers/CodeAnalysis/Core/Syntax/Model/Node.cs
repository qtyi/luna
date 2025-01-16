// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml.Serialization;

namespace Luna.Tools.Syntax.Model;

#pragma warning disable CS8618
public sealed class Node : SyntaxTreeType
{
    [XmlAttribute]
    public string? Root;

    [XmlAttribute]
    public string? Errors;

    [XmlElement(ElementName = "Kind", Type = typeof(Kind))]
    public List<Kind> Kinds;

    public readonly List<Field> Fields = new();
}
#pragma warning restore CS8618
