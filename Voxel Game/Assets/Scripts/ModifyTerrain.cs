using UnityEngine;
using System.Collections;
using System;

public class ModifyTerrain : MonoBehaviour {

	World world;

	void Start() {
		world = gameObject.GetComponent ("World") as World;
	}

	void Update() {

	}

	public void DestroyAreaOnCollision(Collision collision, float radius) {
		Vector3 point = collision.contacts [0].point + (collision.contacts [0].normal * -0.5f);
		point.x = Mathf.RoundToInt (point.x);
		point.y = Mathf.RoundToInt (point.y);
		point.z = Mathf.RoundToInt (point.z);
		int c = (int)(radius / 2);
		for (int i = -c; i <= c; i++) {
			for (int j = -c; j <= c; j++) {
				for (int k = -c; j <= c; k++) {
					int x = (int)(point.x + i);
					int y = (int)(point.y + j);
					int z = (int)(point.z + k);
					float distance = Mathf.Sqrt(x*x + y*y + z*z);
					if (distance <= radius) {
						SetBlockAt(x,y,z,BlockType.Air);
					}
				}
			}
		}
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
		print("Adding Block: " + x + ", " + y + ", " + z + ", " + block.ToString());
		world.data [x, y, z] = block;
		UpdateChunkAt (x, y, z);
	}
	
	public void UpdateChunkAt(int x, int y, int z){
		int updateX = Mathf.FloorToInt (x / world.GetChunkSize());
		int updateY = Mathf.FloorToInt (y / world.GetChunkSize());
		int updateZ = Mathf.FloorToInt (z / world.GetChunkSize());

		print("Updating Chunk: " + updateX + ", " + updateY + ", " + updateZ);

		world.chunks [updateX, updateY, updateZ].update = true;

		//X-
		if(x-(world.GetChunkSize() * updateX) == 0 && updateX != 0) {
			world.chunks[updateX-1, updateY, updateZ].update = true;
		}
		//X+
		if(x-(world.GetChunkSize() * updateX) == 15 && updateX != world.chunks.GetLength(0) - 1) {
			world.chunks[updateX+1, updateY, updateZ].update = true;
		}
		//Y-
		if(y-(world.GetChunkSize() * updateY) == 0 && updateY != 0) {
			world.chunks[updateX, updateY-1, updateZ].update = true;
		}
		//Y+
		if(y-(world.GetChunkSize() * updateY) == 15 && updateY != world.chunks.GetLength(1) - 1) {
			world.chunks[updateX, updateY+1, updateZ].update = true;
		}
		//Z-
		if(z-(world.GetChunkSize() * updateZ) == 0 && updateZ != 0) {
			world.chunks[updateX, updateY, updateZ-1].update = true;
		}
		//Z+
		if(z-(world.GetChunkSize() * updateZ) == 15 && updateZ != world.chunks.GetLength(2) - 1) {
			world.chunks[updateX, updateY, updateZ+1].update = true;
		}
	}

}

