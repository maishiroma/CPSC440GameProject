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
    FredWaypoint lastWaypoint = null;
    private bool interupted = false;
    public float scanStopDistance = 1.5f;
    public Animator ScannerAnims;
   
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
                //Move to best Waypoint
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
                        weightedWaypoints[i] = 3;
                    }
                    else if (reachedWaypoints.Contains(_potentialWaypoint))
                    {
                        //you've been there, average weight
                        weightedWaypoints[i] = 4;
                    }
                    else
                    {
                        //new place, highest weight
                        weightedWaypoints[i] = 6;
                    }

                    weightedTotal = weightedTotal + weightedWaypoints[i];

                }

                int randomWeight = Random.Range(0, weightedTotal);
                FredWaypoint targetWaypoint = null;


                //loop through weights until total is greater than randomweight, then select that waypoint
                int counter = 0;
                for(int i = 0; targetWaypoint == null; i++)
                {
                    counter++;
                    if(i == weightedWaypoints.Length)
                    {
                        i = 0;
                    }
                    int currentWeight = weightedWaypoints[i] + counter;
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

            //reached waypoint, set as current, add to reached waypoints
            if(currentWaypoint != null)
            {
                lastWaypoint = currentWaypoint;
            }
            currentWaypoint = targetWaypoint;
            if (!reachedWaypoints.Contains(currentWaypoint))
            {
                reachedWaypoints.Add(currentWaypoint);
            }
            readyToMove = false;
            StartCoroutine(Scanning());
            //yield return new WaitForSeconds(2f);
            //readyToMove = true;
        }
        else if(currentState == State.Fetching)
        {

        }

        yield break;
    }

   


    IEnumerator Scanning()
    {
        ScannableObject[] objectsToScan = currentWaypoint.ObjectsToScan();

        //loop for as many objects to scan
        for(int i = 0; i < objectsToScan.Length; i++)
        {
            if(objectsToScan[i] == null)
            {
                continue;
            }
            //get transform of current scan object
            Vector3 scanPos;
            if(objectsToScan[i].ScanPoint != null)
            {
                scanPos = objectsToScan[i].ScanPoint.position;
            }
            else
            {
                scanPos = objectsToScan[i].transform.position;
            }

            //set scan transform to equal fred's flying height
            scanPos.Set(scanPos.x, transform.position.y, scanPos.z);


            //turn To look at scan pos
            float startTurnTime = Time.time;

            while (Time.time < startTurnTime + TurnTime)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(scanPos - transform.position), Time.deltaTime * turnSpeed);
                yield return null;
            }


            //if not close enough to scan pos, move towards it
            float distance = Vector3.Distance(transform.position, scanPos);
            if(distance > scanStopDistance)
            {
                Vector3 moveDirection = transform.forward;
                float startAccelerationTime = Time.time;
                float speed = 0;
                
                //start moving to scan pos
                while (distance > scanStopDistance)
                {
                    float currentSpeed = Mathf.Lerp(0, maxDistPerSec/2f, AccelerationCurve.Evaluate((Time.time - startAccelerationTime) / AccelerationTime));
                    speed = currentSpeed;
                    transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
                    distance = Vector3.Distance(transform.position, scanPos);
                    yield return null;
                }

                //reached target, slow down and stop
                float startSlowTime = Time.time;
                FredAnims.enabled = true;
                FredAnims.SetBool("Flying", false);
                while (Time.time < startSlowTime + .3f)
                {
                    float currentSpeed = Mathf.Lerp(speed, 0, (Time.time - startSlowTime) / .3f);
                    transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);


                    yield return null;
                }

            }


            //look at rocks
            if(objectsToScan[i].ScanPoint != null)
            {
                float startTurnScanTime = Time.time;
                Vector3 dir = objectsToScan[i].transform.position;
                dir.Set(dir.x, transform.position.y, dir.z);
                dir = dir - transform.position;

                while (Time.time < startTurnScanTime + .6f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * turnSpeed);
                    yield return null;
                }
            }
            


            //scan target
            ScannerAnims.SetTrigger("Scan");
            yield return new WaitForSeconds(4);
            if (objectsToScan[i].containsGems)
            {
                //trigger found gems particles over rocks
            }


            //delay before next iteration
            yield return new WaitForSeconds(3);
        }





        readyToMove = true;
        StartCoroutine(Wandering());

        yield break;
    }



	// Update is called once per frame
	void Update () {
		
	}
}
