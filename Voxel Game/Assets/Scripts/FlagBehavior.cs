using UnityEngine;
using System.Collections;

public class FlagBehavior : MonoBehaviour 
{
	public Renderer[] flagRend;
	public BoxCollider[] boxCollider;

	void Start () 
	{
		flagRend = GetComponentInChildren<MeshRenderer> ();
		boxCollider = GetComponentsInChildren<BoxCollider> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		//Check if colliding object is a player on the opposite team
		if (collision.collider.CompareTag ("Player"))
		{
			foreach (Renderer rend in flagRend)
			{
				rend.enabled = false;
			}
			foreach (BoxCollider box in boxCollider)
			{
				box.enabled = false;
			}
		}
	}

}
