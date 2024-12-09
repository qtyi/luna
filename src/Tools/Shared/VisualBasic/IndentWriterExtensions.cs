// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.VisualBasic;

namespace Luna.Compilers.Generators
{
    partial class IndentWriter
    {
        /// <summary>
        /// Write a syntax that is to open a block.
        /// </summary>
        public void OpenVisualBasicBlock()
        {
            this.WriteLine();
            this.Indent();
        }

        /// <summary>
        /// Write a syntax that is to close a block.
        /// </summary>
        /// <param name="blockType">The block type.</param>
        public void CloseVisualBasicBlock(string blockType)
        {
            this.Unindent();
            this.WriteLine("End ");
            this.Writer.WriteLine(blockType);
        }

        /// <summary>
        /// Gets an escaped name of C# identifier if the original one conflict with C# keywords.
        /// </summary>
        /// <param name="name">An identifier name.</param>
        /// <returns>Returns <paramref name="name"/> with leading '@' if it conflict with C# keywords.</returns>
        protected internal static string FixVisualBasicKeyword(string name) => IsVisualBasicKeyword(name) ? "[" + name + "]" : name;

        /// <summary>
        /// Gets a value indicate if a name is any C# keyword.
        /// </summary>
        /// <param name="name">An identifier name.</param>
        /// <returns>Returns <see langword="true"/> if <paramref name="name"/> is a C# keyword; otherwise, <see langword="false"/>.</returns>
        protected internal static bool IsVisualBasicKeyword(string name) => SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None;
    }

    namespace VisualBasic
    {
        internal static class IndentWriterExtensions
        {
            /// <inheritdoc cref="IndentWriter.OpenVisualBasicBlock"/>
            /// <seealso cref="IndentWriter.OpenVisualBasicBlock"/>
            public static void OpenBlock(this IndentWriter writer) => writer.OpenVisualBasicBlock();

            /// <inheritdoc cref="IndentWriter.CloseVisualBasicBlock(string)"/>
            /// <seealso cref="IndentWriter.CloseVisualBasicBlock(string)"/>
            public static void CloseBlock(this IndentWriter writer, string blockType) => writer.CloseVisualBasicBlock(blockType);

            /// <inheritdoc cref="IndentWriter.FixVisualBasicKeyword"/>
            /// <seealso cref="IndentWriter.FixVisualBasicKeyword"/>
            internal static string FixKeyword(this string name) => IndentWriter.FixVisualBasicKeyword(name);

            /// <inheritdoc cref="IndentWriter.IsVisualBasicKeyword"/>
            /// <seealso cref="IndentWriter.IsVisualBasicKeyword"/>
            internal static bool IsKeyword(this string name) => IndentWriter.IsVisualBasicKeyword(name);
        }
    }
}
