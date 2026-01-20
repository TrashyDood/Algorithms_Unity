using System;
using System.Collections;
using System.Collections.Generic;

// Generic tree structure.
public abstract class TreeNode<TValue, TCollection, TNode> : IEnumerable<TNode>
    where TCollection : ICollection<TNode>
    where TValue : IEquatable<TValue>
    where TNode : TreeNode<TValue, TCollection, TNode>, new()
{
    TNode _derived;
    TValue _value;
    ICollection<TNode> _children;

    public TValue Value
    {
        get => _value;
        set
        {
            OnNewValue(value);
            _value = value;
        }
    }

    protected TreeNode(TCollection children)
    {
        _derived = (TNode)this;
        _children = children;
        foreach (var child in children)
            child?.OnAdded(_derived);
    }

    public TNode AddChild(TValue value)
    {
        if (!CanAdd(value))
            return default;

        return AddChild(new TNode() { _value = value });
    }

    public abstract TNode AddChild(TNode node);

    public void AddChildren(ICollection<TValue> values)
    {
        foreach (var value in values)
            AddChild(value);
    }

    public void AddChildren(params TValue[] values)
    {
        for (int i = 0; i < values.Length; i++)
            AddChild(values[i]);
    }

    public virtual bool CanAdd(TValue value) => true;
    protected virtual void OnAdded(TNode parent) { }
    public virtual void OnNewValue(TValue newValue) { }

    #region IEnumerable
    public IEnumerator<TNode> GetEnumerator() => _children.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _children.GetEnumerator();
    #endregion
}

public class TreeNode<T> : TreeNode<T, TreeNode<T>[], TreeNode<T>> where T : IEquatable<T>
{
    public TreeNode() : base(new TreeNode<T>[2]) { }

    public override TreeNode<T> AddChild(TreeNode<T> node)
    {
        throw new NotImplementedException();
    }
}
