// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Symbols;

/// <summary>
/// 此枚举描述了所有能提供诊断的组件的类型。
/// 在读取诊断列表前我们需要完成这些类型的组件。
/// </summary>
[Flags]
internal enum CompletionPart
{
    None = 0,

    /// <summary>函数参数。</summary>
    Parameters = 1 << 0,

    /// <summary>符号的类型。</summary>
    Type = 1 << 1,

    All = (1 << 18) - 1,

    // 对程序集符号重写：
    Module = 1 << 2,
    StartValidatingAddedModules = 1 << 3,
    FinishValidatingAddedModules = 1 << 4,
    AssemblySymbolAll = Module | StartValidatingAddedModules | FinishValidatingAddedModules,

    // 对.NET模块符号重写：
    StartValidatingReferencedAssemblies = 1 << 2,
    FinishValidatingReferencedAssemblies = 1 << 3,
}
