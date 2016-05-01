using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
{
    public Behaviour[] thingsToEnable;

    bool isAlive = true;
    float lerpSmoothing = 5f;

    Vector3 position;
    Quaternion rotation;
    Animator anim;

	void Start()
    {
        anim = this.GetComponentInChildren<Animator>();

        SetTagAndColor();

        if (photonView.isMine) // this is the equivalent to isLocalPlayer
        {
            EnableThings();
        }
        else
        {
            StartCoroutine("Alive"); // lerp all motion to make it smooth
        }

	}
	
	void Update()
    {
	
	}

    void SetTagAndColor()
    {
        FlagAudioBroadcaster[] temps = GameObject.FindObjectsOfType<FlagAudioBroadcaster>();
        GameObject[] flags = { temps[0].gameObject, temps[1].gameObject };

        if (Vector3.Distance(this.transform.position, flags[0].transform.position) < Vector3.Distance(this.transform.position, flags[1].transform.position)) // check which flag the player spawned closer to
        {
            this.gameObject.tag = flags[0].tag;
            flags[0].GetComponent<FlagAudioBroadcaster>().GetPlayersOnTeam(); // tell the flag to update its teammates
        }
        else
        {
            this.gameObject.tag = flags[1].tag;
            flags[1].GetComponent<FlagAudioBroadcaster>().GetPlayersOnTeam(); // tell the flag to update its teammates
        }

        this.gameObject.GetComponent<SetTeamColor>().SetColor(); // set the players color based on his tag
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) // use this to allow for smooth lerping
    {
        if (stream.isWriting) // sending things to all other clients
        {
            // sending transform to other clients
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            // sending animations to other clients
            stream.SendNext(anim.GetBool("HasGun"));
            stream.SendNext(anim.GetBool("Idle"));
            stream.SendNext(anim.GetBool("Walking"));
            stream.SendNext(anim.GetBool("Running"));
            stream.SendNext(anim.GetBool("Shooting"));

            // sending score to other clients
            //stream.SendNext(scoreboard.GetComponent<Scoreboard>().score_b);
            //stream.SendNext(scoreboard.GetComponent<Scoreboard>().score_c);
        }
        else // getting things from all other clients
        {
            // getting transforms from other clients
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();

            // getting animations from other clients
            anim.SetBool("HasGun", (bool)stream.ReceiveNext());
            anim.SetBool("Idle", (bool)stream.ReceiveNext());
            anim.SetBool("Walking", (bool)stream.ReceiveNext());
            anim.SetBool("Running", (bool)stream.ReceiveNext());
            anim.SetBool("Shooting", (bool)stream.ReceiveNext());

            // getting score from other clients
            //scoreboard.GetComponent<Scoreboard>().score_b = (int)stream.ReceiveNext();
            //scoreboard.GetComponent<Scoreboard>().score_c = (int)stream.ReceiveNext();
        }
    }

    void EnableThings()
    {
        for (int i = 0; i < thingsToEnable.Length; i++)
        {
            thingsToEnable[i].enabled = true;
        }
    }

    IEnumerator Alive()
    {
        while (isAlive)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * lerpSmoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * lerpSmoothing);

            yield return null;
        }
    }
}
