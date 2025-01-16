// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript;

partial class SyntaxAndDeclarationManager
{
    partial class State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/>
        /// </summary>
        /// <param name="syntaxTrees">A collection of syntax trees in ordinal order.</param>
        /// <param name="syntaxTreeOrdinalMap">The inverse of <paramref name="syntaxTrees"/> array.</param>
        /// <param name="loadedSyntaxTreeMap">The loaded syntax trees.</param>
        /// <param name="modules">The root declarations.</param>
        /// <param name="declarationTable">The declaration table</param>
        internal State(
            ImmutableArray<SyntaxTree> syntaxTrees,
            ImmutableDictionary<SyntaxTree, int> syntaxTreeOrdinalMap,
            ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap,
            ImmutableDictionary<SyntaxTree, Lazy<ModuleDeclaration>> modules,
            DeclarationTable declarationTable)
        {
            Debug.Assert(syntaxTrees.All(tree => syntaxTrees[syntaxTreeOrdinalMap[tree]] == tree));
            Debug.Assert(syntaxTrees.SetEquals(modules.Keys.AsImmutable(), EqualityComparer<SyntaxTree>.Default));

            SyntaxTrees = syntaxTrees;
            OrdinalMap = syntaxTreeOrdinalMap;
            LoadedSyntaxTreeMap = loadedSyntaxTreeMap;
            Modules = modules;
            DeclarationTable = declarationTable;
        }
    }
}
