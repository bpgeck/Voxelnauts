using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Health : MonoBehaviour 
{
	public Image gui_health;
	public Image heart;
	Color burgundy;
	Color cerulean;
	
	void Start ()
	{
		burgundy = new Vector4 (128f, 0f, 0f, 1f);
		cerulean = new Vector4 (0f, 123f, 167f, 1f);
	}
	

	void Update () 
	{
		GameObject player = GameObject.Find ("GeckstroNOT");
		gui_health.fillAmount = (float) player.GetComponent<PlayerHealth> ().health;
		if (player.tag.Equals ("Burgundy"))
		{
			heart.color = burgundy;
		}
		else if (player.tag.Equals ("Cerulean"))
		{
			heart.color = cerulean;
		}
	}
}
