// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    partial struct TokenInfo
    {
        internal int InnerIndent;
        /// <summary>
        /// 语法标记的语法标记列表类型值。
        /// </summary>
        internal ImmutableArray<SyntaxToken> SyntaxTokenArrayValue;
    }
}
