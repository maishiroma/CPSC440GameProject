using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVRMode : MonoBehaviour {

    private bool VRenabled;
    public GvrViewer vrViewer;

	// Use this for initialization
	void Start () {
        VRenabled = true;
	}
	
	// Update is called once per frame
	public void toggle ()
    {
        VRenabled = !VRenabled;

        if (VRenabled)
        {
            vrViewer.VRModeEnabled = true;
        }
        else if (!VRenabled)
        {
            vrViewer.VRModeEnabled = false;
        }
    }
}
