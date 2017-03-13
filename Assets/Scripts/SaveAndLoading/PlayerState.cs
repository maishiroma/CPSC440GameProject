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