// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Test.Utilities;
using Roslyn.Utilities;
using Xunit;
using Xunit.Abstractions;
using InternalSyntaxList<T> = Microsoft.CodeAnalysis.Syntax.InternalSyntax.SyntaxList<T> where T : Microsoft.CodeAnalysis.GreenNode;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests;

using ThisInternalSyntaxNode = Syntax.InternalSyntax.LuaSyntaxNode;
using ThisInternalSyntaxAccumulator<T> = Syntax.InternalSyntax.LuaSyntaxAccumulator<T>;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests;

using ThisInternalSyntaxNode = Syntax.InternalSyntax.MoonScriptSyntaxNode;
using ThisInternalSyntaxAccumulator<T> = Syntax.InternalSyntax.MoonScriptSyntaxAccumulator<T>;
#endif

using Syntax.InternalSyntax;

public delegate void NodeValidator(
    SyntaxKind kind,
    SyntaxKind? contextualKind = null,
    string? text = null,
    object? value = null,
    TriviaLocation location = TriviaLocation.None,
    DiagnosticDescription[]? diagnostics = null);

public enum TriviaLocation { None, Leading, Trailing }

internal readonly struct ValidatorImpl
{
    public readonly record struct Result(ThisInternalSyntaxNode Node, ThisInternalSyntaxNode? Parent, int IndentLevel);

    private readonly IEnumerator<Result> _etor;
    private readonly ITestOutputHelper? _output;

    public ValidatorImpl(
        Lexer lexer,
        LexerMode initialLexerMode = LexerMode.Syntax,
        bool withNode = false,
        bool withTrivia = false,
        bool intoStructuredTrivia = false,
        ITestOutputHelper? output = null)
        : this(LexTokens(lexer, initialLexerMode), withNode, withTrivia, intoStructuredTrivia, output) { }

    public ValidatorImpl(
        ThisInternalSyntaxNode node,
        bool withNode = false,
        bool withTrivia = false,
        bool intoStructuredTrivia = false,
        ITestOutputHelper? output = null)
        : this(SpecializedCollections.SingletonEnumerable(node), withNode, withTrivia, intoStructuredTrivia, output) { }

    public ValidatorImpl(
        IEnumerable<ThisInternalSyntaxNode> nodes,
        bool withNode = false,
        bool withTrivia = false,
        bool intoStructuredTrivia = false,
        ITestOutputHelper? output = null)
    {
        var accumulator = new Accumulator(withNode, withTrivia, intoStructuredTrivia);
        _etor = nodes.SelectMany(accumulator.Visit).GetEnumerator();

        _output = output;
    }

    private static IEnumerable<SyntaxToken> LexTokens(Lexer lexer, LexerMode mode)
    {
        SyntaxToken token;
        do
        {
            token = lexer.Lex(ref mode);
            yield return token;
        }
        while (token.Kind != SyntaxKind.EndOfFileToken);
    }

    private sealed class Accumulator : ThisInternalSyntaxAccumulator<Result>
    {
        private readonly bool _visitWithNode;
        private readonly bool _visitWithTrivia;

        public Accumulator(bool withNode, bool withTrivia, bool intoStructuredTrivia) : base(intoStructuredTrivia)
        {
            _visitWithNode = withNode;
            _visitWithTrivia = withTrivia;
        }

        [return: NotNullIfNotNull(nameof(node))]
        public override IEnumerable<Result>? Visit(ThisInternalSyntaxNode? node)
        {
            if (node is not null)
            {
                var isTokenOrTrivia = node.IsToken || node.IsTrivia;
                var intoNode = _visitWithNode || node.IsStructuredTrivia;
                if (isTokenOrTrivia)
                {
                    foreach (var result in base.Visit(node))
                        yield return result;
                }
                else if (intoNode)
                {
                    yield return new Result(node, null, 0);
                    foreach (var result in base.Visit(node))
                        yield return new Result(result.Node, result.Parent ?? node, result.IndentLevel + 1);
                }
            }
        }

        public override IEnumerable<Result> VisitToken(SyntaxToken token)
        {
            if (_visitWithTrivia)
            {
                foreach (var result in VisitList(token.LeadingTrivia))
                    yield return new Result(result.Node, result.Parent ?? token, result.IndentLevel);

                yield return new Result(token, null, 0);

                foreach (var result in VisitList(token.TrailingTrivia))
                    yield return new Result(result.Node, result.Parent ?? token, result.IndentLevel);
            }
            else
                yield return new Result(token, null, 0);
        }

        public override IEnumerable<Result> VisitTrivia(SyntaxTrivia trivia) => SpecializedCollections.SingletonEnumerable(new Result(trivia, null, 0));
    }

    public void NextNode(SyntaxKind kind, SyntaxKind? contextualKind, string? text, object? value, TriviaLocation location, DiagnosticDescription[]? diagnostics)
    {
        if (_etor.MoveNext())
        {
            var result = _etor.Current;
            result.Node.Validate(kind, contextualKind, text, value, result.Parent, location, diagnostics);
        }
        else
            ValidatorExtensions.ValidateNone(kind);
    }

    public bool TryWriteNextValidation()
    {
        Debug.Assert(_output is not null);
        var oldResult = _etor.Current;
        if (_etor.MoveNext())
        {
            var newResult = _etor.Current;

            if (oldResult.Node is not null) // Not initial state.
                WriteBrackets(_output, from: oldResult.IndentLevel, to: newResult.IndentLevel);

            _output.WriteValidation(newResult.Node, newResult.Parent, newResult.IndentLevel * 2);
            return true;
        }
        else
        {
            if (oldResult.Node is not null) // Not initial state.
                WriteBrackets(_output, from: oldResult.IndentLevel, to: 0);

            return false;
        }

        static void WriteBrackets(ITestOutputHelper output, int from, int to)
        {
            var step = Math.Sign(to - from);
            for (var indentLevel = from;
                indentLevel != to;
                indentLevel += step)
            {
                if (step > 0)
                    output.WriteLine("{0}{{", new string(' ', indentLevel * 2));
                else
                    output.WriteLine("{0}}}", new string(' ', (indentLevel - 1) * 2));
            }
        }
    }
}

