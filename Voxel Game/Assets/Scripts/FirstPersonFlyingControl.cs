﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonFlyingControl : MonoBehaviour 
{
	public float movementSpeed = 15.0f;
	public float mouseSensitivity = 20.0f;
	public float jumpSpeed = 20.0f;
	public float upDownRange = 60.0f;
	float verticalRotation = 0;
	float verticalVelocity = 0;
	Vector3 startPosition;
	
	Camera firstPersonCamera;
	CharacterController characterController;
	// Use this for initialization
	void Start()
	{
		startPosition = this.transform.position;
		characterController = GetComponent <CharacterController> ();
		firstPersonCamera = this.transform.Find("Eyes").GetComponent<Camera> ();
		GetComponent<Animator>().SetBool("isMoving", false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.LeftAlt)) {
			if (Cursor.lockState == CursorLockMode.Locked) {
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
		// Rotation stuff
		float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotLeftRight, 0);
		
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity/2;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);
		
		firstPersonCamera.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);
		
		// Movement stuff
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;
		
		verticalVelocity = Input.GetAxis ("Fly") * jumpSpeed;

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);
		
		speed = transform.rotation * speed;
		
		characterController.Move (speed * Time.deltaTime);
		
		// Adjust animation depending on speed of player
		if (speed.magnitude > 2.0F)
			GetComponent<Animator>().SetBool("isMoving", true);
		else
			GetComponent<Animator>().SetBool("isMoving", false);
	}
	
	// On death, drop all items
	// Flag drops in a special way
	public void Die()
	{
		this.GetComponent<Inventory>().DropAll();
		this.transform.position = startPosition;
	}
}
