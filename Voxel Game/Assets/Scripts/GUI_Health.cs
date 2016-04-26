using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUI_Health : MonoBehaviour 
{
	public Image gui_health;
	public Image heart;
	Color burgundy;
	Color cerulean;

    GameObject player;
	
	void Start ()
	{
		burgundy = new Vector4 (128f, 0f, 0f, 1f);
		cerulean = new Vector4 (0f, 123f, 167f, 1f);

        player = this.transform.parent.gameObject.transform.parent.gameObject; // gets the parent of the parent of this script

        if (player.tag.Equals("Burgundy"))
        {
            heart.color = burgundy;
        }
        else if (player.tag.Equals("Cerulean"))
        {
            heart.color = cerulean;
        }
    }
	

	void Update () 
	{
        gui_health.fillAmount = (float)player.GetComponent<PlayerHealth>().health;
    }
}
