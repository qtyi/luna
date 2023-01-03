// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators;

internal static class Extensions
{
    public static bool IsTrue(this string? val)
        => val is not null && string.Compare(val, "true", true) == 0;
}
