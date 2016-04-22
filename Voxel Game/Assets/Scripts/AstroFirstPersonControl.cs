using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class AstroFirstPersonControl : MonoBehaviour 
{
	public float movementSpeed = 5.0f;
	private float mouseSensitivity;
	public float jumpSpeed = 20.0f;
	public float upDownRange = 60.0f;
	private float rotLeftRight;
	float verticalRotation = 0;
	float verticalVelocity = 0;
    Vector3 startPosition;
	public Vector3 deathPosition;

	private GameObject manager;

    Camera firstPersonCamera;
    Animator geckAnimator;
	CharacterController characterController;
	// Use this for initialization
	void Start()
    {
        startPosition = this.transform.position; // this wiill be the place the player responds to each time he dies
		Cursor.visible = true; // temporarily set to true for testing
		characterController = GetComponent <CharacterController> ();
        firstPersonCamera = this.transform.Find("Eyes").GetComponent<Camera> ();
        geckAnimator = this.transform.Find("geckstronautAnimatedWithGun").GetComponent<Animator> ();
        geckAnimator.SetBool("HasGun", false);
        geckAnimator.SetBool("Idle", false);
        geckAnimator.SetBool("Walking", false);
        geckAnimator.SetBool("Running", false);
        geckAnimator.SetBool("Shooting", false);
		manager = GameObject.FindGameObjectWithTag ("GameController");
		mouseSensitivity = manager.GetComponent<GameManagerScript> ().mouseSensitivity;
    }
	
	// Update is called once per frame
	void Update()
    {
        // Rotation stuff
		if (manager.GetComponent<GameManagerScript> ().rawMouse == false) {
			rotLeftRight = Input.GetAxis ("Mouse X") * mouseSensitivity;
		} 
		else if (manager.GetComponent<GameManagerScript> ().rawMouse == true) {
			rotLeftRight = Input.GetAxisRaw ("Mouse X") * mouseSensitivity;
		}
		transform.Rotate (0, rotLeftRight, 0);
		
		if (manager.GetComponent<GameManagerScript>().rawMouse == false){
			verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		}
		if (manager.GetComponent<GameManagerScript>().rawMouse == true){
				verticalRotation -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;	
		}
		verticalRotation = Mathf.Clamp (verticalRotation, -upDownRange, upDownRange);

        firstPersonCamera.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		// Movement stuff
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

        // If the player is in the air then accelerate the player toward the ground
        if (!characterController.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // Check if player is holding a gun
        if (this.transform.Find("geckstronautAnimatedWithGun").transform.Find("SpaceAR:SpaceAR:Mesh") != null)
        {
            geckAnimator.SetBool("HasGun", true);
            this.transform.Find("geckstronautAnimatedWithGun").transform.Find("SpaceAR:SpaceAR:Mesh").gameObject.SetActive(true);
        }
        else
        {
            geckAnimator.SetBool("HasGun", false);
            this.transform.Find("geckstronautAnimatedWithGun").transform.Find("SpaceAR:SpaceAR:Mesh").gameObject.SetActive(false);
        }

        // If player is pressing left CTRL, then walk
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (forwardSpeed > 5.0F)
            {
                forwardSpeed = 4.9F;
            }
        }

        // If player is shooting, set shooting animation
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            geckAnimator.SetBool("Shooting", true);
            if (forwardSpeed > 5.0F)
            {
                geckAnimator.SetBool("Running", false);
                geckAnimator.SetBool("Walking", true);
                forwardSpeed = 4.9F;
            }
        }
        else
        {
            geckAnimator.SetBool("Shooting", false);
        }

        // set the speed
        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        // Adjust animation depending on speed of player
        if (speed.magnitude > 0.5F)
        {
            if (speed.magnitude > 5.0F)
            {
                geckAnimator.SetBool("Idle", false);
                geckAnimator.SetBool("Walking", false);
                geckAnimator.SetBool("Running", true);
            }
            else
            {
                geckAnimator.SetBool("Idle", false);
                geckAnimator.SetBool("Walking", true);
                geckAnimator.SetBool("Running", false);
            }
        }
        else
        {
            geckAnimator.SetBool("Idle", true);
            geckAnimator.SetBool("Walking", false);
            geckAnimator.SetBool("Running", false);
        }

        // move that bitch
        characterController.Move(speed * Time.deltaTime);
    }

    // On death, drop all items
    // Flag drops in a special way
	public void Die()
	{
		deathPosition =  new Vector3(this.transform.position.x, this.transform.position.y + 1.95f, this.transform.position.z);
		StartCoroutine("wait");
		this.GetComponentInChildren<RaycastGun> ().heat = 0;
		this.transform.position = startPosition;
	}
	
	IEnumerator wait()
	{
		yield return new WaitForSeconds (.01f);
		this.GetComponent<Inventory>().DropAll();
	}
}
