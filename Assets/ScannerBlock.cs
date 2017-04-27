using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerBlock : MonoBehaviour {

    public Transform Scanner;

	// Use this for initialization
	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localRotation.eulerAngles.Set(Scanner.transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);	
	}
}
