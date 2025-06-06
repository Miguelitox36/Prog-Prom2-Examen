using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private Dictionary<string, Item> availableItems = new Dictionary<string, Item>();
    private Inventory<Item> playerInventory = new Inventory<Item>(); // GENÉRICO usage

    void Awake()
    {
        try
        {
            AddItem("POTION_HEALTH", new Potion("Health Potion", "Restores health.", 30));
            AddItem("SWORD_IRON", new Item("Iron Sword", "A basic sword."));
            AddItem("ARMOR_LEATHER", new Item("Leather Armor", "Light protection."));
            DisplayItems();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error initializing ItemManager: {e.Message}");
        }
    }

    public void AddItem(string id, Item item)
    {
        if (!availableItems.ContainsKey(id))
        {
            availableItems.Add(id, item);
            playerInventory.AddItem(item);
            Debug.Log($"Item '{item.Name}' (ID: {id}) added.");
        }
    }

    public Item GetItem(string id)
    {
        availableItems.TryGetValue(id, out Item item);
        return item;
    }

    public void DisplayItems()
    {
        Debug.Log("--- Available Items ---");
        foreach (var entry in availableItems)
        {
            Debug.Log($"ID: {entry.Key}, Name: {entry.Value.Name}");
        }
        Debug.Log($"Player inventory count: {playerInventory.Count}");
    }
}