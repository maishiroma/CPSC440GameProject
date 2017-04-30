using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerParticleManager : MonoBehaviour {

    public ParticleSystem[] scannerParticles;

	// Use this for initialization
	void Start ()
    {
        foreach (ParticleSystem ps in scannerParticles)
        {
            ps.Stop();
        }
    }

    public void EnableParticles()
    {
        foreach (ParticleSystem ps in scannerParticles)
        {
            ps.Play();
        }
    }


    public void DisableParticles()
    {
        foreach (ParticleSystem ps in scannerParticles)
        {
            ps.Stop();
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
