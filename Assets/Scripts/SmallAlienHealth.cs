﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAlienHealth : Health {

    Animator anims;
    SmallAlienAI ai;
    SmallAlienPhysicsManager Physics;
    HitMarkerScript Reticle;

	// Use this for initialization
	void Start ()
    {
        currentHealth = health;
        ai = gameObject.GetComponent<SmallAlienAI>();
        Physics = gameObject.GetComponent<SmallAlienPhysicsManager>();
        anims = gameObject.GetComponent<Animator>();
        Reticle = GameObject.Find("Reticle").GetComponent<HitMarkerScript>();
	}

//    private void Update()
//    {
//
//    }
    


    public override void Die()
    {
        if (Physics.isInAir)
        {
            currentHealth += 10f;
        }
        else if (!Physics.grounded)
        {
            
            anims.SetTrigger("FallToDeath");
            Reticle.Death();
            Invoke("disableCollider", 1);
            dead = true;
            GameObject.Find("AlienSpawners").GetComponent<test_AlienSpawner>().currSpawendAliens--;
            GameObject.Find("AlienSpawners").GetComponent<test_AlienSpawner>().LiveAliens.Remove(this.gameObject);
        }
        else
        {
            anims.SetTrigger("Dead");
            Reticle.Death();
            Invoke("disableCollider", 1);
            dead = true;
            GameObject.Find("AlienSpawners").GetComponent<test_AlienSpawner>().currSpawendAliens--;
            GameObject.Find("AlienSpawners").GetComponent<test_AlienSpawner>().LiveAliens.Remove(this.gameObject);
        }
    }

    void disableCollider()
    {
        Physics.mesh.GetComponent<MeshCollider>().enabled = false;
        Invoke("Destroy", 3f);
        
    }


    void Destroy()
    {
        Destroy(gameObject.GetComponent<AlienNavMeshInterface>()._SmallAlienAgent.gameObject);
        Destroy(gameObject); 
    }

   
}
