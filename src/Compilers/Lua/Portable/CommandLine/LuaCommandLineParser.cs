// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaCommandLineParser
{
    /// <summary>
    /// File extension of a regular Lua source file.
    /// </summary>
    protected override string RegularFileExtension => ".lua";
    /// <summary>
    /// File extension of a Lua script file.
    /// </summary>
    protected override string ScriptFileExtension => ".lua";

    public new partial LuaCommandLineArguments Parse(IEnumerable<string> args, string? baseDirectory, string? sdkDirectory, string? additionalReferenceDirectories)
    {
#warning 未实现。
        throw new NotImplementedException();
    }
}
