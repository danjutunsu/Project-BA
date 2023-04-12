using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryList = new List<Item>(); // items in the inventory
    public GameObject inventoryPanel;

    public void ListItems()
    {
        foreach(Item item in inventoryList) 
        {
            Debug.Log("Items in inventory: " + item.itemName);
            Debug.Log("Name: " + item.itemName);
            Debug.Log("Quantity: " + item.quantity);
        }
    }
    
    public void Start()
    {
        FillInventory();
    }


    public void AddToList(Item item)
    {
        if (!inventoryList.Contains(item))
        {
            inventoryList.Add(new Item(item.itemName, item.quantity, item.itemIcon, item.itemId));
            FillInventory();
        }
        else
        {
            inventoryList[inventoryList.IndexOf(item)].quantity++;
            FillInventory();
        }
    }

    public bool FindItem(string name)
    {
        foreach (Item item in inventoryList)
        {
            if (item.itemName.Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    { 
        if (inventoryList.Contains(item)) 
        { 
            inventoryList.Remove(item);
            ClearInventory();
            FillInventory();
        }
    }

    public void AddAllToList(Item item)
    {
        if (!inventoryList.Contains(item))
        {
            // Debug.Log("Adding to Inv");
            inventoryList.Add(new Item(item.itemName, item.quantity, item.itemIcon, item.itemId));
            FillInventory();
        }
        else
        {
            // Debug.Log("Already contains");
            inventoryList[inventoryList.IndexOf(item)].quantity+= item.quantity;
            ClearInventory();
            FillInventory();
        }
    }

    //Add items from chest List to chest contents until chestContents List is empty
    public void FillInventory()
    {
        IterateThroughNestedGameObjects(inventoryPanel);
    }
    public void IterateThroughNestedGameObjects(GameObject parentObject)
    {
        // Create a queue to hold the game objects to be processed
        Queue<GameObject> queue = new Queue<GameObject>();

        // Enqueue the initial game object(s) to the queue
        foreach (Transform child in parentObject.transform)
        {
            queue.Enqueue(child.gameObject);
        }

        // Keep track of how many disabled "Icon" and "LabelText" components we've found so far
        int numDisabledIconsFound = 0;
        int numDisabledLabelTextFound = 0;
        int numDisabledStackSizeTextFound = 0;
        int numIndexFound = 0;

        // Process the queue until we've found two disabled "Icon" images and two disabled "LabelText" components
        while (numDisabledIconsFound < inventoryList.Count || numDisabledLabelTextFound < inventoryList.Count || numDisabledStackSizeTextFound < inventoryList.Count || numIndexFound < inventoryList.Count && queue.Count > 0)
        {
            // Dequeue the next game object from the queue
            GameObject currentGameObject = queue.Dequeue();

            // Check if this game object has a disabled "Icon" image component
            Image iconImage = currentGameObject.GetComponent<Image>();
            if (iconImage != null && iconImage.name == "Icon" && numDisabledIconsFound < inventoryList.Count)
            {
                // We've found a disabled "Icon" image, increment the counter
                Sprite sourceSprite = Resources.Load<Sprite>(inventoryList[numDisabledIconsFound].itemIcon);
                iconImage.enabled = true;
                iconImage.sprite = sourceSprite;
                numDisabledIconsFound++;
            }

            // Check if this game object has a disabled "LabelText" TextMeshProUI component
            TextMeshProUGUI labelText = currentGameObject.GetComponent<TextMeshProUGUI>();
            if (labelText != null && labelText.name == "LabelText" && numDisabledLabelTextFound < inventoryList.Count)
            {
                // We've found a disabled "LabelText" text, increment the counter
                labelText.text = inventoryList[numDisabledLabelTextFound].itemName;
                labelText.enabled = true;
                numDisabledLabelTextFound++;
            }

            // Check if this game object has a disabled "StackSizeText" TextMeshProUI component
            TextMeshProUGUI stackSizeText = currentGameObject.GetComponent<TextMeshProUGUI>();
            if (stackSizeText != null && stackSizeText.name == "StackSizeText" && numDisabledStackSizeTextFound < inventoryList.Count)
            {
                // We've found a disabled "stackSizeText" text, increment the counter
                stackSizeText.text = inventoryList[numDisabledStackSizeTextFound].quantity.ToString();
                stackSizeText.enabled = true;
                numDisabledStackSizeTextFound++;
            }

            // Enqueue any child game objects to the queue
            foreach (Transform child in currentGameObject.transform)
            {
                queue.Enqueue(child.gameObject);
            }
        }
    }

    public void ClearInventory()
    {
        IterateClear(inventoryPanel);
    }

    public void IterateClear(GameObject parentObject)
    {
        // Create a queue to hold the game objects to be processed
        Queue<GameObject> queue = new Queue<GameObject>();

        // Enqueue the initial game object(s) to the queue
        foreach (Transform child in parentObject.transform)
        {
            queue.Enqueue(child.gameObject);
        }

        // Process the queue until we've found two disabled "Icon" images and two disabled "LabelText" components
        while (queue.Count > 0)
        {
            // Dequeue the next game object from the queue
            GameObject currentGameObject = queue.Dequeue();

            // Check if this game object has a disabled "Icon" image component
            Image iconImage = currentGameObject.GetComponent<Image>();
            if (iconImage != null && iconImage.name == "Icon")
            {
                iconImage.enabled = false;
                iconImage.sprite = null;
            }

            // Check if this game object has a disabled "LabelText" TextMeshProUI component
            TextMeshProUGUI labelText = currentGameObject.GetComponent<TextMeshProUGUI>();
            if (labelText != null && labelText.name == "LabelText")
            {
                // We've found a disabled "LabelText" text, increment the counter
                labelText.text = null;
            }

            // Check if this game object has a disabled "StackSizeText" TextMeshProUI component
            TextMeshProUGUI stackSizeText = currentGameObject.GetComponent<TextMeshProUGUI>();
            if (stackSizeText != null && stackSizeText.name == "StackSizeText")
            {
                // We've found a disabled "stackSizeText" text, increment the counter
                stackSizeText.text = null;
                stackSizeText.enabled = true;
            }

            // Enqueue any child game objects to the queue
            foreach (Transform child in currentGameObject.transform)
            {
                queue.Enqueue(child.gameObject);
            }
        }
    }
}
