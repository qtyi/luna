// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;
#endif

partial class NameSyntax
{

    public abstract int Arity { get; }

    /// <summary>
    /// Returns the unqualified (right-most) part of a qualified or alias-qualified name, or the name itself if already unqualified. 
    /// </summary>
    /// <returns>The unqualified (right-most) part of a qualified or alias-qualified name, or the name itself if already unqualified.
    /// If called on an instance of <see cref="QualifiedNameSyntax"/> returns the value of the <see cref="QualifiedNameSyntax.Right"/> property.
    /// If called on an instance of <see cref="SimpleNameSyntax"/> returns the instance itself.
    /// </returns>
    internal abstract SimpleNameSyntax GetUnqualifiedName();

    /// <summary>
    /// Return the name in string form, without trivia or generic arguments, for use in diagnostics.
    /// </summary>
    internal abstract string ErrorDisplayName();
}
