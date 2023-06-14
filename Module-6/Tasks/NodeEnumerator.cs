using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks;

internal class NodeEnumerator<T> : IEnumerator<T>
{
    private Node<T> _headNode;
    private Node<T> _currentNode;
    public NodeEnumerator(Node<T> headNode)
    {
        ArgumentNullException.ThrowIfNull(headNode, nameof(headNode));
        _headNode = headNode;
    }
    public bool MoveNext()
    {
        if (_currentNode is null)
        {
            _currentNode = _headNode;
            return true;
        }
        if (_currentNode?.Next is null)
        {
            return false;
        }
        _currentNode = _currentNode.Next;
        return true;
    }

    public void Reset()
    {
        _currentNode = _headNode;
    }

    public T Current => _currentNode.Data;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}