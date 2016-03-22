using UnityEngine;
using System.Collections;

public class TeamCheck : MonoBehaviour 
{
	public int SameTeam(GameObject flag)
	{
		string charTag = this.gameObject.tag;
		string flagTag = flag.gameObject.tag;
		
		if ((charTag.Equals ("Burgundy") && flagTag.Equals ("Burgundy")) || (charTag.Equals ("Cerulean") && flagTag.Equals ("Cerulean"))) 
		{
			return 1;
		}
		if ((charTag.Equals ("Burgundy") && flagTag.Equals ("Cerulean")) || (charTag.Equals ("Cerulean") && flagTag.Equals ("Burgundy"))) 
		{
			return 2;
		}
		
		return 0;
	}
}
