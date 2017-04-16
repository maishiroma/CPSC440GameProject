using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will freeze any enemy that is nearby. Uses most of the code from ExplosiveBarrel.

public class FreezeBarrel : Health {

	public float explosionRange = 5;
	public LayerMask explosionMask;

	public GameObject impactParticles;
	public GameObject explosionParticles;
	public GameObject explosionSound;			// Contains the object that plays the sound for this trap

	private List<GameObject> objToDestroy = new List<GameObject>();

	public override void impact(Collision collision)
	{
		increaseHealthRateMultiplier();

		Vector3 impactParticlePosition = collision.contacts[0].point;
		Vector3 pointDirection = -collision.contacts[0].normal;

		GameObject _impactParticles = (GameObject)Instantiate(impactParticles, impactParticlePosition, Quaternion.LookRotation(pointDirection), transform);
		objToDestroy.Add(_impactParticles);
	}

	public override void Die()
	{
		GameObject _explosionParticles = (GameObject)Instantiate(explosionParticles, transform.position - (Vector3.up * 0.8f), Quaternion.identity);
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRange, explosionMask);

		foreach(Collider col in hitColliders)
		{
			GameObject go = col.gameObject;

			if (go.layer == 16)
			{
				// Insert code here that will freeze enemies
				go.transform.parent.GetComponent<SmallAlienHealth>().dealDamage(30f);
			}

		}

		// Creates the object that will play the sound at this trap's location.
		Instantiate(explosionSound,transform.position, Quaternion.identity);
		Destroy(gameObject);
		foreach(GameObject go in objToDestroy)
		{
			Destroy(go);
		}
	}
}
