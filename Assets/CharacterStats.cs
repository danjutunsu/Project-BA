using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public TextMeshProUGUI HPValue;
    public TextMeshProUGUI ManaValue;
    public TextMeshProUGUI ExpValue;
    public TextMeshProUGUI DeadMessage;
    public Animator animator;

    public int maxHealth { get; set; }
    public int maxMana { get; set; }
    public int currentHealth { get; set; }
    public int currentMana { get; set; }
    public int experience { get; set; }

    void Start()
    {
        maxHealth = 10000000;
        maxMana = 500000;
        currentHealth = maxHealth;
        currentMana = maxMana;
        ExpValue.SetText(experience.ToString());
    }

    public void LoseHP(int damage)
    {
        if (currentHealth > 0) 
        {
            currentHealth -= damage;
            HPValue.SetText(currentHealth.ToString());
        }
        if ((currentHealth - damage) <= 0)
        {
            currentHealth = 0;
            HPValue.SetText(currentHealth.ToString());
            Die();
        }
    }

    public void LoseMana(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            ManaValue.SetText(currentMana.ToString());
        }
        if ((currentHealth - damage) <= 0)
        {
            //Die();
        }
    }

    public void GainExperience(int exp)
    {
        experience += exp;
    }

    public void Update()
    {
        ExpValue.SetText(experience.ToString());
    }

    public void TakeDamage()
    {
        animator.Play("GetHit");
    }

    public void Die()
    {
        animator.StopPlayback();
        Debug.Log("dies");
        DeadMessage.enabled = true;
        animator.SetBool("isDead", true);
    }
}
