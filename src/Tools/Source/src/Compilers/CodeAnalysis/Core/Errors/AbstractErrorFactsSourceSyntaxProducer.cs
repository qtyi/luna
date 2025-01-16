// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Luna.Tools.ErrorFacts;

public abstract class AbstractErrorFactsSourceSyntaxProducer : ISourceSyntaxProducer
{
    protected const string Identifier_CodeAnalysis = "CodeAnalysis";
    protected const string Identifier_ErrorCode = "ErrorCode";
    protected const string Identifier_ErrorFacts = "ErrorFacts";
    protected const string Identifier_Qtyi = "Qtyi";

#pragma warning disable CS8618
    protected string _languageName;
    protected ImmutableDictionary<string, ImmutableArray<string>> _errorCodeNames;
    protected CancellationToken _cancellationToken;
#pragma warning restore CS8618

    protected abstract SyntaxTree Produce();

    #region ISourceSyntaxProducer
    void ISourceSyntaxProducer.Initialize(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    SyntaxTree ISourceSyntaxProducer.Produce(params object[] args)
    {
        Debug.Assert(args[0] is string, "Missing parameter for this language name.");
        Debug.Assert(args[1] is ImmutableDictionary<string, ImmutableArray<string>>, "Missing parameter for categorized ErrorCode names.");

        _languageName = (string)args[0];
        _errorCodeNames = (ImmutableDictionary<string, ImmutableArray<string>>)args[1];
        return Produce();
    }
    #endregion
}
