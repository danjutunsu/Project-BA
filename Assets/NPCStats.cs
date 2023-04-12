using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class NPCStats : MonoBehaviour
{
    public int maxHealth { get; set; }
    public int maxMana { get; set; }
    public int currentHealth { get; set; }
    public int currentMana { get; set; }
    public int assignedHP;
    public int assignedMana;
    public bool lootable = false;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public GameObject npcObject;
    public Vector3 startingPosition;
    public Chest chest;


    void Awake()
    {
        startingPosition = transform.position;
        maxHealth = assignedHP;
        maxMana = assignedMana;
        currentHealth = assignedHP;
        currentMana = assignedMana;
        chest = gameObject.transform.parent.GetComponentInChildren<Chest>();
    }

    public void LoseHP(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log("NPC Health: " + currentHealth);
        }
        if ((currentHealth - damage) <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void LoseMana(int damage)
    {
        if (currentMana > 0)
        {
            currentMana -= damage;
        }
        if ((currentMana - damage) <= 0)
        {
            currentMana = 0;
        }
    }

    //Bring NPC back to life and teleport it back to the starting position
    public void Revive()
    {
        currentHealth = assignedHP;
        currentMana = assignedMana;
        animator.SetBool("isDead", false);
        animator.SetBool("attacking", false);
        animator.SetFloat("speed", 0f);

        npcObject.transform.position = startingPosition;
        lootable = false;
        chest.ResetChest();
    }

    public void Update()
    {

    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        animator.SetBool("attacking", false);
        Debug.Log("NPC dies");
        StopLongAnimationOnDeath(animator, "AGIA_Idle_generic_01");
    }

    public void StopLongAnimationOnDeath(Animator animator, string longAnimationName)
    {
        // Check if the long animation is currently playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(longAnimationName))
        {
            // Crossfade to a death animation state with zero transition time
            animator.CrossFade("Death", 0f);
        }
    }
}
