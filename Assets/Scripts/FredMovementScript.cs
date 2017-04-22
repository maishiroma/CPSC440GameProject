using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredMovementScript : MonoBehaviour {

    public Transform[] Waypoints;
    private FredWaypoint[] FredWaypoints;
    public float distToReachWaypoint = 3f;

    private enum State {Wandering, Fetching};
    private State currentState;
    private Transform currentWaypoint;
    private bool readyToMove;
    private bool busy;

	// Use this for initialization
	void Start ()
    {
        FredWaypoints = new FredWaypoint[Waypoints.Length];
        for(int i = 0; i < Waypoints.Length; i++)
        {
            FredWaypoints[i] = Waypoints[i].GetComponent<FredWaypoint>();
        }
	}
	
    IEnumerator Wandering()
    {
        List<Transform> reachedWaypoints = new List<Transform>();
        Transform lastWaypoint;

        //If don't have a current waypoint
        if(currentWaypoint == null)
        {
            float bestDist = 1000;
            Transform bestWaypoint;
            foreach (Transform _waypoint in Waypoints)
            {
                float dist = Vector3.Distance(_waypoint.position, transform.position);
                if(dist < bestDist)
                {
                    bestDist = dist;
                    bestWaypoint = _waypoint;
                }
            }

            //Move to best Waypoint;
            StartCoroutine(MovingToWaypoint());
            readyToMove = false;
        }
        else
        {
            readyToMove = true;
        }

        //Wandering Update Loop 
        while (true)
        {
            //If ready to move to next wandering waypoint
            if (readyToMove)
            {
                //find new waypoint by choosing from the waypoints connected to the current waypoint.  Don't choose lastWaypoint, and weight reached waypoints as less weight.



                //StartMovingToWaypoint and set readyToMove to false
                StartCoroutine(MovingToWaypoint());
                readyToMove = false;
            }


            yield return new WaitForSeconds(1);
        }



        yield break;
    }


    IEnumerator MovingToWaypoint()
    {
        yield break;
    }


	// Update is called once per frame
	void Update () {
		
	}
}
