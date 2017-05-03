﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	This script keeps track of the various stats that the player has acheived.
 * 	It will also have methods that will display respective numbers.
 * 	
 * 	For the trailer, only show total kills, deaths by traps, deaths by gunfire, and resources.
 */
public class GameManager : MonoBehaviour {

	public int overallScore = 0;		// The overall score that the player has.
	public int overallKillCount = 0;	// The overall number of enemies that the player has killed.

	public int killAggressiveCount = 0;	// The number of enemies that the player killed when the enemy is attacking them.
	public int killEnemyNoTraps = 0;	// The number of enemies that the player killed when they ran out of traps.

	public int killComboGun = 0;		// The number of gun kills the player has gotten without getting hit or if gunComboTime ran out.
	public int killComboTrap = 0;		// The number of trap kills the player got without getting hit or before trapComboCombo ran out.
	public int highestKillCombo = 0;	// The highest combo that the player has gotten. Keeps into account all other combos.

	public float gunComboTime = 0f;		// The amount of time it takes for killGunCombo to reset.
	public float trapComboTime = 0f;	// The amount of time it takes for killTrapCombo to reset.

	public int resourceCount = 0;		// The number of resources that the player has obtained.

	public int smallAlienPoints = 100;	// The number of points a small alien is worth
	public int bigAlienPoints = 200;	// The number of points a big alien is worth
	public int comboMultiplier = 300;	// The multiplier that the combo Vars are modified.

	public float currTimeCombo = 0f;	// Keeps track of the amount of time left for the player to keep up the combo.

	// This keeps track of the comboTimer and sees how much time has passes from the last kill.
	void Update()
	{
		if(currTimeCombo != 0f)
		{
			if(currTimeCombo > 0f)
			{
				currTimeCombo -= Time.deltaTime;
			}
			else
			{
				if(killComboGun > highestKillCombo)
				{
					highestKillCombo = killComboGun;
				}
				else if(killComboTrap > highestKillCombo)
				{
					highestKillCombo = killComboTrap;
				}
				killComboGun = 0;
				killComboTrap = 0;
				currTimeCombo = 0f;
			}
		}

        overallScore = calculateComboScore();
	}
		
	// This method resets all of the numbers for the next round.
	public void resetCounters()
	{
		overallScore = 0;
		overallKillCount = 0;
		killAggressiveCount = 0;
		killEnemyNoTraps = 0;
		killComboGun = 0;
		killComboTrap = 0;
		highestKillCombo = 0;
		resourceCount = 0;
	}

	// These next methods are used specifically to increment the counters, since they also increment overallKillCount too.
	public void incrementKillAggressiveCount()
	{
		killAggressiveCount++;
		overallKillCount++;
	}

	public void incrementKillEnemyNoTrapCount()
	{
		killEnemyNoTraps++;
		// overallKillCount++;
	}

	public void incrementKillComboGun()
	{
		killComboGun++;
		overallKillCount++;

		if(killComboGun > 1 || overallKillCount > 1)
		{
			currTimeCombo = gunComboTime;
            obtainTrap();
        }

    }

	public void incrementKillComboTrap()
	{
        killComboTrap++;
		overallKillCount++;

		if(killComboTrap > 1 || overallKillCount > 1)
		{
			currTimeCombo = trapComboTime;
            obtainTrap();
        }

	}

	// This method calculates the combo scores after the level is complete.
	public int calculateComboScore()
	{
		return (killComboGun * comboMultiplier) + (killComboTrap * comboMultiplier);
	}

    public void obtainTrap()
    {
        // calculate probability of gaining new trap during a combo
        // success rate: 40% == 2/5 ; range(1, 6) where 1 is inclusive, 6 is exclusive
        // only increase if the current number of traps is less than the maximum number of traps
        int chance = Random.Range(1, 6);
        if (chance >= 1 && chance <= 2)
        {
            // print("Probability: " + chance);
            if (GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerManager>().CurrentNumTraps < GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerManager>().MaxNumTraps)
            {
                // print("Current: " + "Max: " + GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerManager>().MaxNumTraps);
                // print("Max: " + GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerManager>().MaxNumTraps);
                GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerManager>().IncrementNumTraps(true);
            }

        }
    }
    
}
