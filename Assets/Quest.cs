using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public static int questCounter = 0;
    public int questId;
    public string questName;
    public string questDescription;
    public int questExperience;
    public List<Quest> QuestList = new List<Quest>();

    public Quest(int id, string name, string description, int experience)
    {
        questId = questCounter++;
        questName = name;
        questDescription = description;
        questExperience = experience;

    }

    // Start is called before the first frame update
    void Awake()
    {
        Quest familyRing = new Quest
            (
            questId,
            "Family Ring",
            "An NPC in the town square has lost a precious ring that has been in their" +
            " family for generations. They have searched everywhere but can't seem to " +
            "find it. The NPC is asking for your help in locating the lost ring" +
            " and would be eternally grateful for any assistance provided. The player" +
            " is requested to inform the NPC if they find anything that may lead them" +
            " to the lost ring.",
            500
            );

        QuestList.Add(familyRing);

        Quest familyRings = new Quest
            (
            questId,
            "Rat Roundup at the Tavern",
            "The local tavern is overrun with rats, and the owner needs someone to clear them out. Kill 10 rats and return to the tavern owner for a reward.",
            300
            );

        QuestList.Add(familyRings);

        Debug.Log("QUESTS----");
        foreach (Quest quest in QuestList)
        {
            Debug.Log(quest.questId);
            Debug.Log(quest.questName);
            Debug.Log(quest.questDescription);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
