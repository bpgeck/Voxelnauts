using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class OldAstroFirstPersonControl : MonoBehaviour
{
    GameObject eyes;
    GameObject body;
    public float movementSpeed = 5.0f;
    public float mouseSensitivity;
    public float jumpSpeed = 20.0f;
    public float upDownRange = 60.0f;
    private float rotLeftRight;
    float verticalRotation = 0;
    float verticalVelocity = 0;
    float timeDead = 0;
    float timeStayDead = 5;
    public float spinSpeed = 0;
    Vector3 startPosition;
    Vector3 eyesStartPos;
    Vector3 bodyStartPos;
    Quaternion startRot;
    Quaternion eyesStartRot;
    Quaternion bodyStartRot;
    public Vector3 deathRotationAxis;
	public Vector3 deathPosition;
    bool alive = true;

    private GameObject manager;


    Camera firstPersonCamera;
    Animator geckAnimator;
    CharacterController characterController;
    // Use this for initialization
    void Start()
    {
        Cursor.visible = true; // temporarily set to true for testing

        eyes = this.transform.Find("Eyes").gameObject;
        body = this.transform.Find("geckstronautAnimatedWithGun").gameObject;

        startPosition = this.transform.position; // this will be the place the player responds to each time he dies
        eyesStartPos = eyes.transform.position;
        bodyStartPos = body.transform.position;

        startRot = this.transform.rotation;
        eyesStartRot = eyes.transform.rotation;
        bodyStartRot = body.transform.rotation;

        characterController = GetComponent<CharacterController>();

        firstPersonCamera = this.transform.Find("Eyes").GetComponent<Camera>();

        geckAnimator = this.transform.Find("geckstronautAnimatedWithGun").GetComponent<Animator>();
        geckAnimator.SetBool("HasGun", false);
        geckAnimator.SetBool("Idle", false);
        geckAnimator.SetBool("Walking", false);
        geckAnimator.SetBool("Running", false);
        geckAnimator.SetBool("Shooting", false);

        manager = GameObject.FindGameObjectWithTag("GameController");
		//mouseSensitivity = PlayerPrefs.GetFloat ("Mouse Sensitivity");
		//manager.GetComponent<GameManagerScript>().mouseSensitivity; // get mouse sensitivity from GameController object
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            // Rotation stuff
            if (manager.GetComponent<GameManagerScript>().rawMouse == false)
            {
                rotLeftRight = Input.GetAxis("Mouse X") * (mouseSensitivity * 1.5f);
            }
            else if (manager.GetComponent<GameManagerScript>().rawMouse == true)
            {
				rotLeftRight = Input.GetAxisRaw("Mouse X") * (mouseSensitivity * 1.5f);
            }
            transform.Rotate(0, rotLeftRight, 0);

            if (manager.GetComponent<GameManagerScript>().rawMouse == false)
            {
				verticalRotation -= Input.GetAxis("Mouse Y") * (mouseSensitivity * 1.5f);
            }
            if (manager.GetComponent<GameManagerScript>().rawMouse == true)
            {
				verticalRotation -= Input.GetAxisRaw("Mouse Y") * (mouseSensitivity * 1.5f);
            }
            verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

            firstPersonCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            // Movement stuff
            float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
            float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

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
        else
        {
            this.transform.Find("geckstronautAnimatedWithGun").RotateAround(this.GetComponent<CharacterController>().bounds.center, deathRotationAxis, spinSpeed);
            this.transform.Find("Eyes").RotateAround(this.GetComponent<CharacterController>().bounds.center, deathRotationAxis, spinSpeed);
            timeDead += Time.deltaTime;
            spinSpeed -= spinSpeed * Time.deltaTime * timeStayDead;
            if (timeDead >= timeStayDead) // stay dead for the proper amount of time
            {
                alive = true;
                timeDead = 0;
                Respawn();
            }
        }
    }

    // On death, drop all items
    // Flag drops in a special way

	public void Die()
	{
		deathPosition =  new Vector3(this.transform.position.x, this.transform.position.y + 1.95f, this.transform.position.z);
		geckAnimator.SetBool("Idle", true);
		geckAnimator.SetBool("Walking", false);
		geckAnimator.SetBool("Running", false);
		StartCoroutine("wait");
		this.GetComponentInChildren<RaycastGun> ().enabled = false;
		this.GetComponentInChildren<RaycastGun> ().heat = 0;
		this.GetComponentInChildren<PlayerHealth> ().health = 1;
		alive = false;
	}
	
	IEnumerator wait()
	{
		yield return new WaitForSeconds (.01f);
		this.GetComponent<Inventory>().DropAll();
	}

    public void Respawn()
    {
        spinSpeed = 0;

		this.GetComponentInChildren<RaycastGun> ().enabled = true;

        this.transform.rotation = startRot;
        eyes.transform.rotation = eyesStartRot;
        body.transform.rotation = bodyStartRot;

        this.transform.position = startPosition;
        eyes.transform.position = eyesStartPos;
        body.transform.position = bodyStartPos;
    }
}
