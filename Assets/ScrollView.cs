using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scrollViewContent;
    public TextMeshProUGUI QuestTitle;
    public TextMeshProUGUI QuestDescription;
    public CharacterStats characterStats;
    public GameObject prefab;
    public Quest Quest;
    public int questIndex;

    void Start()
    {
        scrollViewContent = GameObject.Find("Content");
    }

    public void AddNewQuest(int index)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.SetParent(scrollViewContent.transform, false);

        // Get a reference to the TextMeshProUGUI component of the new object
        TextMeshProUGUI textComponent = newObject.GetComponentInChildren<TextMeshProUGUI>();

        // Set the text of the TextMeshProUGUI component
        textComponent.text = Quest.QuestList[index].questName;

        // Get the button component of the new object
        Button button = newObject.GetComponentInChildren<Button>();

        // Add a listener to the button's onClick event
        button.onClick.AddListener(() => {
            // Call a method on the ScrollView script and pass the index
            OnButtonClick(index);
        });
    }

    public void CompleteQuest(int index)
    {
        characterStats.GainExperience(Quest.QuestList[index].questExperience);
        RemoveQuest(index);
    }

    private void OnButtonClick(int index)
    {
        QuestTitle.SetText(Quest.QuestList[index].questName);
        QuestDescription.SetText(Quest.QuestList[index].questDescription);
    }

    public void RemoveQuest(int index)
    {
        // Find the quest object in the scroll view content
        Transform questObject = scrollViewContent.transform.GetChild(index);

        QuestTitle.SetText("");
        QuestDescription.SetText("");

        // Remove the quest object from the scroll view content
        Destroy(questObject.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
