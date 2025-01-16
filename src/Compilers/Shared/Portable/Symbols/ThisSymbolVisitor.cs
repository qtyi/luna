// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// Represents a <see cref="Symbol"/> visitor that visits only the single <see cref="Symbol"/> with argument passed into its Visit method and produces a value.
/// </summary>
/// <typeparam name="TResult">
/// The type of the return value this visitor's Visit method.
/// </typeparam>
/// <typeparam name="TArgument">
/// The argument passed into this visitor's Visit method.
/// </typeparam>
internal abstract partial class
#if LANG_LUA
    LuaSymbolVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSymbolVisitor
#endif
    <TArgument, TResult>
{
    /// <summary>
    /// Visit a symbol with argument and produce result.
    /// </summary>
    /// <param name="symbol">The symbol visited.</param>
    /// <param name="argument">The argument used during visiting symbol.</param>
    /// <returns>The visiting result.</returns>
    public virtual TResult? Visit(Symbol? symbol, TArgument? argument = default)
    {
        if (symbol is null) return default;

        return symbol.Accept(this, argument);
    }

    /// <summary>
    /// Gets the default result after visiting a symbol with argument.
    /// </summary>
    /// <param name="symbol">The symbol visited.</param>
    /// <param name="argument">The argument used during visiting symbol.</param>
    /// <returns>The visiting result.</returns>
    protected virtual TResult? DefaultVisit(Symbol symbol, TArgument argument) => default;
}

/// <summary>
/// Represents a <see cref="Symbol"/> visitor that visits only the single <see cref="Symbol"/> passed into its Visit method and produces a value.
/// </summary>
/// <typeparam name="TResult">
/// The type of the return value this visitor's Visit method.
/// </typeparam>
internal abstract partial class
#if LANG_LUA
    LuaSymbolVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSymbolVisitor
#endif
    <TResult>
{
    /// <summary>
    /// Visit a symbol and produce result.
    /// </summary>
    /// <param name="symbol">The symbol visited.</param>
    /// <returns>The visiting result.</returns>
    public virtual TResult? Visit(Symbol? symbol)
    {
        if (symbol is null) return default;

        return symbol.Accept(this);
    }

    /// <summary>
    /// Gets the default result after visiting a symbol.
    /// </summary>
    /// <param name="symbol">The symbol visited.</param>
    /// <returns>The visiting result.</returns>
    protected virtual TResult? DefaultVisit(Symbol symbol) => default;
}

/// <summary>
/// 表示访问者基类。访问者每次访问和处理一个<see cref="Symbol"/>符号。
/// </summary>
internal abstract partial class
#if LANG_LUA
    LuaSymbolVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSymbolVisitor
#endif
{
    /// <summary>
    /// 处理这个符号。
    /// </summary>
    /// <param name="symbol">要进行处理的符号。</param>
    public virtual void Visit(Symbol? symbol)
    {
        if (symbol is null) return;

        symbol.Accept(this);
    }

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个符号。
    /// </summary>
    /// <param name="symbol">要进行处理的符号。</param>
    protected virtual void DefaultVisit(Symbol symbol) { }
}
