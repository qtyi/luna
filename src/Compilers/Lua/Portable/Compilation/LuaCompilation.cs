// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Symbols;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaCompilation
{
    /// <summary>
    /// Gets the source language.
    /// </summary>
    /// <value>
    /// Same as <see cref="LanguageNames.Lua"/>.
    /// </value>
    public sealed override string Language => LanguageNames.Lua;

    /// <value>
    /// Always <see langword="true"/> as Lua is case-sensitive.
    /// </value>
    /// <inheritdoc/>
    public sealed override bool IsCaseSensitive => true;

    #region References
    /// <value>
    /// Always empty as Lua do not support directives.
    /// </value>
    /// <inheritdoc/>
    public override ImmutableArray<MetadataReference> DirectiveReferences => ImmutableArray<MetadataReference>.Empty;

    /// <value>
    /// Always empty as Lua do not support directives.
    /// </value>
    /// <inheritdoc/>
    internal override IDictionary<(string path, string content), MetadataReference> ReferenceDirectiveMap => ImmutableDictionary<(string path, string content), MetadataReference>.Empty;

    /// <value>
    /// Always empty as Lua do not support directives.
    /// </value>
    /// <inheritdoc/>
    internal override IEnumerable<ReferenceDirective> ReferenceDirectives => Enumerable.Empty<ReferenceDirective>();
    #endregion

    #region Symbols
    #endregion
}
