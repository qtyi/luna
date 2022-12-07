using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public static class Enumerator
    {
        public static IEnumerable<object?> GetEnumerable(this IEnumerator etor)
        {
            while (etor.MoveNext())
                yield return etor.Current;
        }

        public static IEnumerable<T?> GetEnumerable<T>(this IEnumerator etor)
        {
            while (etor.MoveNext())
                yield return (T?)etor.Current;
        }

        public static IEnumerable<T> GetEnumerable<T>(this IEnumerator<T> etor)
        {
            while (etor.MoveNext())
                yield return etor.Current;
        }
    }
}
