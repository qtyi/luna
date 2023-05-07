// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

partial class SyntaxAndDeclarationManager
{
    /// <summary>
    /// Represent a immutable state of syntax and declaration.
    /// </summary>
    internal sealed partial class State
    {
        /// <summary>
        /// The syntax trees this state of in ordinal order.
        /// </summary>
        internal readonly ImmutableArray<SyntaxTree> SyntaxTrees;
        /// <summary>
        /// The inverse of <see cref="SyntaxTrees"/> array (i.e. maps tree to index).
        /// </summary>
        internal readonly ImmutableDictionary<SyntaxTree, int> OrdinalMap;
        /// <summary>
        /// The loaded syntax trees (map from file name to tree).
        /// </summary>
        internal readonly ImmutableDictionary<string, SyntaxTree> LoadedSyntaxTreeMap;
        /// <summary>
        /// The root declarations (map from syntax to declaration).
        /// </summary>
        internal readonly ImmutableDictionary<SyntaxTree, Lazy<ModuleDeclaration>> Modules;
        /// <summary>
        /// The declaration table which keeps track of declarations.
        /// </summary>
        internal readonly DeclarationTable DeclarationTable;
    }
}
