using UnityEngine;
using System.Collections;

public class LocalSpaceDecalSpawner : MonoBehaviour {
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
			Transform target = hit.transform;

			FastDecalsAtlasRenderer atlasRenderer = target.GetComponentInChildren<FastDecalsAtlasRenderer>();

			if(atlasRenderer != null && atlasRenderer.atlas != null) {
				Vector3 worldPos = hit.point + hit.normal * 0.05f;
				Vector3 localPosition = atlasRenderer.transform.InverseTransformPoint(worldPos);
				Vector3 localNormal = atlasRenderer.transform.InverseTransformDirection(hit.normal);

				atlasRenderer.DrawDecal(localPosition, Random.Range(0.1f, 0.5f), Random.Range(0, atlasRenderer.atlas.atlasItems.Length), target.name.Contains("1") ? Color.red : Color.blue, localNormal);
			}
		}
	}
}
