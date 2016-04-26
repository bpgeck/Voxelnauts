using UnityEngine;
using System.Collections;


public class Menu : MonoBehaviour {
	
	private bool showOptions = false;
	private GameObject manager;
	public float shadowDrawDistance;
	private float hSlider = 5.0f;
	private string mouseSens = "Mouse Sensitivity";
	private int raw = 0;
	private int vsync = 0;
	public int ResX;
	public int ResY;
	public bool Fullscreen;


	public AudioClip click;
	AudioSource aud;

	//All the button Rects
	private static Rect rectCNT = new Rect(900, 250, 300, 100);
	private static Rect rectDIS = new Rect(900, 360, 300, 100);
	private static Rect rectOPT = new Rect(900, 470, 300, 100);
	private static Rect rectIQ = new Rect(1210, 250, 300, 100);
	private static Rect rectDQ = new Rect(1210, 360, 300, 100);
	private static Rect rectNOA = new Rect(1210, 470, 65, 100);
	private static Rect rect2A = new Rect(1279, 470, 65, 100);
	private static Rect rect4A = new Rect(1354, 470, 65, 100);
	private static Rect rect8A = new Rect(1428, 470, 65, 100);
	private static Rect rectTBON = new Rect(1210, 580, 140, 100);
	private static Rect rectTBOFF = new Rect(1355, 580, 140, 100);
	private static Rect rectAFON = new Rect(590, 250, 300, 100);
	private static Rect rectAFOFF = new Rect(590, 360, 300, 100);
	private static Rect rect60Hz = new Rect(590, 470, 300, 100);
	private static Rect rect120Hz = new Rect(590, 580, 300, 100);
	private static Rect rect1080p = new Rect(900, 580, 93, 100);
	private static Rect rect720p = new Rect(996, 580, 93, 100);
	private static Rect rect480p = new Rect(1092, 580, 93, 100);
	private static Rect rectVSync = new Rect(905, 140, 140, 100);
	private static Rect rectRM = new Rect(1055, 140, 140, 100);
	private Rect[] rectMAIN = {rectCNT, rectDIS, rectOPT};
	private Rect[] rectALLOPT = {rectIQ, rectDQ, rectNOA, rect2A, rect4A, rect8A, rectTBON, rectTBOFF, rectAFON, rectAFOFF, rect60Hz, rect120Hz, rect1080p, rect720p, rect480p, rectVSync, rectRM};
	private string[] buttons = {"CNT", "DIS", "OPT", "IQ", "DQ", "NOA", "2A", "4A", "8A", "TBON", "TBOFF", "AFON", "AFOFF",
		"60Hz", "120Hz", "1080p", "720p", "480p", "VS0", "VS1", "RM0", "RM1"};

	private bool play = false;
	private bool overButton = false;
	private string hover;
	private string lastToolTip;

	// Use this for initialization
	void Start () {


		showOptions = false;

		manager = GameObject.FindGameObjectWithTag ("GameController");

		hSlider = PlayerPrefs.GetFloat ("Mouse Sensitivity", 5.0f);
		raw = PlayerPrefs.GetInt ("Raw Mouse", 0);

		aud = GetComponent<AudioSource> ();

		if (raw == 1)
			manager.GetComponent<GameManagerScript> ().rawMouse = true;
		else
			manager.GetComponent<GameManagerScript> ().rawMouse = false;

		vsync = PlayerPrefs.GetInt ("VSync", 0);

		if (vsync == 1)
			QualitySettings.vSyncCount = 1;
		else
			QualitySettings.vSyncCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerPrefs.SetFloat ("Mouse Sensitivity", hSlider);
		manager.GetComponent<GameManagerScript>().mouseSensitivity = hSlider;


		//Checks if the mouse is hovering over a button
		switch (hover) {
		case "CNT":
		case "DIS":
		case "OPT":
		case "IQ":
		case "DQ":
		case "NOA":
		case "2A":
		case "4A":
		case "8A":
		case "TBON":
		case "TBOFF":
		case "AFON":
		case "AFOFF":
		case "60Hz":
		case "120Hz":
		case "1080p":
		case "720p":
		case "480p":
		case "VS0":
		case "VS1":
		case "RM0":
		case "RM1":
		{
			
			//If hovering, play sound
			overButton = true;
			lastToolTip = hover;
			break;
		}
		case "":
		{
			//If not, don't play sound
			overButton = false;
			lastToolTip = hover;
			break;
		}
		}

		if (overButton) {
			if (!play) {
				aud.PlayOneShot (click);
				play = true;
			}
		} 
		else {
			play = false;
		}

	}

