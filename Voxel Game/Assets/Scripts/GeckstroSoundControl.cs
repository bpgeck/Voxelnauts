using UnityEngine;
using System.Collections;

public class GeckstroSoundControl : MonoBehaviour {
    AudioSource gunAudioSource;
    AudioSource runDirtAudioSource;
    AudioSource walkDirtAudioSource;
    AudioSource runMetalAudioSource;
    AudioSource walkMetalAudioSource;

    GameObject groundDetector;
    Ray downRay;
    RaycastHit whatWalkingOn;

    Animator animatorBools;

    // Use this for initialization
    void Start ()
    {
        groundDetector = this.transform.Find("Ground Detector").gameObject;

        animatorBools = this.transform.Find("geckstronautAnimatedWithGun").GetComponent<Animator>();

        gunAudioSource = this.transform.Find("geckstronautAnimatedWithGun/SpaceAR:SpaceAR:Mesh/Gun Sounds").gameObject.GetComponents<AudioSource>()[0];
        runDirtAudioSource = this.transform.Find("Feet Sounds").gameObject.GetComponents<AudioSource>()[0];
        walkDirtAudioSource = this.transform.Find("Feet Sounds").gameObject.GetComponents<AudioSource>()[1];
        runMetalAudioSource = this.transform.Find("Feet Sounds").gameObject.GetComponents<AudioSource>()[2];
        walkMetalAudioSource = this.transform.Find("Feet Sounds").gameObject.GetComponents<AudioSource>()[3];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (animatorBools.GetBool("Shooting") && !gunAudioSource.isPlaying) // if you START shooting, start the gun audio
        {
            gunAudioSource.Play();
        }
        else if (!animatorBools.GetBool("Shooting")) // if you let go of the mouse button, stop the audio
        {
            gunAudioSource.Stop();
        }

        downRay = new Ray(groundDetector.transform.position, -this.transform.up);
        Physics.Raycast(downRay, out whatWalkingOn); // cast one ray downward to see the surface below us

        if (whatWalkingOn.distance < 2) // check to see if you are on the ground
        {
            if (whatWalkingOn.collider.tag == "Chunk") // if you are walking on a chunk, play the dirt sounds (chunk is moon rock)
            {
                if (animatorBools.GetBool("Running") && !runDirtAudioSource.isPlaying) // check if running
                {
                    runDirtAudioSource.Play();

                    walkDirtAudioSource.Stop();
                    runMetalAudioSource.Stop();
                    walkMetalAudioSource.Stop();
                }
                else if (!animatorBools.GetBool("Running")) // stopped running
                {
                    runDirtAudioSource.Stop();
                }

                if (animatorBools.GetBool("Walking") && !walkDirtAudioSource.isPlaying) // check if started walking on dirt
                {
                    walkDirtAudioSource.Play();

                    runDirtAudioSource.Stop();
                    runMetalAudioSource.Stop();
                    walkMetalAudioSource.Stop();
                }
                else if (!animatorBools.GetBool("Walking")) // stopped walking
                {
                    walkDirtAudioSource.Stop();
                }
            }
            else
            {
                if (animatorBools.GetBool("Running") && !runMetalAudioSource.isPlaying) // check if started running on metal
                {
                    runMetalAudioSource.Play();

                    runDirtAudioSource.Stop();
                    walkDirtAudioSource.Stop();
                    walkMetalAudioSource.Stop();
                }
                else if (!animatorBools.GetBool("Running")) // check if stopped running
                {
                    runMetalAudioSource.Stop();
                }

                if (animatorBools.GetBool("Walking") && !walkMetalAudioSource.isPlaying)
                {
                    walkMetalAudioSource.Play();

                    runDirtAudioSource.Stop();
                    walkDirtAudioSource.Stop();
                    runMetalAudioSource.Stop();
                }
                else if (!animatorBools.GetBool("Walking"))
                {
                    walkMetalAudioSource.Stop();
                }
            }
        }
        else
        {
            runDirtAudioSource.Stop();
            walkDirtAudioSource.Stop();
            runMetalAudioSource.Stop();
            walkMetalAudioSource.Stop();
        }
    }
}
