// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Symbols;

[Flags]
internal enum FlowAnalysisAnnotations
{
    None = 0,
    AllowNull = 1 << 0,
    DisallowNull = 1 << 1,
    MaybeNullWhenTrue = 1 << 2,
    MaybeNullWhenFalse = 1 << 3,
    MaybeNull = MaybeNullWhenTrue | MaybeNullWhenFalse,
    NotNullWhenTrue = 1 << 4,
    NotNullWhenFalse = 1 << 5,
    NotNull = NotNullWhenTrue | NotNullWhenFalse,
    DoesNotReturnIfFalse = 1 << 6,
    DoesNotReturnIfTrue = 1 << 7,
    DoesNotReturn = DoesNotReturnIfTrue | DoesNotReturnIfFalse,
}
