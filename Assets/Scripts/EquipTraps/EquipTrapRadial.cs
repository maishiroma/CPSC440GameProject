using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	This script works in conjunction with the TrapSelect script to allow the player to select a spot in the radial menu and
 * 	place a trap in this spot. Each square will be assigned a section from the PlayerState GameObject array. (so this script goes on
 *  each square).
 */


public class EquipTrapRadial : MonoBehaviour {

	public int representWhichSpot;	// Which index spot does this square represent in the GameObject array in PlayerState?
	public bool isSelected;			// This will be true when the player selects this spot
	public GameObject currTrap;		// What trap is currently on this spot?

	public GameObject playerState;	// Keeps a refrence to the playerState so that it can refer back to it when needed.


	// This will set the trap here to whatever is selected in the GameObject array. 
	void Start()
	{
		playerState = GameObject.FindGameObjectWithTag("Player").gameObject;

		currTrap = playerState.GetComponent<PlayerState>().equippedTraps[representWhichSpot];
	}

	// This method is called in the event system in order to let the object know it's been selected.
	public void SelectSpot()
	{
		isSelected = true;
	}

	// This method is called in the event system or wherever in order to let the object know it's been deselected.
	public void DeselectSpot()
	{
		isSelected = false;
	}



}
