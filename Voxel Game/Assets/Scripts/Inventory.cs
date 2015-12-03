using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Inventory : MonoBehaviour {
    List<Item> inventory = new List<Item>();
    ItemDictionary dictionary;

    void Start () {
	}

    void OnCollisionEnter (Collision col)
    {
        Debug.Log("Collided");
        if (col.gameObject.name.Contains("Flag"))
        {
            Debug.Log("Touching the flag!!");
            pickUp(col.gameObject);
        }
    }

    void pickUp (GameObject item)
    {
        inventory.Add(dictionary.getItemByID(item.GetComponent<ItemInformation>().ID));
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
