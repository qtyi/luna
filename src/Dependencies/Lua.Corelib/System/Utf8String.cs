// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace System;

[Serializable]
[DebuggerStepThrough]
public sealed partial class Utf8String
    : IComparable,
      IEnumerable,
      //IConvertible,
      IEnumerable<byte>,
      IComparable<
#if NETSTANDARD2_0
      Utf8String
#else
      Utf8String?
#endif
      >,
      IEquatable<
#if NETSTANDARD2_0
      Utf8String
#else
      Utf8String?
#endif
      >,
      ICloneable
{
    public static readonly Utf8String Empty =
#if NETSTANDARD2_0
        new((byte[]?)null);
#else
        ""u8;
#endif

    private readonly byte[] _data;

    public Utf8String(
#if NETSTANDARD2_0
        byte[]
#else
        byte[]?
#endif
        value)
    {
        if (value is null || value.Length == 0)
            _data = [];
        else
        {
            var length = value.Length;
            _data = new byte[length];
            value.CopyTo(_data, index: 0);
        }
    }

    public Utf8String(byte[] value, int startIndex, int length)
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(value);
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, value.Length - length);
#else
        if (value is null) throw new ArgumentNullException(nameof(value));
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (startIndex > value.Length - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

        if (length == 0)
            _data = [];
        else
        {
            _data = new byte[length];
            Array.Copy(value, startIndex, _data, 0, length);
        }
    }

    [CLSCompliant(false)]
    public unsafe Utf8String(byte* value)
    {
        var dataLength = 0;
        while (*value != 0)
        {
            dataLength++;
        }

        _data = new byte[dataLength];
        if (dataLength > 0)
        {
            fixed (byte* p = &_data[0])
            {
                Buffer.MemoryCopy(value, p, 1, dataLength);
            }
        }
    }

    [CLSCompliant(false)]
    public unsafe Utf8String(byte* value, int startIndex, int length)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
#else
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(length));
#endif

        if (length == 0)
            _data = [];
        else
        {
            var dataLength = 0;
            while (*value != 0)
            {
                dataLength++;
                value++;
            }

#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, dataLength - length);
#else
            if (startIndex > dataLength - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

            _data = new byte[length];
            if (length > 0)
            {
                fixed (byte* p = &_data[0])
                {
                    Buffer.MemoryCopy(value, p, 1, length);
                }
            }
        }
    }

    public Utf8String(byte b, int count)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(count);
#endif

        _data = new byte[count];
        if (count > 0)
        {
#if NETSTANDARD2_0
            for (var i = 0; i < count; i++)
            {
                _data[i] = b;
            }
#else
            Array.Fill(_data, b);
#endif
        }
    }

#if !NETSTANDARD2_0
    public Utf8String(ReadOnlySpan<byte> value)
    {
        if (value.IsEmpty)
            _data = [];
        else
        {
            var length = value.Length;
            _data = new byte[length];
            value.CopyTo(_data);
        }
    }
#endif

    [IndexerName("Chars")]
    public byte this[int index]
    {
        get
        {
            if ((uint)index >= (uint)_data.Length) throw new ArgumentOutOfRangeException(nameof(index));

            return _data[index];
        }
    }

    public int Length => _data.Length;

    public object Clone() => this;

    public int CompareTo(
#if NETSTANDARD2_0
        object
#else
        object?
#endif
        value)
    {
        if (value is null)
            return 1;

        if (value is not Utf8String strB)
            throw new ArgumentException();

        return CompareTo(strB);
    }

    public int CompareTo(
#if NETSTANDARD2_0
        Utf8String
#else
        Utf8String?
#endif
        strB) => Compare(this, strB);

    public static int Compare(
#if NETSTANDARD2_0
        Utf8String
#else
        Utf8String?
#endif
        strA,
#if NETSTANDARD2_0
        Utf8String
#else
        Utf8String?
#endif
        strB)
    {
        if (ReferenceEquals(strA, strB))
            return 0;

        if (strA is null)
            return -1;

        if (strB is null)
            return 1;

        return CompareOrdinalIgnoreCase(strA, strB);
    }

    private static int CompareOrdinalIgnoreCase(Utf8String strA, Utf8String strB)
    {
        var length1 = strA._data.Length;
        var length2 = strB._data.Length;
        var minLength = Math.Min(length1, length2);

        for (var i = 0; i < minLength; i++)
        {
            var char1 = strA._data[i];
            var char2 = strB._data[i];
            if (char1 != char2)
                return char1 - char2;
        }

        if (minLength == length1)
            return minLength - length2;
        else
            return length1 - minLength;
    }

    public override bool Equals(
#if !NETSTANDARD2_0
        [NotNullWhen(true)] object?
#else
        object
#endif
        obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Utf8String text)
            return false;

        if (Length != text.Length)
            return false;

        return EqualsHelper(this, text);
    }

    public bool Equals(
#if !NETSTANDARD2_0
        [NotNullWhen(true)] Utf8String?
#else
        Utf8String
#endif
        value)
    {
        if (ReferenceEquals(this, value))
            return true;

        if (value is null)
            return false;

        if (Length != value.Length)
            return false;

        return EqualsHelper(this, value);
    }

    public static bool Equals(
#if !NETSTANDARD2_0
        [NotNullWhen(true)] Utf8String?
#else
        Utf8String
#endif
        a,
#if !NETSTANDARD2_0
        [NotNullWhen(true)] Utf8String?
#else
        Utf8String
#endif
        b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a is null || b is null || a.Length != b.Length)
            return false;

        return EqualsHelper(a, b);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EqualsHelper(Utf8String strA, Utf8String strB)
    {
        return strA._data.SequenceEqual(strB._data);
    }

    public ByteEnumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => new ByteEnumerator(this);

    IEnumerator<byte> IEnumerable<byte>.GetEnumerator() => new ByteEnumerator(this);

    public override int GetHashCode() => _data.GetHashCode();

    public Utf8String Substring(int startIndex)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
#else
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

        var length = _data.Length - startIndex;
        if (length == 0)
            return Empty;

        return SubstringInternal(startIndex, length);
    }

    public Utf8String Substring(int startIndex, int length)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, _data.Length - length);
#else
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (startIndex > _data.Length - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

        if (length == 0)
            return Empty;

        return SubstringInternal(startIndex, length);
    }

    private Utf8String SubstringInternal(int startIndex, int length) => new(_data, startIndex, length);

    public byte[] ToByteArray()
    {
        var length = _data.Length;
        if (length == 0)
            return [];

        var bytes = new byte[length];
        _data.CopyTo(bytes, index: 0);

        return bytes;
    }

    public byte[] ToByteArray(int startIndex, int length)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, _data.Length - length);
#else
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(length));
        if (startIndex > _data.Length - length) throw new ArgumentOutOfRangeException(nameof(startIndex));
#endif

        if (length == 0)
            return [];

        var bytes = new byte[length];
        Array.Copy(_data, startIndex, bytes, 0, length);

        return bytes;
    }

#if !NETSTANDARD2_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Utf8String(ReadOnlySpan<byte> value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ReadOnlySpan<byte>(Utf8String? value) => value is not null ? value._data : ReadOnlySpan<byte>.Empty;
#endif
}
