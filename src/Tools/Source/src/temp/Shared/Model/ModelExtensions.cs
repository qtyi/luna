// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Model;

internal static class ModelExtensions
{
    /// <summary>
    /// Gets a value indicate if a text represents <see langword="true"/>.
    /// </summary>
    /// <param name="val">Text value.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="val"/> represents <see langword="true"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsTrue(this string? val)
        => val is not null && string.Compare(val, "true", true) == 0;
}
