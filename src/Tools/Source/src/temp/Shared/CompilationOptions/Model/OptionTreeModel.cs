// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using Luna.Compilers.Generators.Model;
using Roslyn.Utilities;

namespace Luna.Compilers.Generators.CompilationOptions.Model;

#pragma warning disable CS8618
[XmlRoot(ElementName = "OptionList")]
public sealed class OptionList : ITree<Option>
{
    [XmlElement(ElementName = "Option", Type = typeof(Option))]
    public List<Option> Options;

    string ITree<Option>.Root => typeof(Microsoft.CodeAnalysis.CompilationOptions).FullName;

    ImmutableList<Option> ITree<Option>.Types => this.Options.ToImmutableList();
}

public sealed class Option : ITreeType
{
    [XmlAttribute]
    public string Name;

    [XmlAttribute]
    public string? Base;

    string ITreeType.Name => this.Name;

    string? ITreeType.Base => this.Base;
}
#pragma warning restore CS8618
