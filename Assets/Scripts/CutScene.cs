using System.Collections;
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
	public GvrAudioSource cutsceneSource;	// Used for spatial audio for this object.
	public float soundVolume;				// The univesal sound volume for the cutscene.
	public bool isReady;					// Does this cutscene play immediatly when the scene is loaded?

	private int currAudioIndex;				// The current index that the array is currently in.

	void Start()
	{
		cutsceneSource = GetComponent<GvrAudioSource>();
	}

	// If the current source finished playing its sound clip, the next one is loaded up.
	void Update()
	{
		if(isReady == true)
		{
			if(cutsceneSource.isPlaying == false)
			{
				if(currAudioIndex < cutsceneAudio.Length)
				{
					cutsceneSource.PlayOneShot(cutsceneAudio[currAudioIndex],soundVolume);
					currAudioIndex++;
				}
//				else
//				{
//					// After playing out this script, does script allow the player to transition to a different scene?
//					if(GetComponent<loadScene>() != null)
//					{
//						GetComponent<loadScene>().LoadScene();
//					}
//					enabled = false;
//				}
			}
		}
	}

	// Allows the cutscene to play. This can be triggered by an event call.
	public void PlayCutscene()
	{
		isReady = true;
	}
}
