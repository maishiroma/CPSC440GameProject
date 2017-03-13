using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowableObject : MonoBehaviour {

    public GameObject throwableObject;
    public Transform throwTransform;
    public float rotationalForce;
    public bool ready;
    public bool objectThrown = false;

    void Start()
    {
        throwTransform = GameObject.Find("ThrowPoint").transform;
        ready = true;
        objectThrown = false;
    }

    public void throwObject(GameObject throwableObject)
    {
        Vector3 throwVector = GameUtilities.ProjectileMotionIV(transform.position, throwTransform.position, 0f);


        GameObject _throwableObejct = (GameObject)Instantiate(throwableObject, throwTransform.position, Quaternion.identity, GameObject.Find("AllBullets").transform);
        _throwableObejct.GetComponent<Rigidbody>().AddForce(throwVector, ForceMode.Impulse);

        Vector3 torque = new Vector3(Random.Range(-rotationalForce, rotationalForce), Random.Range(-rotationalForce, rotationalForce), Random.Range(-rotationalForce, rotationalForce));
        _throwableObejct.GetComponent<Rigidbody>().AddTorque(torque);

        objectThrown = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ready && !objectThrown)
        {
            throwObject(throwableObject);
            objectThrown = true;
        }
	}
}
