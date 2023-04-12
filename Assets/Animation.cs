using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        // Get a reference to the Animator component
        anim = GetComponent<Animator>();
    }

    public void MyMethod()
    {
        // Call the Play method of the Animator component to play the animation
        anim.Play("DisplayText");
    }
}
