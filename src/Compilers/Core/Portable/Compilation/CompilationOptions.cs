// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents common compilation options.
/// </summary>
public abstract class CompilationOptions : Microsoft.CodeAnalysis.CompilationOptions
{
    /// <summary>
    /// Gets a kind of assembly generated when emitted.
    /// </summary>
    /// <value>
    /// The kind of assembly generated when emitted.
    /// </value>
    public new OutputKind OutputKind
    {
#pragma warning disable RS0030
        get => (OutputKind)base.OutputKind;
        protected set => base.OutputKind = (Microsoft.CodeAnalysis.OutputKind)value;
#pragma warning restore RS0030
    }

    /// <summary>
    /// Gets a name of the primary module.
    /// </summary>
    /// <value>
    /// Name of the primary module, or <see langword="null"/> if a default name should be used.
    /// </value>
    /// <remarks>
    /// The name usually (but not necessarily) includes an extension, e.g. "MyNetmodule.dll".
    /// 
    /// If <see cref="NetmoduleName"/> is <see langword="null"/> the actual name written to metadata
    /// is derived from the name of the compilation (<see cref="Compilation.AssemblyName"/>)
    /// by appending a default extension for <see cref="OutputKind"/>.
    /// </remarks>
    public string? NetmoduleName
    {
#pragma warning disable RS0030
        get => base.ModuleName;
        protected set => base.ModuleName = value;
#pragma warning restore RS0030
    }

    /// <summary>
    /// The full name of a global implicit module (script module). This module implicitly encapsulates top-level statements and member declarations.
    /// </summary>
    public string? ScriptModuleName
    {
#pragma warning disable RS0030
        get => base.ScriptClassName;
        protected set => base.ScriptClassName = value;
#pragma warning restore RS0030
    }

    /// <summary>
    /// The full name of a module that will be the entry point (containing a implicit Main method).
    /// <see langword="null"/> if any module is a candidate for containing an entry point.
    /// </summary>
    public string? MainModuleName
    {
#pragma warning disable RS0030
        get => base.MainTypeName;
        protected set => base.MainTypeName = value;
#pragma warning restore RS0030
    }

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

    #region Options
    #region OutputKind
    public CompilationOptions WithOutputKind(OutputKind kind) => this.CommonWithOutputKind(kind);

    protected abstract CompilationOptions CommonWithOutputKind(OutputKind kind);

    protected sealed override Microsoft.CodeAnalysis.CompilationOptions CommonWithOutputKind(Microsoft.CodeAnalysis.OutputKind kind) => this.CommonWithOutputKind((OutputKind)kind);
    #endregion

    #region NetmoduleName
    public CompilationOptions WithNetmoduleName(string? netmoduleName) => this.CommonWithNetmoduleName(netmoduleName);

    protected abstract CompilationOptions CommonWithNetmoduleName(string? netmoduleName);

    protected sealed override Microsoft.CodeAnalysis.CompilationOptions CommonWithModuleName(string? moduleName) => this.CommonWithNetmoduleName(moduleName);
    #endregion

    #region ScriptModuleName
    public CompilationOptions WithScriptModuleName(string? scriptModuleName) => this.CommonWithScriptModuleName(scriptModuleName);

    protected abstract CompilationOptions CommonWithScriptModuleName(string? scriptModuleName);

    protected sealed override Microsoft.CodeAnalysis.CompilationOptions CommonWithScriptClassName(string? scriptClassName) => this.CommonWithScriptModuleName(scriptClassName);
    #endregion

    #region MainModuleName
    public CompilationOptions WithMainModuleName(string? mainModuleName) => this.CommonWithMainModuleName(mainModuleName);

    protected abstract CompilationOptions CommonWithMainModuleName(string? mainModuleName);

    protected sealed override Microsoft.CodeAnalysis.CompilationOptions CommonWithMainTypeName(string? mainTypeName) => this.CommonWithMainModuleName(mainTypeName);
    #endregion
    #endregion
}
