using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SpellCast : MonoBehaviour
{
    public float spellRange = 30f; // The range of the spell
    public int spellDamage = 10; // The damage of the spell
    public GameObject Player;
    public Animator animator;
    public GameObject TargetObj;
    public GameObject characterGameObject;
    public GameObject npc;
    public GameObject fireballPrefab;
    public float t = 0.5f;
    public float speed = 10.0f;
    public float distanceThreshold = 2f;

    private bool isCastingSpell = false;

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            CastSpell();
        }

        if (fireballPrefab != null && npc != null)
        {
            Vector3 direction = npc.transform.position - fireballPrefab.transform.position;
            float distance = direction.magnitude;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 velocity = normalizedDirection * speed;
            fireballPrefab.transform.position += velocity * Time.deltaTime;

            if (distance <= distanceThreshold)
            {
                // Fireball has hit the target, do damage, etc.
                //DestroyImmediate(fireballPrefab, true);
            }
        }
    }

    void CastSpell()
    {
        Debug.Log("FIRE");
        TargetStats targetStats = TargetObj.GetComponent<TargetStats>();
        npc = targetStats.selectedNPC;
        // Spawn the fire ball particle effect at the character's hands
        Vector3 characterPosition = Player.transform.position;
        GameObject fireBall = Instantiate(fireballPrefab, characterPosition, Quaternion.identity);
        StartCoroutine(TrackNPC(fireBall, npc));
    }


    IEnumerator TrackNPC(GameObject fireBall, GameObject npc)
    {
        Vector3 characterPosition = Player.transform.position;
        Vector3 npcPosition = npc.transform.position;
        float distance = Vector3.Distance(characterPosition, npcPosition);

        while (distance > distanceThreshold)
        {
            // Calculate the direction and velocity of the fireball towards the NPC
            Vector3 direction = npcPosition - fireBall.transform.position;
            Vector3 normalizedDirection = direction.normalized;
            Vector3 velocity = normalizedDirection * speed;

            // Update the fireball's position and rotation
            fireBall.transform.position += velocity * Time.deltaTime;
            fireBall.transform.rotation = Quaternion.LookRotation(direction);

            // Update the distance to the NPC
            distance = Vector3.Distance(fireBall.transform.position, npcPosition);

            yield return null;
        }
        var randomDmg = UnityEngine.Random.Range(10, 20000);
        NPCStats npcStats = npc.GetComponentInChildren<NPCStats>();
        GameObject npcDamage = GameObject.Find("NPCDamageText");
        TextMeshProUGUI NPCDamageText = npcDamage.GetComponent<TextMeshProUGUI>(); 
        npcStats.LoseHP(randomDmg);
        NPCDamageText.enabled = true;

        NPCDamageText.SetText($"-{randomDmg}");


        // Fireball has hit the target, do damage, etc.
        Destroy(fireBall);
    }


    void ApplySpellEffect(GameObject target)
    {
        // Apply the spell effect to the target, such as damage or a debuff
        // You can access the target's health or other components to apply the effect
    }
}
