// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Collections.Generic
{
    public static class EnumeratorExtensions
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
