using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour {

    public bool visible = false;

    public Color[] markerColors;

    public LayerMask groundMask;

    public LayerMask hitMask;

    public float maxPlacementDist = 5f;

    public float overlapRadius = 1f;

    Material mat;

    Camera mainCam;

    public bool holdingTrigger;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        SetMarkerState("yes");
        mainCam = Camera.main;
    }

	// Use this for initialization
	public void SetMarkerState (string state)
    {
	    if(state == "off")
        {
            visible = false;
            GetComponent<Renderer>().enabled = false;
            visible = false;
        }
        else if(state == "no")
        {
            visible = true;
            GetComponent<Renderer>().enabled = true;
            mat.color = markerColors[0];
        }
        else if(state == "yes")
        {
            visible = true;
            GetComponent<Renderer>().enabled = true;
            mat.color = markerColors[1];
        }
	}
	
    void Update()
    {
        if (!holdingTrigger)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, maxPlacementDist, groundMask))
            {
                SetMarkerPos(hit.point);
            }
            else
            {
                SetMarkerState("off");
            }
        }
        

        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            if (holdingTrigger)
            {
                holdingTrigger = false;
            }
        }

    }


	// Update is called once per frame
	public void SetMarkerPos (Vector3 pos)
    {
        Vector3 newPosition = new Vector3(pos.x, pos.y, pos.z);
        transform.position = newPosition;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius, hitMask);
        if(hitColliders.Length > 0)
        {
            SetMarkerState("no");
        }
        else
        {
            SetMarkerState("yes");
        }
	}
}
