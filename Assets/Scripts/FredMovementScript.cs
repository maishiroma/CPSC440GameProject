using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredMovementScript : MonoBehaviour {

    public Transform[] Waypoints;
    private FredWaypoint[] FredWaypoints;
    public float distToReachWaypoint = 3f;
    private Animator FredAnims;
    private enum State {Wandering, Fetching};
    private State currentState;
    private FredWaypoint currentWaypoint;
    private bool readyToMove;
    private bool busy;
    List<FredWaypoint> reachedWaypoints = new List<FredWaypoint>();

    public float AccelerationTime = 3;
    public float maxDistPerSec = 2;
    public AnimationCurve AccelerationCurve;
    public float TurnTime = 3;
    public float turnSpeed = 3;
    public float StoppingTime = 1;

	// Use this for initialization
	void Start ()
    {
        FredAnims = gameObject.GetComponent<Animator>();
        FredWaypoints = new FredWaypoint[Waypoints.Length];
        for(int i = 0; i < Waypoints.Length; i++)
        {
            FredWaypoints[i] = Waypoints[i].GetComponent<FredWaypoint>();
        }

        currentState = State.Wandering;
        readyToMove = true;
        StartCoroutine(Wandering());
	}
	
    IEnumerator Wandering()
    {
        FredWaypoint lastWaypoint = null;

        //If don't have a current waypoint
        if(currentWaypoint == null)
        {
            FredWaypoint bestWaypoint = null;

            //if you have a last waypoint, go to that
            if (lastWaypoint != null)
            {
                bestWaypoint = lastWaypoint;
            }
            else
            {
                //find the closest waypoint and move to that
                float bestDist = 1000;
                foreach (Transform _waypoint in Waypoints)
                {
                    float dist = Vector3.Distance(_waypoint.position, transform.position);
                    if (dist < bestDist)
                    {
                        bestDist = dist;
                        bestWaypoint = _waypoint.GetComponent<FredWaypoint>();
                    }
                }
            }
            
            if(bestWaypoint != null)
            {
                //Move to best Waypoint; not ready to move until you get there
                StartCoroutine(MovingToWaypoint(bestWaypoint));
                yield break;
            }
            
        }
        else
        {
            //you already have a waypoint, so you're on track and ready to move

            if (!reachedWaypoints.Contains(currentWaypoint))
            {
                reachedWaypoints.Add(currentWaypoint);
            }
        }

        //Wandering Update Loop 
        while (true)
        {
            //If ready to move to next wandering waypoint
            if (readyToMove)
            {
                readyToMove = false;
                //find new waypoint by choosing from the waypoints connected to the current waypoint.  Don't choose lastWaypoint, and weight reached waypoints as less weight.
                //find weight for each waypoint, and then add to total weight
                int[] weightedWaypoints = new int[currentWaypoint.connectedWaypoints.Length];
                int weightedTotal = 0;
                for (int i = 0; i < weightedWaypoints.Length; i++)
                {
                    FredWaypoint _potentialWaypoint = currentWaypoint.connectedWaypoints[i];

                    if( lastWaypoint != null && _potentialWaypoint == lastWaypoint)
                    {
                        //you were just there, low weight
                        weightedWaypoints[i] = 1;
                    }

                    if (reachedWaypoints.Contains(_potentialWaypoint))
                    {
                        //you've been there, average weight
                        weightedWaypoints[i] = 2;
                    }
                    else
                    {
                        //new place, highest weight
                        weightedWaypoints[i] = 3;
                    }

                    weightedTotal = weightedTotal + weightedWaypoints[i];

                }

                int randomWeight = Random.Range(0, weightedTotal);
                FredWaypoint targetWaypoint = null;


                //loop through weights until total is greater than randomweight, then select that waypoint
                for(int i = 0; i < weightedWaypoints.Length; i++)
                {
                    int currentWeight = weightedWaypoints[i] + i;
                    if(currentWeight > randomWeight)
                    {
                        targetWaypoint = currentWaypoint.connectedWaypoints[i];
                        break;
                    }
                }

                if(targetWaypoint != null)
                {
                    //StartMovingToWaypoint and set readyToMove to false
                    StartCoroutine(MovingToWaypoint(targetWaypoint));
                    readyToMove = false;
                    yield break;
                }
                else
                {
                    Debug.Log("no good points");
                    StartCoroutine(MovingToWaypoint(currentWaypoint.connectedWaypoints[Random.Range(0, currentWaypoint.connectedWaypoints.Length -1)]));
                    readyToMove = false;
                    yield break;
                }

            }


            yield return new WaitForSeconds(1);
        }



        yield break;
    }


    IEnumerator MovingToWaypoint(FredWaypoint targetWaypoint)
    {

        if(currentState == State.Wandering)
        {
            float startTurnTime = Time.time;
            //turn to look towards destination
            while (Time.time < startTurnTime + TurnTime)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetWaypoint.transform.position - transform.position), Time.deltaTime * turnSpeed);
                yield return null;
            }

            //looking at target, start moving
            Vector3 moveDirection = transform.forward;
            float startAccelerationTime = Time.time;
            float distance = 1000;
            float speed = 0;
            FredAnims.SetBool("Flying", true);
            FredAnims.enabled = false;

            while (distance > distToReachWaypoint)
            {
                float currentSpeed = Mathf.Lerp(0, maxDistPerSec, AccelerationCurve.Evaluate((Time.time - startAccelerationTime) / AccelerationTime));
                speed = currentSpeed;
                transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
                distance = Vector3.Distance(transform.position, targetWaypoint.transform.position);
                yield return null;
            }

            //reached target, slow down and stop
            float startSlowTime = Time.time;
            FredAnims.enabled = true;
            FredAnims.SetBool("Flying", false);
            while (Time.time < startSlowTime + StoppingTime)
            {
                float currentSpeed = Mathf.Lerp(speed, 0, (Time.time - startSlowTime) / StoppingTime);
                transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);


                yield return null;
            }

            //reached waypoint, set as current, add to reached waypoints, and continue wandering.
            currentWaypoint = targetWaypoint;
            reachedWaypoints.Add(currentWaypoint);
            readyToMove = false;
            StartCoroutine(Wandering());
            yield return new WaitForSeconds(3f);
            readyToMove = true;
        }
        else if(currentState == State.Fetching)
        {

        }

        yield break;
    }


	// Update is called once per frame
	void Update () {
		
	}
}
