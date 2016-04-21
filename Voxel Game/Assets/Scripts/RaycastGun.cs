using UnityEngine;
using System.Collections;

public class GunHit
{
	public float damage;
	public RaycastHit raycastHit;
}

public class RaycastGun : MonoBehaviour
{
	public float fireDelay = 0.1f;
	public float damage = 1.0f;
	public string buttonName = "Fire1";
	public float maxBulletSpreadAngle = 15.0f;
	public float timeTillMaxSpreadAngle = 1.0f;
	public AnimationCurve bulletSpreadCurve;
	public LayerMask layerMask = -1;
	public double heat;
	public bool canFire;
	public bool firing;
	public float fireRate;
	public float nextFire = 0f;

	private bool readyToFire = true;
	public float range;
	private float fireTime;

	Camera camera;
	
	// Use this for initialization
	void Start ()
	{
		GameObject eyes = GameObject.Find ("Eyes");
		camera = eyes.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		 //GetButtonDown for semi-auto, GetButton for automatic fire
		if (canFire)
		{
			if(Input.GetButton(buttonName))
			{
				firing = true;
				if(Time.time > nextFire)
				{
					heat += .02;
					nextFire = Time.time + fireRate;
					fireTime += Time.deltaTime;
					RaycastHit hit;

					Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0f));

					Vector3 fireDirection = ray.direction;

					Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
			
					Quaternion randomRotation = Random.rotation;
			
					float currentSpread = bulletSpreadCurve.Evaluate(fireTime/timeTillMaxSpreadAngle)*maxBulletSpreadAngle;
				
					//float currentSpread = Mathf.Lerp(0.0f, maxBulletSpreadAngle, fireTime/timeTillMaxSpreadAngle);
				
					fireRotation = Quaternion.RotateTowards(fireRotation,randomRotation,Random.Range(0.0f,currentSpread));
			
					if(Physics.Raycast(transform.position,fireRotation*Vector3.forward,out hit,Mathf.Infinity,layerMask))
					{
						GunHit gunHit = new GunHit();
						gunHit.damage = damage;
						gunHit.raycastHit = hit;
						hit.collider.SendMessage("Damage",gunHit,SendMessageOptions.DontRequireReceiver);
						readyToFire = false;
						Invoke("SetReadyToFire", fireDelay);
					}
					Debug.DrawRay(this.transform.position, fireRotation*Vector3.forward*range, Color.red, 0f, false);
				}
			}
			else
			{
				firing = false;
			}
		}

		if(!firing)
		{
			fireTime = 0.0f;
			heat -= .01;
		}

		if (heat >= 1) 
		{
			canFire = false;
			firing = false;
		} 
		else if (heat <= .2 && heat > 0) 
		{
			canFire = true;
		} 
		else if (heat <= 0) 
		{
			heat = 0;
			canFire = true;
		}
	}

	
	void SetReadyToFire()
	{
		readyToFire = true;
	}
}


