using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Flag : MonoBehaviour 
{
	public Image flag;

	Color burgundy;
	Color cerulean;

	void Start ()
	{
		burgundy = new Vector4 (125f/255, 30f/255, 29f/255, 1f);
		cerulean = new Vector4 (0f, 123f, 167f, 1f);
	}

	void Update () 
	{
		GameObject character = GameObject.Find ("GeckstroNOT");
		if (character.GetComponent<Inventory> ().IsInInventory (0)) 
		{
			flag.enabled = true;
			flag.color = burgundy;
		} 
		else if (character.GetComponent<Inventory> ().IsInInventory (1)) 
		{
			flag.enabled = true;
			flag.color = cerulean;
		} 
		else 
		{
			flag.enabled = false;
		}
	}
}
