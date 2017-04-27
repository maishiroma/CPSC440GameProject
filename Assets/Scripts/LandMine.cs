using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script does the behavior for the trap, in particular, when it is stepped on.

public class LandMine : MonoBehaviour {

	// Copied and pasted from Grenade
	public LayerMask explosionMask;
	public float maxDamage = 60f;
	public float minDamage = 10f;
	public AnimationCurve damageFalloff;
	public float maxExplosionForce = 6f;
	public float minExplosionForce = 1f;
	public float rotationalForce = 60;
	public AnimationCurve explosionForceFalloff;
	public AnimationCurve YForce;
	public float explosionRange = 4f;
	public GameObject explosionParticles;

	public GameObject explosionSound;			// Stores the gameobject that will play the trap sound.

	// When an alien is on here, the trap explodes.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Alien")
		{
			other.transform.parent.GetComponent<Health>().dealDamage(30f);
			other.transform.parent.GetComponent<SmallAlienPhysicsManager>().impactFlash();
			Explode();

			Instantiate(explosionSound,transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}

	// Copied and pasted from Grenade.
	void Explode()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRange, explosionMask);
		foreach(Collider col in hitColliders)
		{
			GameObject go = col.gameObject;
			if (go.tag == "Alive" && !go.transform.parent.GetComponent<SmallAlienHealth>().dead)
			{
				go.transform.parent.GetComponent<SmallAlienPhysicsManager>().InAir();
				AddExplosiveForce(col.gameObject.transform.parent.gameObject);
			}
			else
			{
				AddExplosiveForce(col.gameObject);
			}
		}
	}

	// Copied and pasted from Grenade.
	void AddExplosiveForce(GameObject Obj)
	{

		GameObject _explosionParticles = (GameObject)Instantiate(explosionParticles, transform.position, Quaternion.identity);

		Vector3 myPos = transform.position;
		myPos.Set(myPos.x, 0, myPos.z);
		Vector3 ObjPos = Obj.transform.position;
		ObjPos.Set(ObjPos.x, 0, ObjPos.z);
		float distance = Vector3.Distance(myPos, ObjPos);

		Vector3 direction = (Obj.transform.position - transform.position).normalized;

		//float force = Mathf.Lerp(minExplosionForce, maxExplosionForce,explosionForceFalloff.Evaluate(distance / explosionRange));
		float t = 1 - (distance / explosionRange);
		float force = Mathf.Lerp(minExplosionForce, maxExplosionForce, t);
		//direction.y *= force;
		if (Obj.layer == 16)
		{
			Obj.transform.parent.position.Set(Obj.transform.parent.position.x, Obj.transform.parent.position.y + 0.1f, Obj.transform.parent.position.z);
			Obj.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Obj.transform.parent.GetComponent<SmallAlienPhysicsManager>().InAir(true);
			Obj.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.up * force + direction * 3f;
			Vector3 torque = new Vector3(Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t));
			Obj.transform.parent.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
		}
		else
		{
			Obj.transform.position.Set(Obj.transform.position.x, Obj.transform.position.y + 0.1f, Obj.transform.position.z);
			Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
			//Vector3 newForce = Vector3.Scale(direction, new Vector3(.75f, 7f, .75f)) * force;
			// newForce.Set(newForce.x, Mathf.Clamp(newForce.y, 0, 3.5f), newForce.z);
			Obj.GetComponent<Rigidbody>().velocity = Vector3.up * force + direction * (force / 2);
			Vector3 torque = new Vector3(Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t));
			Obj.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
		}
	}    
}
