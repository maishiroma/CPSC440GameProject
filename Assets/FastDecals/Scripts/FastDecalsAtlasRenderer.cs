using UnityEngine;
using System.Collections;
using pmjo.FastDecals;

[AddComponentMenu("Fast Decals/Atlas Renderer")]
public class FastDecalsAtlasRenderer : MonoBehaviour {
	public Material material;
	public FastDecalsAtlas atlas;
	public FastDecals.RenderingMode type = FastDecals.RenderingMode.Degenerate;
	public FastDecals.SpaceMode space = FastDecals.SpaceMode.WorldSpace;
	public int maxDecalCount = 64;
	public bool generateNormals = false;
	public Vector3 bounds = new Vector3(50.0f, 25.0f, 50.0f);
	public bool suppressDebugMessages = false;

	private Transform mTransform;
	private FastDecals mFastDecals;
	private FastDecalsAtlas mFastDecalsAtlas;
	private Quaternion mDefaultRotation = Quaternion.LookRotation(Vector3.up);
	private Vector2 mDestinationTextureOffset;
	private Vector2 mDestinationTextureTiling;

	void Awake() {
		mTransform = GetComponent<Transform>();

		mFastDecals = new FastDecals();

		if(mFastDecals.Initialize(type, mTransform, MeshInstanceCreated, 2, 2, maxDecalCount, generateNormals, suppressDebugMessages, CalculateBounds(mTransform.position))) {
			if(Debug.isDebugBuild && !suppressDebugMessages) {
				Debug.Log(mFastDecals.GetProductName() + " " + mFastDecals.GetVersion().ToString() + " instance initialized with " + maxDecalCount + " decals.");
			}
		}
		else {
			if(Debug.isDebugBuild && !suppressDebugMessages) {
				Debug.Log(mFastDecals.GetProductName() + " error: initialization failed.");
			}
		}

		if(atlas == null) {
			mFastDecalsAtlas = GetComponent<FastDecalsAtlas>();
		}
		else {
			mFastDecalsAtlas = atlas;
		}

		material.mainTexture = mFastDecalsAtlas.atlasTexture;
	}

