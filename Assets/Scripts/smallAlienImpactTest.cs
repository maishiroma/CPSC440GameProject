using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallAlienImpactTest : MonoBehaviour {

    bool lerp = false;
    bool hit = false;
    bool hitAgain = false;

	// Use this for initialization
	void Start () {
		
	}

    public float force = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Vector3 pushDirection = collision.contacts[0].normal;
            pushDirection.Set(pushDirection.x, 0, pushDirection.z);
            gameObject.GetComponent<Animator>().applyRootMotion = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * force);
            gameObject.GetComponent<Animator>().SetBool("Hit", true);
            Invoke("restore", 1.5f);
            Invoke("hitDelay", .5f);
            lerp = true;
            if (hit)
            {
                hitAgain = true;
                gameObject.GetComponent<Animator>().SetBool("HitAgain", true);
            }
            else
            {
                hit = true;
            }
        }
    }

    void restore()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
        gameObject.GetComponent<Animator>().applyRootMotion = true;
        lerp = false;

    }

    void hitDelay()
    {
        if (hitAgain)
        {
            hitAgain = false;
            gameObject.GetComponent<Animator>().SetBool("HitAgain", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Hit", false);
            hit = false;
        }
    }

    // Update is called once per frame
    void Update ()
    {

        if (lerp)
        {
            Vector3 lookRotation = (Vector3.zero - transform.position);
            lookRotation.Set(lookRotation.x, 0, lookRotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime);
        }
	}
}
