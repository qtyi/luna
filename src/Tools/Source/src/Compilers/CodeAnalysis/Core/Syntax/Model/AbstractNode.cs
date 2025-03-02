﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Tools.Syntax.Model;

#pragma warning disable CS8618
public sealed class AbstractNode : SyntaxTreeType
{
    public readonly List<Field> Fields = new();
}
#pragma warning restore CS8618
