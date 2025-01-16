// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators;

/// <summary>
/// Helper class to create <see cref="DiagnosticDescriptor"/>s.
/// </summary>
internal static class DiagnosticDescriptors
{
    /// <summary>
    /// Create a description about missing-file diagnostic.
    /// </summary>
    /// <typeparam name="TSourceGenerator">Type of source generator.</typeparam>
    /// <param name="fileName">Name of the missing file.</param>
    /// <returns>A description about missing-file diagnostic.</returns>
    public static DiagnosticDescriptor CreateMissingFile<TSourceGenerator>(string fileName)
        => new(
            "CASG1001",
            title: $"{fileName} is missing",
            messageFormat: $"The {fileName} file was not included in the project, so we are not generating source.",
            category: typeof(TSourceGenerator).Name,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

    /// <summary>
    /// Create a description about unable-to-read-file diagnostic.
    /// </summary>
    /// <typeparam name="TSourceGenerator">Type of source generator.</typeparam>
    /// <param name="fileName">Name of the unable-to-read file.</param>
    /// <returns>A description about unable-to-read file diagnostic.</returns>
    public static DiagnosticDescriptor CreateUnableToReadFile<TSourceGenerator>(string fileName)
        => new(
            "CASG1002",
            title: $"{fileName} could not be read",
            messageFormat: $"The {fileName} file could not even be read. Does it exist?",
            category: typeof(TSourceGenerator).Name,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    /// <summary>
    /// Create a description about file-has-syntax-error diagnostic.
    /// </summary>
    /// <typeparam name="TSourceGenerator">Type of source generator.</typeparam>
    /// <param name="fileName">Name of the syntax-error file.</param>
    /// <returns>A description about syntax-error file diagnostic.</returns>
    public static DiagnosticDescriptor CreateFileSyntaxError<TSourceGenerator>(string fileName)
        => new(
            "CASG1003",
            title: $"{fileName} has a syntax error",
            messageFormat: "{0}",
            category: typeof(TSourceGenerator).Name,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
}
