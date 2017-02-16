using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramElement : MonoBehaviour {

    private MeshRenderer mr;
    private bool visible = false;
    public string HologramMaskTag;

	// Use this for initialization
	void Start ()
    {
        mr = gameObject.GetComponent<MeshRenderer>();	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == HologramMaskTag)
        {
            if(visible == false)
            {
                visible = true;
                toggleVisibility();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == HologramMaskTag)
        {
            if(visible == true)
            {
                visible = false;
                toggleVisibility();
            }
        }
    }


    void toggleVisibility()
    {
        if (visible)
        {
            mr.enabled = true;
        }
        else if (!visible)
        {
            mr.enabled = false;
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
