using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    Dictionary<T, float> _allNodes = new Dictionary<T, float>();

    public int Count { get { return _allNodes.Count; }}


    public void Put(T node, float cost)
    {
        if (_allNodes.ContainsKey(node))
            _allNodes[node] = cost;
        else
            _allNodes.Add(node, cost);
    }

    public T Get()
    {
        T node = default(T);
        float lowestValue = Mathf.Infinity;

        foreach (var item in _allNodes)
        {
            if (item.Value < lowestValue)
            {
                lowestValue = item.Value;
                node = item.Key;
            }
        }

        if (!EqualityComparer<T>.Default.Equals(node, default(T)))
            _allNodes.Remove(node);

        return node;
    }

    public T Peek()
    {
        T node = default(T);
        float lowestValue = Mathf.Infinity;

        foreach (var item in _allNodes)
        {
            if (item.Value < lowestValue)
            {
                lowestValue = item.Value;
                node = item.Key;
            }
        }
        return node;
    }

    public bool IsEmpty()
    {
        return _allNodes.Count == 0;
    }

    public void Clear()
    {
        _allNodes.Clear();
    }

    public bool TryGetValue(T node, out float cost)
    {
        return _allNodes.TryGetValue(node, out cost);
    }
}