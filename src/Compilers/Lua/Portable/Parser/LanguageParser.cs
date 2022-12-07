// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using System.Xml.Linq;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

partial class LanguageParser
{
    internal ChunkSyntax ParseCompilationUnit()
    {
        var block = this.ParseBlock(SyntaxKind.Chunk);
        var endOfFile = this.EatToken(SyntaxKind.EndOfFileToken);
        return this._syntaxFactory.Chunk(block, endOfFile);
    }

#if TESTING
    internal
#else
    private
#endif
        BlockSyntax ParseBlock(SyntaxKind structure)
    {
        this._syntaxFactoryContext.EnterStructure(structure);

        var statementBuilder = this._pool.Allocate<StatementSyntax>();
        this.ParseStatements(statementBuilder);

        SyntaxList<StatementSyntax> statements;
        if (LanguageParser.IsLargeEnoughNonEmptyStatementList(statementBuilder))
            statements = new SyntaxList<StatementSyntax>(SyntaxList.List(((SyntaxListBuilder)statementBuilder).ToArray()));
        else
            statements = statementBuilder;

        this._syntaxFactoryContext.LeaveStructure(structure);

        var returnStat = this.CurrentTokenKind == SyntaxKind.ReturnKeyword ? this.ParseReturnStatement() : null;
        if (returnStat is not null)
        {
            // 跳过后方的连续的表示空语句的分号标志。
            var skippedSyntax = this.SkipTokens(token => token.Kind == SyntaxKind.SemicolonToken);
            if (skippedSyntax is not null)
                returnStat = this.AddTrailingSkippedSyntax(returnStat, skippedSyntax);
        }

        var block = this._syntaxFactory.Block(statements, returnStat);

        this._pool.Free(statementBuilder);
        return block;
    }

    internal static partial bool MatchesFactoryContext(GreenNode green, SyntaxFactoryContext context) =>
        green.ParsedInAsync == false && green.ParsedInQuery == false;

    // Is this statement list non-empty, and large enough to make using weak children beneficial?
    private static bool IsLargeEnoughNonEmptyStatementList(SyntaxListBuilder<StatementSyntax> statements)
    {
        if (statements.Count == 0)
            return false;
        else if (statements.Count == 1)
            // If we have a single statement, it might be small, like "return null", or large,
            // like a loop or if or switch with many statements inside. Use the width as a proxy for
            // how big it is. If it's small, its better to forgo a many children list anyway, since the
            // weak reference would consume as much memory as is saved.
            return statements[0]!.Width > 60;
        else
            // For 2 or more statements, go ahead and create a many-children lists.
            return true;
    }
}
