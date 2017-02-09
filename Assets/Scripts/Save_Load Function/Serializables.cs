using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//TUTORIAL
[Serializable]
// The Serializable keyword tells the engine that the data here can be serialized into a binary.
public class PlayerStatistics
{
	/*
	 * Here we declare variables that are going to be saved. 
	 * So in our game, we will need to keep track of the current world and level the player has unlocked to, the points they have
	 * accumulated, the traps they have unlocked, and anything else that we are planning that needs to be carried over.
	 * 
	 * This is a tutorial, so I'm just going to go with the flow here.
	 * https://www.sitepoint.com/saving-and-loading-player-game-data-in-unity/
	 * https://www.sitepoint.com/community/t/how-to-save-and-load-a-binary-file-in-android-i-am-building-a-unity-game/245469/5
	 * 
	*/ 

	public int SceneID;
	public float PositionX, PositionY, PositionZ;

	public float HP;
    public float Ammo;
    public float XP;
}
