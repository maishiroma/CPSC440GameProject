using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour {

    public int minDropAmt = 30;
    public int maxDropAmt = 78;
    public int DropAmt;

    GemGroupScript GemGroup;

	// Use this for initialization
	void Start ()
    {
        DropAmt = Random.Range(minDropAmt, maxDropAmt);
	}
	
    public void ScanGem()
    {
        //GemGroupScript > CollectGems()
    }

    public void Highlight()
    {
        //if GemGroupScript > not highlighted ...  highlight()
    }

	// Update is called once per frame
	void Update () {
		
	}
}
