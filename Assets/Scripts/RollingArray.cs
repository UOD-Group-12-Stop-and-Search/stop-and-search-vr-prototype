using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RollingArray<T> : ICollection<T>
{
    private readonly int m_capacity;
    private readonly LinkedList<T> m_list;

    public RollingArray(int capacity)
    {
        m_capacity = capacity;
        m_list = new LinkedList<T>();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return m_list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        m_list.AddLast(item);

        if (m_list.Count > m_capacity)
            m_list.RemoveFirst();
    }

    public void Clear()
    {
        m_list.Clear();
    }

    public bool Contains(T item)
    {
        return m_list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        m_list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return m_list.Remove(item);
    }

    public int Count => m_list.Count;
    public bool IsReadOnly => true;
    public int Capacity => m_capacity;
}