using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
	public float health;

	void Start () 
	{

	}

	void Update()
	{
		if (health <= 0)
		{
			this.GetComponent<AstroFirstPersonControl>().Die ();
		}
	}

    [PunRPC]
	void Damage(float damage)
	{
        if (this.gameObject.GetComponent<AstroFirstPersonControl>().alive)
    		health -= damage;
	}
}
