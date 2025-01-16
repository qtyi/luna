// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.CSharp;

namespace Luna.Tools;

internal static class IndentWriterExtensions
{
    /// <summary>
    /// Write a syntax that is to open a block.
    /// </summary>
    public static void OpenBlock(this IndentWriter writer)
    {
        writer.WriteLine('{');
        writer.Indent();
    }

    /// <summary>
    /// Write a syntax that is to close a block.
    /// </summary>
    public static void CloseBlock(this IndentWriter writer)
    {
        writer.Unindent();
        writer.WriteLine('}');
    }

    /// <summary>
    /// Write a syntax that is to close a block.
    /// </summary>
    /// <param name="trailing">The immediate trailing text.</param>
    public static void CloseBlock(this IndentWriter writer, char trailing)
    {
        writer.Unindent();
        writer.Write('}');
        writer.WriteLine(trailing);
    }

    /// <summary>
    /// Write a syntax that is to close a block.
    /// </summary>
    /// <param name="trailing">The immediate trailing text.</param>
    public static void CloseBlock(this IndentWriter writer, string? trailing)
    {
        writer.Unindent();
        writer.Write('}');
        writer.WriteLine(trailing);
    }

    /// <summary>
    /// Gets an escaped name of C# identifier if the original one conflict with C# keywords.
    /// </summary>
    /// <param name="name">An identifier name.</param>
    /// <returns>Returns <paramref name="name"/> with leading '@' if it conflict with C# keywords.</returns>
    public static string FixKeyword(this string name) => IsKeyword(name) ? "@" + name : name;

    /// <summary>
    /// Gets a value indicate if a name is any C# keyword.
    /// </summary>
    /// <param name="name">An identifier name.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="name"/> is a C# keyword; otherwise, <see langword="false"/>.</returns>
    public static bool IsKeyword(this string name) => SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None;
}
