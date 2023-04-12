using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    public GameObject focusedNPC;
    public AddResponse addResponse;

    public void focus(GameObject npc)
    {
        focusedNPC = npc;
    }

    public GameObject getNpc()
    {
        return focusedNPC;
    }

}
