using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public bool rawMouse = false;
	public float mouseSensitivity;
	public string playerName = "";

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		playerName = PlayerPrefs.GetString ("Player Name");
		mouseSensitivity = PlayerPrefs.GetInt ("Mouse Sensitivity");
		if (PlayerPrefs.GetInt ("Raw Mouse") == 0)
			rawMouse = false;
		else 
			rawMouse = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
