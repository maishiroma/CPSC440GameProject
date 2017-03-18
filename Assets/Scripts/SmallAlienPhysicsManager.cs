using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAlienPhysicsManager : MonoBehaviour {

    Rigidbody rb;
    Animator anims;
    SmallAlienAI ai;
    SmallAlienHealth health;
    AlienNavMeshInterface nav;

    //public vars
    public bool lerpToPlayerOnImpactact;
    public bool flashDamage = true;
    public float impactSlideTime = 1.5f;

    public Color damageColor;
    public float flashTime = .2f;
    public float impactForce = 10f;
    public LayerMask groundMask;
    public float inAirDrag = 0.3f;

    //private vars
    private bool lerp;
    private Color startEmission;
    float startDrag;
    public GameObject mesh;
    bool hit = false;
    bool hitAgain = false;
    bool touchingGround;
    public bool grounded;
    public bool isInAir = false;
    float startY;
    float explosionStartY;

    bool foundLandPos;
    Vector3 landPos;

    // Use this for initialization
    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anims = gameObject.GetComponent<Animator>();
        ai = gameObject.GetComponent<SmallAlienAI>();
        health = gameObject.GetComponent<SmallAlienHealth>();
        nav = gameObject.GetComponent<AlienNavMeshInterface>();

        mesh = gameObject.transform.FindChild("SmallAlienMesh").gameObject;
        startEmission = mesh.GetComponent<SkinnedMeshRenderer>().material.GetColor("_Emission");
        startDrag = gameObject.GetComponent<Rigidbody>().drag;
        startY = transform.position.y;
    }

    void ProjectileImpact(Collision collision)
    {
        Vector3 pushDirection = collision.contacts[0].normal;
        pushDirection.Set(pushDirection.x, 0, pushDirection.z);
        gameObject.GetComponent<Rigidbody>().AddForce(pushDirection * impactForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            touchingGround = true;
            if (isInAir)
            {
                LandOnGround();
            }
        }
        if (collision.gameObject.layer == 8 && !health.dead)
        {
            //SHOT WITH BULLET
            impactFlash();
            ReactToPhysics();
            ProjectileImpact(collision);
            anims.SetBool("Hit", true);

            if (ai.currentState != SmallAlienAI.States.Hit)
            {
                ai.SetState(SmallAlienAI.States.Hit);
            }

            lerp = true;
            Invoke("ProjectileReactionTime", impactSlideTime);
            Invoke("hitDelay", .5f);
            if (hit)
            {
                hitAgain = true;
                anims.SetTrigger("HitAgain");
            }
            else
            {
                hit = true;
            }
        }
    }

    void ProjectileReactionTime()
    {
        StopReactingToPhysics(true);
    }

    public void InAir(bool _isInAir = true)
    {
        if (_isInAir)
        {
            ReactToPhysics();
            grounded = false;
            isInAir = true;
            health.dealDamage(20f);
            impactFlash();
            ai.SetState(SmallAlienAI.States.InAir);
            anims.SetBool("IsInAir", true);
            anims.SetTrigger("ForceToAir");
            explosionStartY = transform.position.y;
            foundLandPos = false;
            rb.drag = inAirDrag;
            
        }
    }


    void LandOnGround()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f, groundMask))
        {
            transform.position = new Vector3(transform.position.x, startY + hit.point.y, transform.position.z);
        }

        StopReactingToPhysics();
        anims.SetBool("IsInAir", false);
        isInAir = false;
        health.dealDamage(20);
        impactFlash();
        grounded = true;
        rb.drag = startDrag;
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
            touchingGround = false;
        }
    }






    public void impactFlash()
    {
        if (flashDamage)
        {
            mesh.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Emission", damageColor);
            Invoke("resetEmission", flashTime);
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
        nav.ToggleNavmeshAgent(false);
    }

    public void StopReactingToPhysics(bool checkForHit = false)
    {
        if (!checkForHit)
        {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        anims.applyRootMotion = true;
        gameObject.GetComponent<Animator>().SetBool("Hit", false);
        lerp = false;
        //Add AI idle choose state
        ai.SetState(SmallAlienAI.States.Navigating);
        }
        else
        {
            if(!hit || hitAgain)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                anims.applyRootMotion = true;
                gameObject.GetComponent<Animator>().SetBool("Hit", false);
                lerp = false;
                //Add AI idle choose state
                ai.SetState(SmallAlienAI.States.Navigating);
                nav.ToggleNavmeshAgent(true);
            }
        }
    }





    void lerpToPlayer()
    {
        Vector3 lookRotation = (Vector3.zero - transform.position);
        lookRotation.Set(lookRotation.x, 0, lookRotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), Time.deltaTime);
    }


    Vector3 GroundPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f, groundMask))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }



    // Update is called once per frame
    void Update ()
    {
		if(lerpToPlayerOnImpactact && lerp)
        {
            lerpToPlayer();
        }

        if (isInAir && rb.velocity.y <= 0)
        {
            CheckForGround();
        }
	}

    private void CheckForGround()
    {
        if (Mathf.Abs(transform.position.y - explosionStartY) >= 2f && !foundLandPos)
        {
            landPos = GroundPoint();
            foundLandPos = true;
        }
        else
        {
            if(Mathf.Abs(transform.position.y - landPos.y + startY) <= 0.05f)
            {
                LandOnGround();
            }
        }
    }
}
