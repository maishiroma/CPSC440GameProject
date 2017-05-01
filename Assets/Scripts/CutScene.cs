﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*	This script takes care of any cutscenes. 
 * 	This plays out the selected audio clips provided in the AudioClip array in order.
 * 	
 * 	If there's a pause in the cutscene till another clip, the last clip should have empty noise until the
 * 	cutscene is ready to proceed.
 */

public class CutScene : MonoBehaviour {

	public AudioClip[] cutsceneAudio;		// The array of cutscene audio clips.
	public bool incrementStateAfterPlaying;	// After this cutscene plays out, should the game state be incremented?
	public int playOnGameState;				// What game state does this cutscene play on?

	private GvrAudioSource cutsceneSource;	// Used for spatial audio for this object.
	private PlayerState player;				// Used to increment the gameState.
	private int currAudioIndex;				// The current index that the array is currently in.

	void Awake()
	{
		cutsceneSource = GetComponent<GvrAudioSource>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
	}

	// If this cutscene has played when is is loaded into the scene, it is automatically destroyed.
	void Start()
	{
		if(player.currGameState > playOnGameState)
			Destroy(gameObject);
	}

	// If the current source finished playing its sound clip, the next one is loaded up.
	void Update()
	{
		if(playOnGameState <= player.currGameState)
		{
			if(cutsceneSource.isPlaying == false)
			{
				if(currAudioIndex < cutsceneAudio.Length)
				{
					cutsceneSource.PlayOneShot(cutsceneAudio[currAudioIndex]);
					currAudioIndex++;
				}
				else
				{
					// Once the cutscene finishes playing out, the game state increments (if selected, and this gameObject is destroyed.
					if(incrementStateAfterPlaying == true)
						player.currGameState++;
					
					Destroy(gameObject);
				}
			}
		}
	}

	// Allows the cutscene to play. This can be triggered by an event call.
	public void PlayCutscene()
	{
		gameObject.SetActive(true);
	}
}
