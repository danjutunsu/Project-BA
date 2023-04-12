using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index : MonoBehaviour
{
    public int arrayIndex = 0;

    public void SetIndex(int number)
    {
        arrayIndex += number;
    }
}