using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public bool rawMouse = false;
	public float mouseSensitivity;
	public string playerName = "";


	void Start () {
		DontDestroyOnLoad (this);

		playerName = PlayerPrefs.GetString ("Player Name");
		mouseSensitivity = PlayerPrefs.GetFloat ("Mouse Sensitivity");

		if (PlayerPrefs.GetInt ("Raw Mouse") == 0)
			rawMouse = false;
		else 
			rawMouse = true;

	}

}
