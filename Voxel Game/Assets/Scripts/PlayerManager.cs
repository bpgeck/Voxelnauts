using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject[] playerPrefabs;
	public NetworkManager nm;

	int[] teamSize = {0,0};
	int nextPlayer = -1;
	NetworkHash128[] assetIds;
	NetworkStartPosition[] spawnPts;

	public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
	public delegate void UnSpawnDelegate(GameObject spawned);
	
	void Start () {
		assetIds = new NetworkHash128[2];
		nm = FindObjectOfType<NetworkManager> ();
		assetIds[0] = playerPrefabs[0].GetComponent<NetworkIdentity> ().assetId;
		assetIds[1] = playerPrefabs[1].GetComponent<NetworkIdentity> ().assetId;
		ClientScene.RegisterSpawnHandler (assetIds[0], SpawnObject, UnSpawnObject);
	}

	void Update() {
		if (nm.numPlayers != teamSize[0] + teamSize[1]) {
			if( teamSize[0] <= teamSize[1]) {
				nextPlayer = 0;
			} else {
				nextPlayer = 1;
			}
			print("Connected: " + nm.numPlayers);
			print("Visible: " + teamSize[0] + teamSize[1]);
			print("Team 0: " + teamSize[0] + ", Team 1: " + teamSize[1]);
		}
	}

	void OnLevelWasLoaded(int level) {
		spawnPts = GameObject.FindObjectsOfType<NetworkStartPosition> ();
	}

	public GameObject SpawnObject(Vector3 position, NetworkHash128 assetId) {
		GameObject player = (GameObject)Instantiate (playerPrefabs[nextPlayer], spawnPts[nextPlayer].transform.position, spawnPts[nextPlayer].transform.rotation);
		player.transform.Rotate (new Vector3 ((float)((nextPlayer-1)*180.0), 0.0f, 0.0f));
		teamSize[nextPlayer]++;
		return player;
	}

	public void UnSpawnObject(GameObject spawned) {
		int team;
		team = (spawned.tag == "Burgundy") ? 0 : 1;
		teamSize [team]--;
		Destroy (spawned);
	}
	
}
