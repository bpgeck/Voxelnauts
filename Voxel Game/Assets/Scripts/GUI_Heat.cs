using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Heat : MonoBehaviour 
{
	public Image gui_heat;

    GameObject gun;

	void Start () 
	{
        gun = this.transform.parent.gameObject.transform.parent.gameObject.transform.Find("geckstronautAnimatedWithGun/SpaceAR:SpaceAR:Mesh/GunTip").gameObject; ; // gets the parent of the parent of this script
    }

	void Update () 
	{
		gui_heat.fillAmount = (float) gun.GetComponent<RaycastGun> ().heat;
	}
}
