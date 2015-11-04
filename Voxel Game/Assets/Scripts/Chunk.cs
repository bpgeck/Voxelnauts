using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

	public bool update;

	public int chunkX;
	public int chunkY;
	public int chunkZ;

	public int chunkSize;
	public GameObject worldGO;
	private World world;

	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();

	private float tUnit = .25f;
	private Vector2 tStone = new Vector2 (1, 0);
	private Vector2 tGrass = new Vector2 (0, 1);
	private Vector2 tGrassTop = new Vector2 (1, 1);

	private Mesh mesh;
	private MeshCollider col;

	private int faceCount;

	// Use this for initialization
	void Start () {
		world = worldGO.GetComponent ("World") as World;
		chunkSize = world.GetChunkSize ();

		mesh = GetComponent<MeshFilter> ().mesh;
		col = GetComponent<MeshCollider> ();

		GenerateMesh();
	}
	
	// Update is called once per frame
	void Update() {

	}

	void LateUpdate () {
		if (update) {
			GenerateMesh ();
			update = false;
		}
	}

	byte Block(int x, int y, int z) {
		return world.Block (x + chunkX, y + chunkY, z + chunkZ);
	}

	void CubePY (int x, int y, int z, byte block) {
		newVertices.Add (new Vector3 (x, y, z + 1));
		newVertices.Add (new Vector3 (x + 1, y, z + 1));
		newVertices.Add (new Vector3 (x + 1, y, z));
		newVertices.Add (new Vector3 (x, y, z));
		
		Vector2 texturePos = new Vector2 (0, 0);
		if (Block (x, y, z) == 1) {
			texturePos = tStone;
		} else if (Block (x, y, z) == 2) {
			texturePos = tGrassTop;
		}


		Cube (texturePos);
	}

	void CubePZ (int x, int y, int z, byte block) {
		newVertices.Add (new Vector3 (x + 1, y - 1, z + 1));
		newVertices.Add (new Vector3 (x + 1, y, z + 1));
		newVertices.Add (new Vector3 (x, y, z + 1));
		newVertices.Add (new Vector3 (x, y - 1, z + 1));
		
		Vector2 texturePos = new Vector2 (0, 0);
		if (Block (x, y, z) == 1) {
			texturePos = tStone;
		} else if (Block (x, y, z) == 2) {
			texturePos = tGrass;
		}

		Cube (texturePos);
	}

	void CubePX (int x, int y, int z, byte block) {
		newVertices.Add (new Vector3 (x + 1, y - 1, z));
		newVertices.Add (new Vector3 (x + 1, y, z));
		newVertices.Add (new Vector3 (x + 1, y, z + 1));
		newVertices.Add (new Vector3 (x + 1, y - 1, z + 1));
		
		Vector2 texturePos = new Vector2 (0, 0);
		if (Block (x, y, z) == 1) {
			texturePos = tStone;
		} else if (Block (x, y, z) == 2) {
			texturePos = tGrass;
		}
		
		Cube (texturePos);
	}

	void CubeNZ (int x, int y, int z, byte block) {
		newVertices.Add (new Vector3 (x, y - 1, z));
		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x + 1, y, z));
		newVertices.Add (new Vector3 (x + 1, y - 1, z));
		
		Vector2 texturePos = new Vector2 (0, 0);
		if (Block (x, y, z) == 1) {
			texturePos = tStone;
		} else if (Block (x, y, z) == 2) {
			texturePos = tGrass;
		}
		
		Cube (texturePos);
	}

	void CubeNX (int x, int y, int z, byte block) {
		newVertices.Add (new Vector3 (x, y - 1, z + 1));
		newVertices.Add (new Vector3 (x, y, z + 1));
		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x, y - 1, z));
		
		Vector2 texturePos = new Vector2 (0, 0);
		if (Block (x, y, z) == 1) {
			texturePos = tStone;
		} else if (Block (x, y, z) == 2) {
			texturePos = tGrass;
		}
		
		Cube (texturePos);
	}

	void CubeNY (int x, int y, int z, byte block) {
		newVertices.Add (new Vector3 (x, y - 1, z));
		newVertices.Add (new Vector3 (x + 1, y - 1, z));
		newVertices.Add (new Vector3 (x + 1, y - 1, z + 1));
		newVertices.Add (new Vector3 (x, y - 1, z + 1));
		
		Vector2 texturePos = new Vector2 (0, 0);
		if (Block (x, y, z) == 1) {
			texturePos = tStone;
		} else if (Block (x, y, z) == 2) {
			texturePos = tGrass;
		}
		
		Cube (texturePos);
	}

	void Cube (Vector2 texturePos) {
		newTriangles.Add (faceCount * 4);
		newTriangles.Add (faceCount * 4 + 1);
		newTriangles.Add (faceCount * 4 + 2);
		newTriangles.Add (faceCount * 4);
		newTriangles.Add (faceCount * 4 + 2);
		newTriangles.Add (faceCount * 4 + 3);

		newUV.Add (tUnit * (new Vector2 (texturePos.x + 1, texturePos.y)));
		newUV.Add (tUnit * (new Vector2 (texturePos.x + 1, texturePos.y + 1)));
		newUV.Add (tUnit * (new Vector2 (texturePos.x, texturePos.y + 1)));
		newUV.Add (tUnit * (new Vector2 (texturePos.x, texturePos.y)));

		faceCount++;
	}

	public void GenerateMesh() {
		for (int x = 0; x < chunkSize; x++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int z = 0; z < chunkSize; z++) {
					if (Block (x, y, z) != 0) {
						if (Block (x, y+1, z) == 0) {
							CubePY (x, y, z, Block (x, y, z));
						}
						if (Block (x, y-1, z) == 0) {
							CubeNY (x, y, z, Block (x, y, z));
						}
						if (Block (x+1, y, z) == 0) {
							CubePX (x, y, z, Block (x, y, z));
						}
						if (Block (x-1, y, z) == 0) {
							CubeNX (x, y, z, Block (x, y, z));
						}
						if (Block (x, y, z+1) == 0) {
							CubePZ (x, y, z, Block (x, y, z));
						}
						if (Block (x, y, z-1) == 0) {
							CubeNZ (x, y, z, Block (x, y, z));
						}
					}
				}
			}
		}
		UpdateMesh ();
	}

	void UpdateMesh () {
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray ();
		mesh.uv = newUV.ToArray ();
		mesh.triangles = newTriangles.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();

		col.sharedMesh = null;
		col.sharedMesh = mesh;

		newVertices.Clear ();
		newUV.Clear ();
		newTriangles.Clear ();

		faceCount = 0;
	}
}
