using UnityEngine;
using System.Collections;

public class playerName : MonoBehaviour {
	public string name;

	void Start () {
		name = PlayerPrefs.GetString ("Player Name", "Geckstro");
	}

}
