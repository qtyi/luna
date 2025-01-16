// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA

global using ThisCommandLineArguments = Qtyi.CodeAnalysis.Lua.LuaCommandLineArguments;
global using ThisCommandLineParser = Qtyi.CodeAnalysis.Lua.LuaCommandLineParser;
global using ThisCompilation = Qtyi.CodeAnalysis.Lua.LuaCompilation;
global using ThisCompilationOptions = Qtyi.CodeAnalysis.Lua.LuaCompilationOptions;
global using ThisCompilationReference = Qtyi.CodeAnalysis.Lua.LuaCompilationReference;
global using ThisCompiler = Qtyi.CodeAnalysis.Lua.LuaCompiler;
global using ThisDeterministicKeyBuilder = Qtyi.CodeAnalysis.Lua.LuaDeterministicKeyBuilder;
global using ThisDiagnostic = Qtyi.CodeAnalysis.Lua.LuaDiagnostic;
global using ThisDiagnosticFormatter = Qtyi.CodeAnalysis.Lua.LuaDiagnosticFormatter;
global using ThisDiagnosticInfo = Qtyi.CodeAnalysis.Lua.LuaDiagnosticInfo;
global using ThisGeneratorDriver = Qtyi.CodeAnalysis.Lua.LuaGeneratorDriver;
global using ThisInternalSyntaxFactory = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.SyntaxFactory;
global using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxNode;
global using ThisInternalSyntaxVisitor = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxVisitor;
global using ThisInternalSyntaxVisitor<T> = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxVisitor<T>;
global using ThisMessageProvider = Qtyi.CodeAnalysis.Lua.MessageProvider;
global using ThisParseOptions = Qtyi.CodeAnalysis.Lua.LuaParseOptions;
global using ThisScriptCompilationInfo = Qtyi.CodeAnalysis.Lua.LuaScriptCompilationInfo;
global using ThisSemanticModel = Qtyi.CodeAnalysis.Lua.LuaSemanticModel;
global using ThisSymbolVisitor = Qtyi.CodeAnalysis.Lua.LuaSymbolVisitor;
global using ThisSymbolVisitor<TResult> = Qtyi.CodeAnalysis.Lua.LuaSymbolVisitor<TResult>;
global using ThisSymbolVisitor<TArgument, TResult> = Qtyi.CodeAnalysis.Lua.LuaSymbolVisitor<TArgument, TResult>;
global using ThisSyntaxHelper = Qtyi.CodeAnalysis.Lua.LuaSyntaxHelper;
global using ThisSyntaxNode = Qtyi.CodeAnalysis.Lua.LuaSyntaxNode;
global using ThisSyntaxRewriter = Qtyi.CodeAnalysis.Lua.LuaSyntaxRewriter;
global using ThisSyntaxTree = Qtyi.CodeAnalysis.Lua.LuaSyntaxTree;
global using ThisSyntaxVisitor = Qtyi.CodeAnalysis.Lua.LuaSyntaxVisitor;
global using ThisSyntaxVisitor<TResult> = Qtyi.CodeAnalysis.Lua.LuaSyntaxVisitor<TResult>;
global using ThisRequiredLanguageVersion = Qtyi.CodeAnalysis.Lua.LuaRequiredLanguageVersion;
global using ThisResources = Qtyi.CodeAnalysis.Lua.LuaResources;

#elif LANG_MOONSCRIPT

global using ThisCommandLineArguments = Qtyi.CodeAnalysis.MoonScript.MoonScriptCommandLineArguments;
global using ThisCommandLineParser = Qtyi.CodeAnalysis.MoonScript.MoonScriptCommandLineParser;
global using ThisCompilation = Qtyi.CodeAnalysis.MoonScript.MoonScriptCompilation;
global using ThisCompilationOptions = Qtyi.CodeAnalysis.MoonScript.MoonScriptCompilationOptions;
global using ThisCompilationReference = Qtyi.CodeAnalysis.MoonScript.MoonScriptCompilationReference;
global using ThisCompiler = Qtyi.CodeAnalysis.MoonScript.MoonScriptCompiler;
global using ThisDeterministicKeyBuilder = Qtyi.CodeAnalysis.MoonScript.MoonScriptDeterministicKeyBuilder;
global using ThisDiagnostic = Qtyi.CodeAnalysis.MoonScript.MoonScriptDiagnostic;
global using ThisDiagnosticFormatter = Qtyi.CodeAnalysis.MoonScript.MoonScriptDiagnosticFormatter;
global using ThisDiagnosticInfo = Qtyi.CodeAnalysis.MoonScript.MoonScriptDiagnosticInfo;
global using ThisGeneratorDriver = Qtyi.CodeAnalysis.MoonScript.MoonScriptGeneratorDriver;
global using ThisInternalSyntaxFactory = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.SyntaxFactory;
global using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.MoonScriptSyntaxNode;
global using ThisInternalSyntaxVisitor = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.MoonScriptSyntaxVisitor;
global using ThisInternalSyntaxVisitor<T> = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.MoonScriptSyntaxVisitor<T>;
global using ThisMessageProvider = Qtyi.CodeAnalysis.MoonScript.MessageProvider;
global using ThisParseOptions = Qtyi.CodeAnalysis.MoonScript.MoonScriptParseOptions;
global using ThisScriptCompilationInfo = Qtyi.CodeAnalysis.MoonScript.MoonScriptScriptCompilationInfo;
global using ThisSemanticModel = Qtyi.CodeAnalysis.MoonScript.MoonScriptSemanticModel;
global using ThisSymbolVisitor = Qtyi.CodeAnalysis.MoonScript.MoonScriptSymbolVisitor;
global using ThisSymbolVisitor<TResult> = Qtyi.CodeAnalysis.MoonScript.MoonScriptSymbolVisitor<TResult>;
global using ThisSymbolVisitor<TArgument, TResult> = Qtyi.CodeAnalysis.MoonScript.MoonScriptSymbolVisitor<TArgument, TResult>;
global using ThisSyntaxHelper = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxHelper;
global using ThisSyntaxNode = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxNode;
global using ThisSyntaxRewriter = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxRewriter;
global using ThisSyntaxTree = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxTree;
global using ThisSyntaxVisitor = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxVisitor;
global using ThisSyntaxVisitor<TResult> = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxVisitor<TResult>;
global using ThisRequiredLanguageVersion = Qtyi.CodeAnalysis.MoonScript.MoonScriptRequiredLanguageVersion;
global using ThisResources = Qtyi.CodeAnalysis.MoonScript.MoonScriptResources;

#elif LANG_TERRA
// Terra
#error Terra not supported.
#elif LANG_IDLE
// Idle
#error Idle not supported.
#elif LANG_LUAU
// Luau
#error Luau not supported.
#else
#error Language not supported.
#endif
