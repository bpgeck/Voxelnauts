using UnityEngine;
using System.Collections;

public class AsteroidTailEffect : MonoBehaviour {
    public int numParticles = 300;
    public float distanceBehind = 10;

    ArrayList particles = new ArrayList();

    class Cuboidicle
    {
        public Vector3 startPoint;
        public Vector3 endPoint;
        public float lerpTime;
        public GameObject cube;

        private float startTime;

        public Cuboidicle(GameObject parent, float distanceBehind, float time) // cuboid-shaped particle
        {
            startPoint = new Vector3(parent.transform.position.x + (Random.Range(-parent.GetComponent<Collider>().bounds.extents.x, parent.GetComponent<Collider>().bounds.extents.x)),
                                     parent.transform.position.y + (Random.Range(-parent.GetComponent<Collider>().bounds.extents.y, parent.GetComponent<Collider>().bounds.extents.y)),
                                     parent.transform.position.z); // spawn at random point inside the cube

            Vector3 localOffset = new Vector3(0, 0, parent.GetComponent<Collider>().bounds.extents.z - distanceBehind); // convert local coordinates to world coordinates
            Vector3 worldOffset = parent.transform.rotation * localOffset; // take into account the current rotation of the object
            endPoint = parent.transform.position + worldOffset; // set the end point to be behind the object

            cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // the actual cube itself
            cube.transform.parent = parent.gameObject.transform; // make the tail a child of the asteroid itself
            Destroy(cube.GetComponent<BoxCollider>());
            cube.transform.position = startPoint; // set the position
            cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // make it small and particley

            lerpTime = time; // the time it takes for the particle to move toward the rear of the asteroid
            startTime = Time.time;
        }

        public void lerp() // move the cube
        {
            cube.transform.position = Vector3.Lerp(startPoint, endPoint, ((Time.time - startTime) / lerpTime));
        }

        public bool checkAndDestroy() // if cube has reached the end point, destroy it
        {
            if (cube.transform.position.x <= endPoint.x + 0.5f && cube.transform.position.x >= endPoint.x - 0.5f &&
                cube.transform.position.y <= endPoint.y + 0.5f && cube.transform.position.y >= endPoint.y - 0.5f &&
                cube.transform.position.z <= endPoint.z + 0.5f && cube.transform.position.z >= endPoint.z - 0.5f )
            {
                Destroy(cube.gameObject); // if particle has gone back far enough, destroy it
                return true;
            }
            return false;
        }

        public void justDestroy()
        {
            Destroy(cube.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        while (particles.Count < numParticles) // make lots of particles
        {
            Cuboidicle temp = new Cuboidicle(this.gameObject, distanceBehind, 0.1f);
            particles.Add(temp);
        }

        for (int i = 0; i < particles.Count; i++) // move all the particles
        {
            ((Cuboidicle)particles[i]).lerp();
            if (((Cuboidicle)particles[i]).checkAndDestroy())
            {
                particles.RemoveAt(i);
            }
        }
    }

    public void DeleteAll()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            ((Cuboidicle)particles[i]).justDestroy();
            particles.RemoveAt(i);
            i--;
        }
    }
}
