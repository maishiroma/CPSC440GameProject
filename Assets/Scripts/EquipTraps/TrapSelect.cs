using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	This script is placed on the trap holograms in the menu. This essentially works with the EquipTrapRadial in that it assigns the
 * 	trap that this spot is assigned into the selected spot selected in EquipTrapRadial.
 */
using System;

public class TrapSelect : MonoBehaviour {

	public GameObject displayedTrap;	// What trap is here?
	public bool isUsed;					// Is this trap already used in the radial?

	public GameObject[] trapRadials;	// A refrence to the objects with the EquipTrapRadials is here. (used to access the player state as well as what's in each spot)

	// Marks this trap as used if it's already in the radial menu.
	void Start()
	{
		for(int i = 0; i < trapRadials.Length; i++)
		{
			// Code where if the traps in the trap radial contain the displayedTrap, mark this as isUsed and break from the loop.
		}
	}

	// This finds a trapRadial that's been selected and assigns this trap to that spot. Also marks this trap as isUsed. Called in events.
	public void AssignTrapToSpot()
	{
		for(int i = 0; i < trapRadials.Length; i++)
		{
			EquipTrapRadial currOne = trapRadials[i].GetComponent<EquipTrapRadial>();
			if(currOne.isSelected)
			{
				currOne.currTrap = displayedTrap;
				currOne.playerState.GetComponent<PlayerState>().equippedTraps[currOne.representWhichSpot] = currOne.currTrap;
				isUsed = true;
				break;
			}
		}
	}
}
