using UnityEngine;
using UnityEngine.InputSystem;


public class ClickHandler : MonoBehaviour
{
    public LayerMask UI;
    public InputAction clickAction;

    private void OnEnable()
    {
        clickAction = new InputAction("clickAction", binding: "<Mouse>/leftButton");
        clickAction.Enable();
        clickAction.performed += OnPointerClick;
    }

    private void OnDisable()
    {
        clickAction.performed -= OnPointerClick;
        clickAction.Disable();
    }

    public void OnPointerClick(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, UI))
        {
            // if (hit.collider.gameObject == gameObject)
            // {
                Debug.Log("Clicked Chest");
            // }
        }
    }
}