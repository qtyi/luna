// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Syntax;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaSyntaxHelper
{
    public override partial bool IsLambdaExpression(SyntaxNode node) => node is FunctionDefinitionExpressionSyntax;
}
