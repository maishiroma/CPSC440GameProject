using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOuterShellScript : MonoBehaviour {

    BreakableRock Rock;

	// Use this for initialization
	void Start ()
    {
        Rock = transform.parent.GetComponent<BreakableRock>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Rock.BulletImpact(collision);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
