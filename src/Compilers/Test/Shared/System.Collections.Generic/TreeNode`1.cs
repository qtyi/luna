using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic;

internal sealed class TreeNode<T> : IList<T>, IReadOnlyList<T>
{
    internal Tree<T> tree;
    internal TreeNode<T>? parent;
    internal T value;
    internal List<TreeNode<T>> children = new();

    public Tree<T>? Tree => this.tree;

    public TreeNode<T>? Parent => this.Parent;

    public T Value { get => this.value; set => this.value = value; }

    public IReadOnlyList<TreeNode<T>> Children => this.children;

    public int Count => this.children.Count;

    internal int ChildNodesCount => this.children.Sum(node => node.ChildNodesCount);

    bool ICollection<T>.IsReadOnly => ((ICollection<T>)this.children).IsReadOnly;

    public T this[int index] { get => this.children[index].Value; set => this.children[index].Value = value; }

    public TreeNode(Tree<T> tree, T value)
    {
        this.tree = tree;
        this.value = value;
    }

    internal TreeNode(Tree<T> tree, TreeNode<T>? parent, T value)
    {
        this.tree = tree;
        this.parent = parent;
        this.value = value;
    }

    internal void Invalidate()
    {
        this.tree = null!;
    }

    public TreeNode<T> Add(T value)
    {
        var node = new TreeNode<T>(this.tree, this, value);
        this.children.Add(node);
        return node;
    }

    public void Add(TreeNode<T> node)
    {
        if (node.Tree is null)
            throw new ArgumentException("节点不合法。", nameof(node));
        else if (!object.ReferenceEquals(this.tree, node.tree))
            throw new ArgumentException("节点位于其他树上。", nameof(node));

        this.children.Add(node);
    }

    void ICollection<T>.Add(T value) => this.Add(value);

    public void Clear() => this.children.Clear();

    public bool Contains(T value) => this.IndexOf(value) >= 0;

    public void CopyTo(T[] array, int arrayIndex)
    {
        for (int i = arrayIndex, n = array.Length; i < n; i++)
            array[i] = this.children[i - arrayIndex].value;
    }

    public TreeNode<T>? Find(T value)
    {
        var index = this.IndexOf(value);
        if (index < 0) return null;
        else return this.children[index];
    }

    public int IndexOf(T value)
    {
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0, n = this.children.Count; i < n; i++)
        {
            var node = this.children[i];
            if (comparer.Equals(node.value, this.value))
                return i;
        }
        return -1;
    }

    public void Insert(int index, T value) => this.children.Insert(index, new TreeNode<T>(this.tree, this, value));

    public void RemoveAt(int index) => this.children.RemoveAt(index);

    public bool Remove(T value)
    {
        var node = this.Find(value);
        if (node is null) return false;
        else return this.children.Remove(node);
    }

    public Enumerator GetEnumerator() => new(this);

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

    public struct Enumerator : IEnumerator<T>
    {
        private readonly List<TreeNode<T>>.Enumerator _childrenEtor;
        private IEnumerator<T>? _etor = null;
        private bool initialized = false;

        internal Enumerator(TreeNode<T> node) => this._childrenEtor = node.children.GetEnumerator();

        public T Current
        {
            get
            {
                if (this._etor is null) throw new InvalidOperationException();
                return this._etor.Current;
            }
        }

        object? IEnumerator.Current => this.Current;

        public void Dispose()
        {
            this._etor?.Dispose();
            this._childrenEtor.Dispose();
        }

        public bool MoveNext()
        {
            if (!this.initialized)
            {
                if (this._childrenEtor.MoveNext())
                    this._etor = this._childrenEtor.Current.GetEnumerator();
                else
                    return false;
            }

            Debug.Assert(this._etor is not null);
            while (true)
            {
                if (this._etor.MoveNext())
                    return true;
                else if (!this._childrenEtor.MoveNext())
                    return false;

                this._etor = this._childrenEtor.Current.GetEnumerator();
            }
        }

        void IEnumerator.Reset()
        {
            this.initialized = true;
            ((IEnumerator)this._childrenEtor).Reset();
        }
    }
}
