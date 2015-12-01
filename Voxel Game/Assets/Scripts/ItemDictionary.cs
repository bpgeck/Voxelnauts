using UnityEngine;
using System.Collections;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;

public class ItemDictionary : MonoBehaviour {
    private List<Item> dictionary = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Streaming Assets/Items.json"));
        fillDictionary();
    }

    void fillDictionary()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            dictionary.Add(new Item((int)itemData[i]["id"], (string)itemData[i]["title"]));
        }
    }

    public Item getItemByID(int id)
    {
        Item returnItem;
        try
        {
            returnItem = dictionary[id];
        }
        catch (Exception e)
        {
            Debug.Log("The ID you requested does not exist in the dictionary: " + id);
            returnItem = null;
        }
        return returnItem;
    }
}

public class Item {
    public int ID { get; set; }
    public string Title { get; set; }

    public Item(int id, string title)
    {
        this.ID = id;
        this.Title = title;
    }
}