using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject targetPlayer;
    public NPCAttack attack;
    public CharacterStats CharacterStats;

    public void setTarget(GameObject playerObject)
    {
        targetPlayer = playerObject;
        CharacterStats = targetPlayer.GetComponent<CharacterStats>();
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
