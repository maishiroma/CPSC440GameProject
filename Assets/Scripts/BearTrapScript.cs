using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapScript : MonoBehaviour {
    
	public bool isActive = false;

	private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
	void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Alien" && isActive == false)
        {
			isActive = true;
			anim.SetTrigger("Triggered");
			other.transform.parent.GetComponent<Health>().dealDamage(10f, true);
			other.transform.parent.GetComponent<SmallAlienPhysicsManager>().impactFlash();
			Invoke("DestroyTrap",3f);
        }
    }

	void DestroyTrap()
	{
		Destroy(gameObject);
	}
}
