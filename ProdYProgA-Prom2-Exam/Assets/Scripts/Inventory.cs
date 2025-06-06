using System.Collections.Generic;
using UnityEngine;

public class Inventory<T> where T : Item
{
    private List<T> items = new List<T>();
    public void AddItem(T item)
    {
        items.Add(item);
        Debug.Log($"Added {item.Name} to inventory. Total items: {items.Count}");
    }
    public T GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }
    public List<T> GetAllItems()
    {
        return new List<T>(items);
    }
    public int Count => items.Count;
}