using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReadStats : MonoBehaviour
{
    public Inventory inventory;
    public Chest chest;
    public GameObject Player;
    private InputAction _clickAction;

    public float interactionRange = 5.0f;

    void Start()
    {
        GameObject inv = GameObject.Find("Inventory");
        inventory = inv.GetComponent<Inventory>();
        GameObject thisChest = transform.parent.parent.parent.gameObject;
        chest = thisChest.GetComponentInChildren<Chest>();
        //if (thisChest != null )
        //{
        //    Debug.Log("Chest Name: " + chest.name);
        //}
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
            float distance = 1000;

            if (transform.root.gameObject.name != "Chest")
            {
                distance = Vector3.Distance(Player.transform.position, transform.parent.parent.parent.parent.position);

            }

            else if (transform.root.gameObject.name == "Chest")
            {
                distance = Vector3.Distance(Player.transform.position, transform.root.position);
            }

            //Debug.Log("Distance: " + distance);
            //Debug.Log("Step 1");
            //Debug.Log(icon.IsActive());
            if (distance <= interactionRange && icon.IsActive())
            {
                //Debug.Log("Step 2");

                // Get the file name of the sprite's texture
                string fileName = icon.sprite.texture.name;

                // Output the file name to the console
                //Debug.Log("Image file name: " + fileName);
                if (Keyboard.current.leftShiftKey.isPressed)
                {
                    //Debug.Log("Step 3");

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
