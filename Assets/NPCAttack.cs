using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NPCAttack : MonoBehaviour
{
    public CharacterStats characterStats;
    public NPCStats npcStats;
    public float maxHealth; // The maximum health of the player
    public float characterHealth; // The current health of the player
    public float characterMana; // The current mana of the player
    public float distanceThreshold = 8.0f; // The distance threshold for losing health
    private bool combatRunning = false;
    public GameObject Player;
    public Transform Enemy; // The transform of this enemy object
    public TextMeshProUGUI DamageTaken;
    public Animator animator;
    public Animator npcAnimator;
    public string meleeAttack;
    public Target target;
    public TextMeshProUGUI npcName;


    public LayerMask layerMask; // The layer mask to use for the raycast
    public InputAction clickAction;

    public void Start()
    {
        CharacterStats characterStats = target.CharacterStats.GetComponent<CharacterStats>();
        characterHealth = characterStats.currentHealth;
        characterMana = characterStats.currentMana;
        Player.layer = LayerMask.NameToLayer("Player");
    }

    public void FixedUpdate()
    {
        
        float distanceToEnemy = Vector3.Distance(Player.transform.position, Enemy.position);
        Vector3 direction = (Player.transform.position - transform.position).normalized;
        //Debug.Log("Step One");
        if (distanceToEnemy <= distanceThreshold && !combatRunning)
        {
            //Debug.Log("Step Two");

            combatRunning = true;
            StartCoroutine(EnemyCombat());
        }
        else if (distanceToEnemy > distanceThreshold && combatRunning)
        {
            //Debug.Log("Step Three");

            combatRunning = false;
            StopCoroutine(EnemyCombat());
        }
    }

    public void Update()
    {
        Player = target.targetPlayer;

        Transform charStats = Player.transform.Find("CharacterStats");
        characterStats = charStats.GetComponent<CharacterStats>();
        characterHealth = characterStats.currentHealth;
        characterMana = characterStats.currentMana;

        //Debug.Log(npcName.text + " current Health: " + npcStats.currentHealth);
    }

    IEnumerator EnemyCombat()
    {
        Debug.Log(npcName.name);

        
        while (combatRunning && characterHealth > 0 && npcStats.currentHealth > 0)
        {
            var randomDmg = UnityEngine.Random.Range(1, 100);
            Debug.Log("Taking Damage from " + npcName.text + ": " + randomDmg.ToString());
            characterStats.LoseHP(randomDmg);
            characterStats.LoseMana(randomDmg / 2);

            //Call Animation to simulate Damage taken
            characterStats.TakeDamage();
            
            DamageTaken.enabled = true;
            animator.SetBool("DisplayText", true);
            var randomMelee = UnityEngine.Random.Range(1, 100);

            if (randomMelee >= 50) 
            {
                npcAnimator.Play("PunchRight");
            }
            else
            {
                npcAnimator.Play("PunchLeft");
            }
            if (npcName.text == "Golem")
            {
                npcAnimator.Play("Hit");
            }

            DamageTaken.SetText($"-{randomDmg}");

            // Enable DamageTaken for 2 seconds
            yield return new WaitForSeconds(2.5f);

            animator.SetBool("DisplayText", false);
            DamageTaken.enabled = false;
        }
    }
}
