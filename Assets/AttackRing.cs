using UnityEngine;

public class RedRingController : MonoBehaviour
{
    void OnMouseDown()
    {
        // Get the clicked NPC's position
        Vector3 npcPosition = transform.position;

        // Set the red ring's position to the NPC's position
        transform.position = npcPosition;

        // Show the red ring
        gameObject.SetActive(true);
    }
}
