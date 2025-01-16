﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Syntax;

namespace Luna.Tools.LexerCache;

using static SyntaxFactory;

public sealed class LexerCacheSourceSyntaxProvider : AbstractLexerCacheSourceSyntaxProvider
{
    private const string EOL = SyntaxNodeExtensions.DefaultEOL;
    private const string Indentation = SyntaxNodeExtensions.DefaultIndentation;

    protected override SyntaxTree Produce()
    {
        var root = CompilationUnit(
            externs: default,
            usings: SingletonList(UsingDirective(
                name: QualifiedName(QualifiedName(
                    IdentifierName(Identifier_System),
                    IdentifierName(Identifier_Collections)),
                    IdentifierName(Identifier_Immutable)))),
            attributeLists: default,
            members: SingletonList<MemberDeclarationSyntax>(ProduceNamespace()))
            .WithLeadingTrivia(
                Comment("// <auto-generated />"))
            .NormalizeWhitespace(indentation: Indentation, eol: EOL);

        return CSharpSyntaxTree.Create(root, encoding: Encoding.UTF8);
    }

    // namespace Qtyi.CodeAnalysis.[This].Syntax.InternalSyntax
    private FileScopedNamespaceDeclarationSyntax ProduceNamespace()
    {
        return FileScopedNamespaceDeclaration(
            attributeLists: default,
            modifiers: default,
            name: QualifiedName(QualifiedName(QualifiedName(QualifiedName(
                IdentifierName(Identifier_Qtyi),
                IdentifierName(Identifier_CodeAnalysis)),
                IdentifierName(_languageName)),
                IdentifierName(Identifier_Syntax)),
                IdentifierName(Identifier_InternalSyntax)),
            externs: default,
            usings: default,
            members: SingletonList<MemberDeclarationSyntax>(ProduceClass()));
    }

    // partial class LexerCache
    private ClassDeclarationSyntax ProduceClass()
    {
        var group = (from name in _keywordKindNames
                     let text = CaseInsensitiveComparison.ToLower(name[..^LexerCacheGenerator.KeywordKindNamePostfix.Length])
                     group (KindName: name, KeywordText: text) by text.Length
                     into g
                     orderby g.Key descending
                     select g)
                    .First();
        var length = group.Key;
        var tuples = group.ToArray();

        _cancellationToken.ThrowIfCancellationRequested();

        return ClassDeclaration(
            attributeLists: default,
            modifiers: TokenList(Token(SyntaxKind.PartialKeyword)),
            identifier: Identifier(Identifier_LexerCache),
            typeParameterList: null,
            baseList: null,
            constraintClauses: default,
            members: ProduceFields(length, tuples));
    }

    private static SyntaxList<MemberDeclarationSyntax> ProduceFields(int length, (string KindName, string KeywordText)[] tuples)
    {
        var builder = SyntaxListBuilder<MemberDeclarationSyntax>.Create();

        // /// <summary>Max keyword length. (...)</summary>
        // internal const int MaxKeywordLength = ...;
        {
            // Documentation comment
            var xmlBuilder = SyntaxListBuilder<XmlNodeSyntax>.Create();
            {
                xmlBuilder.Add(XmlText("Max keyword length. ("));
                bool first = true;
                foreach (var tuple in tuples)
                {
                    if (first)
                        first = false;
                    else
                        xmlBuilder.Add(XmlText(", "));

                    xmlBuilder.Add(XmlElement("c", SingletonList<XmlNodeSyntax>(XmlText(tuple.KeywordText))));
                }
                xmlBuilder.Add(XmlText(")"));
            }
            var trivia = Trivia(DocumentationComment(
                XmlElement("summary", xmlBuilder.ToList()),
                XmlText(EOL + Indentation)
            ));

            // Field declaration
            builder.Add(FieldDeclaration(
                attributeLists: default,
                modifiers: TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ConstKeyword)),
                declaration: VariableDeclaration(
                    type: PredefinedType(Token(SyntaxKind.IntKeyword)),
                    variables: SingletonSeparatedList(VariableDeclarator(
                        identifier: Identifier("MaxKeywordLength"),
                        argumentList: null,
                        initializer: EqualsValueClause(
                            value: LiteralExpression(
                                kind: SyntaxKind.NumericLiteralExpression,
                                token: Literal(length)))))))
            .WithLeadingTrivia(trivia));
        }

        // #if DEBUG
        // /// <summary>Max keyword syntax kinds.</summary>
        //     internal static readonly ImmutableArray<SyntaxKind> MaxKeywordKinds = [...];
        // #endif
        {
            // Field declaration
            var syntaxBuilder = SeparatedSyntaxListBuilder<CollectionElementSyntax>.Create();
            {
                var first = true;
                foreach (var tuple in tuples)
                {
                    if (first)
                        first = false;
                    else
                        syntaxBuilder.AddSeparator(Token(SyntaxKind.CommaToken));

                    syntaxBuilder.Add(ExpressionElement(
                        QualifiedName(IdentifierName(Identifier_SyntaxKind), IdentifierName(tuple.KindName))));
                }
            }
            builder.Add(FieldDeclaration(
                attributeLists: default,
                modifiers: TokenList(
                    Token(SyntaxKind.InternalKeyword),
                    Token(SyntaxKind.StaticKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword)),
                declaration: VariableDeclaration(
                    type: GenericName(
                        identifier: Identifier("ImmutableArray"),
                        typeArgumentList: TypeArgumentList(SingletonSeparatedList<TypeSyntax>(
                            IdentifierName(Identifier_SyntaxKind)))),
                    variables: SingletonSeparatedList(VariableDeclarator(
                        identifier: Identifier("MaxKeywordKinds"),
                        argumentList: null,
                        initializer: EqualsValueClause(
                            value: CollectionExpression(
                                syntaxBuilder.ToList()))))))
            .WithLeadingTrivia(
                // If directive
                Trivia(IfDirectiveTrivia(
                condition: IdentifierName("DEBUG"),
                isActive: true, // No matter.
                branchTaken: true, // No matter.
                conditionValue: true // No matter.
                )),
                // Documentation comment
                Trivia(DocumentationComment(
                    XmlElement("summary", SingletonList<XmlNodeSyntax>(XmlText("Max keyword syntax kinds."))),
                    XmlText(EOL + Indentation)
                ))
            )
            .WithTrailingTrivia(
                // EndIf directive
                Trivia(EndIfDirectiveTrivia(
                isActive: true
                ))
            ));
        }

        return builder.ToList();
    }
}
