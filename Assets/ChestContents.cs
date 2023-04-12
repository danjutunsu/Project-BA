using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestContents: MonoBehaviour
{
    public ItemList itemList;
    public LayerMask chestMask; // The layer mask to use for the 
    public GameObject Player; 
    public Chest Chest;
    public int Size;
    public int Quantity;
    public float interactionRange = 5.0f;
    public GameObject chestPanel;
    public List<Item> chestContents = new List<Item>();

    void Start()
    {

    }
}
