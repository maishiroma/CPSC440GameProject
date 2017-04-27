using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will do chip damage to any alien that's in its range. The more ThunderRods there are, the bigger the damage.

public class ThunderRod : MonoBehaviour {

	public float damageRate = 1f;		// How much damage does this trap do while the enemy is in the trap's range?

	private Health trapHealth;			// How long does this trap stay out?

	void Awake()
	{
		trapHealth = GetComponent<Health>();
	}

	// When an alien comes into range, it'll slowly lose health.
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Alien")
		{
			other.transform.parent.GetComponent<SmallAlienHealth>().healthRateMultiplier = damageRate;
		}

		// Code where if another ThunderRod is nearby, the damageRate increases.
	}

	// When an alien is out of range, it'll stop losing health
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Alien")
		{
			other.transform.parent.GetComponent<SmallAlienHealth>().healthRateMultiplier = 0f;
		}

		// Code where if another ThunderRod is nearby, the damageRate decreases.

	}
}
