using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scoreboard : MonoBehaviour 
{
	public int score_b;
	public int score_c;
	Text text; //Reference to the score text gui
	
	void Start ()
	{
		text = GetComponent <Text> (); //Referencing
		score_b = 0; //Initially begins at zero
		score_c = 0;
		//Initialize in the other code 
		//Or whatever value, then
		//Scoreboard.score += scoreValue; 
		//That would be in the script where it tells when a flag has made it to the opposing teams side
		
	}
	
	void Update () 
	{
		text.text = "| Burgundy: " + score_b + " || " +"Cerulean: " + score_c+ " |"; // How the text gui be set
	}
}
