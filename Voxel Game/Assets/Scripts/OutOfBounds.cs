﻿using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Contains("Geck"))
        {
            col.gameObject.GetComponent<OldAstroFirstPersonControl>().Die();
        }
    }
}
