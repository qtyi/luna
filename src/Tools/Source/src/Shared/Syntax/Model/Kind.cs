// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml.Serialization;

namespace Luna.Compilers.Generators.Syntax.Model;

#pragma warning disable CS8618
public class Kind : IEquatable<Kind>
{
    [XmlAttribute]
    public string Name;

    public override bool Equals(object? obj)
        => Equals(obj as Kind);

    public bool Equals(Kind? other)
        => Name == other?.Name;

    public override int GetHashCode()
        => Name is null ? 0 : Name.GetHashCode();
}
#pragma warning restore CS8618
