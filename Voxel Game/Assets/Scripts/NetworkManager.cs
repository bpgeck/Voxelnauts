using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
    const string VERSION = "1.0";

    public string ROOM_NAME = "GeckstroROOM";

    public GameObject[] spawnPoints;

	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings(VERSION);
    }
	
	void OnJoinedLobby ()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default); // this means that the first person to join will be the host
    }

    void OnJoinedRoom()
    {
        Transform spawnLocationTransform = spawnPoints[Random.Range(0, spawnPoints.Length)].transform; // randomly choose a team
        PhotonNetwork.Instantiate("GeckstroNET", spawnLocationTransform.position, spawnLocationTransform.rotation, 0);
    }
}