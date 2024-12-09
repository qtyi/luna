// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

internal static partial class LanguageVersionExtensionsInternal
{
    /// <summary>
    /// 检查指定的值是否合法（在<see cref="LanguageVersion"/>枚举中）。
    /// </summary>
    /// <param name="value">指定的语言版本枚举值。</param>
    /// <returns>指定的值是否合法。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsValid(this LanguageVersion value) => Enum.IsDefined(typeof(LanguageVersion), value);

    /// <summary>
    /// 获取与指定<see cref="LanguageVersion"/>枚举值对应的“该特性在当前版本中不支持”的<see cref="ErrorCode"/>。
    /// </summary>
    /// <param name="version">指定的语言版本枚举值。</param>
    /// <returns>对应的错误码。</returns>
    internal static partial ErrorCode GetErrorCode(this LanguageVersion version);
}

internal sealed partial class
#if LANG_LUA
    LuaRequiredLanguageVersion
#elif LANG_MOONSCRIPT
    MoonScriptRequiredLanguageVersion
#endif
    : RequiredLanguageVersion
{
    internal LanguageVersion Version { get; init; }

    internal
#if LANG_LUA
        LuaRequiredLanguageVersion
#elif LANG_MOONSCRIPT
        MoonScriptRequiredLanguageVersion
#endif
        (LanguageVersion version) => this.Version = version;

    public override string ToString() => this.Version.ToDisplayString();
}

public static partial class LanguageVersionFacts
{
    /// <summary>
    /// 返回在控制行中（开启/langver开关）显示文本的格式一致的版本数字。
    /// 例如："5"、"5.4"、"latest"。
    /// </summary>
    /// <param name="version">要获取显示文本的语言版本。</param>
    /// <returns>语言版本的显示文本。</returns>
    public static partial string ToDisplayString(this LanguageVersion version);

    /// <summary>
    /// 尝试从字符串输入中分析出<see cref="LanguageVersion"/>，若<paramref name="result"/>为<see langword="null"/>时返回<see cref="LanguageVersion.Default"/>。
    /// </summary>
    /// <param name="version">字符串输入。</param>
    /// <param name="result">分析出的语言版本。</param>
    /// <returns></returns>
    public static partial bool TryParse(string? version, out LanguageVersion result);

    /// <summary>
    /// 将一个特定的语言版本（例如<see cref="LanguageVersion.Default"/>、<see cref="LanguageVersion.Latest"/>）映射到一个具体的版本。
    /// </summary>
    /// <param name="version">要映射的语言版本。</param>
    /// <returns></returns>
    public static partial LanguageVersion MapSpecifiedToEffectiveVersion(this LanguageVersion version);
}
