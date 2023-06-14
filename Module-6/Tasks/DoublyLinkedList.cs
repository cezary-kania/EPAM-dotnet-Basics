using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks;

public class DoublyLinkedList<T> : IDoublyLinkedList<T>
{
    private Node<T> _head;
    private Node<T> _tail;
    public int Length { get; private set; }
    
    public void Add(T e)
    {
        AddAt(Length, e);
    }

    public void AddAt(int index, T e)
    {
        if (index > Length || index < 0)
        {
            throw new IndexOutOfRangeException();
        }
        var newNode = new Node<T>(e);
        if (index == Length)
        {
            if (_head is null)
            {
                _head = newNode;
            }
            else
            {
                newNode.Previous = _tail;
                _tail.Next = newNode;
            }
            _tail = newNode;
        } else if (index == 0)
        {
            newNode.Next = _head;
            _head = newNode;
        }
        else
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
        return new NodeEnumerator<T>(_head);
    }

    public void Remove(T item)
    {
        var currentNode = _head;
        for (var i = 0; currentNode is not null; i++)
        {
            if (item.Equals(currentNode.Data))
            {
                RemoveAt(i);
                break;
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
        else
        {
            _head = currentNode.Next;
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
    
    private Node<T> GetNodeAt(int index)
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
}