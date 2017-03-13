using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTurnOffVR : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<GvrViewer>().VRModeEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
