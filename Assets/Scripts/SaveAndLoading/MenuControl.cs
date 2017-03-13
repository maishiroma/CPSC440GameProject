using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This is where the menu stuff takes place, including saving, loading, getting EXP after levels, and unlocking new items.
 * In the final build, this should be functioning in the main menu. Else, it should be deactivated.
 */

public class MenuControl : MonoBehaviour {

	public static MenuControl Instance;
	public int[] currLevelHighScores;

	// Pseudo-singleton concept from Unity dev tutorial video.
	void Awake()
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
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

	// This is here for debugging purposes. In the actual game, these functions are tied to button clicks.
	void Update () {
		if(Input.GetKey(KeyCode.I))
			SaveGame();
		else if(Input.GetKey(KeyCode.O))
			LoadGame();
	}

	// Saves all of the important data into a PlayerState Instance and turns it into a .bat file.
	public void SaveGame()
	{
		// We retrieve the player and "shop" gameobjects, since they have necessary data to save
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		GameObject unlockables = GameObject.FindGameObjectWithTag("Unlockables");

		// We then store the important variables into the PlayerState's localPlayerData
		PlayerState.Instance.localPlayerData.Level = player.GetComponent<PlayerState>().currLevel;
		PlayerState.Instance.localPlayerData.NextLevel = player.GetComponent<PlayerState>().toNextLevel;
		PlayerState.Instance.localPlayerData.XP = player.GetComponent<PlayerState>().currXP;
		PlayerState.Instance.localPlayerData.Currency = player.GetComponent<PlayerState>().currCurrency;

		PlayerState.Instance.localPlayerData.WeaponDamage = player.GetComponent<PlayerState>().currWeaponDamage;
		PlayerState.Instance.localPlayerData.WeaponAmmo = player.GetComponent<PlayerState>().currWeaponAmmo;
		PlayerState.Instance.localPlayerData.WeaponShotRate = player.GetComponent<PlayerState>().currWeaponShotRate;
		PlayerState.Instance.localPlayerData.WeaponReloadRate = player.GetComponent<PlayerState>().currWeaponReloadRate;

		PlayerState.Instance.localPlayerData.UnlockWeapons = unlockables.GetComponent<Unlockables>().currUnlockWeapons;
		PlayerState.Instance.localPlayerData.LevelHighScores = currLevelHighScores;

		// With the localPlayerData filled, we save the data to the .bat file
		GlobalControl.Instance.SaveData();
		print("Saved game!");
	}

	// If there's a save file, loads it into the game.
	public void LoadGame()
	{
		bool foundSave = GlobalControl.Instance.LoadData();
		if(foundSave == true)
		{
			// We first load the level (Rigt now, it's set to scene 0 in the build)
			GlobalControl.Instance.IsSceneBeingLoaded = true;
			SceneManager.LoadScene(0);

			// After we loaded the scene, we find the player and the "shop" gameobjects, since we will be filling in details in them
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			GameObject unlockables = GameObject.FindGameObjectWithTag("Unlockables");

			// Then, we load the saved data extracted from GlobalControl into the player and the "shop".
			player.GetComponent<PlayerState>().currLevel = GlobalControl.Instance.savedPlayerData.Level;
			player.GetComponent<PlayerState>().toNextLevel = GlobalControl.Instance.savedPlayerData.NextLevel;
			player.GetComponent<PlayerState>().currXP = GlobalControl.Instance.savedPlayerData.XP;
			player.GetComponent<PlayerState>().currCurrency = GlobalControl.Instance.savedPlayerData.Currency;

			player.GetComponent<PlayerState>().currWeaponDamage = GlobalControl.Instance.savedPlayerData.WeaponDamage;
			player.GetComponent<PlayerState>().currWeaponAmmo = GlobalControl.Instance.savedPlayerData.WeaponAmmo;
			player.GetComponent<PlayerState>().currWeaponShotRate = GlobalControl.Instance.savedPlayerData.WeaponShotRate;
			player.GetComponent<PlayerState>().currWeaponReloadRate = GlobalControl.Instance.savedPlayerData.WeaponReloadRate;

			unlockables.GetComponent<Unlockables>().currUnlockWeapons = GlobalControl.Instance.savedPlayerData.UnlockWeapons;
			currLevelHighScores = GlobalControl.Instance.savedPlayerData.LevelHighScores;

			print("Loaded game!");
			GlobalControl.Instance.IsSceneBeingLoaded = false;
		}
	}
}
