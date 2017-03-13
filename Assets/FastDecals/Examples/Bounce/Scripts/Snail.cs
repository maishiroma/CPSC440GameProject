using UnityEngine;
using System.Collections;

public class Snail : MonoBehaviour {	
	private Transform mTransform;
	private Color32 mSnailColor;
	
	void Awake() {
		mTransform = GetComponent<Transform>();
		Color materialColor = GetComponent<MeshRenderer>().material.color;
		mSnailColor = new Color32((byte)(materialColor.r * 255), (byte)(materialColor.g * 255), (byte)(materialColor.b * 255), 255);
	}

	void OnCollisionEnter(Collision collision) {
		Ball ball = collision.transform.GetComponent<Ball>();
		
		if(ball) {
			// If a ball hits the snail, snail will die and leave a blood effect to the ground which will automatically age and fade away when new blood effects are added
			MyDecalRenderers.SharedInstance.bloodEffectsRenderer.DrawDecal(mTransform.position - Vector3.up * 0.05f, Random.Range(2.0f, 2.5f),  Random.Range(0, 4), mSnailColor);

			Destroy(gameObject);
		}
	}
}
