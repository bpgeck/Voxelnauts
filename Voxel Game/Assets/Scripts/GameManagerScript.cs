using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public bool rawMouse = false;
	public float mouseSensitivity;
	public string playerName = "";

	// Use this for initialization
	void Start () {
			DontDestroyOnLoad (this);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
