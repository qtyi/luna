// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Luna.Tools;

[Generator(LanguageNames.CSharp)]
public class ModifySourceGenerator : AbstractModificativeSourceGenerator
{
    protected override void Register(ModificativeSourceGeneratorRegistrationContext context)
    {
        context.RegisterFileName("BuildClient.cs", ProduceSource_BuildClient);
        context.RegisterFileName("BuildProtocol.cs", ProduceSource_BuildProtocol);
        context.RegisterFileName("BuildServerConnection.cs", ProduceSource_BuildServerConnection);
        context.RegisterFileName("CompilerServerLogger.cs", ProduceSource_CompilerServerLogger);
    }

    private void ProduceSource_BuildClient(SourceProductionContext context, string? thisLanguageName, ImmutableArray<SyntaxTree> inputs)
    {
        Debug.Assert(inputs.Length == 1);
        var tree = (CSharpSyntaxTree)inputs[0];

        if (tree is null)
            return;

        var BuildClient_Luna = ExtractType(tree, ["Microsoft.CodeAnalysis.CommandLine.BuildClient", "Microsoft.CodeAnalysis.CommandLine.CompileOnServerFunc"], out var BuildClient_Roslyn, context.CancellationToken);
        var mi = typeof(SyntaxFactory).GetMethods().Where(mi => mi.Name == nameof(SyntaxFactory.UsingDirective)).ToArray();
        BuildClient_Luna = Modify_BuildClient_Luna(BuildClient_Luna, context.CancellationToken);
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(BuildClient_Luna, context.CancellationToken), "BuildClient.Luna.cs");
        BuildClient_Roslyn = Modify_BuildClient_Roslyn(BuildClient_Roslyn, context.CancellationToken);
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(BuildClient_Roslyn, context.CancellationToken), "BuildClient.Roslyn.cs");

        context.RemoveSource(tree);
    }

    private CSharpSyntaxTree Modify_BuildClient_Luna(CSharpSyntaxTree old, CancellationToken cancellationToken)
    {
        var root = old.GetCompilationUnitRoot(cancellationToken);

        // Add using to `Microsoft.CodeAnalysis`.
        AppendNamespaceUsingDirective(ref root, ["Microsoft", "CodeAnalysis"]);

        // Add using to `Microsoft.CodeAnalysis.CommandLine`.
        AppendNamespaceUsingDirective(ref root, ["Microsoft", "CodeAnalysis", "CommandLine"]);

        // Replace `Microsoft` to `Qtyi` in namespace.
        ReplaceMicrosoftToQtyiInNamespaceDeclaration(ref root);

        return (CSharpSyntaxTree)root.SyntaxTree;
    }

    private CSharpSyntaxTree Modify_BuildClient_Roslyn(CSharpSyntaxTree old, CancellationToken cancellationToken)
    {
        var root = old.GetCompilationUnitRoot(cancellationToken);

        // Add using to `Qtyi.CodeAnalysis.CommandLine`.
        AppendNamespaceUsingDirective(ref root, ["Qtyi", "CodeAnalysis", "CommandLine"]);

        return (CSharpSyntaxTree)root.SyntaxTree;
    }

    private void ProduceSource_BuildProtocol(SourceProductionContext context, string? thisLanguageName, ImmutableArray<SyntaxTree> inputs)
    {
        Debug.Assert(inputs.Length == 1);
        var tree = (CSharpSyntaxTree)inputs[0];

        if (tree is null)
            return;

        var BuildProtocol_Luna = Modify_BuildProtocol_Luna(tree, context.CancellationToken);
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(BuildProtocol_Luna, context.CancellationToken), "BuildProtocol.Luna.cs");

        context.RemoveSource(tree);
    }

    private CSharpSyntaxTree Modify_BuildProtocol_Luna(CSharpSyntaxTree old, CancellationToken cancellationToken)
    {
        var root = old.GetCompilationUnitRoot(cancellationToken);

        // Add using to `Microsoft.CodeAnalysis`.
        AppendNamespaceUsingDirective(ref root, ["Microsoft", "CodeAnalysis"]);

        // Replace
        //   a) `Microsoft.CodeAnalysis.CommandLine.BuildProtocolConstants` to `Qtyi.CodeAnalysis.CommandLine.BuildProtocolConstants`
        //   b) `Microsoft.CodeAnalysis.CommandLine.CompilerServerLogger` to `Qtyi.CodeAnalysis.CommandLine.CompilerServerLogger`
        // in usings.
        {
            var tokensToReplace = root.Usings.Where(static usingDirective => usingDirective.NamespaceOrType.ToString() is "Microsoft.CodeAnalysis.CommandLine.BuildProtocolConstants" or "Microsoft.CodeAnalysis.CommandLine.CompilerServerLogger")
                                             .Select(usingDirective => usingDirective.DescendantTokens()
                                                                                     .Single(static token => token.ValueText == "Microsoft"));
            root = root.ReplaceTokens(tokensToReplace, (original, _) => SyntaxFactory.Identifier(original.LeadingTrivia, "Qtyi", original.TrailingTrivia));
        }

        // Replace `Microsoft` to `Qtyi` in namespace.
        ReplaceMicrosoftToQtyiInNamespaceDeclaration(ref root);

        var lunaLanguageNames = typeof(Qtyi.CodeAnalysis.LanguageNames).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                                                                       .Select(fi => fi.Name)
                                                                       .ToArray();

        // Append `[LangName]Compile = 0x????????` to Qtyi.CodeAnalysis.CommandLine.RequestLanguage.
        {
            var enumDecl = root.DescendantNodes(static node => !node.IsKind(SyntaxKind.EnumDeclaration))
                               .OfType<EnumDeclarationSyntax>()
                               .Single(static decl => decl.Identifier.ValueText == "RequestLanguage");
            (string name, int value) maxEnumMember = enumDecl.Members.OfType<EnumMemberDeclarationSyntax>()
                                                                     .Select(static decl => (decl.Identifier.ValueText, (int)((LiteralExpressionSyntax)decl.EqualsValue!.Value).Token.Value!))
                                                                     .OrderByDescending(static tuple => tuple.Item2)
                                                                     .First();
            var builder = ImmutableArray.CreateBuilder<EnumMemberDeclarationSyntax>();
            foreach (var nextName in lunaLanguageNames)
            {
                var nextValue = maxEnumMember.value + 1;
                var nextValueText = "0x" + nextValue.ToString("X");
                builder.Add(SyntaxFactory.EnumMemberDeclaration(attributeLists: default,
                    SyntaxFactory.Identifier(nextName + "Compile"),
                    SyntaxFactory.EqualsValueClause(
                        SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(nextValueText, nextValue))
                    ))
                );

                maxEnumMember = (nextName, nextValue);
            }
            root = root.ReplaceNode(enumDecl, enumDecl.WithMembers(SyntaxFactory.SeparatedList(builder.ToArray())));
        }

        // Replace all `RequestLanguage.CSharpCompile` to `RequestLanguage.LuaCompile`.
        {
            var tokensToReplace = root.DescendantNodes(static node => !node.IsKind(SyntaxKind.ClassDeclaration))
                                      .OfType<ClassDeclarationSyntax>()
                                      .SelectMany(decl => decl.DescendantNodes()
                                                              .OfType<MemberAccessExpressionSyntax>()
                                                              .Where(static access =>
                                                                  access.Expression is IdentifierNameSyntax { Identifier.ValueText: "RequestLanguage" } &&
                                                                  access.Name is IdentifierNameSyntax { Identifier.ValueText: "CSharpCompile" })
                                                              .Select(static access => ((IdentifierNameSyntax)access.Name).Identifier));
            root = root.ReplaceTokens(tokensToReplace, (original, _) => SyntaxFactory.Identifier(original.LeadingTrivia, lunaLanguageNames[0] + "Compile", original.TrailingTrivia));
        }

        return (CSharpSyntaxTree)root.SyntaxTree;
    }

    private void ProduceSource_BuildServerConnection(SourceProductionContext context, string? thisLanguageName, ImmutableArray<SyntaxTree> inputs)
    {
        Debug.Assert(inputs.Length == 1);
        var tree = (CSharpSyntaxTree)inputs[0];

        if (tree is null)
            return;

        var BuildServerConnection_Luna = ExtractType(tree, ["Microsoft.CodeAnalysis.CommandLine.BuildServerConnection", "Microsoft.CodeAnalysis.CommandLine.ServerFileMutex", "Microsoft.CodeAnalysis.CommandLine.ServerNamedMutex"], out var BuildServerConnection_Roslyn, context.CancellationToken);
        BuildServerConnection_Luna = Modify_BuildServerConnection_Luna(BuildServerConnection_Luna, context.CancellationToken);
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(BuildServerConnection_Luna, context.CancellationToken), "BuildServerConnection.Luna.cs");
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(BuildServerConnection_Roslyn, context.CancellationToken), "BuildServerConnection.Roslyn.cs");

        context.RemoveSource(tree);
    }

    private CSharpSyntaxTree Modify_BuildServerConnection_Luna(CSharpSyntaxTree old, CancellationToken cancellationToken)
    {
        var root = old.GetCompilationUnitRoot(cancellationToken);

        // Add using to `Microsoft.CodeAnalysis`.
        AppendNamespaceUsingDirective(ref root, ["Microsoft", "CodeAnalysis"]);

        // Add using to `Microsoft.CodeAnalysis.CommandLine`.
        AppendNamespaceUsingDirective(ref root, ["Microsoft", "CodeAnalysis", "CommandLine"]);

        // Replace `Microsoft` to `Qtyi` in namespace.
        ReplaceMicrosoftToQtyiInNamespaceDeclaration(ref root);

        // Replace all `Roslyn.sln` to `Luna.sln` in tokens.
        {
            var tokensToReplace = root.Members.SelectMany(static member => member.DescendantTokens(descendIntoTrivia: true)
                                                                                 .Where(token => token.Text.Contains("Roslyn.sln") || token.Text.Contains(".roslyn")));
            root = root.ReplaceTokens(tokensToReplace, (original, _) => SyntaxFactory.Token(original.LeadingTrivia, original.Kind(), original.Text.Replace("Roslyn", "Luna").Replace(".roslyn", ".luna"), original.Value is string ? original.ValueText.Replace("Roslyn.sln", "Luna.sln").Replace(".roslyn", ".luna") : original.ValueText, original.TrailingTrivia));
        }

        return (CSharpSyntaxTree)root.SyntaxTree;
    }

    private void ProduceSource_CompilerServerLogger(SourceProductionContext context, string? thisLanguageName, ImmutableArray<SyntaxTree> inputs)
    {
        Debug.Assert(inputs.Length == 1);
        var tree = (CSharpSyntaxTree)inputs[0];

        if (tree is null)
            return;

        var CompilerServerLogger_Luna = ExtractType(tree, ["Microsoft.CodeAnalysis.CommandLine.CompilerServerLogger", "Microsoft.CodeAnalysis.CommandLine.EmptyCompilerServerLogger"], out var CompilerServerLogger_Roslyn, context.CancellationToken);
        CompilerServerLogger_Luna = Modify_CompilerServerLogger_Luna(CompilerServerLogger_Luna, context.CancellationToken);
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(CompilerServerLogger_Luna, context.CancellationToken), "CompilerServerLogger.Luna.cs");
        WriteAndAddSource(context, WriteSyntaxTree, new ModifySourceProductionContext(CompilerServerLogger_Roslyn, context.CancellationToken), "CompilerServerLogger.Roslyn.cs");

        context.RemoveSource(tree);
    }

    private CSharpSyntaxTree Modify_CompilerServerLogger_Luna(CSharpSyntaxTree old, CancellationToken cancellationToken)
    {
        var root = old.GetCompilationUnitRoot(cancellationToken);

        // Add using to `Microsoft.CodeAnalysis.CommandLine`.
        AppendNamespaceUsingDirective(ref root, ["Microsoft", "CodeAnalysis", "CommandLine"]);

        // Replace `Microsoft` to `Qtyi` in namespace.
        ReplaceMicrosoftToQtyiInNamespaceDeclaration(ref root);

        // Replace all `Roslyn` to `Luna` in tokens.
        {
            var tokensToReplace = root.Members.SelectMany(static member => member.DescendantTokens(descendIntoTrivia: true)
                                                                                 .Where(token => token.Text.Contains("Roslyn")));
            root = root.ReplaceTokens(tokensToReplace, (original, _) => SyntaxFactory.Token(original.LeadingTrivia, original.Kind(), original.Text.Replace("Roslyn", "Luna"), original.Value is string ? original.ValueText.Replace("Roslyn", "Luna") : original.ValueText, original.TrailingTrivia));
        }

        return (CSharpSyntaxTree)root.SyntaxTree;
    }

    private static NameSyntax? GetNamespaceName(IEnumerable<string> namespaceParts)
    {
        NameSyntax? nsName = null;
        foreach (var part in namespaceParts)
        {
            if (nsName is null)
                nsName = SyntaxFactory.IdentifierName(part);
            else
                nsName = SyntaxFactory.QualifiedName(nsName, SyntaxFactory.IdentifierName(part));
        }
        return nsName;
    }

    private static void AppendNamespaceUsingDirective(ref CompilationUnitSyntax root, params string[] namespaceParts)
    {
        root = root.AddUsings(SyntaxFactory.UsingDirective(namespaceOrType:
            GetNamespaceName(namespaceParts)!));
    }

    private static void ReplaceMicrosoftToQtyiInNamespaceDeclaration(ref CompilationUnitSyntax root)
    {
        var tokensToReplace = root.DescendantNodes(static node => !node.IsKind(SyntaxKind.NamespaceDeclaration))
                                  .OfType<NamespaceDeclarationSyntax>()
                                  .SelectMany(decl => decl.Name.DescendantNodesAndSelf()
                                                               .OfType<IdentifierNameSyntax>()
                                                               .Select(static identifierName => identifierName.Identifier)
                                                               .Where(static identifier => identifier.ValueText == "Microsoft"));
        root = root.ReplaceTokens(tokensToReplace, (original, _) => SyntaxFactory.Identifier(original.LeadingTrivia, "Qtyi", original.TrailingTrivia));
    }

    private static void WriteSyntaxTree(TextWriter writer, ModifySourceProductionContext context)
    {
        var root = context.SyntaxTree.GetRoot(context.CancellationToken);
        root = root.NormalizeWhitespace();
        writer.Write(root.GetText());
    }

    private CSharpSyntaxTree ExtractType(CSharpSyntaxTree old, string[] fullyQualifiedMetadataNames, out CSharpSyntaxTree modified, CancellationToken cancellationToken)
    {
        var root = old.GetCompilationUnitRoot(cancellationToken);

        var dic = ImmutableDictionary.CreateBuilder<INamedTypeSymbol, MemberDeclarationSyntax[]>();
        var compilation = CSharpCompilation.Create(assemblyName: null, syntaxTrees: [old]);
        var semanticModel = compilation.GetSemanticModel(old);
        foreach (var typeName in fullyQualifiedMetadataNames)
        {
            var typeToExtract = compilation.GetTypeByMetadataName(typeName);
            if (typeToExtract is null || typeToExtract.ContainingType is not null)
                continue;
            dic.Add(typeToExtract, typeToExtract.DeclaringSyntaxReferences.Select(syntaxRef => (MemberDeclarationSyntax)syntaxRef.GetSyntax(cancellationToken)).Where(node => node.SyntaxTree == old).ToArray());
        }

        modified = (CSharpSyntaxTree)root.RemoveNodes(dic.Values.SelectMany(nodes => nodes), SyntaxRemoveOptions.KeepEndOfLine)!.SyntaxTree;

        var newRoot = SyntaxFactory.CompilationUnit(externs: root.Externs, usings: root.Usings, attributeLists: default, members: default);
        var memberBuilder = ImmutableArray.CreateBuilder<MemberDeclarationSyntax>();
        foreach (var typesGroupedByNamespace in dic.Keys.GroupBy(type => type.ContainingNamespace))
        {
            Stack<string> stack = new();
            for (var ns = typesGroupedByNamespace.Key; !ns.IsGlobalNamespace; ns = ns.ContainingNamespace)
            {
                stack.Push(ns.Name);
            }

            NameSyntax? nsName = GetNamespaceName(stack);

            var decls = typesGroupedByNamespace.SelectMany(type => dic[type]).ToArray();
            if (nsName is null)
            {
                memberBuilder.AddRange(decls);
            }
            else
            {
                memberBuilder.Add(SyntaxFactory.NamespaceDeclaration(name: nsName, externs: default, usings: default, members: SyntaxFactory.List(decls)));
            }
        }

        if (memberBuilder.Count != 0)
        {
            newRoot = newRoot.AddMembers(memberBuilder.ToArray());
        }

        return (CSharpSyntaxTree)newRoot.SyntaxTree;
    }
}

internal readonly struct ModifySourceProductionContext
{
    public readonly CSharpSyntaxTree SyntaxTree;
    public readonly CancellationToken CancellationToken;

    public ModifySourceProductionContext(CSharpSyntaxTree syntaxTree, CancellationToken cancellationToken)
    {
        SyntaxTree = syntaxTree;
        CancellationToken = cancellationToken;
    }
}
