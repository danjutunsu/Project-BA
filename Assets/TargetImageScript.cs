using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetImageScript : MonoBehaviour, IDropHandler
{
    public Image sourceImage; // The source image to exchange icons with

    public void OnDrop(PointerEventData eventData)
    {
        string draggedObjectName = eventData.pointerDrag.name;
        // If the dragged object is an image and the drop is inside the target image
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<Image>() != null
            && RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
        {
            Debug.Log(draggedObjectName);
            // // Exchange the sprites between the source and target images
            // Image sourceImageComponent = sourceImage.GetComponent<Image>();
            // Image targetImageComponent = GetComponent<Image>();
            // Sprite tempSprite = sourceImageComponent.sprite;
            // sourceImageComponent.sprite = targetImageComponent.sprite;
            // targetImageComponent.sprite = tempSprite;
        }
    }
}