using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugColorMat : MonoBehaviour {

    public Color color;

	// Use this for initialization
	void Start ()

    {
        gameObject.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Emission", color);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
