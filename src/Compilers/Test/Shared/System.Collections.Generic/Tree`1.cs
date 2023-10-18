using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic;

internal class Tree<T> : IEnumerable<T>, IReadOnlyCollection<T>
{
    internal TreeNode<T>? root;

    [NotNullIfNotNull("value")]
    public TreeNode<T>? Root
    {
        get => this.root;
        set
        {
            if (value is not null)
            {
                if (!object.ReferenceEquals(this, value.tree))
                    throw new ArgumentException("节点位于其他树上。", nameof(value));
            }
            this.root = value;
        }
    }

    public int Count => this.root is null ? 0 : this.root.ChildNodesCount;

    public Tree() { }

    public IEnumerator<T> GetEnumerator() => this.root?.GetEnumerator() ?? Enumerable.Empty<T>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
