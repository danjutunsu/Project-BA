using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationMira : MonoBehaviour
{
    public AddResponse addResponse;
    public CharacterStats characterStats;
    //NPC Name
    public NPCStats NPCStats;

    //Player Inventory
    public Inventory inventory;

    //Item List
    public ItemList itemList;

    //Quest Progression
    public bool greeted = false;
    public bool questGiven = false;
    public bool questGiven2 = false;
    public bool questComplete = false;
    public bool rewardComplete = false;
    public bool thanksGiven = false;

    public bool dialogEnabled = false;

    //AudioSources
    public AudioSource audioGreetings;
    public AudioSource audioWelcomeBack;
    public AudioSource audioRequest;
    public AudioSource audioQuestComplete;
    public AudioSource audioThanks;
    public AudioSource audioThanksForAccepting;
    public AudioSource audioImSorry;
    public AudioSource audioReward;
    public AudioSource audioReluctantGoodbye;
    public AudioSource audioItMightBe;

    //NPC conversation captions
    public TextMeshPro captionText;

    private Coroutine animateHello;
    private Coroutine animateRequest;
    private Coroutine animateWelcomeBack;
    private Coroutine animateImSorry;
    private Coroutine animateQuestComplete;
    private Coroutine animateReward;
    private Coroutine animateThanks;
    private Coroutine animateThanksForAccepting;
    private Coroutine animateReluctantGoodbye;
    private Coroutine animateItMightBe;


    public ScrollView ScrollView;
    public Quest Quest;

    public float interactionRange = 5.0f;
    public float duration = 6.0f;

    public Image responseBackground;

    //// 1
    //public bool hello = false;
    //public bool goodbye = false;

    public bool isDialogEnabled()
    {
        return dialogEnabled;
    }

    private void Start()
    {
        Image responseBackground = GameObject.Find("ResponsePanel").GetComponent<Image>();
    }

    public string npcGreet = "Greetings, traveler.";
    public string npcYell = "Hello. Over here, please.";
    public string npcRequest = "Excuse me, kind stranger, I was out for a walk in the town square and " +
    "I seem to have lost my precious ring. It has been in my family for generations, and I fear it may be " +
    "lost forever. I've searched everywhere, but I cannot seem to find it. Would you be so kind as to help me " +
    "look for it? I would be eternally grateful for any assistance you can provide. Please, let me know if you find anything that may lead us to my lost ring.";
    public string npcItMightBe = "I'm not sure, to be honest. It's possible that I dropped it somewhere," +
        " or that it fell off my finger without me noticing. But I have a feeling that something else may be going" +
        " on. Lately, I've noticed some strange activity in the town square at night. Maybe someone took the ring" +
        " while I wasn't looking.";
    public string npcThanksForAccepting = "Thank you for taking on this task. Please keep me updated on any progress you make. The ring holds a lot of sentimental value to me.";
    public string npcWelcomeBack = "Ah, welcome back. Have you made any progress in your search for my ring? Any information you could provide would be greatly appreciated.";
    public string npcImSorry = "I'm sorry, but that ring doesn't match the description of the one I lost. I'm afraid we'll have to keep looking.";
    public string npcQuestComplete = "My ring! Thank you so much!";
    public string npcReward = "I appreciate your help. Take this as a token of my gratitude.";
    public string npcThanks = "Thank you.";
    public string npcReluctantGoodbye = "I see. Well, safe travels on your journey.";

    void ClearText()
    {
        // Reset the text to an empty string
        captionText.SetText("");
    }

    //Quest audio
    void playGreetings()
    {
        audioGreetings.Play();
    }
    void playRequest()
    {
        audioRequest.Play();
    }
    void playWelcomeBack()
    {
        audioWelcomeBack.Play();
    }
    void playImSorry()
    {
        audioImSorry.Play();
    }
    void playQuestComplete()
    {
        audioQuestComplete.Play();
    }
    void playThanks()
    {
        audioThanks.Play();
    }
    void playThanksForAccepting()
    {
        audioThanksForAccepting.Play();
    }
    void playReward()
    {
        audioReward.Play();
    }
    void playRelucatantGoodbye()
    {
        audioReluctantGoodbye.Play();
    }

    void playItMightBe()
    {
        audioItMightBe.Play();
    }

    List<Coroutine> coroutinesPlayed = new List<Coroutine>();
    List<AudioSource> audioPlayed = new List<AudioSource>();

    public void Say(string message)
    {
        if (NPCStats.currentHealth > 0)
        {
            EnableConversation();

            if (!dialogEnabled && !greeted && message == "Start")
            {
                dialogEnabled = true;
                stopAllAudioAndText();

                animateHello = StartCoroutine(AnimateText(npcGreet));
                playGreetings();

                coroutinesPlayed.Add(animateHello);
                audioPlayed.Add(audioGreetings);


                addResponse.AddNewResponse("Hello!");
                addResponse.AddNewResponse("Goodbye!");

                greeted = true;
                Invoke("ClearText", duration);
            }
            else if (!questGiven && message == "Hello!" || (!questGiven && !dialogEnabled && message == "Start"))
            {
                dialogEnabled = true;

                stopAllAudioAndText();

                addResponse.AddNewResponse("Sure, I'll help you. Do you have any idea where" +
                    " you might have lost it?");
                addResponse.AddNewResponse("I'm sorry, but I can't help you right now. Best" +
                    " of luck in your search.");


                playRequest();
                animateRequest = StartCoroutine(AnimateText(npcRequest));
                coroutinesPlayed.Add(animateRequest);
                audioPlayed.Add(audioRequest);

                questGiven = true;
                Invoke("ClearText", duration);
            }
            else if (!questGiven2 && message == "Sure, I'll help you. Do you have " +
                "any idea where you might have lost it?")
            {
                dialogEnabled = true;

                stopAllAudioAndText();

                //Add Quest to Journal
                Quest QuestOffered = Quest.QuestList[1];
                Debug.Log("Quest Type " + QuestOffered.GetType());

                ScrollView.AddNewQuest(0);


                addResponse.AddNewResponse("Thanks for letting me know. I'll investigate this" +
                    " matter and see if I can find any leads on what happened to your ring.");
                addResponse.AddNewResponse("You mean this ring?");
                addResponse.AddNewResponse("Goodbye!");


                playItMightBe();
                animateItMightBe = StartCoroutine(AnimateText(npcItMightBe));
                coroutinesPlayed.Add(animateItMightBe);
                audioPlayed.Add(audioItMightBe);

                questGiven2 = true;
                Invoke("ClearText", duration);
            }
            else if (!dialogEnabled && questGiven2 == true && message == "Start")
            {
                dialogEnabled = true;

                stopAllAudioAndText();

                playThanksForAccepting();
                animateThanksForAccepting = StartCoroutine(AnimateText(npcThanksForAccepting));
                coroutinesPlayed.Add(animateThanksForAccepting);
                audioPlayed.Add(audioThanksForAccepting);

                addResponse.AddNewResponse("I'm still working on it, but I'll let you know as soon as I have any new information.");
                addResponse.AddNewResponse("Good news! I've found your ring. Here it is. I'm glad I could help you recover it.");

                Invoke("ClearText", duration);
            }
            else if (message == "Thanks for letting me know. I'll investigate this" +
                    " matter and see if I can find any leads on what happened to your ring.")
            {
                stopAllAudioAndText();

                playThanksForAccepting();
                animateThanksForAccepting = StartCoroutine(AnimateText(npcThanksForAccepting));
                coroutinesPlayed.Add(animateThanksForAccepting);
                audioPlayed.Add(audioThanksForAccepting);

                DisableConversation();

                Invoke("ClearText", duration);
            }
            else if (!dialogEnabled && !questComplete && message == "Start")
            {
                dialogEnabled = true;

                stopAllAudioAndText();

                playWelcomeBack();
                animateWelcomeBack = StartCoroutine(AnimateText(npcWelcomeBack));
                coroutinesPlayed.Add(animateWelcomeBack);
                audioPlayed.Add(audioWelcomeBack);

                addResponse.AddNewResponse("I'm still working on it, but I'll let you know as soon as I have any new information.");
                addResponse.AddNewResponse("Good news! I've found your ring. Here it is. I'm glad I could help you recover it.");

                Invoke("ClearText", duration);
            }
            else if (!questComplete && inventory.FindItem("Family Ring") && (message == "Good news! I've found your ring. Here it is. I'm glad I could help you recover it." || (message == "You mean this ring?")))
            {
                dialogEnabled = true;

                stopAllAudioAndText();

                playReward();
                animateReward = StartCoroutine(AnimateText(npcReward));
                coroutinesPlayed.Add(animateReward);
                audioPlayed.Add(audioReward);

                //Quest rewards
                inventory.RemoveItem(itemList.itemsList.GetValueOrDefault("Family Ring"));

                Item randomItem = itemList.RandomItem();
                inventory.AddToList(itemList.itemsList.GetValueOrDefault(randomItem.itemName));

                inventory.ClearInventory();
                inventory.FillInventory();

                ScrollView.CompleteQuest(0);

                addResponse.AddNewResponse("It was my pleasure.");

                Invoke("ClearText", duration);
                questComplete = true;
                rewardComplete = true;
            }
            else if (message == "Good news! I've found your ring. Here it is. I'm glad I could help you recover it." || (message == "You mean this ring?"))
            {
                stopAllAudioAndText();

                playImSorry();
                animateImSorry = StartCoroutine(AnimateText(npcImSorry));

                coroutinesPlayed.Add(animateImSorry);
                audioPlayed.Add(audioImSorry);

                DisableConversation();

                Invoke("ClearText", duration);
            }
            else if (!rewardComplete && message == "It was my pleasure.")
            {
                stopAllAudioAndText();

                DisableConversation();

                Invoke("ClearText", duration);
            }
            else if (rewardComplete)
            {
                dialogEnabled = true;

                stopAllAudioAndText();

                animateThanks = StartCoroutine(AnimateText(npcThanks));
                playThanks();
                coroutinesPlayed.Add(animateThanks);
                audioPlayed.Add(audioThanks);

                DisableConversation();

                Invoke("ClearText", duration);
                thanksGiven = true;
            }

            //End Conversation
            else if (message == "Goodbye!")
            {
                stopAllAudioAndText();

                DisableConversation();
                playRelucatantGoodbye();

                animateReluctantGoodbye = StartCoroutine(AnimateText(npcReluctantGoodbye));

                DisableConversation();

                Invoke("ClearText", duration);
            }
            else if (message == "I'm sorry, but I can't help you right now. Best of luck in your search."
                || message == "I'm still working on it, but I'll let you know as soon as I have any new information.")
            {
                stopAllAudioAndText();

                DisableConversation();

                playRelucatantGoodbye();

                animateReluctantGoodbye = StartCoroutine(AnimateText(npcReluctantGoodbye));

                Invoke("ClearText", duration);
            }
        }
    }

    void stopAllAudioAndText()
    {
        foreach (Coroutine routine in coroutinesPlayed)
        {
            StopRoutine(routine);
        }

        foreach (AudioSource audioSource in audioPlayed)
        {
            StopAudio(audioSource);
            Debug.Log("Audio List: " + audioSource);
        }
    }
    void StopRoutine(Coroutine routine)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
    }

    void StopAudio(AudioSource audio)
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
    }

    void DisableConversation()
    {
        dialogEnabled = false;

        if (responseBackground.enabled == true)
        {
            responseBackground.enabled = false;
        }
    }

    void EnableConversation()
    {
        if (responseBackground.enabled == false)
        {
            responseBackground.enabled = true;
        }
    }

    private void Update()
    {
        // STOP audio from NPC if NPC dies
        if (NPCStats.currentHealth <= 0
            && (audioGreetings.isPlaying
            || audioQuestComplete.isPlaying
            || audioRequest.isPlaying
            || audioReward.isPlaying
            || audioThanks.isPlaying
            || audioReluctantGoodbye.isPlaying))
        {
            DisableConversation();
            audioQuestComplete.Stop();
            audioGreetings.Stop();
            audioRequest.Stop();
            audioReward.Stop();
            audioThanks.Stop();
            audioReluctantGoodbye.Stop();
        }
    }

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
