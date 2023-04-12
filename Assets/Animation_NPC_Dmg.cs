using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_NPC_Dmg : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        //animator.Play("DisplayText");
    }
}
