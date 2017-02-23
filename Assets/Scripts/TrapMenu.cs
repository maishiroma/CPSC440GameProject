using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMenu : MonoBehaviour {

    private Camera mainCam;

    public Animator radialMenuAnimator;
    private bool isOpen;
    private Transform radialMenuLocation;

	// Use this for initialization
	void Start ()
    {
        mainCam = Camera.main;

        radialMenuAnimator = GameObject.Find("radialMenu").GetComponent<Animator>();

        radialMenuLocation = GameObject.Find("RadialMenuLocation").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.LookRotation((mainCam.transform.position - transform.position).normalized);
        


        if(radialMenuAnimator.GetCurrentAnimatorClipInfo(0).ToString() == "Closed" && isOpen)
        {
            radialMenuAnimator.gameObject.SetActive(false);
            isOpen = false;
        }



	}

    public void toggleRadialMenu()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            transform.position = radialMenuLocation.position;
            radialMenuAnimator.gameObject.SetActive(true);
            radialMenuAnimator.SetBool("Open", true); 
        }
        else
        {
            radialMenuAnimator.SetBool("Open", false);
        }
    }

}
