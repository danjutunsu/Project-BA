using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastClickHandler : MonoBehaviour {
public GameObject Player;
public LayerMask Character;

    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Character))
            {

            }
            else if (Physics.Raycast(ray, out hit)) {
                Debug.Log("Clicked on object: " + hit.transform.name);

                // // Check if the clicked object is a canvas
                // if (hit.transform.GetComponent<Canvas>() != null) {
                //     hit.transform.GetComponent<CanvasClickHandler>().OnPointerClick(null);
                // }
            }

            // Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }
}