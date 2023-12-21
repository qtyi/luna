// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Simulators;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ExportAttribute : Attribute
{
    public string[] Languages { get; }

    public string? Trait { get; init; }

    public ExportAttribute(string languageName)
    {
        this.Languages = new[] { languageName };
    }

    public ExportAttribute(string firstLanguage, params string[] additionalLanguages)
    {
        var names = new string[additionalLanguages.Length + 1];
        names[0] = firstLanguage;
        additionalLanguages.CopyTo(names, 1);
        this.Languages = names;
    }
}
