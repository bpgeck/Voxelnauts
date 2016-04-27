using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gameMenu : MonoBehaviour {
	
	private bool showMenu = false;
	private bool showOptions = false;

	private ArrayList fpsController = new ArrayList();

	private GameObject[] cerulean;
	private GameObject[] burgundy;
	private NetworkManager nwManager;
	private GameObject player;

	private int raw = 0;
	private int vsync = 0;

	private float hSlider = 5.0f;
	private string mouseSens = "Mouse Sensitivity";
	private float mS;
	private bool start = false;

	public int ResX;
	public int ResY;
	public bool Fullscreen;
	public float shadowDrawDistance;

	private GameObject manager;
	
	void Start () {
		nwManager = GameObject.FindObjectOfType<NetworkManager> ();
		start = true;
		cerulean = GameObject.FindGameObjectsWithTag ("Cerulean");
		burgundy = GameObject.FindGameObjectsWithTag ("Burgundy");
		
		nwManager.GetComponentInParent<NetworkManagerHUD> ().enabled = false;
		
		manager = GameObject.FindGameObjectWithTag ("GameController");
		
		//Options Menu Settings
		showOptions = false;
		hSlider = PlayerPrefs.GetFloat ("Mouse Sensitivity", 5.0f);
		mS = hSlider;
		manager.GetComponent<GameManagerScript> ().mouseSensitivity = hSlider;
		
		raw = PlayerPrefs.GetInt ("Raw Mouse", 0);
		
		if (raw == 1) { 
			manager.GetComponent<GameManagerScript> ().rawMouse = true;
			
		} else if (raw == 0)
			manager.GetComponent<GameManagerScript> ().rawMouse = false;
		
		vsync = PlayerPrefs.GetInt ("VSync", 0);
		if (vsync == 1)
			QualitySettings.vSyncCount = 1;
		else
			QualitySettings.vSyncCount = 0;
		
		//Gets all players in an ArrayList, looks for local player
		foreach (GameObject e in cerulean) {
			fpsController.Add (e);
		}
		foreach (GameObject e in burgundy) {
			fpsController.Add (e);
		}
		foreach (GameObject p in fpsController) {
			Debug.Log (p);
			if (p.GetComponent<AstroFirstPersonControl> ().isLocalPlayer == true)
				player = p;
			
		}
	}

	void Update () {
		if (start) {
			if (showOptions == true) {
				if (mS != hSlider) {
					PlayerPrefs.SetFloat ("Mouse Sensitivity", hSlider);
					manager.GetComponent<GameManagerScript> ().mouseSensitivity = hSlider;//PlayerPrefs.GetFloat ("Mouse Sensitivity");
					player.GetComponent<AstroFirstPersonControl> ().mouseSensitivity = hSlider;
					mS = hSlider;
				}
			}

			if (Input.GetButtonDown ("Escape")) {
				Debug.Log ("Pressed Escape");
				if (showMenu == false && showOptions == false) {
					showMenu = true;
					Cursor.visible = true;
					//Freezes player movement
					player.GetComponent<AstroFirstPersonControl> ().able = false;
					Debug.Log ("Show Menu True");

				} else if (showMenu == false && showOptions == true) {
					showOptions = false;
					showMenu = true;
					Debug.Log ("Show Options");
				} else {
					showMenu = false;
					Cursor.visible = false;
					player.GetComponent<AstroFirstPersonControl> ().able = true;
					Debug.Log ("Show Menu False");

				}
			}
		}
	}
	
	
	void OnGUI () {

		GUI.backgroundColor = Color.yellow;

		if (showMenu == true && showOptions == false) {
			GUI.Box(new Rect(Screen.width/2-100,Screen.height/2-100,200,110), "");
			if(GUI.Button(new Rect(Screen.width/2-80, Screen.height/2-85, 160, 20), "Options")) {
				//
				showOptions = true;
				showMenu = false;
				Debug.Log ("Options");
			}
		
			if(GUI.Button(new Rect(Screen.width/2-80, Screen.height/2-55, 160, 20), "Disconnect")) {
				//
				Destroy(nwManager);
				Debug.Log ("Disconnect");
			}

			if(GUI.Button(new Rect(Screen.width/2-80, Screen.height/2-25, 160, 20), "Quit")) {
				//
				Application.Quit();
				Debug.Log ("Quit");
			}
		}

		if (showOptions == true) {

			GUI.Box(new Rect(Screen.width/2-135,Screen.height/2-100,280,140), "");

			if(GUI.Button(new Rect(Screen.width/2-85, Screen.height/2+10, 180, 20), "Back")) {
				showOptions = false;
				showMenu = true;
				Debug.Log ("Show Options");
			}
		
			if (QualitySettings.vSyncCount == 0){ 
				//VSync is currently off
				if(GUI.Button(new Rect(Screen.width/2-85, Screen.height/2-50, 180, 20), "Vsync: Off")) {
					QualitySettings.vSyncCount = 1;
					PlayerPrefs.SetInt ("VSync", 1);
				}
			}
			else if (QualitySettings.vSyncCount == 1){
				//Vsync is currently on
				if(GUI.Button(new Rect(Screen.width/2-85, Screen.height/2-50, 180, 20), "Vsync: On")) {
					QualitySettings.vSyncCount = 0;
					PlayerPrefs.SetInt ("VSync", 0);
				}
			}
			if (raw == 0){
				//Turns raw mouse input on
				if(GUI.Button(new Rect(Screen.width/2-85, Screen.height/2-20, 180, 20), "Raw Mouse Input: Off")) {
					raw = 1;
					manager.GetComponent<GameManagerScript>().rawMouse = true;
					PlayerPrefs.SetInt("Raw Mouse", 1);
				}
			}
			if (raw == 1){
				//Turns raw mouse input off
				if(GUI.Button(new Rect(Screen.width/2-85, Screen.height/2-20, 180, 20), "Raw Mouse Input: On")) {
					manager.GetComponent<GameManagerScript>().rawMouse = false;
					raw = 0;
					PlayerPrefs.SetInt("Raw Mouse", 0);
				}
			}
			GUI.Label (new Rect(Screen.width/2-50, Screen.height/2-95, 165, 200), mouseSens);
			hSlider = GUI.HorizontalSlider(new Rect(Screen.width/2-120, Screen.height/2-70, 250, 15), hSlider, 1.0f, 100f);
			/*
			if(GUI.Button(new Rect(900, 150, 140, 100), "Vsync On")) {
				QualitySettings.vSyncCount = 1;
			}
			if(GUI.Button(new Rect(1045, 150, 140, 100), "Vsync Off")) {
				QualitySettings.vSyncCount = 0;
			}
		*/


		}
	
	}

}
