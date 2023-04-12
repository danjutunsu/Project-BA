using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public GameObject imageToDropOnto;

    public void OnDrop(PointerEventData eventData)
    {
        // Get the dragged object's name
        GameObject droppedOn = EventSystem.current.currentSelectedGameObject;

        // Get a reference to the Image component attached to the game object
        Image image = droppedOn.GetComponent<Image>();

        // Get the name of the sprite on the Image component
        string spriteName = image.sprite.name;

        // Check if the dragged object was dropped onto the correct image
        if (imageToDropOnto.name == "Icon")
        {
            Debug.Log("Dropped icon onto slot! " + spriteName);
        }
    }
}