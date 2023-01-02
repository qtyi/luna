// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using MSCA::Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    partial struct TokenInfo
    {
        internal int InnerIndent;
        /// <summary>
        /// 语法标志的语法标志列表类型值。
        /// </summary>
        internal ImmutableArray<SyntaxToken> SyntaxTokenArrayValue;
    }
}
