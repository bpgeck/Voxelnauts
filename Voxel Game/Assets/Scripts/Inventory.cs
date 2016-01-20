using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Inventory : MonoBehaviour {
    public List<Item> inventory = new List<Item>();

    void Start () {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    // adds the game object to your inventory
    public void PickUp (GameObject item)
    {
        Debug.Log("Got an item: " + item.name);
        inventory.Add(ItemDictionary.getItemByID(item.GetComponent<ItemProperties>().ID));
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
        for (int i = 0; i < inventory.Count; i++)
        {
            Drop(inventory[i]);
        }
    }

    public void Drop(Item item)
    {
        //TODO
        // create prefab of Item argument
        // drop prefab
        // remove 1 copy of the corresponding item from `inventory` List
        if (item.ID == 0) // if the user is dropping a flag, don't spawn a prefab, just reset the flag's mesh
        {
            GameObject flag = GameObject.Find("Flag");
            flag.GetComponent<FlagBehavior>();
        }
        else
        {
            GameObject droppedItem = (GameObject)Instantiate(Resources.Load("Item"));
            droppedItem.GetComponent<ItemProperties>().SetInfoFromItem(item);
            Debug.Log("Dropped an item: " + item.Title);
        }

        inventory.Remove(item);
    }
}
