using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//	This script will play the specified sound from the GVR source and after it's done, it'll destroy itself.
using System;

public class DestroyAfterSound : MonoBehaviour {

	private GvrAudioSource audioSource;	// In Inspector, this is already set to a specified sound clip.

	void Awake()
	{
		audioSource = GetComponent<GvrAudioSource>();
	}

	// Once the object has been activated, it'll immediatly start playing its sound.
	void Start()
	{
		audioSource.Play();
	}

	// Once it has been activated, if the audio finishes, this gameObject is destroyed.
	void Update()
	{
		if(!audioSource.isPlaying)
			Destroy(gameObject);
	}
}
