using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonControl : MonoBehaviour 
{
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
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
        startPosition = this.transform.position; // this wiill be the place the player responds to each time he dies
		Cursor.visible = true; // temporarily set to true for testing
		characterController = GetComponent <CharacterController> ();
        firstPersonCamera = this.transform.Find("Eyes").GetComponent<Camera> ();
        GetComponent<Animator>().SetBool("isMoving", false);
	}
	
	// Update is called once per frame
	void Update()
	{
		float rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		transform.Rotate (0, rotLeftRight, 0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);

        firstPersonCamera.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		//Movement
		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            verticalVelocity = jumpSpeed;
        }
        else if (characterController.isGrounded)
        {
            verticalVelocity = 0.0F;
        }

        if (!characterController.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;

		characterController.Move (speed * Time.deltaTime);

        if (speed.magnitude > 2.0F)
        {
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }

    public void Die()
    {
        this.GetComponent<Inventory>().DropAll();
        this.transform.position = startPosition;
    }
}
