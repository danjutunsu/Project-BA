using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public NPCStats NPCStats;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI Mana;
    public GameObject selectedNPC;


    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = NPCStats.maxHealth;
        //currentHealth = NPCStats.currentHealth;
        //maxMana = NPCStats.maxMana;
        //currentMana = NPCStats.currentMana;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedNPC != null)
        {   
            NPCStats selectedStats = selectedNPC.GetComponentInChildren<NPCStats>();
            NPCStats = selectedStats;
            maxHealth = NPCStats.maxHealth;
            currentHealth = NPCStats.currentHealth;
            maxMana = NPCStats.maxMana;
            currentMana = NPCStats.currentMana;
            HP.SetText(NPCStats.currentHealth.ToString());
            Mana.SetText(NPCStats.currentMana.ToString());
        }
    }
}
