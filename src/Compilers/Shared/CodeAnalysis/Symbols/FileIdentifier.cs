// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;
using MSCA::System.Runtime.CompilerServices;
using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal readonly struct FileIdentifier
{
    private static readonly Encoding s_encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

    public string? EncoderFallbackErrorMessage { get; init; }
    public ImmutableArray<byte> FilePathChecksumOpt { get; init; }
    public string DisplayFilePath { get; init; }

    public static FileIdentifier Create(SyntaxTree tree)
        => Create(tree.FilePath);

    public static FileIdentifier Create(string filePath)
    {
        string? encoderFallbackErrorMessage = null;
        ImmutableArray<byte> hash = default;
        try
        {
            var encodedFilePath = s_encoding.GetBytes(filePath);
            using var sha256 = SHA256.Create();
            hash = sha256.ComputeHash(encodedFilePath).ToImmutableArray();
        }
        catch (EncoderFallbackException ex)
        {
            encoderFallbackErrorMessage = ex.Message;
        }

        var displayFilePath = GeneratedNames.GetDisplayFilePath(filePath);
        return new FileIdentifier { EncoderFallbackErrorMessage = encoderFallbackErrorMessage, FilePathChecksumOpt = hash, DisplayFilePath = displayFilePath };
    }
}
