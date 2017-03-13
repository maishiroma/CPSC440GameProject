using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramElement : MonoBehaviour {

    private MeshRenderer mr;
    private bool visible = false;
    public string HologramMaskTag;
    bool children = false;

	// Use this for initialization
	void Start ()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            mr = gameObject.GetComponent<MeshRenderer>();

        }
        else
        {
            children = true;
        }
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
            if (!children)
            {
                mr.enabled = true;
            }
            else
            {
                for(int i = 0; i < gameObject.transform.childCount; i++)
                {
                    GameObject Go = this.gameObject.transform.GetChild(i).gameObject;
                    if(Go.GetComponent<MeshRenderer>()!= null)
                    {
                        Go.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
        }
        else if (!visible)
        {
            if (!children)
            {
                mr.enabled = false;
            }
            else
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    GameObject Go = this.gameObject.transform.GetChild(i).gameObject;
                    if (Go.GetComponent<MeshRenderer>() != null)
                    {
                        Go.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
