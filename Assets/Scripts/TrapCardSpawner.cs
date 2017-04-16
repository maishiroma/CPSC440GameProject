using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCardSpawner : MonoBehaviour {


    public GameObject TrapCard;
    public float TrapCardSpacing = 0.1f;
    public GameObject[] Traps;				// The actual Traps themselves.
	public GameObject[] ThrowableTraps;		// This stores the special GameObject that certain traps will need.
	public EquipTrapRadial[] trapSlots;			// A refrence to the trap slots that store the traps being equipped.

	private PlayerState player;

	// Use this for initialization
	void Start ()
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
		for(int i = 0; i < Traps.Length; i++)
        {
            float width = TrapCard.transform.lossyScale.x + TrapCardSpacing;
            SpawnTrapCard(transform.position + (Vector3.left * i * width), Traps[i]);
        }
	}
	
	// Loads the specified trap into a TrapCard.
	void SpawnTrapCard (Vector3 pos, GameObject trap)
    {
		TrapCard _trapCard = Instantiate(TrapCard, pos, transform.rotation, transform).GetComponent<TrapCard>();
		_trapCard.LoadTrapInSlot(trap);
		AssignThrowableTrap(_trapCard);

		// Checks if the player has equipped this trap already. If so, it sets this card to the respective trapRadial spot.
		for(int i = 0; i < trapSlots.Length; i++)
		{
			int trapIndex = trapSlots[i].representWhichSpot;
			if(player.currEquippedTraps[trapIndex] != null && player.currEquippedTraps[trapIndex].name == _trapCard.associatedTrap.name)
			{
				_trapCard.equipped = true;
				trapSlots[i].associatedTrapCard = _trapCard;
				break;
			}

		}
	}

	/*	If the assigned trap is a throwable trap, this method replaces the trap associated with it with the special gameobject
	 * 	used to use it.
	 * 	
	 * 	Positions of each one:
	 * 	Throwable = 0
	 */
	void AssignThrowableTrap(TrapCard currTrapCard)
	{
		switch(currTrapCard.associatedTrap.name)
		{
			case "Grenade":
				currTrapCard.associatedTrap = ThrowableTraps[0];
			break;
		}
	}

}
