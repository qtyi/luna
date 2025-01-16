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

/// <summary>
/// Provides the ability to manage syntax and declaration state.
/// </summary>
internal sealed partial class SyntaxAndDeclarationManager : CommonSyntaxAndDeclarationManager
{
    /// <summary>
    /// State of syntax and declaration.
    /// </summary>
    private State? _lazyState;

    /// <summary>
    /// Initializes a new instance of the <see cref="SyntaxAndDeclarationManager"/> class with the specified <see cref="State"/>.
    /// </summary>
    /// <param name="externalSyntaxTrees">A collection of external syntax trees.</param>
    /// <param name="scriptModuleName">The name of script module.</param>
    /// <param name="resolver">An object that resolves references to source documents specified in the source.</param>
    /// <param name="messageProvider">An object that classify and load messages for error codes.</param>
    /// <param name="isSubmission">Whether it is submission.</param>
    /// <param name="state">An immutable state of syntax and declaration.</param>
    internal SyntaxAndDeclarationManager(
        ImmutableArray<SyntaxTree> externalSyntaxTrees,
        string? scriptModuleName,
        SourceReferenceResolver? resolver,
        CommonMessageProvider messageProvider,
        bool isSubmission,
        State? state)
#nullable disable
        : base(externalSyntaxTrees, scriptModuleName, resolver, messageProvider, isSubmission)
#nullable enable
    {
        _lazyState = state;
    }

    /// <summary>
    /// Gets the current state of syntax and declaration.
    /// </summary>
    /// <returns>An object that represents the current state of syntax and declaration.</returns>
    internal State GetLazyState()
    {
        if (_lazyState is null)
        {
            Interlocked.CompareExchange(
                ref _lazyState,
                CreateState(
                    ExternalSyntaxTrees,
                    ScriptClassName,
                    Resolver,
                    MessageProvider,
                    IsSubmission),
                null);
        }

        return _lazyState;
    }

    /// <summary>
    /// Create a new instance of <see cref="SyntaxAndDeclarationManager"/> with specified external syntax trees.
    /// </summary>
    /// <param name="trees">The external syntax trees.</param>
    /// <returns>A new instance of <see cref="SyntaxAndDeclarationManager"/>.</returns>
    internal SyntaxAndDeclarationManager WithExternalSyntaxTrees(ImmutableArray<SyntaxTree> trees) =>
        new(
            trees,
            ScriptClassName,
            Resolver,
            MessageProvider,
            IsSubmission,
            state: null);

    /// <summary>
    /// Create a new <see cref="State"/>.
    /// </summary>
    private static partial State CreateState(
        ImmutableArray<SyntaxTree> externalSyntaxTrees,
        string scriptModuleName,
        SourceReferenceResolver? resolver,
        CommonMessageProvider messageProvider,
        bool isSubmission);

    /// <summary>
    /// Add other syntax trees.
    /// </summary>
    /// <param name="trees">A collection of syntax trees to be added.</param>
    /// <returns>A new instance of <see cref="SyntaxAndDeclarationManager"/> with other syntax trees added.</returns>
    public partial SyntaxAndDeclarationManager AddSyntaxTrees(IEnumerable<SyntaxTree> trees);

    /// <summary>
    /// Remove a collection of syntax trees.
    /// </summary>
    /// <param name="trees">A collection of syntax trees to be removed.</param>
    /// <returns>A new instance of <see cref="SyntaxAndDeclarationManager"/> with some of syntax trees in the old one removed.</returns>
    public partial SyntaxAndDeclarationManager RemoveSyntaxTrees(HashSet<SyntaxTree> trees);

    public partial SyntaxAndDeclarationManager ReplaceSyntaxTree(SyntaxTree oldTree, SyntaxTree newTree);
}
