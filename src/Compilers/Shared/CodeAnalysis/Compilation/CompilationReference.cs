// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilation = LuaCompilation;
using ThisCompilationReference = LuaCompilationReference;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilation = MoonScriptCompilation;
using ThisCompilationReference = MoonScriptCompilationReference;
#endif

/// <summary>
/// Represents a reference to a compilation. 
/// </summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal sealed partial class
#if LANG_LUA
    LuaCompilationReference
#elif LANG_MOONSCRIPT
    MoonScriptCompilationReference
#endif
    : CompilationReference
{
    /// <summary>
    /// Gets the referenced compilation.
    /// </summary>
    /// <value>
    /// The compilation referenced.
    /// </value>
    public new ThisCompilation Compilation { get; }

    /// <summary>
    /// Create an instance of <see cref="ThisCompilationReference"/> class.
    /// </summary>
    /// <param name="compilation">The compilation references.</param>
    /// <param name="aliases">The aliases for the reference.</param>
    /// <param name="embedInteropTypes">Whether there are interop types embed in <paramref name="compilation"/>.</param>
    public
#if LANG_LUA
        LuaCompilationReference
#elif LANG_MOONSCRIPT
        MoonScriptCompilationReference
#endif
    (
        ThisCompilation compilation,
        ImmutableArray<string> aliases = default,
        bool embedInteropTypes = false)
        : base(GetProperties(compilation, aliases, embedInteropTypes)) =>
        this.Compilation = compilation;

    private
#if LANG_LUA
        LuaCompilationReference
#elif LANG_MOONSCRIPT
        MoonScriptCompilationReference
#endif
    (ThisCompilation compilation, MetadataReferenceProperties properties) : base(properties) =>
        this.Compilation = compilation;

    #region Debugger Display
#pragma warning disable IDE0051 // 删除未使用的私有成员
    /// <summary>
    /// Get DebugDisplay text.
    /// </summary>
    private partial string GetDebugDisplay();
#pragma warning restore IDE0051 // 删除未使用的私有成员
    #endregion

    #region CompilationReference
    internal override Compilation CompilationCore => this.Compilation;

    internal override CompilationReference WithPropertiesImpl(MetadataReferenceProperties properties) => new ThisCompilationReference(this.Compilation, properties);
    #endregion
}