	void OnGUI() {



		if (GUI.Button (rectCNT, new GUIContent ("Connect", "CNT"))) {
			// Application.LoadLevel(1);
		}

		if (GUI.Button (rectDIS, new GUIContent ("Disconnect", "DIS"))) {
			Application.Quit ();
		}

		if (GUI.Button (rectOPT, new GUIContent ("Options", "OPT"))) {
			if (!showOptions) {
				showOptions = true;
			} else {
				showOptions = false;
			}
		} 


		if (showOptions == true) {

			//INCREASE QUALITY PRESET
			if (GUI.Button (rectIQ, new GUIContent ("Increase Quality", "IQ"))) {
				QualitySettings.IncreaseLevel ();
				Debug.Log ("Increased quality");
			}

			//DECREASE QUALITY PRESET
			if (GUI.Button (rectDQ, new GUIContent ("Decrease Quality", "DQ"))) {
				QualitySettings.DecreaseLevel ();
				Debug.Log ("Decreased quality");
			}

			//0 X AA SETTINGS
			if (GUI.Button (rectNOA, new GUIContent ("No AA", "NOA"))) {
				QualitySettings.antiAliasing = 0;
				Debug.Log ("0 AA");
			}

			//2 X AA SETTINGS
			if (GUI.Button (rect2A, new GUIContent ("2x AA", "2A"))) {
				QualitySettings.antiAliasing = 2;
				Debug.Log ("2 x AA");
			}

			//4 X AA SETTINGS
			if (GUI.Button (rect4A, new GUIContent ("4x AA", "4A"))) {
				QualitySettings.antiAliasing = 4;
				Debug.Log ("4 x AA");
			}

			//8 x AA SETTINGS
			if (GUI.Button (rect8A, new GUIContent ("8x AA", "8A"))) {
				QualitySettings.antiAliasing = 8;
				Debug.Log ("8 x AA");
			}

			//TRIPLE BUFFERING SETTINGS
			if (GUI.Button (rectTBON, new GUIContent ("Triple Buffering On", "TBON"))) {
				QualitySettings.maxQueuedFrames = 3;
				Debug.Log ("Triple buffering on");
			}

			if (GUI.Button (rectTBOFF, new GUIContent ("Triple Buffering Off", "TBOFF"))) {
				QualitySettings.maxQueuedFrames = 0;
				Debug.Log ("Triple buffering off");
			}

			//ANISOTROPIC FILTERING SETTINGS
			if (GUI.Button (rectAFON, new GUIContent ("Anisotropic Filtering On", "AFON"))) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
				Debug.Log ("Force enable anisotropic filtering!");
			}

			if (GUI.Button (rectAFOFF, new GUIContent ("Anisotropic Filtering Off", "AFOFF"))) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
				Debug.Log ("Disable anisotropic filtering!");
			}


			//RESOLUTION SETTINGS
			//60Hz
			if (GUI.Button (rect60Hz, new GUIContent ("60Hz", "60Hz"))) {
				Screen.SetResolution (ResX, ResY, Fullscreen, 60);
				Debug.Log ("60Hz");
			}

			//120Hz
			if (GUI.Button (rect120Hz, new GUIContent ("120Hz", "120Hz"))) {
				Screen.SetResolution (ResX, ResY, Fullscreen, 120);
				Debug.Log ("120Hz");
			}

			//1080p
			if (GUI.Button (rect1080p, new GUIContent ("1080p", "1080p"))) {
				Screen.SetResolution (1920, 1080, Fullscreen);
				ResX = 1920;
				ResY = 1080;
				Debug.Log ("1080p");
			}

			//720p
			if (GUI.Button (rect720p, new GUIContent ("720p", "720p"))) {
				Screen.SetResolution (1280, 720, Fullscreen);
				ResX = 1280;
				ResY = 720;
				Debug.Log ("720p");
			}

			//480p
			if (GUI.Button (rect480p, new GUIContent ("480p", "480p"))) {
				Screen.SetResolution (640, 480, Fullscreen);
				ResX = 640;
				ResY = 480;
				Debug.Log ("480p");
			}

			if (QualitySettings.vSyncCount == 0) { 
				//VSync is currently off
				if (GUI.Button (rectVSync, new GUIContent ("Vsync: Off", "VS0"))) {
					QualitySettings.vSyncCount = 1;
					PlayerPrefs.SetInt ("VSync", 1);
				}
			} else if (QualitySettings.vSyncCount == 1) {
				//Vsync is currently on
				if (GUI.Button (rectVSync, new GUIContent ("Vsync: On", "VS1"))) {
					QualitySettings.vSyncCount = 0;
					PlayerPrefs.SetInt ("VSync", 0);
				}
			}

			if (manager.GetComponent<GameManagerScript> ().rawMouse == false) {
				//Turns raw mouse input on
				if (GUI.Button (rectRM, new GUIContent ("Raw Mouse Input: Off", "RM0"))) {
					manager.GetComponent<GameManagerScript> ().rawMouse = true;
					PlayerPrefs.SetInt ("Raw Mouse", 1);
				}
			}
			if (manager.GetComponent<GameManagerScript> ().rawMouse == true) {
				//Turns raw mouse input off
				if (GUI.Button (rectRM, new GUIContent ("Raw Mouse Input: On", "RM1"))) {
					manager.GetComponent<GameManagerScript> ().rawMouse = false;
					PlayerPrefs.SetInt ("Raw Mouse", 0);
				}
			}



			GUI.Label (new Rect (700, 190, 165, 200), mouseSens);
			hSlider = GUI.HorizontalSlider (new Rect (620, 215, 250, 15), hSlider, 1.0f, 100f);
		}

		hover = GUI.tooltip;


	
		}
	}