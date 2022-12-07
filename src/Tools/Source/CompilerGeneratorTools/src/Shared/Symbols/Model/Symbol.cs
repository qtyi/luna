// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml.Serialization;

namespace Luna.Compilers.Generators.Symbols.Model;

public sealed class Symbol : SymbolTreeType
{
    [XmlAttribute]
    public string? Implement;
}
