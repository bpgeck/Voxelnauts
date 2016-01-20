using UnityEngine;
using System.Collections;

public class ItemProperties : MonoBehaviour {
    public int ID;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Character"))
        {
            // first, add this item to the player's inventory
            col.gameObject.GetComponent<Inventory>().PickUp(this.gameObject);

            if (this.ID == 0) // if flag, just disable mesh
            {
                this.GetComponent<FlagBehavior>().Disappear();
            }
            else
            {
                // next, destroy this object
                GameObject.Destroy(this.gameObject);
            }
        }
    }


    public void SetInfoFromItem(Item item)
    {
        ID = item.ID;
    }
}
