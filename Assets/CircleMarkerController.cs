using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMarkerController : MonoBehaviour
{
    void Update()
    {
        // Get the current position of the player's GameObject
        Vector3 playerPos = transform.position;

        // Set the position of the circle marker to the player's position
        transform.position = new Vector3(playerPos.x, playerPos.y + 0.01f, playerPos.z);
    }

}
