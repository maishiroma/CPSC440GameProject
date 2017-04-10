using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAlienObjectAvoider : MonoBehaviour {

    AlienNavMeshInterface Nav;
    private int objs = 0;


	// Use this for initialization
	void Start ()
    {
        Nav = transform.parent.GetComponent<AlienNavMeshInterface>();	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 16 || other.gameObject.layer == 12)
        {
            objs++;
            if(objs == 1)
            {
                Nav.ObjectInWay = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 16 || other.gameObject.layer == 12)
        {
            Nav.ObjectInWay = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 16 || other.gameObject.layer == 12)
        {
            objs--;
            if(true)
            {
                Nav.ObjectInWay = false;
            }
        }
    }



    // Update is called once per frame
    void Update ()
    {
		
	}
}
