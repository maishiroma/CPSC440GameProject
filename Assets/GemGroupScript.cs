using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGroupScript : MonoBehaviour {

    public GameObject[] gems;
    public float gemSpawnRadius;
    public bool highlightingGems;


	// Use this for initialization
	void Start ()
    {
        //get reference to parent (breakableRockScript)
        //get local variables of breakable rock's min and max gems
        //call CreateGems(min, max)
	}


    void CreateGems(int minGems, int maxGems)
    {
        //create random number of gems from min and max
        //spawn gems in random point in unit sphere offset from this.transform
    }

    public void WakeGems()
    {
        //call from breakable rock script when rock breaks
        //foreach of the gems, set them to active
    }

    public void collectGems()
    {
        //Call fred - Fred.CollectGems(Vector 3 position, WaypointScript waypoint)
        //Disable Sphere Collider on Gems
        //Add game sum mass to current materials in player manager
    }

	// Update is called once per frame
	void Update () {
		
	}
}
