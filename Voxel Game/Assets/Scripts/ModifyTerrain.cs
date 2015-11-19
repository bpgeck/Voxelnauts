using UnityEngine;
using System.Collections;
using System;

public class ModifyTerrain : MonoBehaviour {

	World world;

	void Start() {
		world = gameObject.GetComponent ("World") as World;
	}

	void Update() {
		if (Input.GetMouseButtonDown (0))
			ReplaceBlockCursor (BlockType.Air);
		if (Input.GetMouseButtonDown (1))
			AddBlockCursor (BlockType.Rock); //TODO Change to get selected block
	}

	public void ReplaceBlockCursor(BlockType block) {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			ReplaceBlockAt (hit, block);
			Debug.DrawLine (ray.origin, ray.origin + (ray.direction*hit.distance), Color.green, 2);
		}
	}

	public void AddBlockCursor(BlockType block) {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			
			AddBlockAt(hit, block);
			Debug.DrawLine(ray.origin, ray.origin + (ray.direction*hit.distance), Color.green, 2);
		}
	}

	public void ReplaceBlockAt(RaycastHit hit, BlockType block) {
		Vector3 position = hit.point;
		position += hit.normal * -0.5f;

		SetBlockAt (position, block);
	}
	
	public void AddBlockAt(RaycastHit hit, BlockType block) {
		Vector3 position = hit.point;
		position += (hit.normal * 0.5f);
		
		SetBlockAt (position, block);
	}
	
	public void SetBlockAt(Vector3 position, BlockType block) {
		int x= Mathf.RoundToInt( position.x );
		int y= Mathf.RoundToInt( position.y );
		int z= Mathf.RoundToInt( position.z );
		
		SetBlockAt(x,y,z,block);
	}
	
	public void SetBlockAt(int x, int y, int z, BlockType block) {
		print("Adding: " + x + ", " + y + ", " + z);
		world.data [x, y, z] = block;
		UpdateChunkAt (x, y, z);
	}
	
	public void UpdateChunkAt(int x, int y, int z){
		int updateX = Mathf.FloorToInt (x / world.chunkSize);
		int updateY = Mathf.FloorToInt (y / world.chunkSize);
		int updateZ = Mathf.FloorToInt (z / world.chunkSize);

		print("Updating: " + updateX + ", " + updateY + ", " + updateZ);

		world.chunks [updateX, updateY, updateZ].update = true;
		//X-
		if(x-(world.chunkSize * updateX) == 0 && updateX != 0) {
			world.chunks[updateX-1, updateY, updateZ].update = true;
		}
		//X+
		if(x-(world.chunkSize * updateX) == 15 && updateX != world.chunks.GetLength(0) - 1) {
			world.chunks[updateX+1, updateY, updateZ].update = true;
		}
		//Y-
		if(y-(world.chunkSize * updateY) == 0 && updateY != 0) {
			world.chunks[updateX, updateY-1, updateZ].update = true;
		}
		//Y+
		if(y-(world.chunkSize * updateY) == 15 && updateY != world.chunks.GetLength(1) - 1) {
			world.chunks[updateX, updateY+1, updateZ].update = true;
		}
		//Z-
		if(z-(world.chunkSize * updateZ) == 0 && updateZ != 0) {
			world.chunks[updateX, updateY, updateZ-1].update = true;
		}
		//Z+
		if(z-(world.chunkSize * updateZ) == 15 && updateZ != world.chunks.GetLength(2) - 1) {
			world.chunks[updateX, updateY, updateZ+1].update = true;
		}
	}

}

