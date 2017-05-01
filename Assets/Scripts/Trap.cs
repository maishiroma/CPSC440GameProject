using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public bool unlocked;
    public float cost;
    public bool purchased;
    public GameObject icon;
	public GameObject trapPrefab;		// Holds a prefab of the Trap to be equipped by the player.

    // -1 = not equipped 
    // 1 - left
    // 2 - top
    // 3 - right
    public int slot;

	// Plays the trap placement animation
	public void PlaceTrap()
	{
		//After releasing the button
		if(gameObject.GetComponent<Animator>() != null)
			gameObject.GetComponent<Animator>().SetTrigger("PlaceTrap");
	}    

	
}
