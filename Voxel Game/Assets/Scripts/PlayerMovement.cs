using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 5;
	public float roatateSpeed = 180;
	public float jumpSpeed = 20;
	public float gravity = 9.8f;

	CharacterController controller;
	Vector3 cuMovement;

	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}

	void Update () 
	{
		transform.Rotate (0, Input.GetAxis ("Horizontal") * roatateSpeed * Time.deltaTime, 0);
		cuMovement = new Vector3(0, cuMovement.y, Input.GetAxis ("Vertical") * moveSpeed);
		cuMovement = transform.rotation * cuMovement;

		//cuMovement = new Vector3(Input.GetAxis ("Horizontal") * moveSpeed, 0, Input.GetAxis ("Vertical") * moveSpeed);
		//controller.Move (cuMovement * Time.deltaTime);

		if (!controller.isGrounded)
			cuMovement -= new Vector3 (0, gravity * Time.deltaTime, 0);
		else
			cuMovement.y = 0;

		if (controller.isGrounded && Input.GetButtonDown ("Jump"))
			cuMovement.y = jumpSpeed;

		controller.Move(cuMovement * Time.deltaTime);
	}
}
