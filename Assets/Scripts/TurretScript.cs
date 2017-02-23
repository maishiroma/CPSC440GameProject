using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    public Animator turretAnims;
    public float rotateSmoothing = 1f;
    public GameObject TurretPivot;
    public Transform shootPosition;
    public GameObject bullet;
    public float fireRate;
    private bool canShoot = true;
    private bool opened = false;

    public List<GameObject> targets = new List<GameObject>();
    public GameObject currentTarget;
    public float range = 12;

    public LayerMask targetLayers;

    public Health health;

    public void Start()
    {
        turretAnims = gameObject.GetComponent<Animator>();
        
        health = GetComponent<Health>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(((1<<other.gameObject.layer) & targetLayers) != 0)
        {
            //if obj is in targetLayers
            targets.Add(other.gameObject);
            currentTarget = GetNewTarget();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayers) != 0)
        {
            //if obj is in targetLayers
            if (targets.Contains(other.gameObject))
            {
                targets.Remove(other.gameObject);
                if(other.gameObject == currentTarget)
                {
                    currentTarget = GetNewTarget();
                }
            }
        }
    }

    GameObject GetNewTarget()
    {
        //check for objects not in list
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, targetLayers);

        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (!targets.Contains(hitCollider.gameObject))
                {
                    targets.Add(hitCollider.gameObject);
                }
            }
        }


        float bestDist = 0;
        GameObject bestTarget = null;
        List<GameObject> objsToRemove = new List<GameObject>();
        foreach(GameObject go in targets)
        {
            if(go == null)
            {
                objsToRemove.Add(go);
            }
            else
            {
                if (go.GetComponent<Health>())
                {
                    if (go.GetComponent<Health>().dead == false)
                    {
                        float dist = Vector3.Distance(transform.position, go.transform.position);
                        if (bestDist == 0 || dist < bestDist)
                        {
                            bestDist = dist;
                            bestTarget = go;
                        }
                    }
                }
                else
                {
                    float dist = Vector3.Distance(transform.position, go.transform.position);
                    if (bestDist == 0 || dist < bestDist)
                    {
                        bestDist = dist;
                        bestTarget = go;
                    }
                }
            }
        }

        foreach(GameObject go in objsToRemove)
        {
            targets.Remove(go);
        }

        if(bestTarget != null)
        {
            return bestTarget;
        }
        else
        {
            return null;
        }
    }

    // Use this for initialization
    void Shoot ()
    {
        GameObject _bullet = (GameObject)Instantiate(bullet, shootPosition.position, shootPosition.rotation);
	}

    void resetShoot()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (!health.dead)
        {
            
            if (currentTarget == null)
            {
                GameObject target = GetNewTarget();
                if (target != null)
                {
                    currentTarget = target;
                }

            }
            else
            {
                if (!opened)
                {
                    turretAnims.SetBool("Open", true);
                    opened = true;
                }
                rotateTowardsTarget();
                if (canShoot)
                {
                    Shoot();
                    canShoot = false;
                    Invoke("resetShoot", fireRate);
                }
            }
        }
    }

    void rotateTowardsTarget()
    { 
        TurretPivot.transform.rotation = Quaternion.Slerp(TurretPivot.transform.rotation, Quaternion.LookRotation(TurretPivot.transform.position - currentTarget.transform.position), Time.deltaTime * rotateSmoothing);

    }
}
