using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Flag : MonoBehaviour 
{
	public Image flag;

	Color burgundy;
	Color cerulean;

    GameObject character;

    void Start ()
	{
		burgundy = new Vector4 (125f/255, 30f/255, 29f/255, 1f);
		cerulean = new Vector4 (0f, 123f, 167f, 1f);

        character = this.transform.parent.gameObject.transform.parent.gameObject; // gets the parent of the parent of this script
    }

	void Update () 
	{
		if (character.GetComponent<AstroFirstPersonControl> ().holdingFlag == true && character.tag == "Cerulean") 
		{
			flag.enabled = true;
			flag.color = burgundy;
		} 
		else if (character.GetComponent<AstroFirstPersonControl>().holdingFlag == true && character.tag == "Burgundy") 
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
