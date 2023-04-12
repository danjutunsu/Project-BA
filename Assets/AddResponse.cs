using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddResponse : MonoBehaviour
{
    public GameObject ResponsePanel;
    public GameObject prefab;
    ConversationManager conversationManager;
    public Focus focus;

    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private void Start()
    {
        GameObject focused = GameObject.Find("Focus");
        Focus focus = focused.GetComponent<Focus>();
        GameObject convoManager = GameObject.Find("ConversationManager");
        conversationManager = convoManager.GetComponent<ConversationManager>();
    }

    public void AddNewResponse(string response)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.SetParent(ResponsePanel.transform, false);


        //Debug.Log("Focused NPC: " + conversation.getNpc().name);

        // Get a reference to the TextMeshProUGUI component of the new object
        TextMeshProUGUI textComponent = newObject.GetComponentInChildren<TextMeshProUGUI>();

        // Set the text of the TextMeshProUGUI component
        textComponent.text = response;

        instantiatedObjects.Add(newObject);

        // Add a button component to the new game object
        Button button = newObject.GetComponent<Button>();
        if (button != null)
        {
            // Add a listener to the button that removes and destroys the instantiated object when clicked
            button.onClick.AddListener(() =>
            {
                // Save the text before removing and destroying the object
                string text = textComponent.text;

                // Destroy all objects
                foreach (GameObject obj in instantiatedObjects)
                {
                    Destroy(obj);
                }
                Debug.Log("All objects destroyed.");

                // Clear the list
                instantiatedObjects.Clear();
                Debug.Log("List cleared.");

                // Do something with the text, such as displaying it in a message box
                Debug.Log("Text: " + text);
                conversationManager.Say(text);
            });
        }
    }
}
