using UnityEngine;
using System.Collections;

public class bgRotation : MonoBehaviour {

	public float speed = 10;
	public string dir = "right";
	
	//Slowly rotates object
	void Update () {
		if (dir.Equals("right"))
			transform.Rotate(Vector3.down * (speed * Time.deltaTime)/4);
		 else if (dir.Equals("up"))
			transform.Rotate(Vector3.right * (speed * Time.deltaTime)/4);
	
	}
}
