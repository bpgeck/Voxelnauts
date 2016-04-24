using UnityEngine;
using System.Collections;

public class GeckstroMenu : MonoBehaviour {
    Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = this.transform.Find("geckstronautAnimatedWithGun").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnMouseDown()
    {
        animator.SetInteger("AnimNum", animator.GetInteger("AnimNum") + 1);
        if (animator.GetInteger("AnimNum") > 6)
        {
            animator.SetInteger("AnimNum", 0);
        }
    }
}
