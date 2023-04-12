using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public Dictionary<string, Item> itemsList = new Dictionary<string, Item>();

    void Awake()
    {
        Item ring = new Item("Family Ring", 1, "Ring_2", 1);
        itemsList.Add("Family Ring", ring);

        Item sword = new Item("Sword", 1, "02_Sword", 2);
        itemsList.Add("Sword", sword);

        Item bow = new Item("Bow", 1, "01_Bow", 3);
        itemsList.Add("Bow", bow);

        Item cloak = new Item("Cloak", 1, "01_cloth_chest", 4);
        itemsList.Add("Cloak", cloak);

        Item copperBar = new Item("Copper Bar", 1, "03_Copper_Bar", 5);
        itemsList.Add("Copper Bar", copperBar);

        Item ironBar = new Item("Iron Bar", 1, "04_Iron_Bar", 6);
        itemsList.Add("Iron Bar", ironBar);

        Item antivenom = new Item("Antivenom", 1, "15_Heal_potion", 7);
        itemsList.Add("Antivenom", antivenom);

        Item healthPotion = new Item("Health Potion", 1, "03_Alchemy", 8);
        itemsList.Add("Health Potion", healthPotion);

        Item bacon = new Item("Bacon", 1, "09_meat", 9);
        itemsList.Add("Bacon", bacon);

        Item meat = new Item("Meat", 1, "09_meat2", 10);
        itemsList.Add("Meat", meat);
    }

    public Item RandomItem()
    {
        var random = new System.Random();
        Item randomItem;

        do
        {
            var index = random.Next(itemsList.Count);
            randomItem = itemsList.ElementAt(index).Value;
        }
        while (randomItem.itemName.Equals("Family Ring"));

        //Debug.Log($"Random item: {randomItem.itemName}");

        return randomItem;
    }
}
