// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using Xunit;

namespace Luna.Test.Utilities;

public class EnumTheoryData<T> : TheoryData<T>
    where T : struct, Enum
{
    public EnumTheoryData(
        IEnumerable<T>? includeValues = null,
        Func<T, bool>? includeFunc = null,
        IEnumerable<T>? excludeValues = null,
        Func<T, bool>? excludeFunc = null,
        bool excludeObsolete = true,
        bool ordered = true)
    {
        var data = GetDefinedValues()
            .Where(value =>
            {
                if (includeValues is not null && !includeValues.Contains(value))
                    return false;

                if (includeFunc is not null && !includeFunc(value))
                    return false;

                if (excludeValues is not null && excludeValues.Contains(value))
                    return false;

                if (excludeFunc is not null && excludeFunc(value))
                    return false;

                return true;
            });

        if (ordered)
        {
#if NETFRAMEWORK
            data = data.OrderBy(static value => value);
#else
            data = data.Order();
#endif
        }

        foreach (var value in data)
            Add(value);
    }

    protected virtual IEnumerable<T> GetDefinedValues(bool excludeObsolete = true)
    {
        return from value in
#if NETFRAMEWORK
                   Enum.GetValues(typeof(T)).Cast<T>()
#else
                   Enum.GetValues<T>()
#endif
               let enumType = typeof(T)
               let name = Enum.GetName(enumType, value)!
               let field = enumType.GetField(name)!
               let obsolete = field.GetCustomAttribute<ObsoleteAttribute>()
               // Only include values if:
               // - they are NOT obsolete
               // - they are obsolete and NOT explicitly excluded and NOT raise errors
               where obsolete is null || (!excludeObsolete && !obsolete.IsError)
               select value;
    }
}
