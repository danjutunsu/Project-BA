using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int barSize = 6;
    public int maxHealth;
    public int currentHealth;
    public NPCStats npcStats;

    public RectTransform healthBarTransform;

    public void Start()
    {
        healthBarTransform = GetComponent<RectTransform>();
        maxHealth = npcStats.maxHealth;
        currentHealth = npcStats.currentHealth;
    }

    public void Update()
    {
        currentHealth = npcStats.currentHealth;
        //Debug.Log("Current Health: " + currentHealth);
        float healthPercent = (float)currentHealth / maxHealth;
        healthBarTransform.localScale = new Vector3(healthPercent * barSize, 1, 1);

        //Vector2 size = healthBarTransform.sizeDelta;
        //size.x = healthPercent * 200; // adjust the width of the health bar by changing the value 200
        //healthBarTransform.sizeDelta = size;
    }
}