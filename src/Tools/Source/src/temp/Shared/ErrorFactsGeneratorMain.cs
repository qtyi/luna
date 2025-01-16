// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Generators;

#if LANG_LUA
using Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
using Qtyi.CodeAnalysis.MoonScript;
#else
#error Not implemented.
#endif

namespace Luna.Compilers.Tools;

internal static class Program
{
    public static int Main(string[] args)
    {
        ErrorFactsGenerator.GetCodeNames(Enum.GetNames(typeof(ErrorCode)), out var warningCodeNames, out var fatalCodeNames, out var infoCodeNames, out var hiddenCodeNames);

        Console.WriteLine("Warning:");
        foreach (var name in warningCodeNames)
            Console.WriteLine("  {0}", name);

        Console.WriteLine("Fatal:");
        foreach (var name in fatalCodeNames)
            Console.WriteLine("  {0}", name);

        Console.WriteLine("Info:");
        foreach (var name in infoCodeNames)
            Console.WriteLine("  {0}", name);

        Console.WriteLine("Hidden:");
        foreach (var name in hiddenCodeNames)
            Console.WriteLine("  {0}", name);

        return 0;
    }
}
