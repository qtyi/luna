// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace System;

partial class Utf8String
{
    internal static readonly Encoding Encoding = Encoding.GetEncoding(Encoding.UTF8.CodePage, EncoderFallback.ExceptionFallback, new DecoderFallback());

    private sealed class DecoderFallback : Text.DecoderFallback
    {
        public override int MaxCharCount => 4; // `\xXX`

        public override Text.DecoderFallbackBuffer CreateFallbackBuffer() => new DecoderFallbackBuffer();
    }

    private sealed class DecoderFallbackBuffer : Text.DecoderFallbackBuffer
    {
#pragma warning disable CS8618
        private string _replacement;
#pragma warning restore CS8618
        private int _fallbackCount = -1;
        private int _fallbackIndex = -1;

#if NETSTANDARD
        // WORKAROUND(sanmuru): Workaround for bug https://github.com/dotnet/runtime/issues/26268.
        private static readonly bool s_hasBugCoreclr26268;

        static DecoderFallbackBuffer()
        {
            try
            {
                var codec = new UTF8Encoding(false, throwOnInvalidBytes: true);
                codec.GetCharCount([0xFF]);
                s_hasBugCoreclr26268 = false;
            }
            catch (DecoderFallbackException ex)
            {
                s_hasBugCoreclr26268 = ex.Index < 0;
            }
        }
#endif

        public override int Remaining => _fallbackCount >= 0 ? _fallbackCount : 0;

        public override bool Fallback(byte[] bytesUnknown, int index)
        {
#if NETSTANDARD
            if (s_hasBugCoreclr26268)
                index += bytesUnknown.Length;
#endif

            if (index < 0 || index >= bytesUnknown.Length)
                return false;

            _replacement = $"\\x{bytesUnknown[index]:X2}";
            _fallbackCount = 4;
            _fallbackIndex = -1;
            return true;
        }

        public override char GetNextChar()
        {
            _fallbackCount--;
            _fallbackIndex++;
            if (_fallbackCount < 0)
            {
                return '\0';
            }
            if (_fallbackCount == int.MaxValue)
            {
                _fallbackCount = -1;
                return '\0';
            }
            return _replacement[_fallbackIndex];
        }

        public override bool MovePrevious()
        {
            if (_fallbackCount >= -1 && _fallbackIndex >= 0)
            {
                _fallbackIndex--;
                _fallbackCount++;
                return true;
            }
            return false;
        }
    }

    public override string ToString() => _data.Length == 0 ? "" : Encoding.GetString(_data);
}
