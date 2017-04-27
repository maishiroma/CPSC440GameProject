using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredWaypoint : MonoBehaviour {

    public int WaypointNumber;
    public FredWaypoint[] connectedWaypoints;
    public List<ScannableObject> scannableObjects;

    public int minScans;
    public int maxScans;


    
    public ScannableObject[] ObjectsToScan()
    {
        int numberOfScans = Random.Range(minScans, maxScans);
        List<ScannableObject> unchosenScanObjs = new List<ScannableObject>();
        List<ScannableObject> objsToRemove = new List<ScannableObject>();
        foreach(ScannableObject so in scannableObjects)
        {
            if(so != null)
            {
                unchosenScanObjs.Add(so);
            }
            else
            {
                objsToRemove.Add(so);
            }
        }


        for (int i = 0; i < objsToRemove.Count; i++)
        {
            scannableObjects.Remove(objsToRemove[i]);
        }

        numberOfScans = Mathf.Clamp(numberOfScans, 1, scannableObjects.Count);
        ScannableObject[] chosenScanObjects = new ScannableObject[numberOfScans];

        for (int i = 0; i < numberOfScans; i++)
        {
            int randomNum = Random.Range(0, unchosenScanObjs.Count);
            chosenScanObjects[i] = unchosenScanObjs[randomNum];
            unchosenScanObjs.Remove(unchosenScanObjs[randomNum]);
        }

        return chosenScanObjects;
    }

    public FredWaypoint ConnectedWaypointClosestTo(FredWaypoint targetWaypoint, FredWaypoint lastFredWaypoint = null)
    {
        

        if(connectedWaypoints.Length == 1)
        {
            return lastFredWaypoint;
        }
        else
        {
            float bestDist = 1000;
            FredWaypoint bestWaypoint = null;

            for (int i = 0; i < connectedWaypoints.Length; i++)
            {
                float dist = Vector3.Distance(connectedWaypoints[i].transform.position, targetWaypoint.transform.position);

                if(dist < bestDist && connectedWaypoints[i] != lastFredWaypoint)
                {
                    bestDist = dist;
                    bestWaypoint = connectedWaypoints[i];
                }
            }

            return bestWaypoint;
        }
     

    }
 



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
