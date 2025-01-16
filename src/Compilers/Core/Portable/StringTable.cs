// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.PooledObjects;
using RoslynStringTable = Roslyn.Utilities.StringTable;

namespace Luna.Utilities;

internal class StringTable
{
    private readonly RoslynStringTable _underlyingTable = RoslynStringTable.GetInstance();

    private static readonly SegmentedDictionary<string, Utf8String> s_sharedMap = new(1 << 16);

    // implement Poolable object pattern
    #region "Poolable"

    private StringTable(ObjectPool<StringTable>? pool)
    {
        _pool = pool;
    }

    private readonly ObjectPool<StringTable>? _pool;
    private static readonly ObjectPool<StringTable> s_staticPool = CreatePool();

    private static ObjectPool<StringTable> CreatePool()
    {
        var pool = new ObjectPool<StringTable>(pool => new StringTable(pool), Environment.ProcessorCount * 2);
        return pool;
    }

    public static StringTable GetInstance()
    {
        return s_staticPool.Allocate();
    }

    public void Free()
    {
        // leave cache content in the cache, just return it to the pool
        _underlyingTable.Free();

        _pool?.Free(this);
    }

    #endregion // Poolable

    #region UTF-16
    internal string Add(char[] chars, int start, int len) => _underlyingTable.Add(chars, start, len);

    internal string Add(string chars, int start, int len) => _underlyingTable.Add(chars, start, len);

    internal string Add(char chars) => _underlyingTable.Add(chars);

    internal string Add(StringBuilder chars) => _underlyingTable.Add(chars);

    internal string Add(string chars) => _underlyingTable.Add(chars);

    internal static string AddShared(StringBuilder chars) => RoslynStringTable.AddShared(chars);
    #endregion

    #region UTF-8
    internal Utf8String Add(byte[] bytes, int start, int len)
    {
        Debug.Assert(start >= 0);
        Debug.Assert(len >= 0);
        Debug.Assert(start + len < bytes.Length);

        return AddItem(new ArrayByteProvider(bytes, start, len));
    }

    internal Utf8String Add(Utf8String bytes, int start, int len)
    {
        Debug.Assert(start >= 0);
        Debug.Assert(len >= 0);
        Debug.Assert(start + len < bytes.Length);

        return AddItem(new Utf8StringByteProvider(bytes, start, len));
    }

    internal Utf8String Add(byte bytes)
        => AddItem(new ArrayByteProvider([bytes]));

    internal Utf8String Add(ArrayBuilder<byte> bytes)
        => AddItem(new ArrayBuilderByteProvider(bytes));

    internal Utf8String Add(Utf8String bytes)
        => AddItem(new Utf8StringByteProvider(bytes));

    private Utf8String AddItem(ByteProvider provider)
    {
        var key = _underlyingTable.Add(provider.ToString());
        if (!s_sharedMap.TryGetValue(key, out var value))
        {
            value = provider.ToUtf8String();
            if (!s_sharedMap.TryAdd(key, value))
                value = s_sharedMap[key];
        }
        return value;
    }

    private abstract class ByteProvider
    {
        public sealed override string ToString() => ToUtf8String().ToString();

        public abstract Utf8String ToUtf8String();
    }

    private sealed class ArrayByteProvider : ByteProvider
    {
        private readonly byte[] _bytes;
        private readonly int _start;
        private readonly int _length;

        public ArrayByteProvider(byte[] bytes) : this(bytes, start: 0, length: bytes.Length) { }

        public ArrayByteProvider(byte[] bytes, int start, int length)
        {
            _bytes = bytes;
            _start = start;
            _length = length;
        }

        public override Utf8String ToUtf8String() => new(_bytes, _start, _length);
    }

    private sealed class Utf8StringByteProvider : ByteProvider
    {
        private readonly Utf8String _bytes;
        private readonly int _start;
        private readonly int _length;

        public Utf8StringByteProvider(Utf8String bytes) : this(bytes, start: 0, length: bytes.Length) { }

        public Utf8StringByteProvider(Utf8String bytes, int start, int length)
        {
            _bytes = bytes;
            _start = start;
            _length = length;
        }

        public override Utf8String ToUtf8String()
        {
            if (_start == 0 && _length == _bytes.Length)
                return _bytes;

            return _bytes.Substring(_start, _length);
        }
    }

    private sealed class ArrayBuilderByteProvider : ByteProvider
    {
        private readonly ArrayBuilder<byte> _bytes;
        private readonly int _start;
        private readonly int _length;

        public ArrayBuilderByteProvider(ArrayBuilder<byte> bytes) : this(bytes, start: 0, length: bytes.Count) { }

        public ArrayBuilderByteProvider(ArrayBuilder<byte> bytes, int start, int length)
        {
            _bytes = bytes;
            _start = start;
            _length = length;
        }

        public override Utf8String ToUtf8String() => new(_bytes.ToArray(), _start, _length);
    }
    #endregion
}
