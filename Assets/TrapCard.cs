using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCard : MonoBehaviour {

    public bool visible;
    public bool equipped;
    public Transform trapIconPos;

	// Use this for initialization
	void Start ()
    {
	    	
	}
	
    public void LoadTrapInSlot(GameObject trap)
    {
        Instantiate(trap, trapIconPos.position, Quaternion.identity, GameObject.Find("Icons").transform);
    }


	// Update is called once per frame
	void Update () {
		
	}
}
