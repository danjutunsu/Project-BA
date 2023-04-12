using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleTarget : MonoBehaviour
{
    public GameObject TargetPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Toggle()
    {
        if (TargetPanel.activeSelf)
        {
            TargetPanel.SetActive(false);
        }
        else
        {
            TargetPanel.SetActive(true);
        }
    }
}
