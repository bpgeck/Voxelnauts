using UnityEngine;
using System.Collections;

public class ItemProperties : MonoBehaviour {
    public int ID;
    FlagAudioBroadcaster friendlyAudio;
    FlagAudioBroadcaster enemyAudio;

    void Start()
    {
        GameObject[] possibilities = GameObject.FindGameObjectsWithTag("Burgundy");
        for (int i = 0; i < possibilities.Length; i++)
        {
            if (possibilities[i].name.Contains("Flag") && !possibilities[i].name.Contains("Body"))
            {
                if (possibilities[i].tag == this.tag)
                {
                    friendlyAudio = possibilities[i].GetComponent<FlagAudioBroadcaster>();
                }
                else
                {
                    enemyAudio = possibilities[i].GetComponent<FlagAudioBroadcaster>();
                }
                break;
            }
        }

        possibilities = GameObject.FindGameObjectsWithTag("Cerulean");
        for (int i = 0; i < possibilities.Length; i++)
        {
            if (possibilities[i].name.Contains("Flag") && !possibilities[i].name.Contains("Body"))
            {
                if (possibilities[i].tag == this.tag)
                {
                    friendlyAudio = possibilities[i].GetComponent<FlagAudioBroadcaster>();
                }
                else
                {
                    enemyAudio = possibilities[i].GetComponent<FlagAudioBroadcaster>();
                }
                break;
            }
        }
    }

	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Geck"))
        {   
			GameObject character = col.gameObject;
			int teamState = character.GetComponent<TeamCheck>().SameTeam(this.gameObject);
			GameObject flag_b = GameObject.Find ("Flag Burgundy");
			GameObject flag_c = GameObject.Find ("Flag Cerulean");
			if(this.ID == 2)
			{
				GameObject.Destroy(this.gameObject);
				if(teamState == 1)
				{
					flag_b.GetComponent<FlagBehavior> ().Reappear ();

                    friendlyAudio.BroadcastWeRecoveredOurFlag(); // if this flag is touched and caused to reappear, then the flag's own team touched it (friendly)
                    enemyAudio.BroadcastTheyRecoveredTheirFlag();
                }
				else if(teamState == 4 && character.GetComponent<OldAstroFirstPersonControl>().timeDead == 0)
				{
					character.GetComponent<Inventory>().PickUp(flag_b);

                    enemyAudio.BroadcastWePickedUpTheirFlag(); // if this flag is touched and picked back up, then it was touched by the opposig team (enemy)
                    friendlyAudio.BroadcastTheyPickedUpOurFlag();   
                }
			}
			else if(this.ID == 3)
			{
				GameObject.Destroy(this.gameObject);
				if(teamState == 2)
				{
					flag_c.GetComponent<FlagBehavior> ().Reappear ();

                    friendlyAudio.BroadcastWeRecoveredOurFlag(); // if this flag is touched and caused to reappear, then the flag's own team touched it (friendly)
                    enemyAudio.BroadcastTheyRecoveredTheirFlag();
                }
				else if(teamState == 3 && character.GetComponent<OldAstroFirstPersonControl>().timeDead == 0)
				{
					character.GetComponent<Inventory>().PickUp(flag_c);

                    enemyAudio.BroadcastWePickedUpTheirFlag(); // if this flag is touched and picked back up, then it was touched by the opposig team (enemy)
                    friendlyAudio.BroadcastTheyPickedUpOurFlag();
                }
			}
			else
			{
				GameObject score = GameObject.Find("Scoreboard");
				if (this.ID == 0)
				{
					if(teamState == 4)
					{
						character.GetComponent<Inventory>().PickUp(flag_b);
						flag_b.GetComponent<FlagBehavior>().Disappear();

                        enemyAudio.BroadcastWeGotTheirFlag(); // if this flag is picked up, it has been taken by the enemy
                        friendlyAudio.BroadcastTheyGotOurFlag();
					}
					else if(teamState == 1 && character.GetComponent<Inventory>().IsInInventory(1))
					{
						Debug.Log("You get a point!"); //team burgundy
                        if (score != null)
                        {
                            score.GetComponent<Scoreboard>().score_b++;
                        }
						flag_c.GetComponent<FlagBehavior>().Reappear();
						character.GetComponent<Inventory>().inventory.Remove(character.GetComponent<Inventory>().find(1));

                        friendlyAudio.BroadcastWeCappedTheirFlag(); // if this flag touches the other team's flag, then we got a point
                        enemyAudio.BroadcastTheyCappedOurFlag();
                    }
				}
				else if(this.ID == 1)
				{
					if(teamState == 3)
					{
						character.GetComponent<Inventory>().PickUp(flag_c);
						flag_c.GetComponent<FlagBehavior>().Disappear();

                        enemyAudio.BroadcastWeGotTheirFlag(); // if this flag is picked up, it has been taken by the enemy
                        friendlyAudio.BroadcastTheyGotOurFlag();
                    }
					else if(teamState == 2 && character.GetComponent<Inventory>().IsInInventory(0))
					{
						Debug.Log("You get a point!"); //team cerulean
                        if (score != null)
                        {
                            score.GetComponent<Scoreboard>().score_c++;
                        }
						flag_b.GetComponent<FlagBehavior>().Reappear();
						character.GetComponent<Inventory>().inventory.Remove(character.GetComponent<Inventory>().find(0));

                        friendlyAudio.BroadcastWeCappedTheirFlag(); // if this flag touches the other team's flag, then we got a point
                        enemyAudio.BroadcastTheyCappedOurFlag();
                    }
				}
				else
				{
					// next, destroy this object
					GameObject.Destroy(this.gameObject);
				}
			}
        }
    }


    public void SetInfoFromItem(Item item)
    {
        ID = item.ID;
    }
}