internal static class ValidatorExtensions
{
    public static NodeValidator EndOfFile(this NodeValidator V)
        => (kind, contextualKind, text, value, location, diagnostics) =>
        {
            V(kind, contextualKind, text, value, location, diagnostics);
            V(SyntaxKind.EndOfFileToken);
        };

    public static NodeValidator Single(this NodeValidator V)
        => (kind, contextualKind, text, value, location, diagnostics) =>
        {
            V(kind, contextualKind, text, value, location, diagnostics);
            V(SyntaxKind.None);
        };

    #region ITestOutputHelper Helpers
    internal static void WriteValidation(this ITestOutputHelper output, ThisInternalSyntaxNode node, ThisInternalSyntaxNode? parent = null, int indent = 0)
    {
        if (node is SyntaxToken token)
            output.WriteValidation(token, indent);
        else if (node is SyntaxTrivia trivia)
            output.WriteValidation(trivia, parent as SyntaxToken, indent);
        else if (node is StructuredTriviaSyntax structuredTrivia)
            output.WriteValidation(structuredTrivia, parent as SyntaxToken, indent);
        else
        {
            var kind = nameof(SyntaxKind) + '.' + Enum.GetName(typeof(SyntaxKind), node.Kind);
            WriteDiagnosticValidation(output, leading: $"V({kind}", node.GetDiagnostics(), ");", indent);
        }
    }

