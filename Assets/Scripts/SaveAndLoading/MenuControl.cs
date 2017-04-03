using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This is where the menu stuff takes place, including saving, loading, getting EXP after levels, and unlocking new items.
 * In the final build, this should be functioning in the main menu. Else, it should be deactivated.
 */

public class MenuControl : MonoBehaviour {

	// Keeps a refrence to itself; used in saving and loading
	public static MenuControl Instance;

	// Variables on where main menu stuff get's saved, like high scores.
	public int[] currLevelHighScores;

	// used to optimize save and loading.
	private PlayerState player;
	private Unlockables unlockables;

	// Pseudo-singleton concept from Unity dev tutorial video.
	void Awake()
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;

			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
			unlockables = GameObject.FindGameObjectWithTag("Unlockables").GetComponent<Unlockables>();
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	// If the player reloaded the game, their saved stuff get's readded.
	void Start()
	{
		if (GlobalControl.Instance.IsSceneBeingLoaded == true)
		{
			LoadGame();
			GlobalControl.Instance.IsSceneBeingLoaded = false;
		}
	}

	// This is here for debugging purposes.
	void Update () {
		if(Input.GetKey(KeyCode.I))
			SaveGame();
		else if(Input.GetKey(KeyCode.O))
			LoadGame();
	}

	// Saves all of the important data into a PlayerState Instance and turns it into a .bat file.
	public void SaveGame()
	{
		// We store the important variables into the PlayerState's localPlayerData
		PlayerState.Instance.localPlayerData.Level = player.currLevel;
		PlayerState.Instance.localPlayerData.NextLevel = player.toNextLevel;
		PlayerState.Instance.localPlayerData.XP = player.currXP;
		PlayerState.Instance.localPlayerData.Currency = player.currCurrency;

		PlayerState.Instance.localPlayerData.WeaponDamage = player.currWeaponDamage;
		PlayerState.Instance.localPlayerData.WeaponAmmo = player.currWeaponAmmo;
		PlayerState.Instance.localPlayerData.WeaponShotRate = player.currWeaponShotRate;
		PlayerState.Instance.localPlayerData.WeaponReloadRate = player.currWeaponReloadRate;
		PlayerState.Instance.localPlayerData.EquippedTraps = player.currEquippedTraps;

		// Main Menu stuff, including trap unlocking and gun upgrades.
		PlayerState.Instance.localPlayerData.UnlockWeapons = unlockables.currUnlockWeapons;
		PlayerState.Instance.localPlayerData.GunDamageIndex = player.currGunDamageIndex;
		PlayerState.Instance.localPlayerData.GunAmmoIndex = player.currGunAmmoIndex;
		PlayerState.Instance.localPlayerData.GunReloadIndex = player.currGunReloadIndex;
		PlayerState.Instance.localPlayerData.GunFireIndex = player.currGunFireIndex;
		PlayerState.Instance.localPlayerData.LevelHighScores = currLevelHighScores;

		// With the localPlayerData filled, we save the data to the .bat file
		GlobalControl.Instance.SaveData();
		print("Saved game!");
	}

	// If there's a save file, loads it into the game by using the Global Control's data and putting it into the player's state.
	public void LoadGame()
	{
		bool foundSave = GlobalControl.Instance.LoadData();
		if(foundSave == true)
		{
			// We first load the level (Rigt now, it's set to scene 0 in the build)
			GlobalControl.Instance.IsSceneBeingLoaded = true;
			SceneManager.LoadScene(0);

			// Then, we load the saved data extracted from GlobalControl into the player and the "shop".
			player.currLevel = GlobalControl.Instance.savedPlayerData.Level;
			player.toNextLevel = GlobalControl.Instance.savedPlayerData.NextLevel;
			player.currXP = GlobalControl.Instance.savedPlayerData.XP;
			player.currCurrency = GlobalControl.Instance.savedPlayerData.Currency;

			player.currWeaponDamage = GlobalControl.Instance.savedPlayerData.WeaponDamage;
			player.currWeaponAmmo = GlobalControl.Instance.savedPlayerData.WeaponAmmo;
			player.currWeaponShotRate = GlobalControl.Instance.savedPlayerData.WeaponShotRate;
			player.currWeaponReloadRate = GlobalControl.Instance.savedPlayerData.WeaponReloadRate;
			player.currEquippedTraps = GlobalControl.Instance.savedPlayerData.EquippedTraps;

			// Menu items, including trap unlocking and gun upgrades.
			unlockables.currUnlockWeapons = GlobalControl.Instance.savedPlayerData.UnlockWeapons;
			player.currGunDamageIndex = GlobalControl.Instance.savedPlayerData.GunDamageIndex;
			player.currGunAmmoIndex = GlobalControl.Instance.savedPlayerData.GunAmmoIndex;
			player.currGunReloadIndex = GlobalControl.Instance.savedPlayerData.GunReloadIndex;
			player.currGunFireIndex = GlobalControl.Instance.savedPlayerData.GunFireIndex;

			currLevelHighScores = GlobalControl.Instance.savedPlayerData.LevelHighScores;

			// We then tell the program that the game has finished loading
			print("Loaded game!");
			GlobalControl.Instance.IsSceneBeingLoaded = false;
		}
	}
}
