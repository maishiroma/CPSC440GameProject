using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {

    public float PlayerStartHealth = 100;
    public float HealthRegeneratedPerSecond;
    public float standardRegenDelay;
    public float minimumTimeBetweenDamageTaken;

    private float startRegenDelay;
    private bool canRegenerateHealth;
    private float currentRegenDelay;
    private float currentHealth;


    public void DealDamage(float damage)
    {
        if((currentHealth - damage) <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            currentHealth = currentHealth - damage;
            Debug.Log("Current Health Level: " + currentHealth);
            if (canRegenerateHealth)
            {
                canRegenerateHealth = false;
                startRegenDelay = Time.time;
				currentRegenDelay = standardRegenDelay;
            }
            else
            {
                canRegenerateHealth = false;
				currentRegenDelay = (Time.time - startRegenDelay) + standardRegenDelay;
                startRegenDelay = Time.time;
            }
        }
    }

    void Die()
    {
        Debug.Log("Dead");
    }

	// Use this for initialization
	void Start ()
    {
        currentHealth = PlayerStartHealth;
	}



	// Update is called once per frame
	void Update ()
    {
		if(currentHealth < PlayerStartHealth)
        {
            if (canRegenerateHealth)
            {
                currentHealth += HealthRegeneratedPerSecond * Time.deltaTime;
            }
            else if (!canRegenerateHealth)
            {
                if(Time.time > startRegenDelay + currentRegenDelay)
                {
                    canRegenerateHealth = true;
                    currentRegenDelay = standardRegenDelay;
                }
            }
        }
	}
}
