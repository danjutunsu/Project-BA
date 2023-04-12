using UnityEngine;

public class DisableAllChests: MonoBehaviour
{
    public string tagToDisable = "Panel";

    public void Disable()
    {
        // Find all objects with the specified tag
        GameObject[] objectsToDisable = GameObject.FindGameObjectsWithTag(tagToDisable);

        // Loop through each object and disable it
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
}
