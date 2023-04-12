using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            // Check if the dragged object is an image
            Image draggedImage = eventData.pointerDrag.GetComponent<Image>();
            if (draggedImage != null)
            {
                // Check if the dragged image is released inside the slot
                RectTransform slotRectTransform = GetComponent<RectTransform>();
                if (RectTransformUtility.RectangleContainsScreenPoint(slotRectTransform, eventData.position, eventData.pressEventCamera))
                {
                    Debug.Log("Image dropped in slot!");
                    // Handle the image drop here
                }
            }
        }
    }
}