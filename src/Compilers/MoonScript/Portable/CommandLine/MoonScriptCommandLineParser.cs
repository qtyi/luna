// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MoonScriptCommandLineParser
{
    /// <summary>
    /// File extension of a regular MoonScript source file.
    /// </summary>
    protected override string RegularFileExtension => ".moon";
    /// <summary>
    /// File extension of a MoonScript script file.
    /// </summary>
    protected override string ScriptFileExtension => ".moon";

    public new partial MoonScriptCommandLineArguments Parse(IEnumerable<string> args, string? baseDirectory, string? sdkDirectory, string? additionalReferenceDirectories)
    {
#warning 未实现。
        throw new NotImplementedException();
    }
}
