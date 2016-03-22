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
			int teamState = col.gameObject.GetComponent<TeamCheck>().SameTeam(this.gameObject);
			if(this.ID == 1)
			{
				GameObject.Destroy(this.gameObject);
				if(teamState == 1)
				{
					GameObject flag = GameObject.Find ("Flag");
					flag.GetComponent<FlagBehavior> ().Reappear ();
				}
				else if(teamState == 2)
				{

				}
			}
			else
			{

				if (this.ID == 0) // if flag, just disable mesh
				{
					if(teamState == 2)
					{
						col.gameObject.GetComponent<Inventory>().PickUp(this.gameObject);
						this.GetComponent<FlagBehavior>().Disappear();
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
