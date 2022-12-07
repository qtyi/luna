// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators.Symbols.Model;

#pragma warning disable CS8618
[XmlRoot(ElementName = "Tree")]
public class SymbolTree : ITree<SymbolTreeType, ITreeTypeChild>
{
    [XmlAttribute]
    public string Root;

    [XmlElement(ElementName = "Symbol", Type = typeof(Symbol))]
    [XmlElement(ElementName = "AbstractSymbol", Type = typeof(AbstractSymbol))]
    [XmlElement(ElementName = "PredefinedSymbol", Type = typeof(PredefinedSymbol))]
    public List<SymbolTreeType> Types;

    string ITree<SymbolTreeType, ITreeTypeChild>.Root => this.Root;
    ImmutableList<SymbolTreeType> ITree<SymbolTreeType, ITreeTypeChild>.Types => this.Types.ToImmutableList();
}
#pragma warning restore CS8618
