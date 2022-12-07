// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml;
using System.Xml.Serialization;

namespace Luna.Compilers.Generators.Syntax.Model;

#pragma warning disable CS8618
public class Comment
{
    [XmlAnyElement]
    public XmlElement[] Body;
}
#pragma warning restore CS8618
