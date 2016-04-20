using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Health : MonoBehaviour 
{
	public Image gui_health;

	void Start ()
	{
	
	}
	

	void Update () 
	{
		GameObject player = GameObject.Find ("GeckstroNOT");
		gui_health.fillAmount = (float) player.GetComponent<PlayerHealth> ().health;
	}
}
