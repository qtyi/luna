// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// 表示访问者基类。访问者每次接受类型为<typeparamref name="TArgument"/>的参数，访问和处理一个<see cref="Symbol"/>符号并产生类型为<typeparamref name="TResult"/>的结果。
/// </summary>
/// <typeparam name="TArgument">访问者的处理方法接受的参数的类型。</typeparam>
/// <typeparam name="TResult">访问者的处理方法的返回结果的类型。</typeparam>
internal abstract partial class
#if LANG_LUA
    LuaSymbolVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSymbolVisitor
#endif
    <TArgument, TResult>
{
    /// <summary>
    /// 处理这个符号并产生结果。
    /// </summary>
    /// <param name="symbol">要进行处理的符号。</param>
    /// <param name="argument">处理<paramref name="symbol"/>时接受的参数。</param>
    /// <returns>产生的结果。</returns>
    public virtual TResult? Visit(Symbol? symbol, TArgument? argument = default)
    {
        if (symbol is null) return default;

        return symbol.Accept(this, argument);
    }

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个符号并产生默认结果。
    /// </summary>
    /// <param name="symbol">要进行处理的符号。</param>
    /// <param name="argument">处理<paramref name="symbol"/>时接受的参数。</param>
    /// <returns>产生的结果。</returns>
    protected virtual TResult? DefaultVisit(Symbol symbol, TArgument argument) => default;
}

/// <summary>
/// 表示访问者基类。访问者每次访问和处理一个<see cref="Symbol"/>符号并产生类型为<typeparamref name="TResult"/>的结果。
/// </summary>
/// <typeparam name="TResult">访问者的处理方法的返回结果的类型。</typeparam>
internal abstract partial class
#if LANG_LUA
    LuaSymbolVisitor
#elif LANG_MOONSCRIPT
    MoonScriptSymbolVisitor
#endif
    <TResult>
{
    /// <summary>
    /// 处理这个符号并产生结果。
    /// </summary>
    /// <param name="symbol">要进行处理的符号。</param>
    /// <returns>产生的结果。</returns>
    public virtual TResult? Visit(Symbol? symbol)
    {
        if (symbol is null) return default;

        return symbol.Accept(this);
    }

    /// <summary>
    /// 内部方法，被其他访问方法调用来处理这个符号并产生默认结果。
    /// </summary>
    /// <param name="symbol">要进行处理的符号。</param>
    /// <returns>产生的结果。</returns>
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
