// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ICSharpCode.Decompiler.Metadata;

namespace Xunit;

public static class TheoryDataExtensions
{
    #region Combine
    private class CombineTheoryData<T1, T2> : TheoryData<T1, T2>
    {
        public CombineTheoryData(TheoryData<T1> data1, TheoryData<T2> data2)
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

    private class CombineTheoryData<T1, T2, T3> : TheoryData<T1, T2, T3>
    {
        public CombineTheoryData(TheoryData<T1> data1, TheoryData<T2> data2, TheoryData<T3> data3)
        {
            foreach (var row1 in data1)
            {
                foreach (var row2 in data2)
                {
                    foreach (var row3 in data3)
                    {
                        AddRow(row1[0], row2[0], row3[0]);
                    }
                }
            }
        }

        public CombineTheoryData(TheoryData<T1> data1, TheoryData<T2, T3> data2)
        {
            foreach (var row1 in data1)
            {
                foreach (var row2 in data2)
                {
                    AddRow(row1[0], row2[0], row2[1]);
                }
            }
        }

        public CombineTheoryData(TheoryData<T1, T2> data1, TheoryData<T3> data2)
        {
            foreach (var row1 in data1)
            {
                foreach (var row2 in data2)
                {
                    AddRow(row1[0], row2[1], row2[0]);
                }
            }
        }
    }

    public static TheoryData<T1, T2> Combine<T1, T2>(this TheoryData<T1> first, TheoryData<T2> second) => new CombineTheoryData<T1, T2>(first, second);

    public static TheoryData<T1, T2, T3> Combine<T1, T2, T3>(this TheoryData<T1> first, TheoryData<T2, T3> second) => new CombineTheoryData<T1, T2, T3>(first, second);

    public static TheoryData<T1, T2, T3> Combine<T1, T2, T3>(this TheoryData<T1, T2> first, TheoryData<T3> second) => new CombineTheoryData<T1, T2, T3>(first, second);
    #endregion

    #region Concat
    private class ConcatTheoryData<T> : TheoryData<T>
    {
        public ConcatTheoryData(TheoryData<T> data1, TheoryData<T> data2)
        {
            foreach (var row1 in data1)
                AddRow(row1);

            foreach (var row2 in data2)
                AddRow(row2);
        }
    }

    private class ConcatTheoryData<T1, T2> : TheoryData<T1, T2>
    {
        public ConcatTheoryData(TheoryData<T1, T2> data1, TheoryData<T1, T2> data2)
        {
            foreach (var row1 in data1)
                AddRow(row1);

            foreach (var row2 in data2)
                AddRow(row2);
        }
    }

    public static TheoryData<T> Combine<T>(this TheoryData<T> first, TheoryData<T> second) => new ConcatTheoryData<T>(first, second);

    public static TheoryData<T1, T2> Combine<T1, T2>(this TheoryData<T1, T2> first, TheoryData<T1, T2> second) => new ConcatTheoryData<T1, T2>(first, second);
    #endregion

    #region Where
    private sealed class WhereTheoryData<T> : TheoryData<T>
    {
        public WhereTheoryData(TheoryData<T> source, Func<T, bool> predicate)
        {
            foreach (var row in source)
            {
                if (predicate((T)row[0]))
                    AddRow(row);
            }
        }
    }

    private sealed class WhereTheoryData<T1, T2> : TheoryData<T1, T2>
    {
        public WhereTheoryData(TheoryData<T1, T2> source, Func<T1, T2, bool> predicate)
        {
            foreach (var row in source)
            {
                if (predicate((T1)row[0], (T2)row[1]))
                    AddRow(row);
            }
        }
    }

    public static TheoryData<T> Where<T>(this TheoryData<T> source, Func<T, bool> predicate) => new WhereTheoryData<T>(source, predicate);

    public static TheoryData<T1, T2> Where<T1, T2>(this TheoryData<T1, T2> source, Func<T1, T2, bool> predicate) => new WhereTheoryData<T1, T2>(source, predicate);
    #endregion
}
