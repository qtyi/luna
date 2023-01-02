// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Syntax.Model;

internal static class ModelExtensions
{
    public static bool IsOptional(this Field f)
        => f.Optional.IsTrue();

    public static bool IsOverride(this Field f)
        => f.Override is not null;

    public static bool IsNew(this Field f)
        => f.New.IsTrue();
}
