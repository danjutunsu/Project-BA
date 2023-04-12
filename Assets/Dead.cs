using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    public GameObject Body;
    public NPCStats NPCStats;
    public bool hasDecomposeBeenCalled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDecomposeBeenCalled && NPCStats.currentHealth <= 0) 
        {
            //Debug.Log("NPC Died");
            Decomposing();
            hasDecomposeBeenCalled = true;
        }
    }

    void Decomposing()
    {
        //Debug.Log("NPC Decomposing. Body Lootable.");
        NPCStats.lootable = true;
        Invoke("Disappear", 5f);
    }

    void Disappear()
    {
        Body.SetActive(false);

        Invoke("Reappear", 5f);
    }

    void Reappear()
    {
        Debug.Log("HI");
        Body.SetActive(true);
        NPCStats.Revive();
        hasDecomposeBeenCalled = false;
    }
}
