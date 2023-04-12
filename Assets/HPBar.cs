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
            NPCStats selectedStats = TargetStats.selectedNPC.GetComponentInChildren<NPCStats>();
            currentHealth = selectedStats.currentHealth;
            maxHealth = selectedStats.maxHealth;

            Debug.Log("Current Health: " + currentHealth);
            UpdateHealthBar(currentHealth, maxHealth);

            currentMana = selectedStats.currentMana;
            maxMana = selectedStats.maxMana;
            Debug.Log("Current Mana: " + currentMana);
            float manaPercent = (float)currentMana / maxMana;
            manaBarTransform.localScale = new Vector3(manaPercent, 1, 1);
        }
    }

    void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        float newWidth = maxWidth * healthPercentage;

        // Set the new width of the health bar
        Vector2 size = healthBarTransform.sizeDelta;
        size.x = newWidth;
        healthBarTransform.sizeDelta = size;

        // Calculate the new aspect ratio of the health bar
        float aspectRatio = size.y / size.x;

        // Set the new height of the health bar to preserve the aspect ratio
        size.y = size.x * aspectRatio;
        healthBarTransform.sizeDelta = size;
    }

}
