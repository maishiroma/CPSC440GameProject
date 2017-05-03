using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// After a set time, the particle destroys itself.

public class DestroyParticle : MonoBehaviour {

	public float timeToDestroy;

	void Start()
	{
		Invoke("DestroyMe", timeToDestroy);
	}

	void DestroyMe()
	{
		Destroy(gameObject);
	}
	
}
