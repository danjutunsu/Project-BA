using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class NPCInteractionController : MonoBehaviour
{
    public InputAction clickAction;
    public InputAction leftClick;
    public NPCStats npcStats;
    public float maxHealth; // The maximum health of the player
    public float currentHealth; // The current health of the player
    public float currentMana; // The current mana of the player
    public float distanceThreshold = 40.0f; // The distance threshold for losing health
    private bool combatRunning = false;
    public GameObject Player;
    public GameObject Enemy;
    public TextMeshProUGUI npcDamageTaken;
    public Animator animator;
    public Animator PlayerAnimator;
    public Chest Chest;
    public GameObject chestPanel;
    public Canvas Canvas;

    public GameObject TargetPanel;
    public TargetStats targetStats;

    //public LayerMask layerMask;
    public LayerMask Character;

    private void Start()
    {
        // Set the layer of the player character to "Ignore Raycast"
        //Player.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    private void OnEnable()
    {
        clickAction = new InputAction("clickAction", binding: "<Mouse>/rightButton");
        clickAction.Enable();
        clickAction.performed += OnClickNPC;

        leftClick = new InputAction("clickAction", binding: "<Mouse>/leftButton");
        leftClick.Enable();
        leftClick.performed += LeftClickNPC;
    }

    private void OnDisable()
    {
        clickAction.performed -= OnClickNPC;
        clickAction.Disable();

        leftClick.performed -= LeftClickNPC;
        leftClick.Disable();
    }

    private void LeftClickNPC(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        int layerMask = ~LayerMask.GetMask("Player"); // ignore raycasts on player layer

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (!TargetPanel.activeSelf && hit.collider.gameObject.tag == "NPC")
            {
                targetStats.selectedNPC = hit.collider.gameObject;
                Canvas NPCCanvas = targetStats.selectedNPC.transform.Find("NPCCanvas").GetComponent<Canvas>();
                TextMeshProUGUI NPCName = NPCCanvas.transform.Find("NPCName").GetComponent<TextMeshProUGUI>();
                targetStats.Name.text = NPCName.text;
                //Debug.Log("Clicked Name: " + NPCName.text);

                TargetPanel.SetActive(true);
            }
            else if (TargetPanel.activeSelf && targetStats.selectedNPC != hit.collider.gameObject && hit.collider.gameObject.tag == "NPC")
            {
                //Debug.Log("ASSIGNING");
                Debug.Log(hit.collider.gameObject.tag);
                targetStats.selectedNPC = hit.collider.gameObject;
                Canvas NPCCanvas = targetStats.selectedNPC.transform.Find("NPCCanvas").GetComponent<Canvas>();
                TextMeshProUGUI NPCName = NPCCanvas.transform.Find("NPCName").GetComponent<TextMeshProUGUI>();
                targetStats.Name.text = NPCName.text;
            }
            else if (TargetPanel.activeSelf && hit.collider.gameObject.tag != "NPC")
            { 
                //Debug.Log("CLEAR");
                targetStats.selectedNPC = null;
                targetStats.Name.text = null;

                TargetPanel.SetActive(false);
            }
        }
    }

    // Handles Right Clicks on NPCs
    private void OnClickNPC(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        int layerMask = ~LayerMask.GetMask("Player"); // ignore raycasts on player layer

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("NPC") && hit.collider.gameObject != gameObject)
        {
            if (!TargetPanel.activeSelf)
            {
                TargetPanel.SetActive(true);
            }
                //Debug.Log("Deselect NPC if NPC is current Enemy so you can easily stop attacks");
            if (Enemy != null && hit.collider.gameObject == Enemy && npcStats.currentHealth > 0)
            {
                targetStats.selectedNPC = hit.collider.gameObject;
                Canvas NPCCanvas = targetStats.selectedNPC.transform.Find("NPCCanvas").GetComponent<Canvas>();
                TextMeshProUGUI NPCName = NPCCanvas.transform.Find("NPCName").GetComponent<TextMeshProUGUI>();
                targetStats.Name.text = NPCName.text;
                
                Enemy = null;
                combatRunning = false;
            }
            else
            {
                TargetPanel.SetActive(true);
                targetStats.selectedNPC = hit.collider.gameObject;
                Canvas NPCCanvas = targetStats.selectedNPC.transform.Find("NPCCanvas").GetComponent<Canvas>();
                TextMeshProUGUI NPCName = NPCCanvas.transform.Find("NPCName").GetComponent<TextMeshProUGUI>();
                targetStats.Name.text = NPCName.text;

                //Debug.Log("Clicked on " + hit.collider.gameObject.name);
                Enemy = hit.collider.gameObject;

                NPCStats clickStats = Enemy.GetComponentInChildren<NPCStats>();
                npcStats = clickStats;
                Canvas npcCanvas = Enemy.GetComponentInChildren<Canvas>(); // get the Canvas component
                TextMeshProUGUI damageTaken = npcCanvas.transform.Find("NPCDamageText").GetComponent<TextMeshProUGUI>();
                npcDamageTaken = damageTaken;
                animator = damageTaken.GetComponent<Animator>();
            }
        }

        Chest npcChest = Enemy.transform.Find("Chest").GetComponent<Chest>();
        Chest = npcChest;
        Canvas backpack = Chest.transform.Find("Canvas").GetComponent<Canvas>();
        Canvas = backpack;
        Image panel = backpack.transform.Find("ChestInventoryPanel").GetComponentInChildren<Image>();
        chestPanel = panel.gameObject;

        if (npcStats.lootable && !chestPanel.activeSelf)
        {
            //Debug.Log("Looting!");
            chestPanel.SetActive(true);
        }
        else if (npcStats.lootable && chestPanel.activeSelf)
        {
            chestPanel.SetActive(false);
        }
    }

    public void FixedUpdate()
    {
        if (Enemy != null)
        {
            //Debug.Log("ENEMY: " + Enemy);
            float distanceToEnemy = Vector3.Distance(Player.transform.position, Enemy.transform.position);
            Vector3 direction = (Player.transform.position - transform.position).normalized;

            if (distanceToEnemy <= distanceThreshold && !combatRunning)
            {
                combatRunning = true;
                StartCoroutine(AttackNPC());
            }
            else if (distanceToEnemy > distanceThreshold && combatRunning)
            {
                combatRunning = false;
                StopCoroutine(AttackNPC());
            }
        }
    }

    void CastSpell()
    {
        PlayerAnimator.Play("CastSpell");
    }

    IEnumerator AttackNPC()
    {
        float currentNPCHealth = npcStats.currentHealth;
        while (combatRunning && currentNPCHealth > 0)
        {
            //Debug.Log("ATTACKING");
            var randomDmg = UnityEngine.Random.Range(10, 20000);
            npcStats.LoseHP(randomDmg);
            if (npcStats.currentHealth <= 0)
            {
                //Debug.Log("NPC dies. deselecting");
                Enemy = null;
                combatRunning = false;
            }
            var randomMelee = UnityEngine.Random.Range(1, 100);
            if (randomMelee >= 50)
            {
                PlayerAnimator.Play("PunchRight");
            }
            else
            {
                PlayerAnimator.Play("PunchLeft");
            }

            var randomDrain= UnityEngine.Random.Range(10, 200);
            npcStats.LoseMana(randomDrain / 2);
            animator.SetBool("DisplayText", true);

            npcDamageTaken.SetText($"-{randomDmg}");

            currentNPCHealth = npcStats.currentHealth;
            //Debug.Log("NPC current health: " + currentNPCHealth);

            npcDamageTaken.enabled = true;
            yield return new WaitForSeconds(2.5f);
            animator.SetBool("DisplayText", false);

            npcDamageTaken.enabled = false;
        }

    }
}
