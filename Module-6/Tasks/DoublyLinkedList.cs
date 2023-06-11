using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks;

public class DoublyLinkedList<T> : IDoublyLinkedList<T>
{
    private Node _head;
    public int Length { get; private set; }
    
    public void Add(T e)
    {
        var newNode = new Node(e);
        if (_head is null)
        {
            _head = newNode;
        }
        else
        {
            AddAtTheEnd(newNode);
        }
        Length++;
    }

    public void AddAt(int index, T e)
    {
        if (index > Length || index < 0)
        {
            throw new IndexOutOfRangeException();
        }
        var newNode = new Node(e);
        if (index == Length)
        {
            AddAtTheEnd(newNode);
        }
        else
        {
            PutNodeAt(index, newNode);
        }
        if (index == 0)
        {
            _head = newNode;
        }
        Length++;
    }

    public T ElementAt(int index)
    {
        ValidateIndex(index);
        var node = GetNodeAt(index);
        return node.Data;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new NodeEnumerator(_head);
    }

    public void Remove(T item)
    {
        var currentNode = _head;
        while (currentNode is not null)
        {
            if (item.Equals(currentNode.Data))
            {
                if (currentNode != _head)
                {
                    currentNode.Previous.Next = currentNode.Next;
                }

                if (currentNode.Next is not null)
                {
                    currentNode.Next.Previous = currentNode.Previous;
                }
                
                Length--;
            }
            currentNode = currentNode.Next;
        }
    }

    public T RemoveAt(int index)
    {
        ValidateIndex(index);
        var currentNode = GetNodeAt(index);
        if (currentNode.Previous is not null)
        {
            currentNode.Previous.Next = currentNode.Next;
        }
        if (currentNode.Next is not null)
        {
            currentNode.Next.Previous = currentNode.Previous;
        }
        Length--;
        return currentNode.Data;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private Node GetTail() => GetNodeAt(Math.Max(0, Length - 1));
    
    private void PutNodeAt(int index, Node newNode)
    {
        var current = _head;
        for (var i = 0; i < index; i++)
        {
            current = current.Next;
        }
        newNode.Previous = current.Previous;
        newNode.Next = current;
        if (current.Previous is not null)
        {
            current.Previous.Next = newNode;
        }
        current.Previous = newNode;
    }
    
    private void AddAtTheEnd(Node newNode)
    {
        var tail = GetTail();
        newNode.Previous = tail;
        tail.Next = newNode;
    }
    
    private Node GetNodeAt(int index)
    {
        var current = _head;
        for (var i = 0; i < index; i++)
        {
            current = current.Next;
        }
        return current;
    }

    private void ValidateIndex(int index)
    {
        if (Length == 0 || index >= Length || index < 0)
        {
            throw new IndexOutOfRangeException();
        }
    }

    private class NodeEnumerator : IEnumerator<T>
    {
        private Node _headNode;
        private Node _currentNode;
        public NodeEnumerator(Node headNode)
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

    private class Node
    {
        public T Data { get; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}