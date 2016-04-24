using UnityEngine;
using System.Collections;

public class AsteroidSoundControl : MonoBehaviour {
    AudioSource explosionSource;

	// Use this for initialization
	void Start ()
    {
        explosionSource = this.GetComponents<AudioSource>()[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
	        
	}

    void OnCollisionEnter(Collision col)
    {
        explosionSource.Play();
    }

}
