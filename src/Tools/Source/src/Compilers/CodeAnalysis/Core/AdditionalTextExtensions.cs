// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Luna.Tools;

namespace Microsoft.CodeAnalysis
{
    internal static class AdditionalTextExtensions
    {
        public static SourceText? GetTextOrReportDiagnostic(this AdditionalText additionalText, DiagnosticBag diagnostics, CancellationToken cancellationToken = default)
        {
            var text = additionalText.GetText(cancellationToken);

            if (text is null)
            {
                diagnostics.Add(Diagnostic.Create(DiagnosticDescriptors.FileCannotRead, null, additionalText.Path));
            }

            return text;
        }
    }
}

namespace Luna.Tools
{
    internal static partial class DiagnosticDescriptors
    {

#pragma warning disable RS1017

        public static readonly DiagnosticDescriptor FileCannotRead = new(
            id: CodeAnalysisSourceGeneratorsErrorCode.FileCannotRead.GetDiagnosticId(),
            title: "Cannot read file",
            messageFormat: "Cannot read file '{0}'.",
            category: "Luna.CodeAnalysis",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

#pragma warning restore RS1017
    }
}
