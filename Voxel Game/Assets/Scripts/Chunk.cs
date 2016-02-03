using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

	public bool update;

	public int chunkX;
	public int chunkY;
	public int chunkZ;

	private int chunkSize;
	public GameObject worldGO;
	private World world;

	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();

	private float tUnit = .25f;

	private Mesh mesh;
	private MeshCollider col;

	private int faceCount;

	// Use this for initialization
	void Start () {
		world = worldGO.GetComponent ("World") as World;
		chunkSize = world.GetChunkSize();

		mesh = GetComponent<MeshFilter> ().mesh;
		col = GetComponent<MeshCollider> ();

		GenerateMesh ();
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

	BlockType Block(int x, int y, int z) {
		return world.Block (x + chunkX, y + chunkY, z + chunkZ);
	}

	void CubePY (int x, int y, int z, BlockType block) {
		newVertices.Add (new Vector3 (x, y, z + 1));
		newVertices.Add (new Vector3 (x + 1, y, z + 1));
		newVertices.Add (new Vector3 (x + 1, y, z));
		newVertices.Add (new Vector3 (x, y, z));

		Cube (new Vector2 (((int)block % (int)(.5/tUnit) * 2 + 1), (int)((int)block * tUnit)));
	}

	void CubePZ (int x, int y, int z, BlockType block) {
		newVertices.Add (new Vector3 (x + 1, y - 1, z + 1));
		newVertices.Add (new Vector3 (x + 1, y, z + 1));
		newVertices.Add (new Vector3 (x, y, z + 1));
		newVertices.Add (new Vector3 (x, y - 1, z + 1));
		
		Cube (new Vector2 (((int)block % (int)(.5/tUnit) * 2), (int)((int)block * tUnit)));
	}

	void CubePX (int x, int y, int z, BlockType block) {
		newVertices.Add (new Vector3 (x + 1, y - 1, z));
		newVertices.Add (new Vector3 (x + 1, y, z));
		newVertices.Add (new Vector3 (x + 1, y, z + 1));
		newVertices.Add (new Vector3 (x + 1, y - 1, z + 1));

		Cube (new Vector2 (((int)block % (int)(.5/tUnit) * 2), (int)((int)block * tUnit)));
	}

	void CubeNZ (int x, int y, int z, BlockType block) {
		newVertices.Add (new Vector3 (x, y - 1, z));
		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x + 1, y, z));
		newVertices.Add (new Vector3 (x + 1, y - 1, z));
		
		Cube (new Vector2 (((int)block % (int)(.5/tUnit) * 2), (int)((int)block * tUnit)));
	}

	void CubeNX (int x, int y, int z, BlockType block) {
		newVertices.Add (new Vector3 (x, y - 1, z + 1));
		newVertices.Add (new Vector3 (x, y, z + 1));
		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x, y - 1, z));
		
		Cube (new Vector2 (((int)block % (int)(.5/tUnit) * 2), (int)((int)block * tUnit)));
	}

	void CubeNY (int x, int y, int z, BlockType block) {
		newVertices.Add (new Vector3 (x, y - 1, z));
		newVertices.Add (new Vector3 (x + 1, y - 1, z));
		newVertices.Add (new Vector3 (x + 1, y - 1, z + 1));
		newVertices.Add (new Vector3 (x, y - 1, z + 1));
		
		Cube (new Vector2 (((int)block % (int)(.5/tUnit) * 2), (int)((int)block * tUnit)));
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
					if (Block (x, y, z) != BlockType.Air) {
						if (Block (x, y+1, z) == BlockType.Air) {
							CubePY (x, y, z, Block (x, y, z));
						}
						if (Block (x, y-1, z) == BlockType.Air) {
							CubeNY (x, y, z, Block (x, y, z));
						}
						if (Block (x+1, y, z) == BlockType.Air) {
							CubePX (x, y, z, Block (x, y, z));
						}
						if (Block (x-1, y, z) == BlockType.Air) {
							CubeNX (x, y, z, Block (x, y, z));
						}
						if (Block (x, y, z+1) == BlockType.Air) {
							CubePZ (x, y, z, Block (x, y, z));
						}
						if (Block (x, y, z-1) == BlockType.Air) {
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
