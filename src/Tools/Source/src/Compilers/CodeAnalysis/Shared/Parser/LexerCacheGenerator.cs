﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Tools.LexerCache;
using Microsoft.CodeAnalysis;

namespace Luna.Tools;

[Generator(LanguageNames.CSharp)]
public sealed class LexerCacheGenerator : AbstractLexerCacheGenerator<LexerCacheSourceSyntaxProvider> { }
