using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCard : MonoBehaviour {

    public bool visible;
    public bool equipped;
    public Transform trapIconPos;

	public GameObject displayedTrap;				// What trap is here?
	public static EquipTrapRadial[] trapRadials;	// Refrence to radial buttons. Used to interact with equipTrapRadial

	// Use this for initialization
	void Start ()
    {
	    	
	}

	// This is only needed to be done once
	void Awake()
	{
		if(trapRadials == null)
			trapRadials = GameObject.FindObjectsOfType<EquipTrapRadial>();
	}
	
    public void LoadTrapInSlot(GameObject trap)
    {
        Instantiate(trap, trapIconPos.position, Quaternion.identity, GameObject.Find("Icons").transform);
		displayedTrap = trap;
    }


	// Update is called once per frame
	void Update () {
		
	}

	// Checks if any of the trapRadials are selected. If so, puts this trap onto there, and sets this trap as equipped.
	public void EquipTrap()
	{
		if(equipped == false)
		{
			for(int i = 0; i < trapRadials.Length; i++)
			{
				if(trapRadials[i].isSelected == true)
				{
					trapRadials[i].SetTrap(displayedTrap);
					equipped = true;
					break;
				}
			}
		}
	}
}
