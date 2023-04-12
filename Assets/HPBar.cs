using CodeMonkey.HealthSystemCM;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public int maxWidth = 250;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public NPCStats npcStats;
    public RectTransform healthBarTransform;
    public RectTransform manaBarTransform;
    public TargetStats TargetStats;

    public void Start()
    {
    }

    public void Update()
    {
        if (TargetStats.selectedNPC != null)
        {
            //Debug.Log("Current Health: " + currentHealth);
            //Debug.Log("Current Mana: " + currentMana);
            NPCStats selectedStats = TargetStats.selectedNPC.GetComponentInChildren<NPCStats>();
            currentHealth = selectedStats.currentHealth;
            maxHealth = selectedStats.maxHealth;

            currentMana = selectedStats.currentMana;
            maxMana = selectedStats.maxMana;
            float healthPercent = (float)currentHealth / maxHealth;
            float manaPercent = (float)currentMana / maxMana;
            manaBarTransform.localScale = new Vector3(manaPercent, 1, 1);
            healthBarTransform.localScale = new Vector3(healthPercent, 1, 1);
        }
    }
}
