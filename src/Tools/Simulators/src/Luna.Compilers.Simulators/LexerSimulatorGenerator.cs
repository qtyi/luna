using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Luna.Compilers.Simulators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Generators;

[Generator]
internal sealed class LexerSimulatorGenerator : ISourceGenerator
{
    private static readonly DiagnosticDescriptor s_missingCertianAttributeType = new(
        "LSSG0001",
        title: $"缺少类型“{nameof(LexerSimulatorAttribute)}”",
        messageFormat: $"未能找到类型“{nameof(LexerSimulatorAttribute)}”，可能缺少程序集引用。",
        category: nameof(LexerSimulatorGenerator),
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor s_invalidTypeDeclaration = new(
        "LSSG0002",
        title: $"无法处理抽象、静态或枚举类型",
        messageFormat: "无法处理“{0}”，它不能是抽象、静态或枚举类型。",
        category: nameof(LexerSimulatorGenerator),
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public void Initialize(GeneratorInitializationContext context)
    {
#if false && DEBUG
        if (!Debugger.IsAttached)
            Debugger.Launch();
#endif
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var attributeSymbol = context.Compilation.GetTypeByMetadataName(typeof(LexerSimulatorAttribute).FullName);
        if (attributeSymbol is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(s_invalidTypeDeclaration, location: null));
            return;
        }

        var classesWithAttribute = context.Compilation.SyntaxTrees                                      // 从所有语法树中，
            .SelectMany(tree => tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>())      // 找到所有类定义语法，
            .Where(classDeclaration =>
                classDeclaration.AttributeLists
                .SelectMany(list => list.Attributes)
                .Any())                                                                                 // 筛选出其中附加了特性的，
            .GroupBy(classDeclaration => classDeclaration.SyntaxTree);                                  // 以所在语法树为键分组。

        var declaredClasses = classesWithAttribute                                                      // 从所有以语法树为键分组的附加了特性的类定义语法中，
            .SelectMany(classesInTree =>                                                                // 选择所有
            {
                // 获取语义模型。
                var tree = classesInTree.Key;
                var semanticModel = context.Compilation.GetSemanticModel(tree);

                return classesInTree
                    .Where(classDeclaration =>
                        classDeclaration.AttributeLists
                        .SelectMany(attributeList => attributeList.Attributes)
                        .Where(attribute =>
                            semanticModel.GetTypeInfo(attribute.Name).Type == attributeSymbol)
                        .Any()
                    )                                                                                   // 附加的特性列表中含有LexerSimulatorAttribute的类定义语法，
                    .GroupBy(classDeclaration => semanticModel.GetDeclaredSymbol(classDeclaration)!);   // 并以这个类定义语法表示的名字符号为键分组的类定义语法。
            })
            .GroupBy(
                group => group.Key,
                group => group.AsEnumerable()
            )                                                                                           // 整理类型。
            .Where(group => group.Key.Locations.Length > 1 ||
                group.SelectMany(item => item)
                .Any(classDeclaration =>
                    classDeclaration.Modifiers
                    .Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))) // 确定是分布的类定义。
            .Select(group => group.Key);

        foreach (var declaredClass in declaredClasses)
        {
            // 跳过无法处理的类型。
            if (declaredClass.IsAbstract || declaredClass.IsStatic ||
                declaredClass.BaseType == context.Compilation.GetTypeByMetadataName(typeof(Enum).FullName))
            {
                context.ReportDiagnostic(Diagnostic.Create(s_invalidTypeDeclaration, location: null, declaredClass.Name));
                continue;
            }

            var languageNames = declaredClass.GetAttributes()
                .Where(data => data.AttributeClass == attributeSymbol)
                .SelectMany(data => data.ConstructorArguments)
                .Where(arg => !arg.IsNull && arg.Kind is TypedConstantKind.Array or TypedConstantKind.Primitive)
                .SelectMany(arg => arg.Kind == TypedConstantKind.Array ? arg.Values : ImmutableArray.Create(arg))
                .Where(arg => !arg.IsNull && arg.Kind == TypedConstantKind.Primitive)
                .Where(arg => arg.Value is string languageName && !string.IsNullOrWhiteSpace(languageName))
                .Select(arg => (string)arg.Value!)
                .Distinct()
                .ToArray();
            if (languageNames.Length == 0) continue;

            this.GenerateSource(context, declaredClass, languageNames);
        }
    }

