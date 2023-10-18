// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

internal partial class LanguageParser
{
    [Flags]
    internal enum TerminatorState
    {
        EndOfFile = 0,
#warning 未完成。
    }

    private const int LastTerminatorState = (int)TerminatorState.EndOfFile;

    private partial bool IsTerminalCore(TerminatorState state) => state switch
    {
#warning 未完成。
        _ => false
    };
}
