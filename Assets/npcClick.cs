using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using System.Collections.Generic;

//TODO -- Add function to stop NPC speech/caption if walk away
//TODO -- Add function to accept quest from NPC
//TODO -- Add function to reject quest from NPC

public class npcClick : MonoBehaviour
{
    public LayerMask layerMask; // The layer mask to use for the raycast
    public InputAction clickAction;
    public InputAction rightClickAction;
    public GameObject Player;
    public GameObject Enemy;

    //AudioSources
    public AudioSource audioYell;
    //Player Inventory
    public Inventory inventory;
    public ItemList itemList;
    //NPC conversation captions
    public TextMeshPro captionText;
    // NPC Name
    public NPCStats NPCStats;
    // Target objects to set target UI
    public TargetStats targetStats;
    public AddResponse addResponse;
    public Focus focus;
    public GameObject TargetPanel;
    public Quest Quest;
    public ScrollView ScrollView;
    public ConversationManager conversationManager;

    public float interactionRange = 5.0f;
    public float duration = 6.0f;

    // 1
    public bool hello = false;
    public bool goodbye = false;

    public string npcYell = "Hello. Over here, please.";

    public void Start()
    {

    }

    private void OnEnable()
    {
        clickAction = new InputAction("clickAction", binding: "<Mouse>/leftButton");
        clickAction.Enable();
        clickAction.performed += OnClickNPC;
    }
    private void Update()
    {
        //// STOP audio from NPC if NPC dies
        //if (audioYell.isPlaying && NPCStats.currentHealth <= 0)
        //{
        //    audioYell.Stop();
        //}
    }

    private void OnClickNPC(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject == gameObject)
            {
                GameObject npc = hit.collider.gameObject;
                NPCStats npcStats = npc.GetComponentInChildren<NPCStats>();
                float distance = Vector3.Distance(Player.transform.position, hit.collider.transform.position);
                if (distance <= interactionRange)
                {
                    focus.focus(hit.collider.gameObject);
                    //Quest progression with NPC
                    if (npcStats.currentHealth >= 0)
                    {
                        conversationManager.Say("Start");
                    }
                }
                //If within 20.0f, NPC yells to you
                else if (distance > interactionRange && distance <= 20)
                {
                    playYell();

                    StartCoroutine(AnimateText(npcYell));   
                    Invoke("ClearText", duration);
                }
            }
        }
    }



    void ClearText()
    {
        // Reset the text to an empty string
        captionText.SetText("");
    }

    //NPC audio
    void playYell() {
        audioYell.Play();
    }

    private TextMeshPro textMeshPro;

    IEnumerator AnimateText(string text)
    {
        // captionText = gameObject.GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();

        int totalVisibleCharacters = text.Length;
        int counter = 0;

        //STOP captions from NPC if NPC dies
        while (true && NPCStats.currentHealth > 0)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            captionText.text = text.Substring(0, visibleCount);
            captionText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                yield return new WaitForSeconds(1.0f);
                Invoke("ClearText", 1.0f);
                break;
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }        
    }

}