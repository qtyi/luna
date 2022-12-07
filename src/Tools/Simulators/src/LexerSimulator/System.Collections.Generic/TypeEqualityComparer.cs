namespace System.Collections.Generic;

internal class TypeEqualityComparer<T> : IEqualityComparer<T>
{
    public bool Equals(T? x, T? y) => x?.GetType() == y?.GetType();

    public int GetHashCode(T? obj) => obj?.GetType().GetHashCode() ?? 0;
}
