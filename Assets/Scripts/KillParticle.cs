using UnityEngine;
using System.Collections;

public class KillParticle : MonoBehaviour {

    public float time = 1f;

	// Use this for initialization
	void Start ()
    {
        Invoke("kill", time);
	}
	
	// Update is called once per frame
	void kill ()
    {
        Destroy(gameObject);
	}
}
