using UnityEngine;
using System.Collections;

public class FlagBehavior : MonoBehaviour 
{
	public Renderer[] flagRend;
	public BoxCollider[] boxCollider;

	void Start () 
	{
		flagRend = GetComponentsInChildren<MeshRenderer> ();
		boxCollider = GetComponentsInChildren<BoxCollider> ();
	}

	public void Disappear()
	{
		//Check if colliding object is a player on the opposite team
        foreach (Renderer rend in flagRend)
		{
			rend.enabled = false;
		}
		foreach (BoxCollider box in boxCollider)
		{
			box.enabled = false;
		}
	}

    public void Reappear() // this will be called when the user dies
    {
        foreach (Renderer rend in flagRend)
        {
            rend.enabled = true;
        }
        foreach (BoxCollider box in boxCollider)
        {
            box.enabled = true;
        }
    }
}
