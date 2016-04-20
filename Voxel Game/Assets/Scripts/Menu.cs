using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	private bool showOptions = false;
	private GameObject manager;
	public float shadowDrawDistance;
	private float hSlider = 5.0f;
	private string mouseSens = "Mouse Sensitivity";
	public int ResX;
	public int ResY;
	public bool Fullscreen;
	public GUIStyle backgroundStyle;
	public GUIStyle thumbStyle;

	// Use this for initialization
	void Start () {
		showOptions = false;
		manager = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		manager.GetComponent<GameManagerScript> ().mouseSensitivity = hSlider;
	}
	
	void OnGUI() {

		if(GUI.Button(new Rect(900, 250, 300, 100), "Connect")) {
			// Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(900, 360, 300, 100), "Disconnect")) {
			Application.Quit();
		}
		if(GUI.Button(new Rect(900, 470, 300, 100), "Options")) {
			if (!showOptions) {
				showOptions = true;
			}
			else {
				showOptions = false;
			}
		}
		if(showOptions == true) {
			//INCREASE QUALITY PRESET
			if(GUI.Button(new Rect(1210, 250, 300, 100), "Increase Quality")) {
				QualitySettings.IncreaseLevel();
				Debug.Log ("Increased quality");
			}
			//DECREASE QUALITY PRESET
			if(GUI.Button(new Rect(1210, 360, 300, 100), "Decrease Quality")) {
				QualitySettings.DecreaseLevel();
				Debug.Log ("Decreased quality");
			}
			//0 X AA SETTINGS
			if(GUI.Button(new Rect(1210, 470, 65, 100), "No AA")) {
				QualitySettings.antiAliasing = 0;
				Debug.Log ("0 AA");
			}
			//2 X AA SETTINGS
			if(GUI.Button(new Rect(1279, 470, 65, 100), "2x AA")) {
				QualitySettings.antiAliasing = 2;
				Debug.Log ("2 x AA");
			}
			//4 X AA SETTINGS
			if(GUI.Button(new Rect(1354, 470, 65, 100), "4x AA")) {
				QualitySettings.antiAliasing = 4;
				Debug.Log ("4 x AA");
			}
			//8 x AA SETTINGS
			if(GUI.Button(new Rect(1428, 470, 65, 100), "8x AA")) {
				QualitySettings.antiAliasing = 8;
				Debug.Log ("8 x AA");
			}
			//TRIPLE BUFFERING SETTINGS
			if(GUI.Button(new Rect(1210, 580, 140, 100), "Triple Buffering On")) {
				QualitySettings.maxQueuedFrames = 3;
				Debug.Log ("Triple buffering on");
			}
			if(GUI.Button(new Rect(1355, 580, 140, 100), "Triple Buffering Off")) {
				QualitySettings.maxQueuedFrames = 0;
				Debug.Log ("Triple buffering off");
			}
			//ANISOTROPIC FILTERING SETTINGS
			if(GUI.Button(new Rect(590, 250, 300, 100), "Anisotropic Filtering On")) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
				Debug.Log ("Force enable anisotropic filtering!");
			}
			if(GUI.Button(new Rect(590, 360, 300, 100), "Anisotropic Filtering Off")) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
				Debug.Log ("Disable anisotropic filtering!");
			}
			//RESOLUTION SETTINGS
			//60Hz
			if(GUI.Button(new Rect(590, 470, 300, 100), "60Hz")) {
				Screen.SetResolution(ResX, ResY, Fullscreen, 60);
				Debug.Log ("60Hz");
			}
			//120Hz
			if(GUI.Button(new Rect(590, 580, 300, 100), "120Hz")) {
				Screen.SetResolution(ResX, ResY, Fullscreen, 120);
				Debug.Log ("120Hz");
			}
			//1080p
			if(GUI.Button(new Rect(900, 580, 93, 100), "1080p")) {
				Screen.SetResolution(1920, 1080, Fullscreen);
				ResX = 1920;
				ResY = 1080;
				Debug.Log ("1080p");
			}
			//720p
			if(GUI.Button(new Rect(996, 580, 93, 100), "720p")) {
				Screen.SetResolution(1280, 720, Fullscreen);
				ResX = 1280;
				ResY = 720;
				Debug.Log ("720p");
			}
			//480p
			if(GUI.Button(new Rect(1092, 580, 93, 100), "480p")) {
				Screen.SetResolution(640, 480, Fullscreen);
				ResX = 640;
				ResY = 480;
				Debug.Log ("480p");
			}
			if (QualitySettings.vSyncCount == 0){ 
				//VSync is currently off
				if(GUI.Button(new Rect(900, 150, 140, 100), "Vsync: Off")) {
					QualitySettings.vSyncCount = 1;
				}
			}
			else if (QualitySettings.vSyncCount == 1){
				//Vsync is currently on
				if(GUI.Button(new Rect(900, 150, 140, 100), "Vsync: On")) {
					QualitySettings.vSyncCount = 0;
				}
			}
			if (manager.GetComponent<GameManagerScript>().rawMouse == false){
				//Turns raw mouse input on
				if(GUI.Button(new Rect(1045, 150, 140, 100), "Raw Mouse Input: Off")) {
					manager.GetComponent<GameManagerScript>().rawMouse = true;
				}
			}
			if (manager.GetComponent<GameManagerScript>().rawMouse == true){
				//Turns raw mouse input off
				if(GUI.Button(new Rect(1045, 150, 140, 100), "Raw Mouse Input: On")) {
					manager.GetComponent<GameManagerScript>().rawMouse = false;
				}
			}
			GUI.Label(new Rect(700,190,140,100),mouseSens);
			hSlider = GUI.HorizontalSlider(new Rect(625, 210, 250, 20), hSlider, 1.0f, 100f, backgroundStyle, thumbStyle);
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