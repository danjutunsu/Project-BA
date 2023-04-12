using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    public GameObject Body;
    public NPCStats NPCStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NPCStats.currentHealth <= 0) 
        {
            //Debug.Log("NPC Died");
            Decomposing();
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
        Body.SetActive(true);
        NPCStats.Revive();
    }
}
