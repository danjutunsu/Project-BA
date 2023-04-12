using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyRing : MonoBehaviour
{
    public Chest Chest;
    // Start is called before the first frame update
    void Start()
    {
        Chest.AddNewSlot();
        Chest.AddToList(Chest.itemList.itemsList["Family Ring"]);
        Chest.ClearChest();
        Chest.FillChest();
    }

}
