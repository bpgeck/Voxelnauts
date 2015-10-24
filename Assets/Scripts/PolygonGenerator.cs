using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonGenerator : MonoBehaviour {

	public List<Vector3> newVertices = new List<Vector3>();
	public List<int> newTriangles = new List<int>();
	public List<Vector2> newUV = new List<Vector2>();
	public byte[,] blocks;

	public List<Vector3> colVertices = new List<Vector3> ();
	public List<int> colTriangles = new List<int> ();
	
	private Mesh mesh;
	private MeshCollider col;

	private int squareCount;
	private int colCount;

	private float tUnit = .25f;
	private Vector2 tStone = new Vector2 (1, 0);
	private Vector2 tGrass = new Vector2 (0, 1);



	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		col = GetComponent<MeshCollider> ();

		GenTerrain ();
		BuildMesh ();
		UpdateMesh ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateMesh () {
		mesh.Clear();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.Optimize();
		mesh.RecalculateNormals();

		Mesh newMesh = new Mesh ();
		newMesh.vertices = colVertices.ToArray ();
		newMesh.triangles = colTriangles.ToArray ();
		col.sharedMesh = newMesh;

		colCount = 0;
		colVertices.Clear ();
		colTriangles.Clear ();

		squareCount = 0;
		newVertices.Clear ();
		newTriangles.Clear ();
		newUV.Clear ();

	}

	void GenSquare (int x, int y, Vector2 texture) {
		newVertices.Add(new Vector3(x, y, 0));
		newVertices.Add(new Vector3(x+1, y, 0));
		newVertices.Add(new Vector3(x+1, y-1, 0));
		newVertices.Add(new Vector3(x, y-1, 0));
		
		newTriangles.Add(squareCount*4);
		newTriangles.Add(squareCount*4 + 1);
		newTriangles.Add(squareCount*4 + 3);
		newTriangles.Add(squareCount*4 + 1);
		newTriangles.Add(squareCount*4 + 2);
		newTriangles.Add(squareCount*4 + 3);
		
		newUV.Add (tUnit * (new Vector2(texture.x, texture.y+1)));
		newUV.Add (tUnit * (new Vector2(texture.x+1, texture.y+1)));
		newUV.Add (tUnit * (new Vector2(texture.x+1, texture.y)));
		newUV.Add (tUnit * (new Vector2(texture.x, texture.y)));

		squareCount++;
	}

	void GenCollider(int x, int y) {
		//Top
		colVertices.Add (new Vector3 (x, y, 1));
		colVertices.Add (new Vector3 (x+1, y, 1));
		colVertices.Add (new Vector3 (x+1, y, 0));
		colVertices.Add (new Vector3 (x, y, 0));

		colTriangles.Add (colCount * 4);
		colTriangles.Add (colCount * 4 + 1);
		colTriangles.Add (colCount * 4 + 3);
		colTriangles.Add (colCount * 4 + 1);
		colTriangles.Add (colCount * 4 + 2);
		colTriangles.Add (colCount * 4 + 3);

		colCount++;
	}

	void GenTerrain () {
		blocks = new byte[10, 10];

		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				if (py == 5) {
					blocks [px, py] = 2;
				} else if (py < 5) {
					blocks [px, py] = 1;
				}
			}
		}
	}

	void BuildMesh(){
		for(int px = 0; px < blocks.GetLength(0); px++){
			for(int py = 0 ;py < blocks.GetLength(1); py++){
				if (blocks[px,py] == 1){
					GenSquare (px, py, tStone);
				} else if (blocks[px,py] == 2){
					GenSquare (px, py, tGrass);
				}
			}
		}
	}
}
