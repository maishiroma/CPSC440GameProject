using UnityEngine;
using System.Collections;
using pmjo.FastDecals;

public class Ball : MonoBehaviour {
	private Transform mTransform;
	private Rigidbody mRigidbody;
		
	void Awake() {
		mTransform = GetComponent<Transform>();
		mRigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
		// Draw blob shadow using Fast Decals when ground distance is less than 8 units
		float blobVisibleDistance = 8.0f;
		RaycastHit hit;
		if(Physics.Raycast(mTransform.position, -Vector3.up, out hit, blobVisibleDistance)) {
			float distanceMultiplier = hit.distance / blobVisibleDistance;
			float size = 1.2f * (1.0f + distanceMultiplier * 4.0f);
	
			// Change alpha based on ball and ground distance
			byte maxAlpha = 222;//255;
			byte alpha = (byte)((1.0f - distanceMultiplier) * maxAlpha);
			Color32 color = new Color32(255, 255, 255, alpha);
			
			// Draw blob shadow 0.01 units above the ground to avoid z-fighting
			MyDecalRenderers.SharedInstance.blobShadowRenderer.DrawDecal(new Vector3(mTransform.position.x, hit.point.y + 0.01f, mTransform.position.z), size, 0, color);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.transform == MyDecalRenderers.SharedInstance.ground) {
			mRigidbody.AddForce((Vector3.up + mTransform.forward * 0.5f) * 5000.0f , ForceMode.Force);
		}
	}
}
