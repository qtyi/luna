// Licensed to the Qtyi under one or more agreements.
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
public abstract class AbstractSourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (this.ShouldAttachDebugger && !Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif

        this.InitializeCore(context);
    }

    /// <inheritdoc cref="Initialize(IncrementalGeneratorInitializationContext)"/>
    /// <remarks>Other than <see cref="Initialize(IncrementalGeneratorInitializationContext)"/> which contains codes to attach debugger, this method do the actual initialize job.</remarks>
    protected abstract void InitializeCore(IncrementalGeneratorInitializationContext context);

#if DEBUG
    /// <summary>
    /// Gets a value indicate if we should attach debugger.
    /// </summary>
    protected virtual bool ShouldAttachDebugger => false;
#endif

    /// <summary>
    /// Produce source output and add to context.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="writeAction">Accept a <see cref="TextWriter"/> and write source codes to it.</param>
    /// <param name="hintName">An identifier that can be used to reference this source text, must be unique within this generator.</param>
    protected static void WriteAndAddSource(SourceProductionContext context, Action<TextWriter, CancellationToken> writeAction, string hintName)
    {
        // Write out the contents to a StringBuilder to avoid creating a single large string
        // in memory
        var stringBuilder = new StringBuilder();
        using (var textWriter = new StringWriter(stringBuilder))
        {
            writeAction(textWriter, context.CancellationToken);
        }

        // And create a SourceText from the StringBuilder, once again avoiding allocating a single massive string
        var sourceText = SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8);
        context.AddSource(hintName, sourceText);
    }

    /// <inheritdoc cref="WriteAndAddSource(SourceProductionContext, Action{TextWriter, CancellationToken}, string)"/>
    /// <param name="arg">The parameter of the method that <paramref name="writeAction"/> encapsulates.</param>
    protected static void WriteAndAddSource<T>(SourceProductionContext context, Action<TextWriter, T, CancellationToken> writeAction, T arg, string hintName)
    {
        // Write out the contents to a StringBuilder to avoid creating a single large string
        // in memory
        var stringBuilder = new StringBuilder();
        using (var textWriter = new StringWriter(stringBuilder))
        {
            writeAction(textWriter, arg, context.CancellationToken);
        }

        // And create a SourceText from the StringBuilder, once again avoiding allocating a single massive string
        var sourceText = SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8);
        context.AddSource(hintName, sourceText);
    }

    /// <inheritdoc cref="WriteAndAddSource(SourceProductionContext, Action{TextWriter, CancellationToken}, string)"/>
    /// <param name="arg1">The first parameter of the method that <paramref name="writeAction"/> encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that <paramref name="writeAction"/> encapsulates.</param>
    protected static void WriteAndAddSource<T1, T2>(SourceProductionContext context, Action<TextWriter, T1, T2, CancellationToken> writeAction, T1 arg1, T2 arg2, string hintName)
    {
        // Write out the contents to a StringBuilder to avoid creating a single large string
        // in memory
        var stringBuilder = new StringBuilder();
        using (var textWriter = new StringWriter(stringBuilder))
        {
            writeAction(textWriter, arg1, arg2, context.CancellationToken);
        }

        // And create a SourceText from the StringBuilder, once again avoiding allocating a single massive string
        var sourceText = SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8);
        context.AddSource(hintName, sourceText);
    }
}
