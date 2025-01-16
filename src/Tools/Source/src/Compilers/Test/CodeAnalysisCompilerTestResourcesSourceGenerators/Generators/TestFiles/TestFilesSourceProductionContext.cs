// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Luna.Tools;

/// <summary>
/// Context passed to an <see cref="LuaTestFilesSourceWriter"/> and <see cref="MoonScriptTestFilesSourceWriter"/> to start source production.
/// </summary>
internal readonly struct TestFilesSourceProductionContext
{
    public readonly ImmutableArray<string> FileNames;

    public TestFilesSourceProductionContext(ImmutableArray<string> fileNames)
    {
        FileNames = fileNames;
    }
}
