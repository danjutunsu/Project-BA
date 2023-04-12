using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    private string _itemName;
    private int _quantity;
    private string _itemIcon;
    private int _itemId;

    public string itemName { get; set; }
    public int quantity { get; set; }
    public string itemIcon { get; set; }
    public int itemId { get; set; }

    public Item(string item, int qty, string icon, int id)
    {
        itemName = item;
        quantity = qty;
        itemIcon = icon;
        itemId = id;
    }

    // Override Equals to compare if items are the same
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Item other = (Item)obj;
        return itemName == other.itemName && itemId == other.itemId;
    }

    // Override HashCode to compare if items are the same

    public override int GetHashCode()
    {
        return itemName.GetHashCode() ^ itemId.GetHashCode();
    }
}
