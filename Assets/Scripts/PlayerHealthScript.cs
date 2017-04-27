using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {

    public float PlayerStartHealth = 100;
    public float HealthRegeneratedPerSecond;
    public float standardRegenDelay;
    public float minimumTimeBetweenDamageTaken;
	public Transform debugHealthBar;
    public ContextualScreenManager ContextualScreen;
    public ContextualScreenPage Health;

    private float startRegenDelay;
    private bool canRegenerateHealth;
    private float currentRegenDelay;
    private float currentHealth;
    private HealthBar PlayerHealthBar;

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
			UpdateDebugBar ();
            PlayerHealthBar.SetHealthBar(currentHealth / PlayerStartHealth);
            ContextualScreen.SwitchToPage(Health);
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
        PlayerHealthBar = GameObject.Find("ContextualScreen_ScreenHealthBar").GetComponent<HealthBar>();
	}


	void UpdateDebugBar()
	{
		//Debug.Log ("Updating Health Bar");
		float healthRatio = currentHealth / PlayerStartHealth;
		debugHealthBar.localScale = new Vector3(healthRatio, debugHealthBar.localScale.y, debugHealthBar.localScale.z);
	}


    void ResetContextualScreen()
    {
        ContextualScreen.SwitchToDefaultPage(Health);
    }

	// Update is called once per frame
	void Update ()
    {
		if(currentHealth < PlayerStartHealth)
        {
            if (canRegenerateHealth)
            {
                currentHealth += HealthRegeneratedPerSecond * Time.deltaTime;
				UpdateDebugBar ();
                PlayerHealthBar.SetHealthBar(currentHealth / PlayerStartHealth);
            }
            else if (!canRegenerateHealth)
            {
                if(Time.time > startRegenDelay + currentRegenDelay)
                {
                    Invoke("ResetContextualScreen", 2f);
                    canRegenerateHealth = true;
                    currentRegenDelay = standardRegenDelay;
                }
            }
        }
	}
}
