using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class Chest : MonoBehaviour
{
    public LayerMask chestMask; // The layer mask to use for the 
    public GameObject chestObj;
    public int Size;
    public int Quantity;
    public float interactionRange = 5.0f;
    public GameObject chestPanel;
    public ItemList itemList;
    private InputAction clickAction;
    public GameObject Player;
    public ChestContents ChestContents;
    public DisableAllChests disableAllChests;
    public int slotCount;
    public GameObject prefab;


    public void AddNewSlot()
    {
        GameObject newSlot = Instantiate(prefab);
        newSlot.transform.SetParent(chestPanel.transform, false);
    }
    // define the items that can be stored in the chest

    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            AddNewSlot();
        }
        SetupChest();

        PopulateIndices(chestPanel);
        ClearChest();
        FillChest();
    }

    public void ResetChest()
    {
        //foreach (Item item in ChestContents.chestContents)
        //{
        //    Debug.Log("Item: " + item.itemName);
        //    RemoveItem(item);
        //}

        for (int i = 0; i < Quantity; i++)
        {
            Debug.Log("HI " + i);
        }

        ChestContents.chestContents.Clear();

        SetupChest();

        //PopulateIndices(chestPanel);
        ClearChest();
        FillChest();
    }

    public void SetupChest()
    {
        for (int i = 0; i < Quantity; i++)
        {
            Item randomItem = itemList.RandomItem();
            AddToList(itemList.itemsList.GetValueOrDefault(randomItem.itemName));
        }
    }
    public void AddToList(Item item)
    {
        if (!ChestContents.chestContents.Contains(item))
        {
            //Debug.Log("Adding to chest");
            ChestContents.chestContents.Add(new Item(item.itemName, item.quantity, item.itemIcon, item.itemId));
        }
        else
        {
            ChestContents.chestContents[ChestContents.chestContents.IndexOf(item)].quantity++;
        }
    }

    private void OnEnable()
    {
        clickAction = new InputAction("clickAction", binding: "<Mouse>/leftButton");
        clickAction.Enable();
        clickAction.performed += OnClickChest;
    }

    private void OnDisable()
    {
        clickAction.performed -= OnClickChest;
        clickAction.Disable();
    }

    private void OnClickChest(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, chestMask))
        {
            if (hit.collider.gameObject == gameObject)
            {
                float distance = Vector3.Distance(Player.transform.position, hit.collider.transform.position);
                if (distance <= interactionRange) 
                {
                    Toggle();
                }
                else if (distance > interactionRange)
                {
                    Debug.Log("Too far away");
                }
            }
        }
    }


    public void PopulateIndices(GameObject parentObject)
    {
        // Create a queue to hold the game objects to be processed
        Queue<GameObject> queue = new Queue<GameObject>();

        // Enqueue the initial game object(s) to the queue
        foreach (Transform child in parentObject.transform)
        {
            queue.Enqueue(child.gameObject);
        }

        // Keep track of how many disabled "Icon" and "LabelText" components we've found so 
        int numIndexFound = 0;

        // Process the queue until we've found two disabled "Icon" images and two disabled "LabelText" components
        while (queue.Count > 0)
        {
            // Dequeue the next game object from the queue
            GameObject currentGameObject = queue.Dequeue();

            // Check if this game object has an Index component
            Index thisIndex = currentGameObject.GetComponent<Index>();
            // Call the SetIndex method on the Index object's component
            if (thisIndex != null && thisIndex.name == "Index")
            {
                thisIndex.SetIndex(numIndexFound);
                numIndexFound++;
            }

            // Enqueue any child game objects to the queue
            foreach (Transform child in currentGameObject.transform)
            {
                queue.Enqueue(child.gameObject);
            }
        }
    }

    //Add items from chest List to chest contents until chestContents List is empty
    public void FillChest()
    {
        IterateThroughNestedGameObjects(chestPanel);
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

        // Process the queue until we've found two disabled "Icon" images and two disabled "LabelText" components
        while (numDisabledIconsFound < ChestContents.chestContents.Count || numDisabledLabelTextFound < ChestContents.chestContents.Count || numDisabledStackSizeTextFound < ChestContents.chestContents.Count && queue.Count > 0)
        {
            // Dequeue the next game object from the queue
            GameObject currentGameObject = queue.Dequeue();

            // Check if this game object has a disabled "Icon" image component
            Image iconImage = currentGameObject.GetComponent<Image>();
            if (iconImage != null && iconImage.name == "Icon" && numDisabledIconsFound < ChestContents.chestContents.Count)
            {
                // Found a disabled "Icon" image, increment the counter
                Debug.Log(ChestContents.chestContents[numDisabledIconsFound].itemIcon);
                Sprite sourceSprite = Resources.Load<Sprite>(ChestContents.chestContents[numDisabledIconsFound].itemIcon);
                iconImage.enabled = true;
                iconImage.sprite = sourceSprite;
                numDisabledIconsFound++;
            }

            // Check if this game object has a disabled "LabelText" TextMeshProUI component
            TextMeshProUGUI labelText = currentGameObject.GetComponent<TextMeshProUGUI>();
            if (labelText != null && labelText.name == "LabelText" && numDisabledLabelTextFound < ChestContents.chestContents.Count)
            {
                // We've found a disabled "LabelText" text, increment the counter
                //Debug.Log("Adding Item: " + ChestContents.chestContents[numDisabledLabelTextFound].itemName);
                labelText.text = ChestContents.chestContents[numDisabledLabelTextFound].itemName;
                labelText.enabled = true;
                numDisabledLabelTextFound++;
            }

            // Check if this game object has a disabled "StackSizeText" TextMeshProUI component
            TextMeshProUGUI stackSizeText = currentGameObject.GetComponent<TextMeshProUGUI>();
            if (stackSizeText != null && stackSizeText.name == "StackSizeText" && numDisabledStackSizeTextFound < ChestContents.chestContents.Count)
            {
                // We've found a disabled "stackSizeText" text, increment the counter
                stackSizeText.text = ChestContents.chestContents[numDisabledStackSizeTextFound].quantity.ToString();
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

    public void ClearChest()
    {
        IterateClear(chestPanel);
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

    void Toggle()
    {
        if (chestPanel.activeSelf)
        {
            chestPanel.SetActive(false);
        }
        else
        {
            chestPanel.SetActive(true);
        }
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < ChestContents.chestContents.Count; i++)
        {
            if (ChestContents.chestContents[i].itemId == item.itemId && ChestContents.chestContents[i].quantity > 1)
            {
                ChestContents.chestContents[i].quantity -= 1;

                ClearChest();
                FillChest();
            }
            else if (ChestContents.chestContents[i].itemId == item.itemId && ChestContents.chestContents[i].quantity == 1)
            {
                ChestContents.chestContents.RemoveAt(i);

                ClearChest();
                FillChest();
            }
        }
    }

    public void RemoveAllItem(int index)
    {
        for (int i = 0; i < ChestContents.chestContents.Count; i++)
        {
            ChestContents.chestContents.RemoveAt(index);
            ClearChest();
            FillChest();
        }
    }

    void Update()
    {
        // Close chest when walking too far away
        float distance = Vector3.Distance(Player.transform.position, chestObj.transform.position);

        if (Keyboard.current.escapeKey.wasPressedThisFrame || distance > interactionRange && chestPanel.activeSelf)
        {
            chestPanel.SetActive(false);
        }
        else
        {
            //Debug.Log("Distance: " + distance);
        }
    }
}