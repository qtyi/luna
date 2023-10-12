// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators;

internal static class DiagnosticDescriptors
{
    public static DiagnosticDescriptor CreateMissingFile<TSourceGenerator>(string fileName)
        => new(
            "CASG1001",
            title: $"{fileName} is missing",
            messageFormat: $"The {fileName} file was not included in the project, so we are not generating source.",
            category: typeof(TSourceGenerator).Name,
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

    public static DiagnosticDescriptor CreateUnableToReadFile<TSourceGenerator>(string fileName)
        => new(
            "CASG1002",
            title: $"{fileName} could not be read",
            messageFormat: $"The {fileName} file could not even be read. Does it exist?",
            category: typeof(TSourceGenerator).Name,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    public static DiagnosticDescriptor CreateFileSyntaxError<TSourceGenerator>(string fileName)
        => new(
            "CASG1003",
            title: $"{fileName} has a syntax error",
            messageFormat: "{0}",
            category: typeof(TSourceGenerator).Name,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
}
