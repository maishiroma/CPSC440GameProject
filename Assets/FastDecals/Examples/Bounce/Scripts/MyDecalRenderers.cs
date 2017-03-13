using UnityEngine;
using System.Collections;

public class MyDecalRenderers : MonoBehaviour {
	public FastDecalsRenderer blobShadowRenderer;
	public FastDecalsRenderer bloodEffectsRenderer;
	public Transform ground;

	// Singleton

	private static MyDecalRenderers sharedInstance = null;

	public static MyDecalRenderers SharedInstance {
		get {
			if(!sharedInstance) {
				sharedInstance = (MyDecalRenderers) FindObjectOfType(typeof(MyDecalRenderers));
			}

			return sharedInstance;
		}
	}
	
	void OnApplicationQuit() {
		sharedInstance = null;
	}
}
