using UnityEngine;
using System.Collections;

public class FlagAudioBroadcaster : MonoBehaviour
{
    string myTag;

    GameObject[] playersOnTeam;
    AudioSource[][] headsets;

    float numSeconds = 0;

    /* What need to be broadcasted
    - Got their flag: WeveTakenTheEnemyFlag.mp3 -- done
    - Dropped their flag: WeDroppedTheirFlag.mp3 -- done
    - Picked their flag back up: EnemyFlagAcquired.mp3 -- done
    - Their recovered their flag: TheyReturnedTheirFlag.mp3 -- done
    - Capped their flag: CapturedTheirFlag + FlagGotDrums.mp3 -- done

    - They got our flag: TheEnemysTakenOurFlag.mp3 -- done
    - They dropped our flag: TheEnemyDroppedOurFlag.mp3 -- done
    - They picked our flag back up: EnemyHasOurFlag.mp3 -- done
    - We recovered our flag: OurFlagIsBackOnBase.mp3 -- done
    - They capped our flag: TheyCappedOurFlag.mp3 + FlagLostDrums.mp3 -- done
    */
    
    void Start ()
    {
        myTag = this.tag;
        GetPlayersOnTeam();
	}

    public void GetPlayersOnTeam ()
    {
        playersOnTeam = GameObject.FindGameObjectsWithTag(myTag);
        headsets = new AudioSource[playersOnTeam.Length-1][];

        for (int i = 0, j = 0; i < playersOnTeam.Length; i++) // start at 1 to skip the flag itself
        {
            if (playersOnTeam[i].name.Contains("Flag"))
                continue;

            headsets[j] = playersOnTeam[i].transform.Find("Headset").GetComponents<AudioSource>();
            j++;
        }
    }

    public void BroadcastWeGotTheirFlag ()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][0].Play();
        }
    }

    public void BroadcastWeDroppedTheirFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][1].Play();
        }
    }

    public void BroadcastWePickedUpTheirFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][2].Play();
        }
    }

    public void BroadcastTheyRecoveredTheirFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][3].Play();
        }
    }

    public void BroadcastWeCappedTheirFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][4].Play();
            headsets[i][10].Play();
        }
    }

    public void BroadcastTheyGotOurFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][5].Play();
        }
    }

    public void BroadcastTheyDroppedOurFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][6].Play();
        }
    }

    public void BroadcastTheyPickedUpOurFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][7].Play();
        }
    }

    public void BroadcastWeRecoveredOurFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][8].Play();
        }
    }

    public void BroadcastTheyCappedOurFlag()
    {
        for (int i = 0; i < headsets.Length; i++)
        {
            headsets[i][9].Play();
            headsets[i][11].Play();
        }
    }
}
