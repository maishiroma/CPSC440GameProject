using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public int MaxNumTraps = 10;
    public int StartingNumTraps = 20;
    public int CurrentNumTraps;
    public Text NumberOfTraps;
    public string MaterialName = "Triphosphite";
    public string MaterialUnits = "g";
    public float collectedMaterial;
    public float startCollectedMaterial = 0;
    public int scansToFinishLevel = 20;
    public int currentNumberOfScans = 0;


    public void ScanData()
    {
        currentNumberOfScans++;
    }
    





	// Use this for initialization
	void Start ()
    {
        CurrentNumTraps = StartingNumTraps;
        NumberOfTraps.text = CurrentNumTraps.ToString();
        collectedMaterial = startCollectedMaterial;
    }

    public void IncrementNumTraps(bool Increment = true)
    {
        if (!Increment)
        {
            CurrentNumTraps--;
            NumberOfTraps.text = CurrentNumTraps.ToString();
        }
        else
        {
            CurrentNumTraps++;
            NumberOfTraps.text = CurrentNumTraps.ToString();
        }
    }




	// Update is called once per frame
	void Update () {
		
	}
}
