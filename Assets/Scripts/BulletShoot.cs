using UnityEngine;
using System.Collections;

public class BulletShoot : MonoBehaviour {

    public float damage = 20f;
    Rigidbody rb;
    public float shootForce = 20;
    public ParticleSystem hitParticles;
    public bool isTurret = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootForce);

        Invoke("Die", 2);
    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }

    void OnCollisionEnter(Collision col)
    {
        ParticleSystem _hitParticles = (ParticleSystem)Instantiate(hitParticles, col.contacts[0].point, Quaternion.LookRotation(col.contacts[0].normal), GameObject.Find("AllBullets").transform);
        _hitParticles.Play();
        
        if(col.gameObject.tag == "Alive")
        {
            col.gameObject.GetComponent<Health>().dealDamage(damage, isTurret);
        }



        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }

   
}
