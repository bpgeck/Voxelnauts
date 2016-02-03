using UnityEngine;
using System.Collections;
using System;

public class ModifyTerrain : MonoBehaviour {

	World world;
	BlockType current = BlockType.Bedrock;

	void Start() {
		world = gameObject.GetComponent ("World") as World;
	}

	void Update() {
		//shift commands
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKeyDown (KeyCode.S)) {
			//shift+S
			if (Input.GetKeyDown (KeyCode.S)) {
				print ("saved: " + world.worldName);
				world.SaveWorld ();
			}
		}
		//scrollwheel
		if (Input.GetAxis ("Mouse ScrollWheel") > 0)
			SetCurrentBlock (1);
		else if (Input.GetAxis ("Mouse ScrollWheel") < 0)
			SetCurrentBlock (-1);
		//lmb
		if (Input.GetMouseButtonDown (0))
			ReplaceBlockCursor (BlockType.Air);
		//rmb
		if (Input.GetMouseButtonDown (1))
			AddBlockCursor (current);

	}

	public void SetCurrentBlock (int x) {

		if (x == 0)
			return;
		else if (x > 0 && current == BlockType.RockDust)
			current = BlockType.Bedrock;
		else if (x < 0 && current == BlockType.Bedrock)
			current = BlockType.RockDust;
		else
			current = (BlockType)(byte)((int)current + x);

		print ("Selected BlockType: " + current.ToString ());
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

