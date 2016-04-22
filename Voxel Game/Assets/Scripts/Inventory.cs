using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class Inventory : MonoBehaviour 
{
	public GameObject Flag_B;
	public GameObject Flag_C;

    public List<Item> inventory = new List<Item>();

    void Start () 
	{
		DropAll ();
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
    public bool IsInInventory (int checkId)
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

	public Item find(int checkId)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].ID == checkId)
			{
				return inventory[i];
			}
		}
		return null;
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
        if (item.ID == 0)
		{ 
			//Instantiate(Flag_B,this.GetComponent<AstroFirstPersonControl>().deathPosition, Quaternion.identity);
		} 
		else if (item.ID == 1) 
		{
			//Instantiate(Flag_C,this.GetComponent<AstroFirstPersonControl>().deathPosition, Quaternion.identity);
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
