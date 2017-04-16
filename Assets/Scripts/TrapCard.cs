using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCard : MonoBehaviour {

    public bool visible;
    public bool equipped;				// Is this trap card equipped?
    public Transform trapIconPos;
	public GameObject associatedTrap;	// What Trap prefab is associated to this spot?

	// Use this for initialization
	void Start ()
    {
	    
	}

	// Associated this trap card with the passed in Trap and instanciates an icon of it.
    public void LoadTrapInSlot(GameObject trap)
    {
		Instantiate(trap.GetComponent<Trap>().icon, trapIconPos.position, Quaternion.identity, GameObject.Find("Icons").transform);
		associatedTrap = trap;
    }
		
	// Update is called once per frame
	void Update () {

	}

	// Returns true if this trap name matches one of the names given here.
	bool CheckIfTrapIsThrowable(string name)
	{
		switch(name)
		{
			case "Grenade":
				return true;
		}
		return false;
	}

	// Checks if any of the trapRadials are selected. If so, puts this trap onto there, and sets this trap as equipped.
	public void EquipTrap()
	{
		if(equipped == false)
		{
			EquipTrapRadial[] trapRadials = GameObject.FindObjectsOfType<EquipTrapRadial>();
			for(int i = 0; i < trapRadials.Length; i++)
			{
				if(trapRadials[i].isSelected == true)
				{
					// If there's already a trap in this spot, this current one get's "deequipped"
					if(trapRadials[i].associatedTrapCard != null)
					{
						trapRadials[i].RemoveTrap();
					}
					trapRadials[i].SetTrap(this);
					equipped = true;
					break;
				}
			}
		}
	}
}
