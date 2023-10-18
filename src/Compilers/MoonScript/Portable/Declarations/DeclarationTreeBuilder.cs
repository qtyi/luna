// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.MoonScript;

using Roslyn.Utilities;
using Syntax;

partial class DeclarationTreeBuilder
{
    private partial ModuleDeclaration CreateRootDeclaration(ChunkSyntax chunk)
    {
        var builder = ArrayBuilder<ClassDeclaration>.GetInstance();
        foreach (var node in chunk.DescendantNodes())
        {
            var result = this.Visit(node);
            if (result is ClassDeclaration subClass)
                builder.Add(subClass);
        }

        return new(
        name: this._scriptModuleName,
        syntaxReference: this._syntaxTree.GetReference(chunk),
        children: builder.ToImmutableAndFree(),
        diagnostics: ImmutableArray<Diagnostic>.Empty);
    }

    public override Declaration VisitClassStatement(ClassStatementSyntax node) => new ClassDeclaration(
        name: node.Name.Identifier.ValueText,
        syntaxReference: this._syntaxTree.GetReference(node),
        nameLocation: new(node.Name),
        memberNames: this.GetMemberNames(node.Statements),
        diagnostics: ImmutableArray<Diagnostic>.Empty);

    public override Declaration? VisitClassExpression(ClassExpressionSyntax node) => new ClassDeclaration(
        name: node.Name.Identifier.ValueText,
        syntaxReference: this._syntaxTree.GetReference(node),
        nameLocation: new(node.Name),
        memberNames: this.GetMemberNames(node.Statements),
        diagnostics: ImmutableArray<Diagnostic>.Empty);

    public override Declaration? VisitAnomymousClassExpression(AnomymousClassExpressionSyntax node) => new ClassDeclaration(
        name: "",
        syntaxReference: this._syntaxTree.GetReference(node),
        nameLocation: null,
        memberNames: this.GetMemberNames(node.Statements),
        diagnostics: ImmutableArray<Diagnostic>.Empty);

    private static readonly ObjectPool<ImmutableSegmentedDictionary<string, VoidResult>.Builder> s_memberNameBuilderPool =
        new(ImmutableSegmentedDictionary.CreateBuilder<string, VoidResult>);

    private static ImmutableSegmentedDictionary<string, VoidResult> ToImmutableAndFree(ImmutableSegmentedDictionary<string, VoidResult>.Builder builder)
    {
        var result = builder.ToImmutable();
        builder.Clear();
        s_memberNameBuilderPool.Free(builder);
        return result;
    }

    /// <summary>
    /// Get member names for class declaration from a list of Statement syntax.
    /// </summary>
    private ImmutableSegmentedDictionary<string, VoidResult> GetMemberNames(SyntaxList<StatementSyntax> statements)
    {
        var memberNamesBuilder = DeclarationTreeBuilder.s_memberNameBuilderPool.Allocate();

        foreach (var statement in statements)
        {
            if (statement is not MemberStatementSyntax memberStatement) continue;

            memberNamesBuilder.TryAdd(memberStatement.NameColon.Name.Identifier.ValueText);
        }

        return DeclarationTreeBuilder.ToImmutableAndFree(memberNamesBuilder);
    }
}
