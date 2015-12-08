using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Inventory : MonoBehaviour {
    List<Item> inventory = new List<Item>();

    void Start () {

	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name);
        if (hit.gameObject.GetComponent<ItemInformation>() != null)
        {
            pickUp(hit.gameObject);
        }
    }

    void pickUp (GameObject item)
    {
        inventory.Add(ItemDictionary.getItemByID(item.GetComponent<ItemInformation>().ID));
    }

    int amountInInventory (int checkId)
    {
        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID == checkId)
            {
                count++;
            }
        }
        return count;
    }
}