    private void GenerateSource(GeneratorExecutionContext context, INamedTypeSymbol declaredClass, string[] languageNames)
    {
        var stringBuilder = new StringBuilder();
        using var stringWriter = new StringWriter(stringBuilder);
        var indentWriter = new IndentWriter(stringWriter, 4, default);

        indentWriter.WriteLine("// <auto-generated />");
        indentWriter.WriteLine();
        indentWriter.WriteLine("#nullable enable");
        indentWriter.WriteLine();
        indentWriter.WriteLine($"namespace {declaredClass.ContainingNamespace}");
        indentWriter.OpenBlock();
        indentWriter.WriteLine($"partial {(declaredClass.IsValueType ? "struct" : "class")} {declaredClass.Name}");
        indentWriter.OpenBlock();

        WriteGeneratedCodeMemberAttributes(isField: true);
        indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected")} global::Luna.Compilers.Simulators.LexerSimulatorContext {(declaredClass.IsSealed ? "_context" : "context")};");
        indentWriter.WriteLine();

        WriteGeneratedCodeMemberAttributes(isField: true);
        indentWriter.WriteLine($"private global::Microsoft.CodeAnalysis.SyntaxNode? _root;");
        WriteGeneratedCodeMemberAttributes(isField: true);
        indentWriter.WriteLine($"private int _position;");
        WriteGeneratedCodeMemberAttributes(isField: true);
        indentWriter.WriteLine($"private int _index;");
        indentWriter.WriteLine();

