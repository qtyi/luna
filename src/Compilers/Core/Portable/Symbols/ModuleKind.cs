// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis;

[Flags]
public enum ModuleKind : byte
{
    /// <summary>
    /// Module's kind is undefined.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Module is .NET namespace
    /// </summary>
    Namespace = 1 << 0,

    /// <summary>
    /// Module is type
    /// </summary>
    Type = 1 << 1,

    /// <summary>
    /// Module is field
    /// </summary>
    Field = 1 << 2,
}
