using UnityEngine;
using System.Collections;

public class HitPartBehavior : MonoBehaviour 
{
	private int despawn;
	void Start () 
	{
		despawn = 0;
	}

	void Update () 
	{
		despawn++;
		if (despawn >= 15) 
		{
			Destroy(this.gameObject);
		}
	}
}
