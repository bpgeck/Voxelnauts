using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Heat : MonoBehaviour 
{
	public Image gui_heat;

	void Start () 
	{
	
	}

	void Update () 
	{
		GameObject gun = GameObject.Find ("GunTip");
		gui_heat.fillAmount = (float) gun.GetComponent<RaycastGun> ().heat;
	}
}
