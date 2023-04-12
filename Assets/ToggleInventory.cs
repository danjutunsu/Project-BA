using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (Keyboard.current.bKey.wasPressedThisFrame) 
        {
            if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
        }
        }
    }
}
