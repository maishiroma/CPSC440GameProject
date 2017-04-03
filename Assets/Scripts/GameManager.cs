using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	This script keeps track of the various stats that the player has acheived.
 * 	It will also have methods that will display respective numbers.
 */
public class GameManager : MonoBehaviour {

	public int overallKillCount = 0;	// The overall number of enemies that the player has killed.
	public int killAggresiveCount = 0;	// The number of enemies that the player killed when the enemy is attacking them.
	public int killEnemyNoTraps = 0;	// The number of enemies that the player killed when they ran out of traps.

	public int killComboGun = 0;		// The number of gun kills the player has gotten without getting hit or if gunComboTime ran out.
	public int killComboTrap = 0;		// The  number of trap kills the player got without getting hit or before trapComboCombo ran out.
	public int highestKillCombo = 0;	// The highest combo that the player has gotten

	public float gunComboTime = 0;		// The amount of time it takes for killGunCombo to reset.
	public float trapComboTime = 0;		// The amount of time it takes for killTrapCombo to reset.

	public int resourceCount = 0;		// The number of resources that the player has obtained.

	// This method resets all of the numbers for the next round.
	public void resetCounters()
	{
		// Code here to reset all of these values to 0.
	}


}
