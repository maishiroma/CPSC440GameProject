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
    public bool reactingToPhysics = false;
    private bool hitInAir = false;
    private float inAirTime;
    public float airImpactRotation = 15f;

    private float startReactingTime;
    public float minReactTime = .5f;




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
        gameObject.GetComponent<Rigidbody>().velocity = (pushDirection * impactForce);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 10 && isInAir && (Time.time > (startReactingTime + minReactTime)))
        {
                LandOnGround();
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            //Debug.Log("touchingGround");
            touchingGround = true;
            if (isInAir && (Time.time > (startReactingTime + minReactTime)))
            {
                LandOnGround();
            }
        }
        if (collision.gameObject.layer == 8 && !health.dead)
        {
            impactFlash();
            //SHOT WITH BULLET
            if (grounded)
            {
                
                ReactToPhysics();
                ProjectileImpact(collision);
                anims.SetTrigger("Hit");
                hit = true;


                lerp = true;
                Invoke("ProjectileReactionTime", impactSlideTime);
            }
            else if(!hitInAir)
            {
                anims.SetFloat("HitNum", UnityEngine.Random.Range(0, 1));
                anims.SetBool("InAirHit", true);
                hitInAir = true;
                Invoke("resetHitInAir", 0.3f);
                Invoke("resetAirHit", 0.25f);
                rb.AddTorque(new Vector3(UnityEngine.Random.Range(-airImpactRotation, airImpactRotation), UnityEngine.Random.Range(-airImpactRotation, airImpactRotation), UnityEngine.Random.Range(-airImpactRotation, airImpactRotation)));
            }


        }
    }

    void resetHitInAir()
    {
        hitInAir = false;
    }

    void resetAirHit()
    {
        anims.SetBool("InAirHit", false);
    }

    void ProjectileReactionTime()
    {
        if (!isInAir && touchingGround)
        {
            StopReactingToPhysics();
        }
        hit = false;
    }

    public void InAir(bool _isInAir = true)
    {
        if (_isInAir)
        {
            if (!reactingToPhysics)
            {
                Debug.Log("React To Physics!");
                ReactToPhysics();
            }
            inAirTime = Time.time;
            grounded = false;
            isInAir = true;
            health.dealDamage(20f);
            impactFlash();
            lerp = false;
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
        Debug.Log("Land!");
        //anims.applyRootMotion = true;
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        


        StopReactingToPhysics();
        anims.SetBool("IsInAir", false);
        isInAir = false;
        health.dealDamage(20);
        impactFlash();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 3f, groundMask))
        {
            Debug.Log("snap to ground");
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            transform.rotation.eulerAngles.Set(0, transform.rotation.eulerAngles.y, 0);
        }
       

        grounded = true;
        rb.drag = startDrag;
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10 && !hit)
        {
            //touchingGround = false;
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

    public void ReactToPhysics()
    {
        startReactingTime = Time.time;
        anims.applyRootMotion = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        nav.ToggleNavmeshAgent(false);
        reactingToPhysics = true;
    }

    public void StopReactingToPhysics()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //anims.applyRootMotion = true;
        lerp = false;
        //Add AI idle choose state
        ai.SetState(SmallAlienAI.States.Navigating);
        nav.ToggleNavmeshAgent(true);
        reactingToPhysics = false;
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

        if (isInAir && rb.velocity.y <= 0 && (Time.time > (startReactingTime + minReactTime)))
        {
            Debug.Log("Checked For Ground!");
            CheckForGround();
        }

        if (isInAir)
        {
            anims.SetFloat("InAirTime", Mathf.Lerp(0, 1, (Time.time - inAirTime) / 1.5f));
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
