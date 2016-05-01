using UnityEngine;
//using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    AstroFirstPersonControl[] players;
    FlagAudioBroadcaster[] flags;
    //NetworkManager nm;
    bool update = false;
    //public subPlayer sub = new subPlayer();

    int[] teamSize = { 0, 0 };

    void Start()
    {
        //nm = FindObjectOfType<NetworkManager> ();
    }

    void Update()
    {
        //Debug.LogError (nm.numPlayers);
        //if (nm.numPlayers != teamSize [0] + teamSize [1]) {
        players = FindObjectsOfType<AstroFirstPersonControl>();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].gameObject.tag == "Untagged")
            {
                if (teamSize[0] <= teamSize[1])
                {
                    players[i].gameObject.tag = "Burgundy";
                    players[i].gameObject.GetComponent<SetTeamColor>().SetColor();
                    teamSize[0]++;
                }
                else {
                    players[i].gameObject.tag = "Cerulean";
                    players[i].gameObject.GetComponent<SetTeamColor>().SetColor();
                    teamSize[1]++;
                }
            }
        }
    }
}
