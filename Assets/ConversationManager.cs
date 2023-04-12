using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    public Focus focus;
    public GameObject MageConversation;

    public void Say(string message)
    {
        if (focus.focusedNPC.gameObject.name == "Mage")
        {
            ConversationMira mira = MageConversation.GetComponent<ConversationMira>();
            mira.Say(message);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
