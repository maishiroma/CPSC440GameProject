using UnityEditor;

[CustomEditor(typeof(FastDecalsAtlasRenderer))]
public class FastDecalsAtlasRendererEditor : Editor {
	// Automatically add FastDecalsAtlas but do not require it so atlas can be shared between renderers
	// By using [RequireComponent (typeof (FastDecalsAtlas))] would always require it on same gameobject
	public void OnEnable() {
		FastDecalsAtlasRenderer atlasRenderer = (FastDecalsAtlasRenderer) target;
		
		if(atlasRenderer != null) {
			if(atlasRenderer.atlas == null) {
				atlasRenderer.atlas = atlasRenderer.GetComponent<FastDecalsAtlas>();
				
				if(atlasRenderer.atlas == null) {
					atlasRenderer.atlas = atlasRenderer.gameObject.AddComponent<FastDecalsAtlas>();
				}
			}
		}
	}
}
