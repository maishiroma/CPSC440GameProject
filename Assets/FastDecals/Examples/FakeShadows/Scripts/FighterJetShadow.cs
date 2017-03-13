using UnityEngine;
using System.Collections;

public class FighterJetShadow : MonoBehaviour {
	
	public float shadowFadeOutHeight = 32.0f;
	public float shadowScalingFactor = 2.0f;
	public float shadowMaxAlpha = 0.6f;
	public bool fakeRoll = true;
	public bool fakePitch = true;
	
	private Transform mTransform;
	private FastDecalsRenderer mFastDecalsRenderer;
	private MeshRenderer mRenderer;
	private Vector2 mShadowSizeOnGroundLevel;

	void Awake() {
		mTransform = GetComponent<Transform>();
		mRenderer = GetComponentInChildren<MeshRenderer>();

		// Since there is only one FastDecalsRendeder in this demo we can get access to it easily with FindObjectOfType
		mFastDecalsRenderer = (FastDecalsRenderer) FindObjectOfType(typeof(FastDecalsRenderer));
		
		// Get the size of the mesh from the renderer and scale it a bit bigger
		mShadowSizeOnGroundLevel = new Vector2(mRenderer.bounds.size.x, mRenderer.bounds.size.x) * 1.5f; 
	}
	
	void Update() {
		if(mFastDecalsRenderer) {
			// Our ground plane y is all the time at 0.
			// You could use raycasting to solve your current ground y incase your ground level changes.
			float groundY = 0.0f;
			
			// Render the decal only if the fighter jet is above the ground
			if(mTransform.position.y > groundY) {
				Vector2 decalSize = mShadowSizeOnGroundLevel;
				Color32 decalColor = Color.white;
			
				// Calculate shadow strength based on hight
				float shadowStrength = Mathf.Clamp01((groundY + mTransform.position.y) / shadowFadeOutHeight);
				
				// Make the shadow bigger when the fighter jet is higher
				decalSize += decalSize * shadowScalingFactor * shadowStrength;
				
				// Fade out the fake shadow with alpha
				float alpha = (1.0f - shadowStrength) * shadowMaxAlpha;
				decalColor.a = (byte)(alpha * 255.0f);
				
				// Align the shadow on to the ground and rotate it towards fighter jet's heading
				Quaternion decalRotation = Quaternion.Euler(270.0f, mTransform.localRotation.eulerAngles.y, 0.0f);
				
				// Stretch the shadow width when the fighter jet is rotating around z axis
				if(fakeRoll) {
					float zRotation = Mathf.Abs(Mathf.DeltaAngle(mTransform.localEulerAngles.z, 0));
	
					if(zRotation > 90.0f) {
						zRotation = 180.0f - zRotation;
					}

					float widthMultiplier = 1.0f - zRotation / 90.0f;
					float minWidth = decalSize.x / 5.0f;
	
					decalSize.x = minWidth + (decalSize.x - minWidth) * widthMultiplier;
				}
				
				// Stretch the shadow length when the fighter jet is rotating around x axis
				if(fakePitch) {
					float xRotation = Mathf.Abs(Mathf.DeltaAngle(mTransform.localEulerAngles.x, 0));
	
					if(xRotation > 90.0f) {
						xRotation = 180.0f - xRotation;
					}
					
					float lengthMultiplier = 1.0f - xRotation / 90.0f;
					float minLength = decalSize.y / 4.0f;				
	
					decalSize.y = minLength + (decalSize.y - minLength) * lengthMultiplier;
				}

				// Offset the shadow slightly above the ground to avoid z-fighting.
				Vector3 decalPosition = new Vector3(mTransform.position.x, groundY + 0.05f, mTransform.position.z);
					
				mFastDecalsRenderer.DrawDecal(decalPosition, decalSize, 0, decalColor, decalRotation);
			}
		}
	}
	
}
