﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
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
    protected override void WriteCompilationOptionsCore(JsonWriter writer, Microsoft.CodeAnalysis.CompilationOptions options)
    {
        if (options is not ThisCompilationOptions)
            throw new ArgumentException(null, nameof(options));

        WriteCompilationOptionsCore(writer, (ThisCompilationOptions)options);
    }

    /// <inheritdoc cref="WriteCompilationOptionsCore(JsonWriter, Microsoft.CodeAnalysis.CompilationOptions)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private partial void WriteCompilationOptionsCore(JsonWriter writer, ThisCompilationOptions options);

    /// <inheritdoc/>
    protected override void WriteParseOptionsCore(JsonWriter writer, Microsoft.CodeAnalysis.ParseOptions parseOptions)
    {
        if (parseOptions is not ThisParseOptions)
            throw new ArgumentException(null, nameof(parseOptions));

        WriteParseOptionsCore(writer, (ThisParseOptions)parseOptions);
    }

    /// <inheritdoc cref="WriteParseOptionsCore(JsonWriter, Microsoft.CodeAnalysis.ParseOptions)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private partial void WriteParseOptionsCore(JsonWriter writer, ThisParseOptions parseOptions);
}
