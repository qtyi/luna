// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Test.Utilities;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnostic = LuaDiagnostic;
using ThisInternalSyntaxNode = Syntax.InternalSyntax.LuaSyntaxNode;
using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxVisitor = LuaSyntaxVisitor;
using ThisSyntaxVisitor<TResult> = LuaSyntaxVisitor<TResult>;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnostic = MoonScriptDiagnostic;
using ThisInternalSyntaxNode = Syntax.InternalSyntax.MoonScriptSyntaxNode;
using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxVisitor = MoonScriptSyntaxVisitor;
using ThisSyntaxVisitor<TResult> = MoonScriptSyntaxVisitor<TResult>;
#endif

using ThisInternalSyntaxToken = Syntax.InternalSyntax.SyntaxToken;
using ThisInternalSyntaxTrivia = Syntax.InternalSyntax.SyntaxTrivia;

internal static class DiagnosticExtensions
{
    public static void Verify(this IEnumerable<DiagnosticInfo> actual, params DiagnosticDescription[] expected)
        => actual.Select(static info => new ThisDiagnostic(info, NoLocation.Singleton)).Verify(expected);

    public static void Verify(this ImmutableArray<DiagnosticInfo> actual, params DiagnosticDescription[] expected)
        => actual.Select(static info => new ThisDiagnostic(info, NoLocation.Singleton)).Verify(expected);

    public static void VerifyGreen(this ThisInternalSyntaxNode node, params DiagnosticDescription[] expected)
        => VerifyGreen(node.CreateRed().SyntaxTree, node.GetDiagnostics(), expected);

    public static void VerifyGreen(this ThisInternalSyntaxToken token, params DiagnosticDescription[] expected)
        => VerifyGreen(new TokenNode(token).SyntaxTree, token.GetDiagnostics(), expected);

    public static void VerifyGreen(this ThisInternalSyntaxTrivia trivia, ThisInternalSyntaxToken token, params DiagnosticDescription[] expected)
        => VerifyGreen(new TokenNode(token).SyntaxTree, trivia.GetDiagnostics(), expected);

    private static void VerifyGreen(SyntaxTree tree, DiagnosticInfo[] diagnostics, DiagnosticDescription[] expected)
        => diagnostics.Select(info => new ThisDiagnostic(info, info switch
        {
            SyntaxDiagnosticInfo syntax => Location.Create(tree, new(syntax.Offset, syntax.Width)),
            _ => NoLocation.Singleton
        })).Verify(expected);

    private sealed class TokenNode : ThisSyntaxNode
    {
        public TokenNode(ThisInternalSyntaxToken green) : base(green, parent: null, position: 1) { }

        internal override SyntaxNode? GetCachedSlot(int index) => null;

        internal override SyntaxNode? GetNodeSlot(int slot) => null;

        public override void Accept(ThisSyntaxVisitor visitor) => throw ExceptionUtilities.Unreachable();

        public override TResult? Accept<TResult>(ThisSyntaxVisitor<TResult> visitor) where TResult : default => throw ExceptionUtilities.Unreachable();
    }

    public static string ToLocalizedString(this MessageID id)
        => new LocalizableErrorArgument(id).ToString(format: null, formatProvider: null);
}
