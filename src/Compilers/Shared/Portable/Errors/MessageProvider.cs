// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// Provides the ability to classify and load messages for error codes.
/// </summary>
internal sealed partial class MessageProvider
{
    public static readonly ThisMessageProvider Instance = new();

    private MessageProvider() { }
}
