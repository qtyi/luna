// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Luna.Tools;

internal static partial class DiagnosticDescriptors
{

#pragma warning disable RS1017

    public static DiagnosticDescriptor XmlException(DiagnosticSeverity severity) => new(
        id: CodeAnalysisSourceGeneratorsErrorCode.XmlException.GetDiagnosticId(),
        title: "Exception occurred during XML parsing",
        messageFormat: "{0}",
        category: "Luna.CodeAnalysis",
        defaultSeverity: severity,
        isEnabledByDefault: true
    );

#pragma warning restore RS1017

}
