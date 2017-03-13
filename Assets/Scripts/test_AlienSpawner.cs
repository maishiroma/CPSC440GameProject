using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class test_AlienSpawner : MonoBehaviour {

    public GameObject alienPrefab;
    public float minUpdate = 3;
    public float maxUpdate = 6;
    public float maxNumAliens = 4;
    public int currSpawendAliens = 0;

    public GameObject[] SpawnPoints;

	// Use this for initialization
	void Start ()
    {
        SpawnUpdate();
	}

    float updateTime()
    {
        return Random.Range(minUpdate, maxUpdate);
    }

    void SpawnUpdate()
    {
        StartCoroutine(CheckForSpawn(updateTime()));
    }

    void spawn()
    {
        int randomSpawn = Random.Range(0, SpawnPoints.Length);
        GameObject smallAlien = Instantiate(alienPrefab, SpawnPoints[randomSpawn].transform.position, SpawnPoints[randomSpawn].transform.rotation, GameObject.Find("SpawnedAliens").transform);
        currSpawendAliens++;
    }
	
    bool canSpawn()
    {
        if(currSpawendAliens < maxNumAliens)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator CheckForSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (canSpawn())
        {
            spawn();
            SpawnUpdate();
        }
        else
        {
            SpawnUpdate();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
