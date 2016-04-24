using UnityEngine;
using System.Collections;

public class changePlayerName : MonoBehaviour
{
		public TextMesh gt;
		public int maxNameLength = 10;
		private GameObject manager;
		private bool playerNameEditable = false;

        GameObject background;
        Color backgroundColor;

		void Start() {
			gt = GetComponent<TextMesh>();
			gt.text = PlayerPrefs.GetString ("Player Name", "Player Name");
			manager = GameObject.FindGameObjectWithTag ("GameController");
			manager.GetComponent<GameManagerScript> ().playerName = PlayerPrefs.GetString ("Player Name", "Player Name");

            background = this.transform.Find("Background").gameObject;
            backgroundColor = background.GetComponent<Renderer>().material.color;
            backgroundColor.a = 0;
            background.GetComponent<Renderer>().material.color = backgroundColor;
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

        void OnMouseOver()
        {
            backgroundColor = background.GetComponent<Renderer>().material.color;
            backgroundColor.a = 0.5f;
            background.GetComponent<Renderer>().material.color = backgroundColor;
        }

        void OnMouseExit()
        {
            backgroundColor = background.GetComponent<Renderer>().material.color;
            backgroundColor.a = 0.0f;
            background.GetComponent<Renderer>().material.color = backgroundColor;
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
	}


