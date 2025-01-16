// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Tools.Model;

namespace Luna.Tools.Syntax.Model;

internal static class ModelExtensions
{
    public static bool IsOptional(this Field f)
        => f.Optional.IsTrue();

    public static bool IsOverride(this Field f)
        => f.Override is not null;

    public static bool IsNew(this Field f)
        => f.New.IsTrue();

    public static bool IsToken(this string typeName)
        => typeName == "SyntaxToken";

    public static bool IsNode(this string typeName, string thisLanguageName)
        => typeName == thisLanguageName + "SyntaxNode";

    public static bool IsSeparatedNodeList(this string typeName)
        => typeName.StartsWith("SeparatedSyntaxList", StringComparison.Ordinal);

    public static bool IsNodeList(this string typeName)
        => typeName.StartsWith("SyntaxList", StringComparison.Ordinal);

    public static bool IsAnyNodeList(this string typeName)
        => typeName.IsNodeList() || typeName.IsSeparatedNodeList();

    public static bool IsNodeOrTokenList(this string typeName)
        => typeName == "SyntaxNodeOrTokenList";

    public static bool IsAnyList(this string typeName)
        => typeName.IsAnyNodeList() || typeName.IsNodeOrTokenList();
}
