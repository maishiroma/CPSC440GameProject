using UnityEngine;
using System.Collections;

public class DecalSpawner : MonoBehaviour {
	private FastDecalsAtlasRenderer mAtlasRenderer;
	private int mDecalCount;
	
	void Awake() {
		mAtlasRenderer = GetComponent<FastDecalsAtlasRenderer>();	
		mDecalCount = mAtlasRenderer.atlas.atlasItems.Length;
	}
	
	void Update() {
		
#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0)) {
			SpawnDecal(Input.mousePosition);
		}
#else		
		foreach(Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began) {
				SpawnDecal(touch.position);
			}
		}
#endif
	}
	
	public void SpawnDecal(Vector2 screenPosition) {
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 64.0f)) {
			mAtlasRenderer.DrawDecal(hit.point + hit.normal * 0.05f, Random.Range(0.5f, 1.5f), Random.Range(0, mDecalCount), hit.normal);
		}
	}
}
