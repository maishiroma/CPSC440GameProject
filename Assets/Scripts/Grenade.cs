using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timeAfterHitGround = .5f;
    public float maxTimeAfterThrow = 4f;
    public float explosionRange = 4f;
    public LayerMask explosionMask;
    public float maxDamage = 60f;
    public float minDamage = 10f;
    public AnimationCurve damageFalloff;
    public float maxExplosionForce = 6f;
    public float minExplosionForce = 1f;
    public float rotationalForce = 60;
    public AnimationCurve explosionForceFalloff;
    public AnimationCurve YForce;

    private bool hitGround = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            hitGround = true;
            StartCoroutine(Explode(timeAfterHitGround));
        }
        else if(collision.gameObject.layer == 8)
        {
            StartCoroutine(Explode(.15f));
        }

        
    }


    IEnumerator Explode(float delay = 0)
    {
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(delay);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRange, explosionMask);
        foreach(Collider col in hitColliders)
        {
            GameObject go = col.gameObject;
            if (go.tag == "Alien")
            {
                go.transform.parent.GetComponent<SmallAlienHealth>().ReactToPhysics(.5f);
                go.transform.parent.GetComponent<SmallAlienHealth>().dealDamage(20f);
                Collision collision = new Collision();
                go.transform.parent.GetComponent<SmallAlienHealth>().impact(collision);
                AddExplosiveForce(col.gameObject.transform.parent.gameObject);
            }
            else
            {
                AddExplosiveForce(col.gameObject);
            }
        }
        Destroy(gameObject);
    }
    
    void AddExplosiveForce(GameObject Obj)
    {
        
        Rigidbody rb = Obj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        float distance = Vector3.Distance(transform.position, Obj.transform.position);

        Vector3 direction = (Obj.transform.position - transform.position).normalized;
        direction.y = Mathf.Clamp(direction.y, 0.5f, 1);
        float force = Mathf.Lerp(minExplosionForce, maxExplosionForce,explosionForceFalloff.Evaluate(distance / explosionRange));
        
        Vector3 newForce = Vector3.Scale(direction, new Vector3(.75f, 4f, .75f)) * force;
        //newForce.y = Mathf.Clamp(newForce.y, 0.5f, 7f);
        rb.AddForce(newForce, ForceMode.VelocityChange);
        
        Vector3 torque = new Vector3(Random.Range(-rotationalForce, rotationalForce), Random.Range(-rotationalForce, rotationalForce), Random.Range(-rotationalForce, rotationalForce));
        Obj.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
    }    

}
