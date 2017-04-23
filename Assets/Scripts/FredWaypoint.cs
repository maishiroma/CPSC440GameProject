using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredWaypoint : MonoBehaviour {

    public int WaypointNumber;
    public FredWaypoint[] connectedWaypoints;
    public Transform[] ScannableObjects;


    private void OnDrawGizmos()
    {
        foreach(FredWaypoint waypoint in connectedWaypoints)
        {
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, waypoint.transform.position);
        }   
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
