using System;
using Tasks.DoNotChange;

namespace Tasks;

public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
{
    private DoublyLinkedList<T> _storage = new();
    public T Dequeue()
    {
        ValidateStorageLength();
        return _storage.RemoveAt(0);
    }

    public void Enqueue(T item)
    {
        _storage.Add(item);
    }

    public T Pop()
    {
        ValidateStorageLength();
        return _storage.RemoveAt(_storage.Length - 1);
    }

    public void Push(T item)
    {
        _storage.Add(item);
    }
    
    private void ValidateStorageLength()
    {
        if (_storage.Length == 0)
        {
            throw new InvalidOperationException();
        }
    }
}