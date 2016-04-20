using UnityEngine;
using System.Collections;

public class DeathCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("Deathcube has no mercy");
		col.gameObject.GetComponent<AstroFirstPersonControl> ().Die ();
	}
}
