using UnityEngine;
using System.Collections;

public class AsteroidShit : MonoBehaviour
{
    public float timeToImpact = 5; // time taken to move from spawn point to map
    public bool hitPlayer = false;

    float startTime = 0;
    Vector3 startPoint = new Vector3(0, 0, 0);
    Vector3 endPoint = new Vector3(0, 0, 0);
    Vector3 deathRotationAxis;
    int cubeSize = 0;

    void Start()
    {
        startTime = Time.time;
        startPoint = this.transform.position;
        if (hitPlayer)
        {
            endPoint = GameObject.Find("GeckstroNOT").transform.position;
            endPoint.y += 5;
        }
        else
        {
            endPoint = new Vector3(Random.Range(0, 512), 0, Random.Range(0, 512)); // this is the point that the asteroid will move to
        }

        cubeSize = Random.Range(3, 6);
        this.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        transform.rotation = Quaternion.LookRotation(endPoint - startPoint); // set the block to rotate so that the axis it rotates around is the movement direction
    }

	void Update ()
    {
        this.transform.position = Vector3.Lerp(startPoint, endPoint, ((Time.time - startTime) / timeToImpact)); // linearly interpolates from startPosition to endPosition
        this.transform.RotateAround(this.transform.position, endPoint - startPoint, 20); // sping around the axis going from the start point to the end point

        if (Time.time > startTime + timeToImpact) // failsafe in case glitchy shit happens
        {
            this.GetComponent<AsteroidExplosionEffect>().SpawnCubes();
            this.GetComponent<AsteroidTailEffect>().DeleteAll();
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        this.GetComponent<AsteroidTailEffect>().DeleteAll(); // when the asteroid crashes, delete the tail
        Destroy(this.GetComponent<AsteroidTailEffect>()); // stop generating cubicles
        
        this.GetComponent<MeshRenderer>().enabled = false; // make the block untouchable
        this.GetComponent<BoxCollider>().enabled = false; // make the block invisible

        /* the block must be invisible because if it is destroyed, the explosion effect will not work */

        this.GetComponent<AsteroidExplosionEffect>().SpawnCubes();

        if (col.gameObject.name.Contains("Geck")) // just hit the shit out of the player if collide with player
        {
            Vector3 direction = endPoint - startPoint;
            Vector3 hitDir = direction;
            hitDir.y = 0;
            col.gameObject.GetComponent<ForceOnController>().AddImpact(hitDir.normalized, 1000);

            deathRotationAxis = new Vector3(1, 0, (-direction.x / direction.z));
            deathRotationAxis.Normalize();

            col.gameObject.GetComponent<AstroFirstPersonControl>().spinSpeed = 20;
            col.gameObject.GetComponent<AstroFirstPersonControl>().deathRotationAxis = deathRotationAxis;
            col.gameObject.GetComponent<AstroFirstPersonControl>().Die();
        }
        Destroy(this); // after explosion destroy this script
    }
}
