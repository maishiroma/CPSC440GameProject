using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 24)
        {
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
