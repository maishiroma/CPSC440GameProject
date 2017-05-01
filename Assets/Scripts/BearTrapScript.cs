using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script does the functionality of a basic Bear trap.

public class BearTrapScript : MonoBehaviour {
    
	public bool isActive = false;
	public float minFlinchForce;		// The min amount of force applied vertically
	public float maxFlinchForce;		// The max amount of force applied vertically
	public int trapRange;
	public GameObject sparkParticles;

	private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
    
	// When the alien enters this, the trap is triggered.
	void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Alien" && isActive == false)
        {
			anim.speed = 10;
			isActive = true;
			anim.SetTrigger("Triggered");

			MakeAlienFlinch(other.gameObject);
			Invoke("DestroyTrap",1.5f);
        }
    }

	// "Slightly" flings the alien into the air, to simulate the alien stopping.
	void MakeAlienFlinch(GameObject Obj)
	{
		//GameObject _explosionParticles = (GameObject)Instantiate(explosionParticles, transform.position, Quaternion.identity);

		Vector3 myPos = transform.position;
		myPos.Set(myPos.x, 0, myPos.z);
		Vector3 ObjPos = Obj.transform.position;
		ObjPos.Set(ObjPos.x, 0, ObjPos.z);
		float distance = Vector3.Distance(myPos, ObjPos);

		//Vector3 direction = (Obj.transform.position - transform.position).normalized;

		float t = 1 - (distance / trapRange);
		float force = Mathf.Lerp(minFlinchForce, maxFlinchForce, t);
		if (Obj.layer == 16)
		{
			Obj.transform.parent.position.Set(Obj.transform.parent.position.x, Obj.transform.parent.position.y + 0.1f, Obj.transform.parent.position.z);
			Obj.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Obj.transform.parent.GetComponent<SmallAlienPhysicsManager>().InAir(true);
			Obj.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.up * force;
		}
		else
		{
			Obj.transform.position.Set(Obj.transform.position.x, Obj.transform.position.y + 0.1f, Obj.transform.position.z);
			Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Obj.GetComponent<Rigidbody>().velocity = Vector3.up * force;
		}
	}

	void DestroyTrap()
	{
		Destroy(gameObject);
	}
}
