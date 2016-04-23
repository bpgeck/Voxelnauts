using UnityEngine;
using System.Collections;

public class bgRotation : MonoBehaviour {

	public float speed = 10;
	
	//Slowly rotates object
	void Update () {
		transform.Rotate(Vector3.down * (speed * Time.deltaTime)/4);
	
	}
}
