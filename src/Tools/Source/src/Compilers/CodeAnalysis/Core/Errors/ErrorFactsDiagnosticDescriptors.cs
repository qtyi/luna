// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Tools;

internal static partial class DiagnosticDescriptors
{

#pragma warning disable RS1017

    public static readonly DiagnosticDescriptor UncategorizableErrorCodeField = new(
        id: CodeAnalysisSourceGeneratorsErrorCode.UncategorizableErrorCodeField.GetDiagnosticId(),
        title: "Error code enum field should be categorizable",
        messageFormat: "Name of field '{0}' should starts with 'ERR_', 'WRN_', 'FTL_', 'INF_' or 'HDN_'.",
        category: "Luna.CodeAnalysis",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );

#pragma warning restore RS1017

}
