using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// A dummy class that keeps track of a weapon and its stats. This will be used on all weapons to keep track of the states it has.
// Make sure this is also marked Serializable

[Serializable]
public class WeaponState {

	public string name;
	public int level;
	public int damage;

}