    internal static void WriteValidation(this ITestOutputHelper output, SyntaxToken token, int indent = 0)
    {
        var kind = nameof(SyntaxKind) + '.' + Enum.GetName(typeof(SyntaxKind), token.Kind);
        var text = EscapeCharInQuotes(token.Text, '"');
        var diagnostics = token.GetDiagnostics();
        if (token.Kind is SyntaxKind.IdentifierToken or SyntaxKind.BadToken)
            WriteDiagnosticValidation(
                output,
                leading: $"V({kind}, text: \"{text}\"",
                diagnostics,
                trailing: ");",
                indent);
        else if (token.Kind == SyntaxKind.NumericLiteralToken)
        {
            Debug.Assert(token.Value is not null);
            var value = token.Value switch
            {
                long l => l + "L",
                ulong ul => ul + "UL",
                double d => d + "D",
                _ => token.Value.ToString()
            };
            WriteDiagnosticValidation(
                output,
                leading: $"V({kind}, text: \"{text}\", value: {value}",
                diagnostics,
                trailing: ");",
                indent);
        }
        else if (token.Kind is SyntaxKind.StringLiteralToken or SyntaxKind.MultiLineRawStringLiteralToken)
        {
            Debug.Assert(token.Value is not null);
            string value;
            try
            {
                var encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true, throwOnInvalidBytes: true);
                var strValue = encoding.GetString(((Utf8String)token.Value).ToByteArray());
                value = "new Utf8String([.. \"" + EscapeCharInQuotes(strValue, '"') + "\"U8])";
            }
            catch (Exception)
            {
                value = "new Utf8String([" + string.Join(", ", (Utf8String)token.Value) + "])";
            }
            WriteDiagnosticValidation(
                output,
                leading: $"V({kind}, text: \"{text}\", value: {value}",
                diagnostics,
                trailing: ");",
                indent);
        }
        else
            WriteDiagnosticValidation(
                output,
                leading: $"V({kind}",
                diagnostics,
                trailing: ");",
                indent);
    }

    internal static void WriteValidation(this ITestOutputHelper output, SyntaxTrivia trivia, SyntaxToken? parent = null, int indent = 0) =>
        WriteTriviaValidation(output, trivia, parent, indent);

    internal static void WriteValidation(this ITestOutputHelper output, StructuredTriviaSyntax trivia, SyntaxToken? parent = null, int indent = 0) =>
        WriteTriviaValidation(output, trivia, parent, indent);

    private static void WriteTriviaValidation(ITestOutputHelper output, ThisInternalSyntaxNode trivia, SyntaxToken? parent = null, int indent = 0)
    {
        Debug.Assert(parent is null || (parent.LeadingTrivia.AsEnumerable().Contains(trivia) || parent.TrailingTrivia.AsEnumerable().Contains(trivia)));

        var kind = nameof(SyntaxKind) + '.' + Enum.GetName(typeof(SyntaxKind), trivia.Kind);
        var diagnostics = trivia.GetDiagnostics();
        var text = trivia.Kind switch
        {
            SyntaxKind.WhitespaceTrivia or
            SyntaxKind.EndOfLineTrivia => null,

            _ => EscapeCharInQuotes(trivia.ToString(), '"')
        };
        if (parent is null)
        {
            if (text is null)
                WriteDiagnosticValidation(
                    output,
                    leading: $"V({kind}",
                    diagnostics,
                    trailing: ");",
                    indent);
            else
                WriteDiagnosticValidation(
                    output,
                    leading: $"V({kind}, text: \"{text}\"",
                    diagnostics,
                    trailing: ");",
                    indent);
        }
        else
        {
            var location = nameof(TriviaLocation) + '.' + (parent.LeadingTrivia.AsEnumerable().Contains(trivia) ? nameof(TriviaLocation.Leading) : nameof(TriviaLocation.Trailing));
            if (text is null)
                WriteDiagnosticValidation(
                    output,
                    leading: $"V({kind}, location: {location}",
                    diagnostics,
                    trailing: ");",
                    indent);
            else
                WriteDiagnosticValidation(
                    output,
                    leading: $"V({kind}, text: \"{text}\", location: {location}",
                    diagnostics,
                    trailing: ");",
                    indent);
        }
    }

    private static void WriteDiagnosticValidation(ITestOutputHelper output, string leading, DiagnosticInfo[] diagnostics, string trailing, int indent = 0)
    {
        var indentStr = new string(' ', indent);
        if (diagnostics.Length == 0)
            output.WriteLine("{0}{1}{2}", indentStr, leading, trailing);
        else
        {
            output.WriteLine("{0}{1}, diagnostics: [", indentStr, leading);
            for (int i = 0, length = diagnostics.Length; i < length; i++)
            {
                var separator = i < length ? "," : string.Empty;
                output.WriteLine("{0}Diagnostic(ErrorCode.{1}).WithLocation(1, 1){2}", new string(' ', indent + 2), Enum.GetName(typeof(ErrorCode), diagnostics[i].Code), separator);
            }
            output.WriteLine("{0}]{1}", indentStr, trailing);
        }
    }

    internal static void WriteNoneValidation(this ITestOutputHelper output, int indent = 0)
    {
        var indentStr = new string(' ', indent);
        var kind = nameof(SyntaxKind) + '.' + nameof(SyntaxKind.None);
        output.WriteLine("{0}V({1});", indentStr, kind);
    }

    private static string? EscapeCharInQuotes(string? text, char quote)
    {
        if (text is null) return null;

        var builder = new StringBuilder(text.Length);
        char highSurrogate = default;
        foreach (var c in text)
        {
            if (highSurrogate != default && char.IsSurrogatePair(highSurrogate, c))
            {
                highSurrogate = default;
                builder.Append(EscapeUnicodeChar(c));
                continue;
            }

            var escaped = c switch
            {
                '\0' => "\\0",
                '\a' => "\\a",
                '\b' => "\\b",
                '\f' => "\\f",
                '\n' => "\\n",
                '\r' => "\\r",
                '\t' => "\\t",
                '\v' => "\\v",
                '\\' => "\\\\",
                '\'' => quote == c ? "\\'" : "'",
                '"' => quote == c ? "\\\"" : "\"",

                (< '\u001F') or
                (>= '\u0080' and <= '\u009F') => EscapeUnicodeChar(c),

                _ => null
            };
            if (escaped is not null)
                builder.Append(escaped);
            else if (char.IsHighSurrogate(c))
            {
                highSurrogate = c;
                builder.Append(EscapeUnicodeChar(c));
            }
            else
                builder.Append(c);
        }
        return builder.ToString();
    }

    private static string EscapeUnicodeChar(char c) => $"\\u{c:X4}";
    #endregion

    internal static void Validate(
        this ThisInternalSyntaxNode node,
        SyntaxKind kind,
        SyntaxKind? contextualKind,
        string? text = null,
        object? value = null,
        ThisInternalSyntaxNode? parent = null,
        TriviaLocation location = TriviaLocation.None,
        DiagnosticDescription[]? diagnostics = null,
        ITestOutputHelper? output = null,
        int indent = 0)
    {
        if (node is SyntaxToken token)
            token.Validate(kind, contextualKind, text, value, diagnostics, output, indent);
        else if (node is SyntaxTrivia trivia)
            trivia.Validate(kind, text, parent as SyntaxToken, location, diagnostics, output, indent);
        else if (node is StructuredTriviaSyntax structuredTrivia)
            structuredTrivia.Validate(kind, text, parent as SyntaxToken, location, diagnostics, output, indent);
        else
        {
            Assert.Equal(kind, node.Kind);
            if (diagnostics is not null)
                node.VerifyGreen(diagnostics);
        }
    }

    internal static void Validate(
        this SyntaxToken token,
        SyntaxKind kind,
        SyntaxKind? contextualKind,
        string? text = null,
        object? value = null,
        DiagnosticDescription[]? diagnostics = null,
        ITestOutputHelper? output = null,
        int indent = 0)
    {
        Assert.Equal(kind, token.Kind);
        if (contextualKind.HasValue)
            Assert.Equal(contextualKind.Value, token.ContextualKind);
        if (text is not null)
            Assert.Equal(text, token.Text);
        if (value is not null)
        {
            Assert.NotNull(token.Value);
            if (value is Utf8String)
            {
                Assert.IsType<Utf8String>(token.Value);
                Assert.Equal(value.ToString(), token.Value.ToString());
            }
            else
                Assert.Equal(value, token.Value);
        }
        if (diagnostics is not null)
            token.VerifyGreen(diagnostics);
        output?.WriteValidation(token, indent);
    }

    internal static void Validate(
        this SyntaxTrivia trivia,
        SyntaxKind kind,
        string? text = null,
        SyntaxToken? parent = null,
        TriviaLocation location = TriviaLocation.None,
        DiagnosticDescription[]? diagnostics = null,
        ITestOutputHelper? output = null,
        int indent = 0) =>
        ValidateTrivia(trivia, kind, text, parent, location, diagnostics, output, indent);

    internal static void Validate(
        this StructuredTriviaSyntax trivia,
        SyntaxKind kind,
        string? text = null,
        SyntaxToken? parent = null,
        TriviaLocation location = TriviaLocation.None,
        DiagnosticDescription[]? diagnostics = null,
        ITestOutputHelper? output = null,
        int indent = 0) =>
        ValidateTrivia(trivia, kind, text, parent, location, diagnostics, output, indent);

    private static void ValidateTrivia(
        ThisInternalSyntaxNode trivia,
        SyntaxKind kind,
        string? text = null,
        SyntaxToken? parent = null,
        TriviaLocation location = TriviaLocation.None,
        DiagnosticDescription[]? diagnostics = null,
        ITestOutputHelper? output = null,
        int indent = 0)
    {
        Assert.Equal(kind, trivia.Kind);
        if (text is not null)
            Assert.Equal(text, trivia.ToString());
        if (parent is not null)
        {
            var tokenTrivia = location switch
            {
                TriviaLocation.None => parent.LeadingTrivia.AsEnumerable().Concat(parent.TrailingTrivia.AsEnumerable()),
                TriviaLocation.Leading => parent.LeadingTrivia.AsEnumerable(),
                TriviaLocation.Trailing => parent.TrailingTrivia.AsEnumerable(),
                _ => throw ExceptionUtilities.UnexpectedValue(location)
            };
            Assert.Contains(trivia, tokenTrivia);
        }
        if (diagnostics is not null)
            trivia.VerifyGreen(diagnostics);

        if (output is not null)
            WriteTriviaValidation(output, trivia, parent, indent);
    }

    internal static void ValidateNone(
        SyntaxKind kind,
        ITestOutputHelper? output = null,
        int indent = 0)
    {
        Assert.Equal(SyntaxKind.None, kind);
        output?.WriteNoneValidation(indent);
    }

    internal static IEnumerable<T> AsEnumerable<T>(this InternalSyntaxList<T> list)
        where T : ThisInternalSyntaxNode
    {
        var etor = list.GetEnumerator();
        while (etor.MoveNext())
            yield return etor.Current;
    }
}
