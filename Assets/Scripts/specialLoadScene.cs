using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This does eactly what loadScene does, except has a parameter where it waits for a specified CutScene to finish before executing.

public class specialLoadScene : loadScene {

	public GameObject waitToFinish;
	public bool isActiated;				// Can this run?

	void Start()
	{
		screenFade = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
	}

	// Once the associated gameObject is destroyed, this runs
	void Update()
	{
		if(waitToFinish == null && isActiated == false)
		{
			isActiated = true;
			LoadScene();
		}
	}
}
