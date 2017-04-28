using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemGroupScript : MonoBehaviour {

    public GameObject[] gems;
    public float gemSpawnRadius;
    public bool highlightingGems;
	private List<GameObject> spawnedGems = new List<GameObject>(); 

	// Use this for initialization
	void Start ()
    {
        //get reference to parent (breakableRockScript)
        //get local variables of breakable rock's min and max gems
        //call CreateGems(min, max)

        //CreateGems(2, 6);
	}




    public void CreateGems(int minGems, int maxGems)
    {
		int numberOfGems = Random.Range (minGems, maxGems);
		for (int i = 0; i < numberOfGems; i++) 
		{
			Vector3 spawnLocation = transform.position;
			Vector3 offset = Random.insideUnitSphere * gemSpawnRadius;
			spawnLocation += offset;

			int gemToSpawn = Random.Range (0, gems.Length - 1);
			GameObject _gem = (GameObject)Instantiate (gems [gemToSpawn], spawnLocation, Quaternion.identity, transform);
			spawnedGems.Add (_gem);
		}
    }

    public void WakeGems()
    {
        //call from breakable rock script when rock breaks
        //foreach of the gems, set them to active
		foreach(GameObject gem in spawnedGems)
		{
			gem.SetActive (true);
		}
			
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