	public void DrawDecal(Vector3 pos, float size, int atlasItemIndex) {
		DrawDecal(pos, new Vector2(size, size), atlasItemIndex);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int atlasItemIndex) {
		if(mFastDecalsAtlas.GetTexCoordsByIndex(atlasItemIndex, out mDestinationTextureOffset, out mDestinationTextureTiling)) {
			mFastDecals.DrawDecal(pos, size, mDestinationTextureOffset, mDestinationTextureTiling, (type == FastDecals.RenderingMode.Aging) ? Quaternion.Euler(-90, Random.Range(0, 360), 0) : mDefaultRotation);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int atlasItemIndex, Quaternion rotation) {
		DrawDecal(pos, new Vector2(size, size), atlasItemIndex, rotation);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int atlasItemIndex, Quaternion rotation) {
		if(mFastDecalsAtlas.GetTexCoordsByIndex(atlasItemIndex, out mDestinationTextureOffset, out mDestinationTextureTiling)) {
			mFastDecals.DrawDecal(pos, size, mDestinationTextureOffset, mDestinationTextureTiling, rotation);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int atlasItemIndex, Vector3 normal) {
		DrawDecal(pos, new Vector2(size, size), atlasItemIndex, normal);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int atlasItemIndex, Vector3 normal) {
		if(mFastDecalsAtlas.GetTexCoordsByIndex(atlasItemIndex, out mDestinationTextureOffset, out mDestinationTextureTiling)) {
			mFastDecals.DrawDecal(pos, size, mDestinationTextureOffset, mDestinationTextureTiling, Quaternion.LookRotation(normal));
		}
	}

	public void DrawDecal(Vector3 pos, float size, int atlasItemIndex, Color32 color) {
		DrawDecal(pos, new Vector2(size, size), atlasItemIndex, color);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int atlasItemIndex, Color32 color) {
		if(mFastDecalsAtlas.GetTexCoordsByIndex(atlasItemIndex, out mDestinationTextureOffset, out mDestinationTextureTiling)) {
			mFastDecals.DrawDecal(pos, size, mDestinationTextureOffset, mDestinationTextureTiling, (type == FastDecals.RenderingMode.Aging) ? Quaternion.Euler(-90, Random.Range(0, 360), 0) : mDefaultRotation, color);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int atlasItemIndex, Color32 color, Quaternion rotation) {
		DrawDecal(pos, new Vector2(size, size), atlasItemIndex, color, rotation);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int atlasItemIndex, Color32 color, Quaternion rotation) {
		if(mFastDecalsAtlas.GetTexCoordsByIndex(atlasItemIndex, out mDestinationTextureOffset, out mDestinationTextureTiling)) {
			mFastDecals.DrawDecal(pos, size, mDestinationTextureOffset, mDestinationTextureTiling, rotation, color);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int atlasItemIndex, Color32 color, Vector3 normal) {
		DrawDecal(pos, new Vector2(size, size), atlasItemIndex, color, normal);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int atlasItemIndex, Color32 color, Vector3 normal) {
		if(mFastDecalsAtlas.GetTexCoordsByIndex(atlasItemIndex, out mDestinationTextureOffset, out mDestinationTextureTiling)) {
			mFastDecals.DrawDecal(pos, size, mDestinationTextureOffset, mDestinationTextureTiling, Quaternion.LookRotation(normal), color);
		}
	}

	public void DrawDecal(Vector3 pos, float size, Vector2 textureOffset, Vector2 textureTiling) {
		DrawDecal(pos, new Vector2(size, size), textureOffset, textureTiling);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, Vector2 textureOffset, Vector2 textureTiling) {
		mFastDecals.DrawDecal(pos, size, textureOffset, textureTiling, (type == FastDecals.RenderingMode.Aging) ? Quaternion.Euler(-90, Random.Range(0, 360), 0) : mDefaultRotation);
	}

	public void DrawDecal(Vector3 pos, float size, Vector2 textureOffset, Vector2 textureTiling, Quaternion rotation) {
		DrawDecal(pos, new Vector2(size, size), textureOffset, textureTiling, rotation);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, Vector2 textureOffset, Vector2 textureTiling, Quaternion rotation) {
		mFastDecals.DrawDecal(pos, size, textureOffset, textureTiling, rotation);
	}

	public void DrawDecal(Vector3 pos, float size, Vector2 textureOffset, Vector2 textureTiling, Vector3 normal) {
		DrawDecal(pos, new Vector2(size, size), textureOffset, textureTiling, normal);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, Vector2 textureOffset, Vector2 textureTiling, Vector3 normal) {
		mFastDecals.DrawDecal(pos, size, textureOffset, textureTiling, Quaternion.LookRotation(normal));
	}

	public void DrawDecal(Vector3 pos, float size, Vector2 textureOffset, Vector2 textureTiling, Color32 color) {
		DrawDecal(pos, new Vector2(size, size), textureOffset, textureTiling, color);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, Vector2 textureOffset, Vector2 textureTiling, Color32 color) {
		mFastDecals.DrawDecal(pos, size, textureOffset, textureTiling, (type == FastDecals.RenderingMode.Aging) ? Quaternion.Euler(-90, Random.Range(0, 360), 0) : mDefaultRotation, color);
	}

	public void DrawDecal(Vector3 pos, float size, Vector2 textureOffset, Vector2 textureTiling, Color32 color, Quaternion rotation) {
		DrawDecal(pos, new Vector2(size, size), textureOffset, textureTiling, color, rotation);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, Vector2 textureOffset, Vector2 textureTiling, Color32 color, Quaternion rotation) {
		mFastDecals.DrawDecal(pos, size, textureOffset, textureTiling, rotation, color);
	}

	public void DrawDecal(Vector3 pos, float size, Vector2 textureOffset, Vector2 textureTiling, Color32 color, Vector3 normal) {
		DrawDecal(pos, new Vector2(size, size), textureOffset, textureTiling, color, normal);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, Vector2 textureOffset, Vector2 textureTiling, Color32 color, Vector3 normal) {
		mFastDecals.DrawDecal(pos, size, textureOffset, textureTiling, Quaternion.LookRotation(normal), color);
	}

	public void ClearDecals() {
		mFastDecals.ClearDecals();
	}

	void LateUpdate () {
		mFastDecals.SetSpaceMode(space);
		mFastDecals.LateUpdate(material, CalculateBounds((space == FastDecals.SpaceMode.WorldSpace) ? mTransform.position : Vector3.zero));
	}

	void OnDrawGizmosSelected() {
		Bounds currentBounds = CalculateBounds(transform.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(currentBounds.center, currentBounds.size);
    }

	private Bounds CalculateBounds(Vector3 position) {
		return new Bounds(position, new Vector3(bounds.x, bounds.y, bounds.z));
	}

	private void MeshInstanceCreated(Mesh mesh) {
		// If you are using multithreaded rendering you may not use MarkDynamic since it is not supported.		
		// If you are not using multithreaded rendering you may get an extra performance boost by uncommenting the line below.
#if !(UNITY_2_6	|| UNITY_2_6_1 || UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
		//mesh.MarkDynamic();
#endif
	}
}
