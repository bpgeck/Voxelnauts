using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameMenu : MonoBehaviour {

	private bool showOptions = false;
	private ArrayList fpsController = new ArrayList();
	private GameObject player;
	
	void Start () {
		fpsController.Add(GameObject.FindGameObjectsWithTag ("Cerulean"));
		fpsController.Add(GameObject.FindGameObjectsWithTag ("Burgundy"));
		Debug.Log (fpsController);
		foreach (GameObject p in fpsController){
			Debug.Log (p);
			if (p.GetComponent<AstroFirstPersonControl>().isLocalPlayer == true)
				player = p;
		}
	}

	void Update () {
		if (Input.GetButtonDown ("Escape")) {
			if (showOptions == false){
				showOptions = true;
				Cursor.visible = true;
				player.GetComponent<AstroFirstPersonControl>().able = false;
				Debug.Log ("Show Options True");

			}
			else {
				showOptions = false;
				Cursor.visible = false;
				player.GetComponent<AstroFirstPersonControl>().able = true;
				Debug.Log ("Show Options False");

			}
		}

	}

	void OnGUI () {

		if (showOptions == true) {
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-150,200,300), "Loader Menu");
			if(GUI.Button(new Rect(Screen.width/2-80, 250, 150, 20), "Options")) {
				//
				Debug.Log ("Options");
			}
		
		}
	
	}

}
