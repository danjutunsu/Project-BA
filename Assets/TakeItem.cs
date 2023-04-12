using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour
{
    public Inventory inventory;
    public Chest chest;
    public GameObject npc;
    public GameObject Player;
    private InputAction _clickAction;

    public float interactionRange = 5.0f;

    void Start()
    {
        GameObject inv = GameObject.Find("Inventory");
        inventory = inv.GetComponent<Inventory>();
        GameObject thisChest = GameObject.Find("Chest");
        chest = thisChest.GetComponent<Chest>();
        npc = GameObject.Find("Chest");
        Player = GameObject.Find("PlayerArmature");

        //Slot component attached to this game object
        Image slot = GetComponent<Image>();

        //DragContent - Child of Slot Object
        Transform dragContent = slot.transform.GetChild(1);

        //Icon - Child of dragContent that is to be transferred when clicked
        Image icon = dragContent.GetChild(0).GetComponent<Image>();

        //Index Object of Slot
        Index index = slot.GetComponentInChildren<Index>();

        //Event listener to the Image component that listens for click events
        slot.GetComponent<Button>().onClick.AddListener(() =>
        {
            float distance = Vector3.Distance(Player.transform.position, npc.transform.position);
            //Debug.Log("Distance: " + distance);
            Debug.Log("Step 1");
            if (distance <= interactionRange && icon.IsActive())
            {
                // Get the file name of the sprite's texture
                string fileName = icon.sprite.texture.name;
                Debug.Log("Step 2");

                // Output the file name to the console
                Debug.Log("Image file name: " + fileName);
                if (Keyboard.current.leftShiftKey.isPressed)
                {
                    Debug.Log("Step 3");

                    // Shift and left mouse button are 
                    Item clicked = ScriptableObject.CreateInstance<Item>();
                    clicked.itemName = chest.ChestContents.chestContents[index.arrayIndex].itemName;
                    clicked.quantity = chest.ChestContents.chestContents[index.arrayIndex].quantity;
                    clicked.itemIcon = chest.ChestContents.chestContents[index.arrayIndex].itemIcon;
                    clicked.itemId = chest.ChestContents.chestContents[index.arrayIndex].itemId;

                    inventory.AddAllToList(clicked);
                    chest.RemoveAllItem(index.arrayIndex);
                }
                else
                {
                    //Debug.Log("Index: " + index.arrayIndex);
                    Item clicked = ScriptableObject.CreateInstance<Item>();
                    clicked.itemName = chest.ChestContents.chestContents[index.arrayIndex].itemName;
                    clicked.quantity = 1;
                    clicked.itemIcon = chest.ChestContents.chestContents[index.arrayIndex].itemIcon;
                    clicked.itemId = chest.ChestContents.chestContents[index.arrayIndex].itemId;

                    inventory.AddToList(clicked);
                    chest.RemoveItem(clicked);
                }
            }
            // Debug.Log("Index file value: " + index.arrayIndex
        });      
    }
}
