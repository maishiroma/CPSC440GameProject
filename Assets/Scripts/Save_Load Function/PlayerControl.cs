using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
// This handles the player saving and loading the game themselves.
// In the future, that feature should be in a menu.
// Otherwise, this script holds the current stats of the player.

public class PlayerControl : MonoBehaviour
{
	//Player Variables (dummy ones)
	public float currHP = 100;
	public float currAmmo = 30;
	public float currXP = 0;

	// Makes sure this is persistant
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
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

	// Right now, to save, it's hitting either "I" or "O"
	void Update() 
	{
		if(Input.GetKey(KeyCode.I))
			SaveGame();
		else if(Input.GetKey(KeyCode.O))
			LoadGame();
		// This switches between levels. (As a test to see if you can save the scene you're last in)
		else if(Input.GetKey(KeyCode.L))
		{
			if(SceneManager.GetActiveScene().name == "Scene1")
				SceneManager.LoadScene("Scene2");
			else
				SceneManager.LoadScene("Scene1");

			transform.position = GameObject.Find("SpawnPoint").transform.position;
		}
	}

	// Saves all of the important data into PlayerState and turns it into a .bat file
	void SaveGame()
	{
		PlayerState.Instance.localPlayerData.SceneID = SceneManager.GetActiveScene().buildIndex;
		PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
		PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
		PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;
		PlayerState.Instance.localPlayerData.HP = currHP;
		PlayerState.Instance.localPlayerData.Ammo = currAmmo;
		PlayerState.Instance.localPlayerData.XP = currXP;

		GlobalControl.Instance.SaveData();
		print("Saved game!");
	}

	// If there's a save file, loads it into the game.
	void LoadGame()
	{
		bool foundSave = GlobalControl.Instance.LoadData();
		if(foundSave == true)
		{
			// We first load the level
			GlobalControl.Instance.IsSceneBeingLoaded = true;
			int whichScene = GlobalControl.Instance.savedPlayerData.SceneID;
			SceneManager.LoadScene(whichScene);

			// Then, we store the saved data into the Player and initialize its values.
			PlayerState.Instance.localPlayerData = GlobalControl.Instance.savedPlayerData;
			transform.position = new Vector3(
				GlobalControl.Instance.savedPlayerData.PositionX,
				GlobalControl.Instance.savedPlayerData.PositionY,
				GlobalControl.Instance.savedPlayerData.PositionZ + 0.1f);

			currHP = GlobalControl.Instance.savedPlayerData.HP;
			currAmmo = GlobalControl.Instance.savedPlayerData.Ammo;
			currXP = GlobalControl.Instance.savedPlayerData.XP;
			print("Loaded game!");	
		}
	}
}
