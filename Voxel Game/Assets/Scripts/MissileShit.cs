using UnityEngine;
using System.Collections;

public class MissileShit : MonoBehaviour 
{
	public float timeToImpact = 5; // time taken to move from spawn point to map
	float startTime = 0;
	Vector3 startPoint = new Vector3(0, 0, 0);
	Vector3 endPoint = new Vector3(0, 0, 0);
	int cubeSize = 0;
	Camera camera;
	
	void Start()
	{
		startTime = Time.time;
		startPoint = this.transform.position;
		//endPoint = new Vector3(Random.Range(0, 512), 0, Random.Range(0, 512)); // this is the point that the asteroid will move to

		GameObject eyes = GameObject.Find ("Eyes");
		camera = eyes.GetComponent<Camera> ();
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0f));
		endPoint = ray.GetPoint(250f);

		cubeSize = Random.Range(3, 6);
		this.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
		
		transform.rotation = Quaternion.LookRotation(endPoint - startPoint); // set the block to rotate so that the axis it rotates around is the movement direction
	}
	
	void Update ()
	{
		this.transform.position = Vector3.Lerp(startPoint, endPoint, ((Time.time - startTime) / timeToImpact)); // linearly interpolates from startPosition to endPosition
		this.transform.RotateAround(this.transform.position, endPoint - startPoint, 20); // sping around the axis going from the start point to the end point
		
		if (Time.time > startTime + timeToImpact + 3) // failsafe in case glitchy shit happens
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		this.GetComponent<AsteroidTailEffect>().DeleteAll();
		Destroy(this.GetComponent<AsteroidTailEffect>());
		
		this.GetComponent<MeshRenderer>().enabled = false;
		this.GetComponent<BoxCollider>().enabled = false;
		
		this.GetComponent<AsteroidExplosionEffect>().SpawnCubes();
		Destroy(this);
	}
}
