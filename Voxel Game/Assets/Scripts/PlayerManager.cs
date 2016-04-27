using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	public NetworkManager nm;
	//public subPlayer sub = new subPlayer();

	int[] teamSize = {0,0};
	int nextPlayer = -1;
	NetworkHash128 assetId;
	NetworkStartPosition[] spawnPts;

	public delegate GameObject SpawnDelegate(Vector3 position, NetworkHash128 assetId);
	public delegate void UnSpawnDelegate(GameObject spawned);
	
	void Start () {
		nm = FindObjectOfType<NetworkManager> ();
		assetId = playerPrefab.GetComponent<NetworkIdentity> ().assetId;
		ClientScene.RegisterSpawnHandler (assetId, SpawnObject, UnSpawnObject);
	}

	void Update() {
		if (nm.numPlayers != teamSize[0] + teamSize[1]) {
			if( teamSize[0] <= teamSize[1])
				nextPlayer = 0;
			else
				nextPlayer = 1;

			print("Connected: " + nm.numPlayers);
			print("Visible: " + teamSize[0] + teamSize[1]);
			print("Team 0: " + teamSize[0] + ", Team 1: " + teamSize[1]);
		}
	}

	void OnLevelWasLoaded(int level) {
		spawnPts = GameObject.FindObjectsOfType<NetworkStartPosition> ();
	}

	public GameObject SpawnObject(Vector3 position, NetworkHash128 assetId) {

		FlagAudioBroadcaster[] flags = FindObjectsOfType<FlagAudioBroadcaster> ();

		if (nextPlayer == 1) {
			print ("setting player tag to cerulean");
			playerPrefab.tag = "Cerulean";
		} else {
			print ("setting player tag to burgundy");
			playerPrefab.tag = "Burgundy";
		}

		GameObject player = (GameObject)Instantiate (playerPrefab, spawnPts [nextPlayer].transform.position, spawnPts [nextPlayer].transform.rotation);
		player.transform.Rotate (new Vector3 ((float)((nextPlayer - 1) * 180.0), 0.0f, 0.0f));
		player.GetComponentInChildren<TextMesh> ().text = GameObject.FindObjectOfType<GameManagerScript> ().name;



		teamSize[nextPlayer]++;

		if (flags [0].tag == player.tag)
			flags [0].GetPlayersOnTeam ();
		else
			flags [1].GetPlayersOnTeam ();

		return player;
	}



	public void UnSpawnObject(GameObject spawned) {
		int team;
		team = (spawned.tag == "Burgundy") ? 0 : 1;
		teamSize [team]--;
		Destroy (spawned);
	}
	
}
