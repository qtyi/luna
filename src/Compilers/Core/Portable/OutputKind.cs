// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

/// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind"/>
public enum OutputKind
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind.ConsoleApplication"/>
    ConsoleApplication = Microsoft.CodeAnalysis.OutputKind.ConsoleApplication,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind.WindowsApplication"/>
    WindowsApplication = Microsoft.CodeAnalysis.OutputKind.WindowsApplication,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary"/>
    DynamicallyLinkedLibrary = Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind.NetModule"/>
    NetModule = Microsoft.CodeAnalysis.OutputKind.NetModule,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind.WindowsRuntimeMetadata"/>
    WindowsRuntimeMetadata = Microsoft.CodeAnalysis.OutputKind.WindowsRuntimeMetadata,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.OutputKind.WindowsRuntimeApplication"/>
    WindowsRuntimeApplication = Microsoft.CodeAnalysis.OutputKind.WindowsRuntimeApplication,

    /// <summary>
    /// An .out file which contains pre-compiled bytecodes for Lua virtual machine.
    /// </summary>
    LuaBytecodes = 6,
}

internal static partial class EnumBounds
{
    /// <summary>
    /// Check if a output kind value is valid or not.
    /// </summary>
    /// <param name="value">The output kind value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="value"/> is valid; otherwise, <see langword="false"/>.</returns>
    internal static bool IsValid(this OutputKind value) => value is >= OutputKind.ConsoleApplication and <= OutputKind.LuaBytecodes;

    /// <summary>
    /// Get default output file extension for output kind.
    /// </summary>
    /// <param name="kind">The output kind of compilation.</param>
    /// <returns>Default output file extension corresponding to <paramref name="kind"/>.</returns>
    internal static string GetDefaultExtension(this OutputKind kind) => kind switch
    {
        OutputKind.ConsoleApplication or
        OutputKind.WindowsApplication or
        OutputKind.WindowsRuntimeApplication => ".exe",

        OutputKind.DynamicallyLinkedLibrary => ".dll",

        OutputKind.NetModule => ".netmodule",

        OutputKind.WindowsRuntimeMetadata => ".winmdobj",

        OutputKind.LuaBytecodes => ".out",

        _ => throw ExceptionUtilities.UnexpectedValue(kind),
    };

    /// <summary>
    /// Check if the compilation of this output kind produces an executable application.
    /// </summary>
    /// <param name="kind">The output kind of compilation.</param>
    /// <returns>Returns <see langword="true"/> if compilation produces an executable application; otherwise, <see langword="false"/>.</returns>
    internal static bool IsApplication(this OutputKind kind) => kind switch
    {
        OutputKind.ConsoleApplication or
        OutputKind.WindowsApplication or
        OutputKind.WindowsRuntimeApplication => true,

        OutputKind.DynamicallyLinkedLibrary or
        OutputKind.NetModule or
        OutputKind.WindowsRuntimeMetadata or
        OutputKind.LuaBytecodes => false,

        _ => false,
    };

    /// <summary>
    /// Check if the compilation of this output kind produces a .netmodule file.
    /// </summary>
    /// <param name="kind">The output kind of compilation.</param>
    /// <returns>Returns <see langword="true"/> if compilation produces a .netmodule file; otherwise, <see langword="false"/>.</returns>
    internal static bool IsNetModule(this OutputKind kind) => kind == OutputKind.NetModule;

    /// <summary>
    /// Check if the compilation of this output kind produces a .winmdobj file.
    /// </summary>
    /// <param name="kind">The output kind of compilation.</param>
    /// <returns>Returns <see langword="true"/> if compilation produces a .winmdobj file; otherwise, <see langword="false"/>.</returns>
    internal static bool IsWindowsRuntime(this OutputKind kind) => kind == OutputKind.WindowsRuntimeMetadata;

    /// <summary>
    /// Check if the compilation of this output kind produces an .out file which contains pre-compiled bytecodes for Lua virtual machine.
    /// </summary>
    /// <param name="kind">The output kind of compilation.</param>
    /// <returns>Returns <see langword="true"/> if compilation produces a .out file; otherwise, <see langword="false"/>.</returns>
    internal static bool IsLuaBytecodes(this OutputKind kind) => kind == OutputKind.LuaBytecodes;
}
