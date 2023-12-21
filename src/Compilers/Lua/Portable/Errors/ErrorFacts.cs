// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.Lua;

partial class ErrorFacts
{
    private static partial ImmutableDictionary<ErrorCode, string> CreateCategoriesMap() => ImmutableDictionary<ErrorCode, string>.Empty;

    public static partial string GetHelpLink(ErrorCode code) =>
        $"https://github.com/qtyi/luna/wiki/lua/language-reference/compiler-messages/{GetId(code)}";

    private static partial int GetWarningLevelCore(ErrorCode code) => code switch
    {
        _ => 0
    };
}
