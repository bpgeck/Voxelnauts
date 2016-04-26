using UnityEngine;
using System.Collections;

public class SpawnAsteroid : MonoBehaviour {
    public GameObject asteroid;
    public float timeBetweenAsteroids = 5;
    float timeSinceLastAsteroid = 0;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 startPoint = new Vector3(this.transform.position.x + Random.Range(-256, 256), this.transform.position.y, this.transform.position.z + Random.Range(-256, 256)); // get some random point in the asteroid field

        timeSinceLastAsteroid += Time.deltaTime; // on every frame, increase the amount of time since the last asteroid spawned

	    if (timeSinceLastAsteroid >= timeBetweenAsteroids) // when enough time has elapsed, spawn new asteroid
        {
            timeSinceLastAsteroid = 0;
            GameObject spawnedAsteroid = (GameObject)Instantiate(asteroid, startPoint, new Quaternion(0, 0, 0, 0)); // spawn an asteroid at the designated start point with no rotation
            spawnedAsteroid.GetComponent<AsteroidShit>().endPoint = new Vector3(Random.Range(0, 512), 0, Random.Range(0, 512)); // this is the point that the asteroid will move to
        }
	}
}
