// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.CSharp;

namespace Luna.Compilers.Generators
{
    partial class IndentWriter
    {
        /// <summary>
        /// Write a syntax that is to open a block.
        /// </summary>
        public void OpenCSharpBlock()
        {
            this.WriteLine('{');
            this.Indent();
        }

        /// <summary>
        /// Write a syntax that is to close a block.
        /// </summary>
        public void CloseCSharpBlock()
        {
            this.Unindent();
            this.WriteLine('}');
        }

        /// <summary>
        /// Write a syntax that is to close a block.
        /// </summary>
        /// <param name="trailling">The immediate trailling text.</param>
        public void CloseCSharpBlock(char trailling)
        {
            this.Unindent();
            this.Write('}');
            this.WriteLine(trailling);
        }

        /// <summary>
        /// Write a syntax that is to close a block.
        /// </summary>
        /// <param name="trailling">The immediate trailling text.</param>
        public void CloseCSharpBlock(string? trailling)
        {
            this.Unindent();
            this.Write('}');
            this.WriteLine(trailling);
        }

        /// <summary>
        /// Gets an escaped name of C# identifier if the original one conflict with C# keywords.
        /// </summary>
        /// <param name="name">An identifier name.</param>
        /// <returns>Returns <paramref name="name"/> with leading '@' if it conflict with C# keywords.</returns>
        protected internal static string FixCSharpKeyword(string name) => IsCSharpKeyword(name) ? "@" + name : name;

        /// <summary>
        /// Gets a value indicate if a name is any C# keyword.
        /// </summary>
        /// <param name="name">An identifier name.</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="name"/> is a C# keyword; otherwise, <see langword="false"/>.</returns>
        protected internal static bool IsCSharpKeyword(string name) => SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None;
    }

    namespace CSharp
    {
        internal static class IndentWriterExtensions
        {
            /// <inheritdoc cref="IndentWriter.OpenCSharpBlock"/>
            /// <seealso cref="IndentWriter.OpenCSharpBlock"/>
            public static void OpenBlock(this IndentWriter writer) => writer.OpenCSharpBlock();

            /// <inheritdoc cref="IndentWriter.CloseCSharpBlock()"/>
            /// <seealso cref="IndentWriter.CloseCSharpBlock()"/>
            public static void CloseBlock(this IndentWriter writer) => writer.CloseCSharpBlock();

            /// <inheritdoc cref="IndentWriter.CloseCSharpBlock(char)"/>
            /// <seealso cref="IndentWriter.CloseCSharpBlock(char)"/>
            public static void CloseBlock(this IndentWriter writer, char trailling) => writer.CloseCSharpBlock(trailling);

            /// <inheritdoc cref="IndentWriter.CloseCSharpBlock(string?)"/>
            /// <seealso cref="IndentWriter.CloseCSharpBlock(string?)"/>
            public static void CloseBlock(this IndentWriter writer, string? trailling) => writer.CloseCSharpBlock(trailling);

            /// <inheritdoc cref="IndentWriter.FixCSharpKeyword"/>
            /// <seealso cref="IndentWriter.FixCSharpKeyword"/>
            internal static string FixKeyword(this string name) => IndentWriter.FixCSharpKeyword(name);

            /// <inheritdoc cref="IndentWriter.IsCSharpKeyword"/>
            /// <seealso cref="IndentWriter.IsCSharpKeyword"/>
            internal static bool IsKeyword(this string name) => IndentWriter.IsCSharpKeyword(name);
        }
    }
}
