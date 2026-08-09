using UnityEngine;
using System.Collections.Generic;

public static class ListExtensions
{
    /// <summary>
    /// Draws a random element from the list and removes it from the list. Returns default(T) if the list is empty.
    /// </summary>
    public static T Draw<T>(this List<T> list)
    {
        if (list.Count == 0) return default;
        int r = Random.Range(0, list.Count);
        T t = list[r];
        list.Remove(t);
        return t;
    }
}
