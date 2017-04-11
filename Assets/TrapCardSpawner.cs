using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCardSpawner : MonoBehaviour {


    public GameObject TrapCard;
    public float TrapCardSpacing = 0.1f;
    public GameObject[] Traps;				// The actual Traps themselves.
	public GameObject[] ThrowableTraps;		// This stores the special GameObject that certain traps will need.

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < Traps.Length; i++)
        {
            float width = TrapCard.transform.lossyScale.x + TrapCardSpacing;
            SpawnTrapCard(transform.position + (Vector3.left * i * width), Traps[i]);
        }
	}
	
	// Loads the specified trap into a TrapCard.
	void SpawnTrapCard (Vector3 pos, GameObject trap)
    {
        GameObject _trapCard = (GameObject)Instantiate(TrapCard, pos, transform.rotation, transform);
		_trapCard.GetComponent<TrapCard>().LoadTrapInSlot(trap);
		AssignThrowableTrap(_trapCard.GetComponent<TrapCard>());
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
