using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAlienHealth : Health {

    Animator anims;

    public Color damageColor;
    public float flashTime = .2f;
    private Color startEmission;
    private GameObject mesh;

    float startY;
    float startDrag;

    public float impactForce = 10f;
    bool hit = false;
    bool hitAgain = false;
    SmallAlienAI ai;

    private bool lerp = false;

	// Use this for initialization
	void Start ()
    {
        mesh = gameObject.transform.FindChild("SmallAlienMesh").gameObject;
        startEmission = mesh.GetComponent<SkinnedMeshRenderer>().material.GetColor("_Emission");
        anims = gameObject.GetComponent<Animator>();
        currentHealth = health;
        startY = transform.position.y;
        startDrag = gameObject.GetComponent<Rigidbody>().drag;
        ai = gameObject.GetComponent<SmallAlienAI>();
	}

    private void Update()
    {

        if (lerp)
        {
            Vector3 lookRotation = (Vector3.zero - transform.position);
            lookRotation.Set(lookRotation.x, 0, lookRotation.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime);
        }
    }

    public override void impact(Collision collision)
    {    
            mesh.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Emission", damageColor);
            Invoke("resetEmission", flashTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            grounded = true;
        }
        if(collision.gameObject.layer == 8 && !dead)
        {
            impact(collision);
            Vector3 pushDirection = collision.contacts[0].normal;
            pushDirection.Set(pushDirection.x, 0, pushDirection.z);
            ReactToPhysics();
            gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * impactForce);
            gameObject.GetComponent<Animator>().SetBool("Hit", true);

            if(ai.currentState != SmallAlienAI.States.Hit)
            {

                ai.SetState(SmallAlienAI.States.Hit);
            }

            lerp = true;
            Invoke("StopReactingToPhysics", 1.5f);
            Invoke("hitDelay", .5f);
            if (hit)
            {
                hitAgain = true;
                gameObject.GetComponent<Animator>().SetTrigger("HitAgain");
            }
            else
            {
                hit = true;
            }
        }
    }


    void hitDelay()
    {
        if (hitAgain)
        {
            hitAgain = false;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Hit", false);
            hit = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10 && !hit)
        {
            grounded = false;
            //anims.SetTrigger("ForceToAir");
            //anims.SetBool("IsInAir", true);
            StartCoroutine(CheckIfGrounded(0.5f));
        }
    }

    void resetEmission()
    {
        mesh.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Emission", startEmission);
    }

    public void ReactToPhysics(float delay = 0.5f)
    {
        anims.applyRootMotion = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //gameObject.GetComponent<Rigidbody>().drag = 0.3f;
    }

    public void StopReactingToPhysics()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.GetComponent<Rigidbody>().drag = startDrag;
        anims.applyRootMotion = true;
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
        lerp = false;
        ai.SetState(SmallAlienAI.States.Navigating);
    }




    public bool grounded;
    

    void waitAndCheckAgain()
    {
        StartCoroutine(CheckIfGrounded(.05f));
    }

    IEnumerator CheckIfGrounded(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (grounded)
        {
            LandOnGround();
        }
        else
        {
            waitAndCheckAgain();
        }

    }

    void LandOnGround()
    {
        transform.rotation= Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        StopReactingToPhysics();
        anims.SetBool("IsInAir", false);
        dealDamage(10);
        Collision collision = new Collision();
        impact(collision);
    }

    public override void Die()
    {
        if (!grounded)
        {
            currentHealth += 10f;
        }
        else
        {
            anims.SetTrigger("Dead");
            Invoke("disableCollider", 1);
            dead = true;
            GameObject.Find("AlienSpawners").GetComponent<test_AlienSpawner>().currSpawendAliens--;
        }  
    }

    void disableCollider()
    {
        mesh.GetComponent<MeshCollider>().enabled = false;
        Invoke("Destroy", 3f);
    }


    void Destroy()
    {
        Destroy(gameObject);
        Destroy(gameObject.GetComponent<AlienNavMeshInterface>()._SmallAlienAgent.gameObject);
    }

   
}
