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
        // if this you touch a grab-able object, pick it up
        if (hit.gameObject.GetComponent<ItemInformation>() != null)
        {
            PickUp(hit.gameObject);
        }
    }

    // adds the game object to your inventory
    void PickUp (GameObject item)
    {
        inventory.Add(ItemDictionary.getItemByID(item.GetComponent<ItemInformation>().ID));
    }

    // returns number of specific objects in inventory
    int AmountInInventory (int checkId)
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

    // returns true if the user has even one of the item in inventory
    bool IsInInventory (int checkId)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ID == checkId)
            {
                return true;
            }
        }
        return false;
    }

    public void DropAll()
    {
        //TODO
        // create corresponding prefabs of all objects in inventory
        // drop all prefabs at certain radius around player
        // delete all entries in `inventory` List
    }

    public void Drop(Item item)
    {
        //TODO
        // create prefab of Item argument
        // drop prefab
        // remove 1 copy of the corresponding item from `inventory` List
    }
}
