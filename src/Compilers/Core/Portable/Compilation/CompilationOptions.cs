// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents common compilation options.
/// </summary>
/// <inheritdoc/>
public abstract class CompilationOptions : Microsoft.CodeAnalysis.CompilationOptions
{
    /// <summary>
    /// Gets the source language.
    /// </summary>
    /// <inheritdoc/>
    public abstract override string Language { get; }

#pragma warning disable RS0030
    /// <inheritdoc cref="Microsoft.CodeAnalysis.CompilationOptions.OutputKind"/>
    public new OutputKind OutputKind
    {
        get => (OutputKind)base.OutputKind;
        protected set => base.OutputKind = (Microsoft.CodeAnalysis.OutputKind)value;
    }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.CompilationOptions.WithOutputKind(Microsoft.CodeAnalysis.OutputKind)"/>
    public CompilationOptions WithOutputKind(OutputKind kind) => CommonWithOutputKind(kind);

    protected sealed override Microsoft.CodeAnalysis.CompilationOptions CommonWithOutputKind(Microsoft.CodeAnalysis.OutputKind kind) => CommonWithOutputKind((OutputKind)kind);

    /// <inheritdoc cref="Microsoft.CodeAnalysis.CompilationOptions.CommonWithOutputKind(Microsoft.CodeAnalysis.OutputKind)"/>
    protected abstract CompilationOptions CommonWithOutputKind(OutputKind kind);
#pragma warning restore RS0030

    // Expects correct arguments.
    internal CompilationOptions(
        OutputKind outputKind,
        bool reportSuppressedDiagnostics,
        string? moduleName,
        string? mainTypeName,
        string? scriptClassName,
        string? cryptoKeyContainer,
        string? cryptoKeyFile,
        ImmutableArray<byte> cryptoPublicKey,
        bool? delaySign,
        bool publicSign,
        OptimizationLevel optimizationLevel,
        bool checkOverflow,
        Platform platform,
        ReportDiagnostic generalDiagnosticOption,
        int warningLevel,
        ImmutableDictionary<string, ReportDiagnostic> specificDiagnosticOptions,
        bool concurrentBuild,
        bool deterministic,
        DateTime currentLocalTime,
        bool debugPlusMode,
        XmlReferenceResolver? xmlReferenceResolver,
        SourceReferenceResolver? sourceReferenceResolver,
        SyntaxTreeOptionsProvider? syntaxTreeOptionsProvider,
        MetadataReferenceResolver? metadataReferenceResolver,
        AssemblyIdentityComparer? assemblyIdentityComparer,
        StrongNameProvider? strongNameProvider,
        MetadataImportOptions metadataImportOptions,
        bool referencesSupersedeLowerVersions) : base((Microsoft.CodeAnalysis.OutputKind)outputKind, reportSuppressedDiagnostics, moduleName, mainTypeName, scriptClassName, cryptoKeyContainer, cryptoKeyFile, cryptoPublicKey, delaySign, publicSign, optimizationLevel, checkOverflow, platform, generalDiagnosticOption, warningLevel, specificDiagnosticOptions, concurrentBuild, deterministic, currentLocalTime, debugPlusMode, xmlReferenceResolver, sourceReferenceResolver, syntaxTreeOptionsProvider, metadataReferenceResolver, assemblyIdentityComparer, strongNameProvider, metadataImportOptions, referencesSupersedeLowerVersions) { }
}
