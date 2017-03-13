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


	// Use this for initialization
	void Start ()
    {
        mesh = gameObject.transform.FindChild("SmallAlienMesh").gameObject;
        startEmission = mesh.GetComponent<SkinnedMeshRenderer>().material.GetColor("_Emission");
        anims = gameObject.GetComponent<Animator>();
        currentHealth = health;
        startY = transform.position.y;
        startDrag = gameObject.GetComponent<Rigidbody>().drag;
	}

    public override void impact(Collision collision)
    {    
            mesh.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Emission", damageColor);
            Invoke("resetEmission", flashTime);
            Debug.Log("flash");
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
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            grounded = false;
        }
    }

    void resetEmission()
    {
        mesh.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Emission", startEmission);
    }

    public void ReactToPhysics(float delay = 0.5f)
    {
        anims.applyRootMotion = false;
        anims.SetTrigger("ForceToAir");
        anims.SetBool("IsInAir", true);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        StartCoroutine(CheckIfGrounded(delay));
        gameObject.GetComponent<Rigidbody>().drag = 0.3f;
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
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().drag = startDrag;
        anims.applyRootMotion = true;
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
    }

   
}
