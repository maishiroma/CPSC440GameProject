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
            if (go.tag == "Alive")
            {
                go.transform.parent.GetComponent<SmallAlienPhysicsManager>().InAir();
                //go.transform.parent.GetComponent<SmallAlienHealth>().dealDamage(20f);
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

        Vector3 myPos = transform.position;
        myPos.Set(myPos.x, 0, myPos.z);
        Vector3 ObjPos = Obj.transform.position;
        ObjPos.Set(ObjPos.x, 0, ObjPos.z);
        float distance = Vector3.Distance(myPos, ObjPos);

        Vector3 direction = (Obj.transform.position - transform.position).normalized;
        
        //float force = Mathf.Lerp(minExplosionForce, maxExplosionForce,explosionForceFalloff.Evaluate(distance / explosionRange));
        float t = 1 - (distance / explosionRange);
        float force = Mathf.Lerp(minExplosionForce, maxExplosionForce, t);
        //direction.y *= force;
        if (Obj.layer == 16)
        {
            Obj.transform.parent.position.Set(Obj.transform.parent.position.x, Obj.transform.parent.position.y + 0.1f, Obj.transform.parent.position.z);
            Obj.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Obj.transform.parent.GetComponent<SmallAlienPhysicsManager>().InAir(true);
            //Obj.transform.parent.GetComponent<SmallAlienHealth>().dealDamage(30f);
            //Vector3 newForce = Vector3.Scale(direction, new Vector3(.25f, 7f, .25f)) * force;
            //newForce.Set(newForce.x, Mathf.Clamp(newForce.y, .2f, 4), newForce.z);
            //Debug.Log(Vector3.up * force + direction);
            Obj.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.up * force + direction;
            Vector3 torque = new Vector3(Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t));
            Obj.transform.parent.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
        }
        else
        {
            Obj.transform.position.Set(Obj.transform.parent.position.x, Obj.transform.parent.position.y + 0.1f, Obj.transform.parent.position.z);
            Obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Vector3 newForce = Vector3.Scale(direction, new Vector3(.75f, 7f, .75f)) * force;
           // newForce.Set(newForce.x, Mathf.Clamp(newForce.y, 0, 3.5f), newForce.z);
            Obj.GetComponent<Rigidbody>().velocity = Vector3.up * force + direction * (force / 2);
            Vector3 torque = new Vector3(Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t), Random.Range(-rotationalForce * t, rotationalForce * t));
            Obj.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
        }

        
    }    

}
