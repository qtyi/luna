// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// Provides the ability to classify and load messages for error codes.
/// </summary>
internal sealed partial class MessageProvider : IObjectWritable
{
    public static readonly MessageProvider Instance = new();

    static MessageProvider()
    {
        ObjectBinder.RegisterTypeReader(typeof(MessageProvider), r => Instance);
    }

    private MessageProvider() { }

    bool IObjectWritable.ShouldReuseInSerialization => true;

    void IObjectWritable.WriteTo(ObjectWriter writer)
    {
        // 不进行写入操作，永远读取或反序列化为全局实例
    }
}
