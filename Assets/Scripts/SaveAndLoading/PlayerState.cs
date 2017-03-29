using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 	
 * This class keeps track of the current player state, used in loading saves.
 * This also has the current Player Variables as well, which are either loaded in from PlayerStatistics, or directly from the game.
 * This is placed in the Player, who is also a singleton object.
 * There should be only one copy of this.
 */

public class PlayerState : MonoBehaviour 
{
	public static PlayerState Instance;

	// Has a copy of the local player data, which is used when saving data.
	public PlayerStatistics localPlayerData = new PlayerStatistics();

	// The current Player Stats
	public int currLevel;
	public float toNextLevel;
	public float currXP;
	public float currCurrency;

	public int currWeaponDamage;
	public int currWeaponAmmo;
	public float currWeaponShotRate;
	public float currWeaponReloadRate;

	/* 	This variable keeps track of what traps the player has currently equipped.
		This will store the current weapons that the player will have. This will be a type GameObject that will hold traps.
		The traps will be set in the menu screen.
		Once the player selects the trap menu, the radial menu should be updated to reflect what's in the GameObject[] array.
		(which is done in the PageScript)

		The player simply selects a slot on the radial menu and chooses which trap to put in there. (as well as vice versa)
		If the player selects a trap in the radial menu and clicks on a trap in the selection, that selected trap will replace
		the trap being highlighted.
		If the player selects a trap and selects an empty trap spot, the trap will be removed from the radial menu.
		If the player selexts a slot with a trap and selects another trap in the radial menu, they swap places.

		Selecting a radial spot can use the event system that Cameron already implemeted for selecting a spot in the actual game.
		

		In the actual game, the radial menu will use this list to determine what traps the player has.
	*/ 

	public GameObject[] equippedTraps = new GameObject[3];

	// Singleton method, so that only one of these exist.
	void Awake()
	{
		if (Instance == null)
			Instance = this;

		if (Instance != this)
			Destroy(gameObject);

	}

	//At start, load data from GlobalControl.
	void Start () 
	{
		localPlayerData = GlobalControl.Instance.savedPlayerData;
	}

}