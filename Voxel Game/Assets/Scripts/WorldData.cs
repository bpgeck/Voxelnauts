using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public class WorldData {
	public int worldX;
	public int worldY;
	public int worldZ;
	public int chunkSize;
	public BlockType[,,] data;

	public WorldData(int x, int y, int z, int s, BlockType[,,] d) {
		worldX = x;
		worldY = y;
		worldZ = z;
		chunkSize = s;
		data = d;
	}

	public WorldData() {
	}

}
