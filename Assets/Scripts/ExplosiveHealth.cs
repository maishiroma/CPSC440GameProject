using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveHealth : Health
{

    public float explosionRange = 5;
    public float minExplosionForce;
    public float maxExplosionForce;
    public float upwardExplosionForceNormalized = .5f;
    public float rotationalForce = 60;

    public LayerMask explosionMask;

    

    public GameObject impactParticles;
    public GameObject explosionParticles;

    private List<GameObject> objToDestroy = new List<GameObject>();

    public override void impact(Collision collision)
    {
        increaseHealthRateMultiplier();

        Vector3 impactParticlePosition = collision.contacts[0].point;
        Vector3 pointDirection = -collision.contacts[0].normal;

        GameObject _impactParticles = (GameObject)Instantiate(impactParticles, impactParticlePosition, Quaternion.LookRotation(pointDirection), transform);
        objToDestroy.Add(_impactParticles);
    }



    public override void Die()
    {
        GameObject _explosionParticles = (GameObject)Instantiate(explosionParticles, transform.position - (Vector3.up * 0.8f), Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRange, explosionMask);

        foreach(Collider col in hitColliders)
        {
            GameObject go = col.gameObject;
            Vector3 dir = go.transform.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            float dist = Vector3.Distance(go.transform.position, transform.position);
            float t = 1 - dist / explosionRange;
            float force = Mathf.Lerp(minExplosionForce, maxExplosionForce, t);

            if (go.layer == 16)
            {
                go.transform.parent.GetComponent<SmallAlienPhysicsManager>().InAir(true);
                go.transform.parent.GetComponent<SmallAlienHealth>().dealDamage(30f);
                AddExplosiveForce(go.transform.parent.gameObject, Vector3.Scale(dir, new Vector3(.25f,7,.25f)), force * 1.33f);
            }
            else
            {
                AddExplosiveForce(go, dir, force);
            }

        }


        Destroy(gameObject);
        foreach(GameObject go in objToDestroy)
        {
            Destroy(go);
        }
    }

    public void AddExplosiveForce(GameObject go, Vector3 dir, float force)
    {
        Vector3 forceDir = dir;

        //clamp to minimum upward force
        if (dir.y < upwardExplosionForceNormalized)
        {
            //dir.y = upwardExplosionForceNormalized;
        }

        if (go.GetComponent<Rigidbody>())
        {
            go.GetComponent<Rigidbody>().velocity = Vector3.up * force + dir * force / 2f;

            Vector3 torque = new Vector3(Random.Range(-rotationalForce, rotationalForce), Random.Range(-rotationalForce, rotationalForce), Random.Range(-rotationalForce, rotationalForce));
            go.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.Impulse);
        }
        
    }


}
