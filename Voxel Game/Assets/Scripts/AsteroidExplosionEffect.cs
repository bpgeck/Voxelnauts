using UnityEngine;
using System.Collections;

public class AsteroidExplosionEffect : MonoBehaviour {
    Vector3 asteroidSize;
    bool spawned = false;
    float explosionIntensity = 1000;

    ArrayList pieces = new ArrayList();

    class Cupiece // a Cube-shaped piece. Not as clever as the Astroid tail cubicles, I know
    {
        public Vector3 startPoint;
        public Vector3 parentCenter;
        public GameObject cube;
        public float timeAlive = 10;
        public float deathTime;

        public Cupiece(GameObject parent, Vector3 position, float time) // cuboid-shaped particle
        {
            startPoint = parent.transform.position + position;
            parentCenter = parent.transform.position;

            cube = GameObject.CreatePrimitive(PrimitiveType.Cube); // the actual cube itself
            cube.transform.parent = parent.transform;
            cube.AddComponent<Rigidbody>(); // make the thing have gravity
            cube.GetComponent<Rigidbody>().drag = 1; // make the thing have gravity
            cube.transform.position = startPoint; // set the position
            cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // make it 1x1x1

            deathTime = time + timeAlive; // the amount of time the piece is allowed to stay alive
        }


        public bool checkAndDestroy() // if the cube has been alive for the max amount of time, destroy it
        {
            if (Time.time >= deathTime)
            {
                Destroy(cube.gameObject);
                return true;
            }
            return false;
        }

        public void justDestroy()
        {
            Destroy(cube.gameObject);
        }

        public Vector3 getParentCenter()
        {
            return parentCenter;
        }

        public Vector3 getPosition()
        {
            return startPoint;
        }
    }

    // Use this for initialization
    void Start ()
    {
        asteroidSize = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z); // get the scale of the parent
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (spawned)
        {
            for(int i = 0; i < pieces.Count; i++)
            {
                if (((Cupiece)pieces[i]).checkAndDestroy()) // once those five seconds are up, destroy everything
                {
                    DeleteAll();
                }
            }
        }
	}

    public void SpawnCubes()
    {
        for (float i = -asteroidSize.x / 2; i < asteroidSize.x / 2; i++) // go from -1/2 the size to 1/2 the size since (0, 0, 0) is at the very center of the cube
        {
            for (float j = -asteroidSize.y / 2; j < asteroidSize.y / 2; j++)
            {
                for (float k = -asteroidSize.z / 2; k < asteroidSize.z / 2; k++)
                {
                    Vector3 localPos = new Vector3(i + 0.5f, j + 0.5f, k + 0.5f); // offset by 0.5 since the origin of a cube is the very center
                    Cupiece temp = new Cupiece(this.gameObject, localPos, Time.time);

                    pieces.Add(temp);
                }
            }
        }

        ApplyForce();
        spawned = true;
    }

    void ApplyForce()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            Vector3 forceDirection = ((Cupiece)pieces[i]).getPosition() - ((Cupiece)pieces[i]).getParentCenter(); // have the piece explode away from the center of the cube
            Vector3 explosionForce = forceDirection * explosionIntensity;
            ((Cupiece)pieces[i]).cube.GetComponent<Rigidbody>().AddForce(explosionForce);
        }
    }

    public void DeleteAll()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            ((Cupiece)pieces[i]).justDestroy(); // delete all the explosion pieces
            pieces.RemoveAt(i);
            i--;
        }

        Destroy(this.gameObject); // delete the invisible asteroid
    }
}
