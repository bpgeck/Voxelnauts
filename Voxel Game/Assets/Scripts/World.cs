using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class World : MonoBehaviour {
	
	public Chunk[,,] chunks;
	
	public BlockType[,,] data;
	private int worldX;
	private int worldY;
	private int worldZ;
	private int chunkSize=64;

	public string worldName;

	// Use this for initialization
	void Start () {
		if (worldName == null)
			worldName = "world";

		if (!GenWorldWithTerrain ())
			GenWorld ();

		PopulateChunks ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int GetChunkSize() {
		return chunkSize;
	}

	public BlockType Block(int x, int y, int z) {
		if (x >= worldX)
			return data [worldX-1, y, z];
		if (x < 0)
			return data [0, y, z];
		if (y >= worldY)
			return data [x, worldY-1, z];
		if (y < 0)
			return data [x, 0, z];
		if (z >= worldZ)
			return data [x, y, worldZ-1];
		if (z < 0)
			return data [x, y, 0];

		return data [x, y, z];
	}
	
	public void LoadWorld(string newWorldName) {
		worldName = newWorldName;
		LoadWorld ();
	}

	public bool LoadWorld() {
		if (File.Exists (Application.dataPath + "/Resources/" + worldName + "/" + worldName + ".dat")) {
			WorldData worldData;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.dataPath + "/Resources/" + worldName + "/" + worldName + ".dat", FileMode.Open, FileAccess.Read);
			worldData = (WorldData)bf.Deserialize (file);
			file.Close ();
			Debug.Log ("Loaded World: " + worldName);
			worldX = worldData.worldX;
			worldY = worldData.worldY;
			worldZ = worldData.worldZ;
			chunkSize = worldData.chunkSize;
			data = worldData.data;
			return true;
		} else {
			Debug.Log ("World \"" + worldName + "\" does not exist.");
			return false;
		}
	}
	
	public void SaveWorldAs(string newWorldName) {
		worldName = newWorldName;
		SaveWorld ();
	}

	public void SaveWorld() {
		WorldData worldData = new WorldData (worldX, worldY, worldZ, chunkSize, data);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = new FileStream (Application.dataPath + "/Resources/" + worldName + "/" + worldName + ".dat", FileMode.Create, FileAccess.Write);
		bf.Serialize (file, worldData);
		file.Close ();
	}

	public void GenWorld() {
		worldX = 256;
		worldY = 64;
		worldZ = 256;
		data = new BlockType[worldX, worldY, worldZ];

		for (int x = 0; x < worldX; x++) {
			for (int z = 0; z < worldZ; z++) {
				for (int y = 0; y < worldY; y++) {
					if (y == 0) {
						data[x,y,z] = BlockType.Stone_Black;
					} else {
						data[x,y,z] = BlockType.Air;
					}
				}
			}
		}
	}

	public bool GenWorldWithTerrain() {
		if (File.Exists (Application.dataPath + "/Resources/" + worldName + "/" + worldName + "_terrain.asset")) {
			TerrainData terrain = Resources.Load (worldName + "/" + worldName + "_terrain") as TerrainData;
			worldX = (int)terrain.size.x;
			//worldY = (int)terrain.size.y;
			worldY = 2*chunkSize;
			worldZ = (int)terrain.size.z;
			data = new BlockType[worldX, worldY, worldZ];
			
			for (int x = 0; x < worldX; x++) {
				for (int z = 0; z < worldZ; z++) {
					
					int h = (int)terrain.GetHeight (x, z);
					data [x, 0, z] = BlockType.Stone_Black;
					
					for (int y = 1; y < worldY; y++) {
						if (y <= h)
							data [x, y, z] = BlockType.Stone;
						else
							data [x, y, z] = BlockType.Air;
					}
				}
			}
			return true;
		} else {
			print ("Could not find Terrain object with name: \"" + worldName + "_terrain.asset\".");
			return false;
		}

	}

	public void PopulateChunks() {
		GameObject[] chunkArr = GameObject.FindGameObjectsWithTag ("Chunk");
		chunks = new Chunk[Mathf.FloorToInt (worldX / chunkSize),
		                   Mathf.FloorToInt (worldY / chunkSize),
		                   Mathf.FloorToInt (worldZ / chunkSize)];

		print ("Found " + chunkArr.GetLength(0) + " chunks.");
		print ("Placing " + chunks.GetLength(0) + "x" + chunks.GetLength(1) + "x" + chunks.GetLength(2) + " grid of chunks");

		foreach (GameObject chunkGO in chunkArr) {
			Chunk chunk = chunkGO.GetComponent<Chunk>();
			int x = chunk.chunkX/chunkSize;
			int y = chunk.chunkY/chunkSize;
			int z = chunk.chunkZ/chunkSize;
			chunkGO.transform.position = new Vector3 (x*chunkSize-0.5f, y*chunkSize+0.5f, z*chunkSize-0.5f );
			chunks[x,y,z] = chunk;
			chunk.GetComponent<Chunk>().enabled = true;
			print ("Placed \"" + chunkGO.name + "\" in chunks[" + x + "," + y + "," + z + "].");
		}
	}
}
