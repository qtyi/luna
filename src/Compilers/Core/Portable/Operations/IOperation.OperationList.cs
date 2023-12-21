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
    public new readonly partial struct OperationList : IReadOnlyCollection<IOperation>
    {
        private readonly Operation _operation;

        internal OperationList(Operation operation) => this._operation = operation;

        public int Count => this._operation.ChildOperationsCount;

        public Enumerator GetEnumerator() => new(this._operation);

        public ImmutableArray<IOperation> ToImmutableArray()
        {
            switch (this._operation)
            {
                case { ChildOperationsCount: 0 }:
                    return ImmutableArray<IOperation>.Empty;
                case NoneOperation { Children: var children }:
                    return children;
                case InvalidOperation { Children: var children }:
                    return children;
                default:
                    var builder = ArrayBuilder<IOperation>.GetInstance(Count);
                    foreach (var child in this)
                        builder.Add(child);
                    return builder.ToImmutableAndFree();
            }
        }

        IEnumerator<IOperation> IEnumerable<IOperation>.GetEnumerator()
        {
            if (this.Count == 0)
                return SpecializedCollections.EmptyEnumerator<IOperation>();

            return new EnumeratorImpl(new(this._operation));
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<IOperation>)this).GetEnumerator();

        public bool Any() => this.Count > 0;

        public IOperation First()
        {
            var enumerator = this.GetEnumerator();
            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }

            throw new InvalidOperationException();
        }

        public Reversed Reverse() => new(this._operation);

        public IOperation Last()
        {
            var enumerator = this.Reverse().GetEnumerator();
            if (enumerator.MoveNext())
            {
                return enumerator.Current;
            }

            throw new InvalidOperationException();
        }

        public static implicit operator Microsoft.CodeAnalysis.IOperation.OperationList(OperationList operationList) => new(operationList._operation);

        /// <summary>
        /// Implements a struct-based enumerator for <see cref="Operation"/> nodes. This type is not hardened
        /// to <code>default(Enumerator)</code>, and will null reference in these cases. Calling <see cref="Current"/> after
        /// <see cref="Enumerator.MoveNext"/> has returned false will throw an <see cref="InvalidOperationException"/>.
        /// </summary>
        [NonDefaultable]
        public struct Enumerator
        {
            /// <summary>
            /// Implementation of the <see cref="Enumerator.MoveNext"/> and <see cref="Enumerator.Current"/>
            /// members are delegated to the virtual <see cref="Microsoft.CodeAnalysis.Operation.MoveNext(int, int)"/> and
            /// <see cref="Operation.GetCurrent(int, int)"/> methods, respectively.
            /// </summary>
            private readonly Operation _operation;
            /// <summary>
            /// 
            /// </summary>
            private int _currentSlot;
            private int _currentIndex;

            internal Enumerator(Operation operation)
            {
                this._operation = operation;
                this._currentSlot = -1;
                this._currentIndex = -1;
            }

            public readonly IOperation Current
            {
                get
                {
                    Debug.Assert(this._operation is not null && this._currentSlot >= 0 && this._currentIndex >= 0);
                    return this._operation.GetCurrent(this._currentSlot, this._currentIndex);
                }
            }

            public bool MoveNext()
            {
                (var result, this._currentSlot, this._currentIndex) = this._operation.MoveNext(this._currentSlot, this._currentIndex);
                return result;
            }

            public void Reset()
            {
                this._currentSlot = -1;
                this._currentIndex = -1;
            }
        }

        /// <summary>
        /// Implement <see cref="IEnumerator{T}"/>.
        /// </summary>
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
