using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	This script works in conjunction with the TrapSelect script to allow the player to select a spot in the radial menu and
 * 	place a trap in this spot. Each square will be assigned a section from the PlayerState GameObject array. (so this script goes on
 *  each square).
 */
public class EquipTrapRadial : MonoBehaviour {

    private TrapButtonInteraction _trapButtonInteraction;

	public int representWhichSpot;		// Which index spot does this square represent for the player's equipped traps in PlayerState?
	public bool isSelected;				// This will be true when the player selects this spot
	public bool isDisabled;				// This makes sure that only one of these spots are selected.

	public TrapCard associatedTrapCard;		// What trapCard is currently associated on this spot?
	public EquipTrapRadial[] otherRadials;	// Keeps track of the other radial spots.

	private PlayerState player;				// Keeps a refrence to the playerState so that it can refer back to it when needed.
	private static TrapCard[] trapCards;	// Used to put back the refrences to these cards in associatedTrapCard.

	// This will set which TrapCard is associated to this spot, if the player has traps equipped.
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
    _trapButtonInteraction = gameObject.GetComponent<TrapButtonInteraction>();
	}

	// This method is called in the event system in order to let the object know it's been selected.
	// All other spots are disabled, preventing them from being selected.
	// Selecting the same spot will deselect it. (or removing the trap that was in there?)
	public void SelectSpot()
	{
        _trapButtonInteraction.Select();

		if(isDisabled == false)
		{
			if(isSelected == true)
			{
				DeselectSpot();
			}
			else
			{
				print(representWhichSpot + " is selected.");
				isSelected = true;
				for(int i = 0; i < otherRadials.Length; i++)
					otherRadials[i].isDisabled = true;
			}
		}
	}

	// This method is called in the event system or wherever in order to let the object know it's been deselected.
	// All other spots are then reenabled, allowing them to being selected.
	public void DeselectSpot()
	{
        _trapButtonInteraction.Deselect();

		if(isDisabled == false)
		{
			print(representWhichSpot + " is deselected.");
			isSelected = false;
			for(int i = 0; i < otherRadials.Length; i++)
				otherRadials[i].isDisabled = false;
		}
	}

	// This sets the given trap from the parameter into this spot.
	public void SetTrap(TrapCard selectedTrapCard)
	{
		// This is done so that we know which TrapCard is associated to this spot.
		associatedTrapCard = selectedTrapCard;

		// This sets the trap that's in the TrapCard to the player. 
		player.currEquippedTraps[representWhichSpot] = selectedTrapCard.associatedTrap;
		DeselectSpot();
		print("Set " + player.currEquippedTraps[representWhichSpot].name + " into slot " + representWhichSpot);
	}

	// This removes the current trap that's in the currTrap variable.
	public void RemoveTrap()
	{
		// We tell the TrapCard associated here that it is no longer associated with anything.
		associatedTrapCard.GetComponent<TrapCard>().equipped = false;
		print("Removed " + player.currEquippedTraps[representWhichSpot].name  + " from slot " + representWhichSpot);

		// And then we null out the refrences.
		associatedTrapCard = null;
		player.currEquippedTraps[representWhichSpot] = null;
	}
}