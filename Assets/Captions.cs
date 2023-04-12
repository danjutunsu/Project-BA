using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Captions : MonoBehaviour
{
    public Text captionText;
    public float displayTime = 2.0f;

    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void ShowCaption(string text)
    {
        captionText.text = text;
        canvas.enabled = true;
        Invoke("HideCaption", displayTime);
    }

    public void HideCaption()
    {
        canvas.enabled = false;
    }
}
