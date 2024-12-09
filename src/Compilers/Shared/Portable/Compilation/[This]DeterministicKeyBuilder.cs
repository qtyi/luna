// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#else
#error Language not supported.
#endif

/// <inheritdoc/>
internal sealed partial class
#if LANG_LUA
    LuaDeterministicKeyBuilder
#elif LANG_MOONSCRIPT
    MoonScriptDeterministicKeyBuilder
#endif
    : DeterministicKeyBuilder
{
    /// <summary>
    /// The unique instance of <see cref="ThisDeterministicKeyBuilder"/>.
    /// </summary>
    internal static readonly ThisDeterministicKeyBuilder Instance = new();

    /// <remarks>Prevent anyone else from deriving from this class.</remarks>
    private
#if LANG_LUA
        LuaDeterministicKeyBuilder
#elif LANG_MOONSCRIPT
        MoonScriptDeterministicKeyBuilder
#endif
        ()
    { }

    /// <inheritdoc/>
    protected override void WriteCompilationOptionsCore(JsonWriter writer, CompilationOptions options)
    {
        if (options is not ThisCompilationOptions)
            throw new ArgumentException(null, nameof(options));

        this.WriteCompilationOptionsCore(writer, (ThisCompilationOptions)options);
    }

    /// <inheritdoc cref="WriteCompilationOptionsCore(JsonWriter, CompilationOptions)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private partial void WriteCompilationOptionsCore(JsonWriter writer, ThisCompilationOptions options);

    /// <inheritdoc/>
    protected override void WriteParseOptionsCore(JsonWriter writer, ParseOptions parseOptions)
    {
        if (parseOptions is not ThisParseOptions)
            throw new ArgumentException(null, nameof(parseOptions));

        this.WriteParseOptionsCore(writer, (ThisParseOptions)parseOptions);
    }

    /// <inheritdoc cref="WriteParseOptionsCore(JsonWriter, ParseOptions)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private partial void WriteParseOptionsCore(JsonWriter writer, ThisParseOptions parseOptions);
}
