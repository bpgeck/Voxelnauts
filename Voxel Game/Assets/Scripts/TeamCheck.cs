using UnityEngine;
using System.Collections;

public class TeamCheck : MonoBehaviour 
{
	public int SameTeam(GameObject flag)
	{
		string charTag = this.gameObject.tag;
		string flagTag = flag.gameObject.tag;
		
		if (charTag.Equals ("Burgundy") && flagTag.Equals ("Burgundy")) 
		{
			return 1;
		} 
		else if (charTag.Equals ("Cerulean") && flagTag.Equals ("Cerulean"))
		{
			return 2;
		}
		else if (charTag.Equals ("Burgundy") && flagTag.Equals ("Cerulean"))
		{
			return 3;
		}
		else if(charTag.Equals ("Cerulean") && flagTag.Equals ("Burgundy"))
		{
			return 4;
		}
		return 0;
	}
}
