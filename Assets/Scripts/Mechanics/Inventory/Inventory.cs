using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    public int ItemsCount => items.Count;

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void ApplyData(InventoryData data)
    {
        foreach(var item in data.StoredItems)
        {
            
        }
    }
}