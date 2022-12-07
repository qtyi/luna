// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaParseOptions
{
    /// <summary>
    /// 获取已定义的解析器符号的名称。
    /// </summary>
    /// <remarks>
    /// Lua.NET不支持预处理指令。
    /// </remarks>
    public override IEnumerable<string> PreprocessorSymbolNames => ImmutableArray<string>.Empty;
}
