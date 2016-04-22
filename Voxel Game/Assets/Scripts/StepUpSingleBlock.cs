using UnityEngine;
using System.Collections;

public class StepUpSingleBlock : MonoBehaviour {
    Ray footRay;
    Ray ankleRay;
    RaycastHit footHit;
    RaycastHit ankleHit;
    GameObject footCaster;
    GameObject ankleCaster;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        footCaster = this.transform.Find("FootBlockDetector").gameObject; // continually get the position of the raycasters
        ankleCaster = this.transform.Find("AnkleBlockDetector").gameObject;
        
        
        if (shouldStepUp())
        {
            this.GetComponent<CharacterController>().Move(Vector3.up); // move up one block
        }
	}

    bool shouldStepUp ()
    {
        if(Input.GetKey(KeyCode.W)) // check the direction the person is movin gin and shoot a ray in that direction
        {
            footRay = new Ray(footCaster.transform.position, footCaster.transform.forward);
            ankleRay = new Ray(ankleCaster.transform.position, ankleCaster.transform.forward);
        }
        /*else if (Input.GetKey(KeyCode.S))
        {
            footRay = new Ray(footCaster.transform.position, -footCaster.transform.forward);
            ankleRay = new Ray(ankleCaster.transform.position, -ankleCaster.transform.forward);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            footRay = new Ray(footCaster.transform.position, -footCaster.transform.right);
            ankleRay = new Ray(ankleCaster.transform.position, -ankleCaster.transform.right);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            footRay = new Ray(footCaster.transform.position, footCaster.transform.right);
            ankleRay = new Ray(ankleCaster.transform.position, ankleCaster.transform.right);
        }*/

        Physics.Raycast(footRay, out footHit);
        Physics.Raycast(ankleRay, out ankleHit);

        // if there is a 1-block high barrier in your way, move above it
        if (Vector3.Distance(footHit.point, footCaster.transform.position) < 0.5f && Vector3.Distance(ankleHit.point, ankleCaster.transform.position) > 1)
        {
            return true;
        }
        return false;
    }
}
