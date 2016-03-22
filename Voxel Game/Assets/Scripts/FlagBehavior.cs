using UnityEngine;
using System.Collections;

public class FlagBehavior : MonoBehaviour 
{
	public Renderer[] flagRend;
	public BoxCollider boxCollider;

	void Start () 
	{
		flagRend = GetComponentsInChildren<MeshRenderer> ();
		boxCollider = GetComponent<BoxCollider> ();
	}

	public void Disappear()
	{
		//Check if colliding object is a player on the opposite team
        foreach (Renderer rend in flagRend)
		{
			rend.enabled = false;
		}
			boxCollider.enabled = false;
	}

    public void Reappear() // this will be called when the user dies
    {
        foreach (Renderer rend in flagRend)
        {
            rend.enabled = true;
        }
            boxCollider.enabled = true;
    }
}
