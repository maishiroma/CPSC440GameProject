using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script simply allows for the testing of certain things.
 * From getting EXP, to leveling up, to unlocking items, this has everything.
 */

public class Debugging : MonoBehaviour {

	private PlayerState player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
	}

	// Depending on what key is pressed, something happens.
	// Z = experience
	// X = money
	void Update()
	{
		// Gives the player 1 exp. If they reached the threshold of leveling up, they leveled up.
		if(Input.GetKey(KeyCode.Z))
		{
			player.currXP++;
			if(player.currXP > player.toNextLevel)
			{
				player.currLevel++;
				print("Your level is now at lv " + player.currLevel);
				player.toNextLevel *= 1.5f;
			}
		}
		// Gives the player 1 currency.
		else if(Input.GetKey(KeyCode.X))
		{
			player.currCurrency++;
		}
	}

}