        foreach (var languageName in languageNames)
        {
            indentWriter.WriteLine($"#region {languageName}");

            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected virtual")} partial global::Qtyi.CodeAnalysis.{languageName}.Syntax.InternalSyntax.Lexer Create{languageName}Lexer(global::Microsoft.CodeAnalysis.Text.SourceText text);");
            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected virtual")} partial global::Qtyi.CodeAnalysis.{languageName}.Syntax.InternalSyntax.SyntaxToken LexNode(global::Qtyi.CodeAnalysis.{languageName}.Syntax.InternalSyntax.Lexer lexer);");
            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected virtual")} partial global::System.Collections.Generic.IEnumerable<global::Microsoft.CodeAnalysis.SyntaxToken> DescendTokens(global::Qtyi.CodeAnalysis.{languageName}.Syntax.InternalSyntax.SyntaxToken node);");

            WriteGeneratedCodeMemberAttributes();
            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected")} global::Microsoft.CodeAnalysis.SyntaxToken CreateToken(global::Qtyi.CodeAnalysis.{languageName}.Syntax.InternalSyntax.SyntaxToken node) => this.EatToken(global::Qtyi.CodeAnalysis.{languageName}.SyntaxFactory.Token(parent: this._root!, token: node, position: this._position, index: this._index));");
            indentWriter.WriteLine("#endregion");
        }
        indentWriter.WriteLine();
        {
            WriteGeneratedCodeMemberAttributes();
            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected")} global::Microsoft.CodeAnalysis.SyntaxToken EatToken(global::Microsoft.CodeAnalysis.SyntaxToken token)");
            indentWriter.OpenBlock();
            indentWriter.WriteLine("token = this.EatTokenCore(token);");
            indentWriter.WriteLine("this._position += token.FullSpan.Length;");
            indentWriter.WriteLine("this._index++;");
            indentWriter.WriteLine("return token;");
            indentWriter.CloseBlock();

            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected virtual")} partial global::Microsoft.CodeAnalysis.SyntaxToken EatTokenCore(global::Microsoft.CodeAnalysis.SyntaxToken token);");
        }
        {
            WriteGeneratedCodeMemberAttributes();
            indentWriter.WriteLine($"[global::System.Diagnostics.CodeAnalysis.MemberNotNull(nameof(_root))]");
            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected")} void Reset()");
            indentWriter.OpenBlock();
            indentWriter.WriteLine($"switch(this.{(declaredClass.IsSealed ? "_context" : "context")}.LanguageName)");
            indentWriter.OpenBlock();
            foreach (var languageName in languageNames)
            {
                indentWriter.WriteLine($"case \"{languageName}\": this._root = global::Qtyi.CodeAnalysis.{languageName}.SyntaxFactory.Mock(); break;");
            }
            indentWriter.WriteLine($"default: throw new InvalidOperationException(\"意外的语言名称：\" + this.{(declaredClass.IsSealed ? "_context" : "context")}.LanguageName);");
            indentWriter.CloseBlock();
            indentWriter.WriteLine("this._position = 0;");
            indentWriter.WriteLine("this._index = 0;");
            indentWriter.WriteLine("this.ResetCore();");
            indentWriter.CloseBlock();

            indentWriter.WriteLine($"{(declaredClass.IsSealed ? "private" : "protected virtual")} partial void ResetCore();");
        }
        indentWriter.WriteLine();
        {
            indentWriter.WriteLine($"#region Luna.Compilers.Simulators.ILexerSimulator");

            WriteGeneratedCodeMemberAttributes();
            indentWriter.WriteLine($"void global::Luna.Compilers.Simulators.ILexerSimulator.Initialize(global::Luna.Compilers.Simulators.LexerSimulatorContext context) => this.{(declaredClass.IsSealed ? "_context" : "context")} = context;");

            WriteGeneratedCodeMemberAttributes();
            indentWriter.WriteLine("global::Luna.Compilers.Simulators.TokenKind global::Luna.Compilers.Simulators.ILexerSimulator.GetTokenKind(int rawKind)");
            indentWriter.OpenBlock();
            indentWriter.WriteLine($"switch(this.{(declaredClass.IsSealed ? "_context" : "context")}.LanguageName)");
            indentWriter.OpenBlock();
            foreach (var languageName in languageNames)
            {
                indentWriter.WriteLine($"case \"{languageName}\": return this.GetTokenKind((global::Qtyi.CodeAnalysis.{languageName}.SyntaxKind)rawKind);");
            }
            indentWriter.WriteLine($"default: throw new InvalidOperationException(\"意外的语言名称：\" + this.{(declaredClass.IsSealed ? "_context" : "context")}.LanguageName);");
            indentWriter.CloseBlock();
            indentWriter.CloseBlock();

            WriteGeneratedCodeMemberAttributes();
            indentWriter.WriteLine($"global::System.Collections.Generic.IEnumerable<global::Microsoft.CodeAnalysis.SyntaxToken> global::Luna.Compilers.Simulators.ILexerSimulator.LexToEnd(global::Microsoft.CodeAnalysis.Text.SourceText text)");
            indentWriter.OpenBlock();
            indentWriter.WriteLine("this.Reset();");
            indentWriter.WriteLine($"switch(this.{(declaredClass.IsSealed ? "_context" : "context")}.LanguageName)");
            indentWriter.OpenBlock();
            foreach (var languageName in languageNames)
            {
                indentWriter.WriteLine($"case \"{languageName}\":");
                indentWriter.OpenBlock();
                indentWriter.WriteLine($"var lexer = this.Create{languageName}Lexer(text);");
                indentWriter.WriteLine($"global::Qtyi.CodeAnalysis.{languageName}.Syntax.InternalSyntax.SyntaxToken node;");
                indentWriter.WriteLine("do");
                indentWriter.OpenBlock();
                indentWriter.WriteLine("node = this.LexNode(lexer);");
                indentWriter.WriteLine("foreach (var token in this.DescendTokens(node)) yield return token;");
                indentWriter.CloseBlock();
                indentWriter.WriteLine($"while (node.Kind != global::Qtyi.CodeAnalysis.{languageName}.SyntaxKind.EndOfFileToken);");
                indentWriter.WriteLine("break;");
                indentWriter.CloseBlock();
            }
            indentWriter.WriteLine($"default: throw new InvalidOperationException(\"意外的语言名称：\" + this.{(declaredClass.IsSealed ? "_context" : "context")}.LanguageName);");
            indentWriter.CloseBlock();
            indentWriter.CloseBlock();

            indentWriter.WriteLine("#endregion");
        }

        indentWriter.CloseBlock();
        indentWriter.CloseBlock();

        context.AddSource($"{declaredClass.ContainingNamespace}.{declaredClass.Name}", SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8));

        void WriteGeneratedCodeMemberAttributes(bool isField = false)
        {
            indentWriter.WriteLine($"[global::System.CodeDom.Compiler.GeneratedCode(\"{typeof(LexerSimulatorGenerator).FullName}\", \"{typeof(LexerSimulatorGenerator).Assembly.GetName().Version}\")]");
            if (!isField)
            {
                indentWriter.WriteLine("[global::System.Diagnostics.DebuggerNonUserCode]");
                indentWriter.WriteLine("[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]");
            }
        }
    }
}
