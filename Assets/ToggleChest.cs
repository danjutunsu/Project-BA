using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleChest : MonoBehaviour
{
    public Canvas chestCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Toggle()
    {
        if (chestCanvas.enabled == false)
        {
            chestCanvas.enabled = true;
        }
        else
        {
            chestCanvas.enabled = false;
        }
    }
}
