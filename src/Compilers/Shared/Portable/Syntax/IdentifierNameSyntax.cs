// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript
#endif
{
    using Syntax;

    namespace Syntax
    {
        partial class IdentifierNameSyntax
        {
            internal override IdentifierNameSyntax GetUnqualifiedName() => this;
        }
    }

    partial class SyntaxFactory
    {
        /// <summary>
        /// 创建一个<see cref="IdentifierNameSyntax"/>节点。
        /// </summary>
        /// <param name="name">标识符名称。</param>
        public static IdentifierNameSyntax IdentifierName(string name)
        {
            return IdentifierName(Identifier(name));
        }
    }
}
