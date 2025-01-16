// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Luna.Test.Utilities;

public class CombinatorialTheoryData<T1, T2> : TheoryData<T1, T2>
{
    public CombinatorialTheoryData(IEnumerable<T1> data1, IEnumerable<T2> data2)
    {
        foreach (var value1 in data1)
        {
            foreach (var value2 in data2)
            {
                AddRow(value1, value2);
            }
        }
    }

    public CombinatorialTheoryData(IEnumerable<T1> data1, TheoryData<T2> data2)
    {
        foreach (var value1 in data1)
        {
            foreach (var row2 in data2)
            {
                AddRow(value1, row2[0]);
            }
        }
    }

    public CombinatorialTheoryData(TheoryData<T1> data1, IEnumerable<T2> data2)
    {
        foreach (var row1 in data1)
        {
            foreach (var value2 in data2)
            {
                AddRow(row1[0], value2);
            }
        }
    }

    public CombinatorialTheoryData(TheoryData<T1> data1, TheoryData<T2> data2)
    {
        foreach (var row1 in data1)
        {
            foreach (var row2 in data2)
            {
                AddRow(row1[0], row2[0]);
            }
        }
    }
}
