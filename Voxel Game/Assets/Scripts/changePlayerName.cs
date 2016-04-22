using UnityEngine;
using System.Collections;

public class changePlayerName : MonoBehaviour {

		public TextMesh gt;
		public int maxNameLength = 10;
		private GameObject manager;
		private bool playerNameEditable = false;

		void Start() {
			gt = GetComponent<TextMesh>();
			gt.text = PlayerPrefs.GetString ("Player Name", "Player Name");
			manager = GameObject.FindGameObjectWithTag ("GameController");
			manager.GetComponent<GameManagerScript> ().playerName = PlayerPrefs.GetString ("Player Name", "Player Name");
		}

		void Update() {
		manager.GetComponent<GameManagerScript> ().playerName = gt.text;
		if (playerNameEditable == true) {
			foreach (char c in Input.inputString) {
				if (c == "\b" [0]) {
					if (gt.text.Length != 0) {
						gt.text = gt.text.Substring (0, gt.text.Length - 1);
					}
				}
				else {
						if (c == "\n" [0] || c == "\r" [0]) {
					manager.GetComponent<GameManagerScript> ().playerName = gt.text;
						playerNameEditable = false;
					}
				else{
					if (gt.text.Length <= maxNameLength)
						gt.text += c;
					}
				}
			}
			PlayerPrefs.SetString("Player Name", gt.text);
		}
		}

		void OnMouseDown(){
		if (playerNameEditable == true) {
			playerNameEditable = false;
			Debug.Log ("Editable false");
		}
		else {
			playerNameEditable = true;
			Debug.Log ("Editable true");
			}
		}
	/*
	 foreach (char c in Input.inputString) {
				if (c == "\b"[0])
					if (gt.text.Length != 0)
						gt.text = gt.text.Substring(0, gt.text.Length - 1);
				
				else
					if (c == "\n"[0] || c == "\r"[0])
						print(gt.text);
				else
					gt.text += c;
			}
			*/
	}


