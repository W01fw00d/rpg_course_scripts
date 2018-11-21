using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySystem {

    // Apparently, unity doesnt support serialization on Hashtables right now... so we'll to believe that this works
    private Hashtable itemsByCategory = new Hashtable()
    {
        {BaseItem.ItemCategory.Weapon, new List<InventoryItem>()},
        {BaseItem.ItemCategory.Armour, new List<InventoryItem>()},
        {BaseItem.ItemCategory.Clothing, new List<InventoryItem>()},
        {BaseItem.ItemCategory.Health, new List<InventoryItem>()},
        {BaseItem.ItemCategory.Potion, new List<InventoryItem>()},
    };

    private InventoryItem selectedWeapon;
    private InventoryItem selectedArmour;

    public InventoryItem SelectedWeapon
    {
        get
        {
            return selectedWeapon;
        }

        set
        {
            selectedWeapon = value;
        }
    }

    public InventoryItem SelectedArmour
    {
        get
        {
            return selectedArmour;
        }

        set
        {
            selectedArmour = value;
        }
    }

    public InventorySystem()
    {
        ClearInventory();
    }

    public void ClearInventory()
    {
        foreach (DictionaryEntry categoryItems in itemsByCategory)
        {
            List<InventoryItem> categoryItemsList = categoryItems.Value as List<InventoryItem>;
            categoryItemsList.Clear();
        }
    }

    public void AddItem(InventoryItem item)
    {
        List<InventoryItem> list = 
            itemsByCategory[item.Category] as List<InventoryItem>;
        list.Add(item);
    }

    public void DeleteItem(InventoryItem item)
    {
        List<InventoryItem> list =
            itemsByCategory[item.Category] as List<InventoryItem>;
        list.Remove(item);
    }
}
