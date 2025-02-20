﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

#warning 未实现。
internal static partial class ErrorFacts
{
    private const string TitleSuffix = "_Title";
    private const string DescriptionSuffix = "_Description";

    private static readonly Lazy<ImmutableDictionary<ErrorCode, string>> s_lazyCategoriesMap = new(CreateCategoriesMap);

    private static partial ImmutableDictionary<ErrorCode, string> CreateCategoriesMap();

    private static string GetId(ErrorCode errorCode) => ThisMessageProvider.Instance.GetIdForErrorCode((int)errorCode);

    /// <summary>
    /// Gets a value indicates that specified error code value represents a fatal error.
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="code"/> represents a fatal error; otherwise, <see langword="false"/>.</returns>
    public static partial bool IsFatal(ErrorCode code); // Implementation is in generated code.

    /// <summary>
    /// Gets a value indicates that specified error code value represents a normal error.
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="code"/> represents a normal error; otherwise, <see langword="false"/>.</returns>
    public static partial bool IsError(ErrorCode code); // Implementation is in generated code.

    /// <summary>
    /// Gets a value indicates that specified error code value represents a warning.
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="code"/> represents a warning; otherwise, <see langword="false"/>.</returns>
    public static partial bool IsWarning(ErrorCode code); // Implementation is in generated code.

    /// <summary>
    /// Gets a value indicates that specified error code value represents an info that does not indicate a problem.
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="code"/> represents an info; otherwise, <see langword="false"/>.</returns>
    public static partial bool IsInfo(ErrorCode code); // Implementation is in generated code.

    /// <summary>
    /// Gets a value indicates that specified error code value represents an issue.
    /// </summary>
    /// <param name="code">Error code.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="code"/> represents an issue; otherwise, <see langword="false"/>.</returns>
    public static partial bool IsHidden(ErrorCode code); // Implementation is in generated code.

    internal static DiagnosticSeverity GetSeverity(ErrorCode code)
    {
        if (code == ErrorCode.Void)
            return InternalDiagnosticSeverity.Void;
        else if (code == ErrorCode.Unknown)
            return InternalDiagnosticSeverity.Unknown;
        else if (IsWarning(code))
            return DiagnosticSeverity.Warning;
        else if (IsInfo(code))
            return DiagnosticSeverity.Info;
        else if (IsHidden(code))
            return DiagnosticSeverity.Hidden;
        else
            return DiagnosticSeverity.Error;
    }

    private static ResourceManager? s_resourceManager;
    [MemberNotNull(nameof(s_resourceManager))]
    private static ResourceManager ResourceManager =>
        s_resourceManager ??= new(typeof(ThisResources).FullName!, typeof(ErrorCode).GetTypeInfo().Assembly);

    public static string GetMessage(MessageID code, CultureInfo? culture)
    {
        var message = ResourceManager.GetString(code.ToString(), culture);
        Debug.Assert(!string.IsNullOrEmpty(message), code.ToString());
        return message;
    }

    public static string GetMessage(ErrorCode code, CultureInfo? culture)
    {
        var message = ResourceManager.GetString(code.ToString(), culture);
        Debug.Assert(!string.IsNullOrEmpty(message), code.ToString());
        return message;
    }

    public static LocalizableResourceString GetMessageFormat(ErrorCode code) =>
        new(code.ToString(), ResourceManager, typeof(ErrorFacts));

    public static LocalizableResourceString GetTitle(ErrorCode code) =>
        new(code.ToString() + TitleSuffix, ResourceManager, typeof(ErrorFacts));

    public static LocalizableResourceString GetDescription(ErrorCode code) =>
        new(code.ToString() + DescriptionSuffix, ResourceManager, typeof(ErrorFacts));

    public static partial string GetHelpLink(ErrorCode code);

    public static string GetCategory(ErrorCode code)
    {
        if (s_lazyCategoriesMap.Value.TryGetValue(code, out var category))
            return category;

        return Diagnostic.CompilerDiagnosticCategory;
    }

    internal static int GetWarningLevel(ErrorCode code)
    {
        if (IsInfo(code) || IsHidden(code))
        {
            // Info and hidden diagnostics should always be produced because some analyzers depend on them.
            return Diagnostic.InfoAndHiddenWarningLevel;
        }

        // Warning wave warnings (warning level > 4) should be documented in
        // docs/compilers/CSharp/Warnversion Warning Waves.md
        if (IsWarning(code))
            return GetWarningLevelCore(code);

        return 0;
    }

    private static partial int GetWarningLevelCore(ErrorCode code);
}
