using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SlotClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject slot;

    void Start()
    {
        IterateThroughNestedGameObjects(slot);
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
            GameObject currentGameObject = queue.Dequeue();

            // Check if this game object has a disabled "Icon" image component
            Image iconImage = currentGameObject.GetComponent<Image>();
            if (iconImage != null && iconImage.name == "Icon")
            {
                numDisabledIconsFound++;
            }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObj = eventData.pointerPress;
        Image iconImage = clickedObj.GetComponentInChildren<Image>();
        Index index = clickedObj.GetComponentInChildren<Index>();
        string spriteName = iconImage.sprite.name;
        Debug.Log("Clicked icon image sprite name: " + spriteName);
        Debug.Log("Index is " + index);
    }
}