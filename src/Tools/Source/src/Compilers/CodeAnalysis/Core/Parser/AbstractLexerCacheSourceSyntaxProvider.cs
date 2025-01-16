// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Luna.Tools.LexerCache;

public abstract class AbstractLexerCacheSourceSyntaxProvider : ISourceSyntaxProducer
{
    protected const string Identifier_CodeAnalysis = "CodeAnalysis";
    protected const string Identifier_Collections = "Collections";
    protected const string Identifier_Immutable = "Immutable";
    protected const string Identifier_InternalSyntax = "InternalSyntax";
    protected const string Identifier_LexerCache = "LexerCache";
    protected const string Identifier_Qtyi = "Qtyi";
    protected const string Identifier_Syntax = "Syntax";
    protected const string Identifier_SyntaxKind = "SyntaxKind";
    protected const string Identifier_System = "System";

#pragma warning disable CS8618
    protected string _languageName;
    protected ImmutableArray<string> _keywordKindNames;
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
        Debug.Assert(args[1] is ImmutableArray<string>, "Missing parameter for keyword SyntaxKind names.");

        _languageName = (string)args[0];
        _keywordKindNames = (ImmutableArray<string>)args[1];
        return Produce();
    }
    #endregion
}
