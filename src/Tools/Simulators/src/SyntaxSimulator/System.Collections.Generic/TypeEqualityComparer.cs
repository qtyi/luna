// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Collections.Generic;

internal class TypeEqualityComparer<T> : IEqualityComparer<T>
{
    public bool Equals(T? x, T? y) => x?.GetType() == y?.GetType();

    public int GetHashCode(T? obj) => obj?.GetType().GetHashCode() ?? 0;
}
