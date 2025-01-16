// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System;

partial class Utf8String
{
#if !NETSTANDARD2_0
    public ReadOnlyMemory<byte> AsMemory() => _data.AsMemory();

    public ReadOnlyMemory<byte> AsMemory(int start) => _data.AsMemory(start);

    public ReadOnlyMemory<byte> AsMemory(int start, int length) => _data.AsMemory(start, length);

    public ReadOnlyMemory<byte> AsMemory(Index startIndex) => _data.AsMemory(startIndex);

    public ReadOnlyMemory<byte> AsMemory(Range range) => _data.AsMemory(range);

    public ReadOnlySpan<byte> AsSpan() => _data.AsSpan();

    public ReadOnlySpan<byte> AsSpan(int start) => _data.AsSpan(start);

    public ReadOnlySpan<byte> AsSpan(int start, int length) => _data.AsSpan(start, length);

    public ReadOnlySpan<byte> AsSpan(Index startIndex) => _data.AsSpan(startIndex);

    public ReadOnlySpan<byte> AsSpan(Range range) => _data.AsSpan(range);
#endif
}
