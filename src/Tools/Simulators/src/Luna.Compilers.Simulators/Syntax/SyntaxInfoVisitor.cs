// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Luna.Compilers.Simulators.Syntax;

public abstract class SyntaxInfoVisitor
{
    private readonly bool _visitIntoStructuredTrivia;

    protected SyntaxInfoVisitor(bool visitIntoStructuredTrivia)
    {
        this._visitIntoStructuredTrivia = visitIntoStructuredTrivia;
    }

    public void Visit(SyntaxNodeOrTokenOrTriviaInfo info)
    {
        if (info.IsNode)
        {
            var nodeInfo = (SyntaxNodeInfo)info;
            this.VisitNode(nodeInfo);
            this.VisitList(nodeInfo.ChildNodesAndTokens);
        }
        else if (info.IsToken)
        {
            var tokenInfo = (SyntaxTokenInfo)info;
            this.VisitToken(tokenInfo);
            this.VisitList(tokenInfo.LeadingTrivia);
            this.VisitList(tokenInfo.TrailingTrivia);
        }
        else if (info.IsTrivia)
        {
            var triviaInfo = (SyntaxTriviaInfo)info;
            this.VisitTrivia(triviaInfo);
            if (this._visitIntoStructuredTrivia && triviaInfo.HasStructure)
                this.Visit(triviaInfo.Structure);
        }
        else
            throw ExceptionUtilities.Unreachable();
    }

    private void VisitList<T>(IReadOnlyList<T> list) where T : SyntaxNodeOrTokenOrTriviaInfo
    {
        foreach (var child in list)
            this.Visit(child);
    }

    protected virtual void VisitNode(SyntaxNodeInfo info) => this.DefaultVisit();

    protected virtual void VisitToken(SyntaxTokenInfo info) => this.DefaultVisit();

    protected virtual void VisitTrivia(SyntaxTriviaInfo info) => this.DefaultVisit();

    protected virtual void DefaultVisit() { }
}
