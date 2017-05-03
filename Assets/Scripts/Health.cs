using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health;
    public float healthRate = 10;
    public float healthRateMultiplier;

    public bool dead;

    public float currentHealth;
	// Use this for initialization
	void Start ()
    {
        currentHealth = health;
        //healthRateMultiplier = 0;
        //dead = false;
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            impact(collision);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(healthRateMultiplier > 0 && !dead)
        {
            if(currentHealth >= 0)
            {
                currentHealth = currentHealth - (healthRate * healthRateMultiplier * Time.deltaTime);
            }
            else
            {
                dead = true;
                Die();
            }
        }
	}

    public void increaseHealthRateMultiplier()
    {
        healthRateMultiplier++;
    }

    public void dealDamage(float damage, bool deathByTrap = false)
    {
        currentHealth -= damage;
        if(currentHealth <= 0 && !dead)
        {
            if (deathByTrap == true)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().incrementKillComboTrap();
            }
            else
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().incrementKillComboGun();

                if (GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerManager>().CurrentNumTraps == 0)
                {
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().incrementKillEnemyNoTrapCount();
                }
            }
            Die();
        }
    }

    virtual public void impact(Collision collision)
    {

    }

    public virtual void Die()
    {
        //Destroy(gameObject);
        //BroadcastMessage("Die");
        if (GetComponent<tempDie>())
        {
            GetComponent<tempDie>().Die();
        }
    }
}
