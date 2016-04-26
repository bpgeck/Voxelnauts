using UnityEngine;
using UnityEngine.Networking;

public class AddPlayerMessage : MessageBase {
	public static short MSG_TYPE = 2000;
	public string name = "";
	public short playerControllerID = 0;
	
}
public class subPlayer : NetworkManager {


	public override void OnStartServer(){
		base.OnStartServer ();
		NetworkServer.RegisterHandler (AddPlayerMessage.MSG_TYPE, OnServerAddPlayerName);

	}

	public override void OnStopServer(){
		base.OnStopServer ();
		NetworkServer.UnregisterHandler (AddPlayerMessage.MSG_TYPE);

	}

	public static void AddPlayer(string name, short playerControllerID){
		singleton.client.Send (AddPlayerMessage.MSG_TYPE, new AddPlayerMessage() {name = name, playerControllerID = playerControllerID});

	}


	public override void OnClientConnect (NetworkConnection conn) {
		base.OnClientConnect(conn);
		//AddPlayer (PlayerPrefs.getString("Player Name"), playerControllerID:);

	}
	
	public void OnServerAddPlayerName(NetworkMessage netMSG) {
		AddPlayerMessage msg = netMSG.ReadMessage<AddPlayerMessage> ();
		name = msg.name;


	}



}
