// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Linq;

namespace Qtyi.CodeAnalysis
{
    internal static class ImmutableArrayExtensions
    {
        public static ImmutableArray<T> CastDown<T, TBase>(this ImmutableArray<TBase> items) where T : class?, TBase
        {
            if (items.IsDefault)
            {
                return default;
            }

            if (items.IsEmpty)
            {
                return ImmutableArray<T>.Empty;
            }

            var length = items.Length;
            var array = new T[length];
            for (var i = 0; i < length; i++)
            {
                array[i] = (T)items[i];
            }

            return ImmutableArray.Create(array);
        }
    }
}
