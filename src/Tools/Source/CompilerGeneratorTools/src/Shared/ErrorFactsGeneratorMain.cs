// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using System.Text;
using Luna.Compilers.Generators;

namespace Luna.Compilers.Tools;

internal static class Program
{
    public static int Main(string[] args)
    {
        if (!ErrorFactsGenerator.TryGetCodeNames(out var warningCodeNames, out var fatalCodeNames, out var infoCodeNames, out var hiddenCodeNames)) return 1;

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
