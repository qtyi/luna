﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class SyntaxFactoryContext
{
    internal bool IsInIfOrElseIf => this.CurrentStructure is SyntaxKind.IfStatement or SyntaxKind.ElseIfClause;
}
