// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if false

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Luna.Tools;

[Generator]
public sealed class DiagnosticDescriptionGenerator : AbstractTestResourcesGenerator
{
    protected override string GeneratorName { get; } = nameof(DiagnosticDescriptionGenerator);

    protected override IncrementalValueProvider<ParseOptions> GetRelevantInput(IncrementalGeneratorInitializationContext context) => context.ParseOptionsProvider;

#if DEBUG
    protected override bool ShouldAttachDebugger => false;
#endif

    protected override void GenerateOutput(
        SourceProductionContext context,
        ImmutableArray<AdditionalText> texts,
        ParseOptions options)
    {
        Debug.Assert(texts.Length == 1);
        var text = texts[0].GetText(context.CancellationToken);
        Debug.Assert(text is not null);
        var tree = SyntaxFactory.ParseSyntaxTree(
            text!,
            options: options,
            path: texts[0].Path,
            cancellationToken: context.CancellationToken);
        var root = tree.GetRoot(context.CancellationToken);

        root = ReplaceRoot(root, options);
        tree = tree.WithRootAndOptions(root, options);

        WriteAndAddSource(context, static (writer, tree, _) => writer.Write(tree.ToString()), tree, "DiagnosticDescription.g.cs");
    }

    private static readonly string[] s_lunaLanguageNames = typeof(Qtyi.CodeAnalysis.LanguageNames).GetFields().Where(fi => fi.FieldType == typeof(string) && fi.IsLiteral).Select(fi => (string)fi.GetRawConstantValue()).ToArray();

    private SyntaxNode ReplaceRoot(SyntaxNode root, ParseOptions options)
    {
        var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().Where(static n => n.Identifier.Text == "GetAssertText").Single();
        var stats = method.Body!.Statements;

        AssertLanguageConst(stats[0], "CSharp", "int", "1");
        AssertLanguageConst(stats[1], "VisualBasic", "int", "2");

        // Fix `language` local variable declaration statement.
        var declStat_language = stats[2] as LocalDeclarationStatementSyntax;
        Debug.Assert(declStat_language is not null);
        var decl_language = declStat_language!.Declaration.Variables.FirstOrDefault(n => n.Identifier.Text == "language");
        Debug.Assert(decl_language is not null);
        var declValue_language = SyntaxFactory.ParseExpression("actual.FirstOrDefault() switch { " + string.Join(", ",
            new (string DiagnosticTypeName, string LanguageName)[]
            {
                ("Microsoft.CodeAnalysis.CSharp.CSDiagnostic", "CSharp"),
                ("Microsoft.CodeAnalysis.VisualBasic.VBDiagnostic", "VisualBasic")
            }
            .Concat(s_lunaLanguageNames.Select(name => (DiagnosticTypeName: $"Qtyi.CodeAnalysis.{name}.{name}Diagnostic", LanguageName: name)))
                .Select(static pair => $"{pair.DiagnosticTypeName} => {pair.LanguageName}")
        ) + ", _ => 0 }", options: options);
        var declInitilizer_language = decl_language!.Initializer is null ? SyntaxFactory.EqualsValueClause(declValue_language) : decl_language.Initializer.WithValue(declValue_language);
        stats = stats.Replace(declStat_language, declStat_language.ReplaceNode(decl_language, decl_language.WithInitializer(declInitilizer_language)));

        // Add language constants.
        stats = stats.InsertRange(2, s_lunaLanguageNames.Select((name, index) =>
        {
            var constValue = index + 3;
            return SyntaxFactory.ParseStatement($"const int {name} = {constValue};", options: options);
        }));

        root = root.ReplaceNode(method, ReplaceLanguagePredicate(method.WithBody(method.Body.WithStatements(stats)), options));

        return root;

        static void AssertLanguageConst(StatementSyntax stat, string languageName, string constType, string constValue)
        {
            var declStat = stat as LocalDeclarationStatementSyntax;
            Debug.Assert(declStat is not null && declStat.IsConst && declStat.Declaration.Type.ToString() == constType);
            var value = declStat!.Declaration.Variables.FirstOrDefault(n => n.Identifier.Text == languageName && n.Initializer?.Value.ToString() == constValue);
            Debug.Assert(value is not null);
        }

        static SyntaxNode ReplaceLanguagePredicate(SyntaxNode node, ParseOptions options)
        {
            return node.ReplaceNodes(
                node.DescendantNodes().OfType<BinaryExpressionSyntax>().Where(static bes => bes.IsKind(SyntaxKind.EqualsExpression) && bes.Left.ToString() == "language" && bes.Right.ToString() == "CSharp"),
            (_, _) => SyntaxFactory.ParseExpression("language != VisualBasic", options: options));
        }
    }
}

#endif
