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
	public float damage = .02f;
	public float maxBulletSpreadAngle = 15.0f;
	public float timeTillMaxSpreadAngle = 1.0f;
	public AnimationCurve bulletSpreadCurve;
	public LayerMask layerMask = -1;

	public double heat;
	public double overheat;
	public double cooldown;
	public bool canFire;
	public bool firing;
	public float fireRate;
	public float nextFire = 0f;
	
	public float range;
	private float fireTime;

	public Vector3 fireDirection;

	private GameObject missile;
	public GameObject heatmark;

	private bool launcher;

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
		Automatic ();
	}

	void Automatic()
	{
		if (canFire)
		{
			if(Input.GetButton("Fire1"))
			{
				firing = true;
				if(Time.time > nextFire)
				{
					heat += overheat;
					nextFire = Time.time + fireRate;
					fireTime += Time.deltaTime;
					RaycastHit hit;
					
					Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0f));
					
					fireDirection = ray.direction;
					
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
						//hit.collider.SendMessage("Damage",gunHit,SendMessageOptions.DontRequireReceiver);
						Instantiate(heatmark,gunHit.raycastHit.point,Quaternion.LookRotation(gunHit.raycastHit.normal));
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
			heat -= cooldown;
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
	void Launcher()
	{
		if (canFire)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				heat += 1.1;
				GameObject launcherTip = GameObject.Find("LauncherTip");
				Instantiate(missile,launcherTip.transform.position, Quaternion.identity);
			}
		}
		
		heat -= .01;
		
		if (heat >= 1) 
		{
			canFire = false;
		} 
		else if (heat <= 0) 
		{
			heat = 0;
			canFire = true;
		}
	}
}


