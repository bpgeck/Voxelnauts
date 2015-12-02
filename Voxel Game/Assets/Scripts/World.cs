using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class World : MonoBehaviour {

	public GameObject chunk;
	public Chunk[,,] chunks;
	
	public BlockType[,,] data;
	public int worldX = 64;
	public int worldY = 64;
	public int worldZ = 64;
	public int chunkSize=32;

	public string worldName;

	// Use this for initialization
	void Start () {

		if (worldName == null)
			worldName = "world";

		if (!LoadWorld ())
			GenBaseWorld ();

		InstantiateChunks ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKeyDown (KeyCode.S)) {
			print ("saved: " + worldName);
			SaveWorld ();
		}
	}

	public BlockType Block(int x, int y, int z) {
		if (x >= worldX || x < 0 || y >= worldY || y < 0 || z >= worldZ || z < 0) {
			return BlockType.Air;
		}
		return data [x, y, z];
	}

	int PerlinNoise(int x, int y, int z, float scale, float height, float power) {
		float rValue;
		rValue = Noise.Noise.GetNoise (((double)x) / scale, ((double)y) / scale, ((double)z) / scale);
		rValue *= height;

		if (power != 0) {
			rValue = Mathf.Pow (rValue, power);
		}
		return (int) rValue;
	}

	public int GetChunkSize() {
		return chunkSize;
	}

	public void LoadWorld(string newWorldName) {
		worldName = newWorldName;
		LoadWorld ();
	}

	public bool LoadWorld() {
		if (File.Exists (Application.dataPath + "/SaveData/" + worldName + ".dat")) {
			WorldData worldData;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.dataPath + "/SaveData/" + worldName + ".dat", FileMode.Open, FileAccess.Read);
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
		FileStream file = new FileStream (Application.dataPath + "/SaveData/" + worldName + ".dat", FileMode.Create, FileAccess.Write);
		bf.Serialize (file, worldData);
		file.Close ();
	}

	public void GenBaseWorld() {
		worldX = 64;
		worldY = 64;
		worldZ = 64;
		chunkSize = 32;
		data = new BlockType[worldX, worldY, worldZ];

		for (int x = 0; x < worldX; x++) {
			for (int z = 0; z < worldZ; z++) {
				for (int y = 0; y < worldY; y++) {
					if (y == 0) {
						data[x,y,z] = BlockType.Rock;
					} else {
						data[x,y,z] = BlockType.Air;
					}
				}
			}
		}
		SaveWorldAs (worldName);
	}

	public void InstantiateChunks() {
		chunks = new Chunk[Mathf.FloorToInt (worldX / chunkSize),
		                   Mathf.FloorToInt (worldY / chunkSize),
		                   Mathf.FloorToInt (worldZ / chunkSize)];
		
		for (int x = 0; x < chunks.GetLength (0); x++) {
			for (int y = 0; y < chunks.GetLength (1); y++) {
				for (int z = 0; z < chunks.GetLength (2); z++) {
					GameObject newChunk = Instantiate (chunk, new Vector3 (x*chunkSize-0.5f,  y*chunkSize+0.5f, z*chunkSize-0.5f), new Quaternion(0,0,0,0)) as GameObject;
					chunks[x,y,z] = newChunk.GetComponent ("Chunk") as Chunk;
					chunks[x,y,z].worldGO = gameObject;
					chunks[x,y,z].chunkSize = chunkSize;
					chunks[x,y,z].chunkX = x * chunkSize;
					chunks[x,y,z].chunkY = y * chunkSize;
					chunks[x,y,z].chunkZ = z * chunkSize;
					chunks[x,y,z].update = true;
				}
			}
		}
		Destroy (chunk);
	}
}
