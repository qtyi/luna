// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal static partial class GeneratedNames
{

    internal static string GetDisplayFilePath(string filePath)
    {
        var pooledBuilder = PooledStringBuilder.GetInstance();
        AppendFileName(filePath, pooledBuilder.Builder);
        return pooledBuilder.ToStringAndFree();
    }

    private static void AppendFileName(string filePath, StringBuilder sb)
    {
        var fileName = FileNameUtilities.GetFileName(filePath, includeExtension: false);
        if (fileName is null)
        {
            return;
        }

        foreach (var ch in fileName)
        {
            sb.Append(ch switch
            {
                >= 'a' and <= 'z' => ch,
                >= 'A' and <= 'Z' => ch,
                >= '0' and <= '9' => ch,
                _ => '_'
            });
        }
    }
}
