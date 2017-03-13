using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script simply allows for the testing of certain things.
 * From getting EXP, to leveling up, to unlocking items, this has everything.
 */

public class Debugging : MonoBehaviour {

	private GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Depending on what key is pressed, something happens.
	void Update()
	{
		// Gives the player 1 exp. If they reached the threshold of leveling up, they leveled up.
		if(Input.GetKey(KeyCode.Z))
		{
			player.GetComponent<PlayerState>().currXP++;
			if(player.GetComponent<PlayerState>().currXP > player.GetComponent<PlayerState>().toNextLevel)
			{
				player.GetComponent<PlayerState>().currLevel++;
				print("Your level is now at lv " + player.GetComponent<PlayerState>().currLevel);
				player.GetComponent<PlayerState>().toNextLevel *= 1.5f;
			}
		}
		// Gives the player 1 currency.
		else if(Input.GetKey(KeyCode.X))
		{
			player.GetComponent<PlayerState>().currCurrency++;
		}
	}

}
