using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteCreativity.Services.MapPatherNS;

public class PriorityQueue<T>
{
    private readonly SortedList<(int, int), T> _list = new();
    private int _count;


    public void Enqueue(T item, int priority)
    {
        _list.Add((priority, _count), item);
        _count++;
    }

    public T Dequeue()
    {
        var item = _list[_list.Keys[0]];
        _list.RemoveAt(0);
        return item;
    }

    public bool IsEmpty()
    {
        return _list.Count == 0;
    }

    public bool Contains(T item)
    {
        return _list.ContainsValue(item);
    }

    public T? FirstOrDefault(Func<T, bool> predicate)
    {
        return _list.FirstOrDefault(x => predicate(x.Value)).Value;
    }

    public void Remove(T item)
    {
        _list.RemoveAt(_list.IndexOfValue(item));
    }
}