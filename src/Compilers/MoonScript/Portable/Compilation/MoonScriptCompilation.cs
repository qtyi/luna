// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MoonScriptCompilation
{
    /// <summary>
    /// Gets the language name that this node is compilation of.
    /// </summary>
    /// <value>
    /// The language name that this node is compilation of.
    /// </value>
    public sealed override string Language => LanguageNames.Lua;

    /// <summary>
    /// Gets a value indicating whether the compilation is case-sensitive.
    /// </summary>
    /// <value>
    /// Always <see langword="true"/> as Lua is case-sensitive.
    /// </value>
    public sealed override bool IsCaseSensitive => true;
}
