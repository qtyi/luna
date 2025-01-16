// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;

namespace System;

public sealed class ByteEnumerator : IEnumerator, IEnumerator<byte>, IDisposable, ICloneable
{
    private
#if NETSTANDARD2_0
        Utf8String
#else
        Utf8String?
#endif
        _str;

    private int _index;

    private byte _currentElement;

#if NETSTANDARD2_0
    object
#else
    object?
#endif
    IEnumerator.Current => Current;

    public byte Current
    {
        get
        {
            if (_index == -1 || _index >= _str!.Length)
                throw new InvalidOperationException();

            return _currentElement;
        }
    }

    internal ByteEnumerator(Utf8String str)
    {
        _str = str;
        _index = -1;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public bool MoveNext()
    {
        if (_index < _str!.Length - 1)
        {
            _index++;
            _currentElement = _str[_index];
            return true;
        }
        _index = _str.Length;
        return false;
    }

    public void Dispose()
    {
        if (_str != null)
        {
            _index = _str.Length;
        }
        _str = null;
    }

    public void Reset()
    {
        _currentElement = 0;
        _index = -1;
    }
}
