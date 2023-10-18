// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MoonScriptCompilation
{
    /// <summary>
    /// Gets the source language.
    /// </summary>
    /// <value>
    /// Same as <see cref="LanguageNames.MoonScript"/>.
    /// </value>
    public sealed override string Language => LanguageNames.MoonScript;

    /// <value>
    /// Always <see langword="true"/> as MoonScript is case-sensitive.
    /// </value>
    /// <inheritdoc/>
    public sealed override bool IsCaseSensitive => true;

    #region References
    /// <value>
    /// Always empty as MoonScript do not support directives.
    /// </value>
    /// <inheritdoc/>
    public override ImmutableArray<MetadataReference> DirectiveReferences => ImmutableArray<MetadataReference>.Empty;

    /// <value>
    /// Always empty as MoonScript do not support directives.
    /// </value>
    /// <inheritdoc/>
    internal override IDictionary<(string path, string content), MetadataReference> ReferenceDirectiveMap => ImmutableDictionary<(string path, string content), MetadataReference>.Empty;

    /// <value>
    /// Always empty as MoonScript do not support directives.
    /// </value>
    /// <inheritdoc/>
    internal override IEnumerable<ReferenceDirective> ReferenceDirectives => Enumerable.Empty<ReferenceDirective>();
    #endregion
}
