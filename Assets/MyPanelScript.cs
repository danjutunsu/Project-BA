using UnityEngine;
using UnityEngine.EventSystems;

public class MyPanelScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Left click detected on panel!");
        }
    }
}