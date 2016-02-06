using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

/*
 * This object stores voxel level data
 * It is meant to be used to store a level in an external file
 */

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
