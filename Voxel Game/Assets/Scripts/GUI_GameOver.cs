using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_GameOver : MonoBehaviour
{
    string winningTeam;

    Text text;

    Color burgundy;
    Color cerulean;

    GameObject scoreboard;
    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text> ();

        burgundy = new Vector4(125f / 255, 30f / 255, 29f / 255, 1f);
        cerulean = new Vector4(0f, 123f, 167f, 1f);

        scoreboard = GameObject.Find("Scoreboard");
        if (scoreboard.GetComponent<Scoreboard>().score_b > scoreboard.GetComponent<Scoreboard>().score_c)
        {
            text.color = burgundy;
            winningTeam = "Burgundy";
        }
        else
        {
            text.color = cerulean;
            winningTeam = "Cerulean";
        }
    }
	
	// Update is called once per frame
	void Update () {
        text.text = winningTeam + " Wins!"; // How the text gui be set
    }
}
