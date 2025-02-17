﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaDeterministicKeyBuilder
{
    private partial void WriteCompilationOptionsCore(JsonWriter writer, ThisCompilationOptions options)
    {
        base.WriteCompilationOptionsCore(writer, options);
    }

    private partial void WriteParseOptionsCore(JsonWriter writer, ThisParseOptions parseOptions)
    {
        base.WriteParseOptionsCore(writer, parseOptions);

        writer.Write("languageVersion", parseOptions.LanguageVersion);
        writer.Write("specifiedLanguageVersion", parseOptions.SpecifiedLanguageVersion);
    }
}
