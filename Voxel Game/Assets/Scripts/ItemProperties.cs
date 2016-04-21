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
        if (col.gameObject.name.Contains("GeckstroNOT"))
        {   
			GameObject character = col.gameObject;
			int teamState = character.GetComponent<TeamCheck>().SameTeam(this.gameObject);
			GameObject flag_b = GameObject.Find ("Flag Burgundy");
			GameObject flag_c = GameObject.Find ("Flag Cerulean");
			if(this.ID == 2)
			{
				GameObject.Destroy(this.gameObject);
				if(teamState == 1)
				{
					flag_b.GetComponent<FlagBehavior> ().Reappear ();
				}
				else if(teamState == 4)
				{
					character.GetComponent<Inventory>().PickUp(flag_b);
				}
			}
			else if(this.ID == 3)
			{
				GameObject.Destroy(this.gameObject);
				if(teamState == 2)
				{
					flag_c.GetComponent<FlagBehavior> ().Reappear ();
				}
				else if(teamState == 3)
				{
					character.GetComponent<Inventory>().PickUp(flag_c);
				}
			}
			else
			{

				if (this.ID == 0) // if flag_, just disable mesh
				{
					if(teamState == 4)
					{
						character.GetComponent<Inventory>().PickUp(flag_b);
						flag_b.GetComponent<FlagBehavior>().Disappear();
					}
					else if(teamState == 1 && character.GetComponent<Inventory>().IsInInventory(1))
					{
						Debug.Log("You get a point!");
						flag_c.GetComponent<FlagBehavior>().Reappear();
						character.GetComponent<Inventory>().inventory.Remove(character.GetComponent<Inventory>().find(1));
					}
				}
				else if(this.ID == 1)
				{
					GameObject flag_ = GameObject.Find ("flag_ Cerulean");
					if(teamState == 3)
					{
						character.GetComponent<Inventory>().PickUp(flag_c);
						flag_c.GetComponent<FlagBehavior>().Disappear();
					}
					else if(teamState == 2 && character.GetComponent<Inventory>().IsInInventory(0))
					{
						Debug.Log("You get a point!");
						flag_ = GameObject.Find ("flag_ Burgundy");
						flag_b.GetComponent<FlagBehavior>().Reappear();
						character.GetComponent<Inventory>().inventory.Remove(character.GetComponent<Inventory>().find(0));
					}
				}
				else
				{
					// next, destroy this object
					GameObject.Destroy(this.gameObject);
				}
			}
        }
    }


    public void SetInfoFromItem(Item item)
    {
        ID = item.ID;
    }
}
