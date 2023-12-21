// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.PooledObjects;
using Qtyi.CodeAnalysis.Operations;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

partial interface IOperation
{
    partial struct OperationList
    {
        /// <summary>
        /// Implements a reverse-order struct-based collection of <see cref="Operation"/> nodes.
        /// This collection is ordered, but random access into the collection is not provided.
        /// </summary>
        [NonDefaultable]
        public readonly struct Reversed : IReadOnlyCollection<IOperation>
        {
            private readonly Operation _operation;

            internal Reversed(Operation operation)
            {
                _operation = operation;
            }

            public int Count => _operation.ChildOperationsCount;

            public Enumerator GetEnumerator() => new Enumerator(_operation);

            public ImmutableArray<IOperation> ToImmutableArray()
            {
                Enumerator enumerator = GetEnumerator();
                switch (_operation)
                {
                    case { ChildOperationsCount: 0 }:
                        return ImmutableArray<IOperation>.Empty;
                    case NoneOperation { Children: var children }:
                        return reverseArray(children);
                    case InvalidOperation { Children: var children }:
                        return reverseArray(children);
                    default:
                        var builder = ArrayBuilder<IOperation>.GetInstance(Count);
                        foreach (var child in this)
                            builder.Add(child);
                        return builder.ToImmutableAndFree();
                }

                static ImmutableArray<IOperation> reverseArray(ImmutableArray<IOperation> input)
                {
                    var builder = ArrayBuilder<IOperation>.GetInstance(input.Length);
                    for (var i = input.Length - 1; i >= 0; i--)
                        builder.Add(input[i]);

                    return builder.ToImmutableAndFree();
                }
            }

            IEnumerator<IOperation> IEnumerable<IOperation>.GetEnumerator()
            {
                if (this.Count == 0)
                    return SpecializedCollections.EmptyEnumerator<IOperation>();

                return new EnumeratorImpl(new Enumerator(this._operation));
            }

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<IOperation>)this).GetEnumerator();

            /// <summary>
            /// Implements a reverse-order struct-based enumerator for <see cref="Operation"/> nodes. This type is not hardened
            /// to <code>default(Enumerator)</code>, and will null reference in these cases. Calling <see cref="Current"/> after
            /// <see cref="Enumerator.MoveNext"/> has returned false will throw an <see cref="InvalidOperationException"/>.
            /// </summary>
            [NonDefaultable]
            public struct Enumerator
            {
                private readonly Operation _operation;
                private int _currentSlot;
                private int _currentIndex;

                internal Enumerator(Operation operation)
                {
                    this._operation = operation;
                    this._currentSlot = int.MaxValue;
                    this._currentIndex = int.MaxValue;
                }

                public readonly IOperation Current
                {
                    get
                    {
                        Debug.Assert(this._operation != null && this._currentSlot is >= 0 and not int.MaxValue && this._currentIndex is >= 0 and not int.MaxValue);
                        return this._operation.GetCurrent(this._currentSlot, this._currentIndex);
                    }
                }

                public bool MoveNext()
                {
                    Debug.Assert((this._currentSlot == int.MaxValue) == (this._currentIndex == int.MaxValue));
                    (var result, this._currentSlot, this._currentIndex) = this._operation.MoveNextReversed(this._currentSlot, this._currentIndex);
                    return result;
                }

                public void Reset()
                {
                    this._currentIndex = int.MaxValue;
                    this._currentSlot = int.MaxValue;
                }
            }

            private sealed class EnumeratorImpl : IEnumerator<IOperation>
            {
                private Enumerator _enumerator;

                public EnumeratorImpl(Enumerator enumerator) => this._enumerator = enumerator;

                public IOperation Current => this._enumerator.Current;
                object? IEnumerator.Current => this._enumerator.Current;
                public void Dispose() { }
                public bool MoveNext() => this._enumerator.MoveNext();
                public void Reset() => this._enumerator.Reset();
            }
        }
    }
}
