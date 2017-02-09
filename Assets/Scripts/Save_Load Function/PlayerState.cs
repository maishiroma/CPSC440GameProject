using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// This class keeps track of the current player state, used in loading saves.
// This is placed in the Player, who is also a singleton object.

public class PlayerState : MonoBehaviour 
{
	public static PlayerState Instance;
	public Transform playerPosition;

	//TUTORIAL
	// Has a copy of the local player data, which is used when saving data.
	public PlayerStatistics localPlayerData = new PlayerStatistics();

	// Singleton method, so that only one of these exist.
	void Awake()
	{
		if (Instance == null)
			Instance = this;

		if (Instance != this)
			Destroy(gameObject);

		GlobalControl.Instance.Player = gameObject;
	}

	//At start, load data from GlobalControl.
	void Start () 
	{
		localPlayerData = GlobalControl.Instance.savedPlayerData;
	}

}