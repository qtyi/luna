// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Symbols;

/// <summary>
/// This enum describes the types of components that could give us diagnostics.
/// We shouldn't read the list of diagnostics until all of these types are
/// accounted for.
/// </summary>
/// <remarks>
/// <see cref="PEParameterSymbol"/> reserves all completion part bits and uses
/// them to track the completion state and presence of well known attributes.
/// </remarks>
[Flags]
internal enum CompletionPart
{
    // For all symbols
    None = 0,

    // For function symbols
    /// <summary>Function parameters.</summary>
    Parameters = 1 << 0,

    // For field symbols
    /// <summary>Type of field symbols.</summary>
    Type = 1 << 1,

    // For module symbols
    Members = 1 << 2,
    StartMemberChecks = 1 << 3,
    FinishMemberChecks = 1 << 4,
    MembersCompletedChecksStarted = 1 << 5,
    MembersCompleted = 1 << 6,

    // For field symbols
    ConstantValue = 1 << 7,
    StartFuncionChecks = 1 << 8,
    FinishFunctionChecks = 1 << 9,

    All = (1 << 10) - 1,

    // For assembly symbols
    Module = 1 << 2,
    StartValidatingAddedModules = 1 << 3,
    FinishValidatingAddedModules = 1 << 4,
    AssemblySymbolAll = Module | StartValidatingAddedModules | FinishValidatingAddedModules,

    // For .NET module symbol
    StartValidatingReferencedAssemblies = 1 << 2,
    FinishValidatingReferencedAssemblies = 1 << 3,
}
