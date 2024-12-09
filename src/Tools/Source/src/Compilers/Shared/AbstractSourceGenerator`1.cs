﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Diagnostics;

namespace Luna.Compilers.Generators;

/// <summary>
/// Represents an incremental source generator that is the base class of all Luna compilers generators.  This class is abstract.
/// </summary>
public abstract class AbstractSourceGenerator<TInputs> : IIncrementalGenerator
{
    private IncrementalValueProvider<string?> _thisLanguageNameProvider;
    protected IncrementalValueProvider<string?> ThisLanguageNameProvider => _thisLanguageNameProvider;

    /// <inheritdoc cref="IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext)"/>
    protected void Initialize(IncrementalGeneratorInitializationContext context)
    {
        _thisLanguageNameProvider = context.AnalyzerConfigOptionsProvider.Select((provider, _) =>
        {
            if (provider.GlobalOptions.TryGetValue("build_property.ThisLanguageName", out var thisLanguageName))
                return thisLanguageName;
            return null;
        });

        var inputs = GetRelevantInputs(context);
        context.RegisterSourceOutput(ThisLanguageNameProvider.Combine(inputs), (context, tuple) => ProduceSource(context, tuple.Left, tuple.Right));
    }

    /// <summary>
    /// Get a relevant series of files from <see cref="IncrementalGeneratorInitializationContext.AdditionalTextsProvider"/>.
    /// </summary>
    /// <param name="context">Incremental source generator context during initialization.</param>
    /// <returns>Returns a series of <see cref="AdditionalText"/> that should be the input of source production. Empty if there is nothing.</returns>
    protected abstract IncrementalValueProvider<TInputs> GetRelevantInputs(IncrementalGeneratorInitializationContext context);

    /// <summary>
    /// Produce source files by certain inputs.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="inputs">Inputs that provide information for source production.</param>
    protected abstract void ProduceSource(SourceProductionContext context, string? thisLanguageName, TInputs inputs);

#if DEBUG
    /// <summary>
    /// Gets a value indicate if we should attach debugger.
    /// </summary>
    protected virtual bool ShouldAttachDebugger => false;
#endif

    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (this.ShouldAttachDebugger && !Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif

        this.Initialize(context);
    }

    /// <summary>
    /// Produce source output and add to context.
    /// </summary>
    /// <typeparam name="TContext">Type of <paramref name="writeContext"/>.</typeparam>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="writeAction">Accept a <see cref="TextWriter"/> and write source codes to it.</param>
    /// <param name="writeContext">Context of write source codes for <paramref name="writeAction"/>.</param>
    /// <param name="hintName">An identifier that can be used to reference this source text, must be unique within this generator.</param>
    protected static void WriteAndAddSource<TContext>(SourceProductionContext context, Action<TextWriter, TContext> writeAction, TContext writeContext, string hintName)
    {
        // Write out the contents to a StringBuilder to avoid creating a single large string
        // in memory
        var stringBuilder = new StringBuilder();
        using (var textWriter = new StringWriter(stringBuilder))
        {
            writeAction(textWriter, writeContext);
        }

        // And create a SourceText from the StringBuilder, once again avoiding allocating a single massive string
        var sourceText = SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8);
        context.AddSource(hintName, sourceText);
    }
}
