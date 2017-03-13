using UnityEngine;
using System.Collections;
using pmjo.FastDecals;

[AddComponentMenu("Fast Decals/Renderer")]
public class FastDecalsRenderer : MonoBehaviour {
	public Material material;
	public FastDecals.RenderingMode type = FastDecals.RenderingMode.Degenerate;
	public FastDecals.SpaceMode space = FastDecals.SpaceMode.WorldSpace;
	public int horizontalTiles = 1;
	public int verticalTiles = 1;
	public int maxDecalCount = 64;
	public bool generateNormals = false;
	public Vector3 bounds = new Vector3(50.0f, 25.0f, 50.0f);
	public bool suppressDebugMessages = false;

	private Transform mTransform;
	private FastDecals mFastDecals;
	private Quaternion mDefaultRotation = Quaternion.LookRotation(Vector3.up);
	private Vector2 mTextureTiling;
	private Vector2[] mTextureOffsets;

	void Awake() {
		mTransform = GetComponent<Transform>();

		horizontalTiles = Mathf.Max(Mathf.Min(horizontalTiles, 256), 1);
		verticalTiles = Mathf.Max(Mathf.Min(verticalTiles, 256), 1);

		mFastDecals = new FastDecals();
		mTextureTiling = new Vector2(1.0f / (float)horizontalTiles, 1.0f / (float)verticalTiles);

		// Precalculate texture offsets

		int totalTiles = horizontalTiles * verticalTiles;
		mTextureOffsets = new Vector2[totalTiles];

		for(int i=0; i<totalTiles; i++) {
			mTextureOffsets[i] = CalculateTextureOffset(i, horizontalTiles, verticalTiles);
		}

		if(mFastDecals.Initialize(type, mTransform, MeshInstanceCreated, horizontalTiles, verticalTiles, maxDecalCount, generateNormals, suppressDebugMessages, CalculateBounds(mTransform.position))) {
			if(Debug.isDebugBuild && !suppressDebugMessages) {
				Debug.Log(mFastDecals.GetProductName() + " " + mFastDecals.GetVersion().ToString() + " instance initialized with " + maxDecalCount + " decals.");
			}
		}
		else {
			if(Debug.isDebugBuild && !suppressDebugMessages) {
				Debug.Log(mFastDecals.GetProductName() + " error: initialization failed.");
			}
		}
	}

	public void DrawDecal(Vector3 pos, float size, int tileIndex) {
		DrawDecal(pos, new Vector2(size, size), tileIndex);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int tileIndex) {
		if(tileIndex >= 0 && tileIndex < mTextureOffsets.Length) {
			mFastDecals.DrawDecal(pos, size, mTextureOffsets[tileIndex], mTextureTiling, (type == FastDecals.RenderingMode.Aging) ? Quaternion.Euler(-90, Random.Range(0, 360), 0) : mDefaultRotation);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int tileIndex, Quaternion rotation) {
		DrawDecal(pos, new Vector2(size, size), tileIndex, rotation);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int tileIndex, Quaternion rotation) {
		if(tileIndex >= 0 && tileIndex < mTextureOffsets.Length) {
			mFastDecals.DrawDecal(pos, size, mTextureOffsets[tileIndex], mTextureTiling, rotation);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int tileIndex, Vector3 normal) {
		DrawDecal(pos, new Vector2(size, size), tileIndex, normal);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int tileIndex, Vector3 normal) {
		if(tileIndex >= 0 && tileIndex < mTextureOffsets.Length) {
			mFastDecals.DrawDecal(pos, size, mTextureOffsets[tileIndex], mTextureTiling, Quaternion.LookRotation(normal));
		}
	}

	public void DrawDecal(Vector3 pos, float size, int tileIndex, Color32 color) {
		DrawDecal(pos, new Vector2(size, size), tileIndex, color);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int tileIndex, Color32 color) {
		if(tileIndex >= 0 && tileIndex < mTextureOffsets.Length) {
			mFastDecals.DrawDecal(pos, size, mTextureOffsets[tileIndex], mTextureTiling, (type == FastDecals.RenderingMode.Aging) ? Quaternion.Euler(-90, Random.Range(0, 360), 0) : mDefaultRotation, color);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int tileIndex, Color32 color, Quaternion rotation) {
		DrawDecal(pos, new Vector2(size, size), tileIndex, color, rotation);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int tileIndex, Color32 color, Quaternion rotation) {
		if(tileIndex >= 0 && tileIndex < mTextureOffsets.Length) {
			mFastDecals.DrawDecal(pos, size, mTextureOffsets[tileIndex], mTextureTiling, rotation, color);
		}
	}

	public void DrawDecal(Vector3 pos, float size, int tileIndex, Color32 color, Vector3 normal) {
		DrawDecal(pos, new Vector2(size, size), tileIndex, color, normal);
	}

	public void DrawDecal(Vector3 pos, Vector2 size, int tileIndex, Color32 color, Vector3 normal) {
		if(tileIndex >= 0 && tileIndex < mTextureOffsets.Length) {
			mFastDecals.DrawDecal(pos, size, mTextureOffsets[tileIndex], mTextureTiling, Quaternion.LookRotation(normal), color);
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
		mFastDecals.SetSpaceMode (space);
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

	private static Vector2 CalculateTextureOffset(int i, int xTiles, int yTiles) {
		int x = i % xTiles;
		int y = i / yTiles;
		return new Vector2((float) x / xTiles, (float) y / yTiles);
	}

	private void MeshInstanceCreated(Mesh mesh) {
		// If you are not using multithreaded rendering you may get an extra performance boost by uncommenting the line below.
		// If you are using multithreaded rendering you may not use MarkDynamic since it is not supported.
#if !(UNITY_2_6	|| UNITY_2_6_1 || UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)
		//mesh.MarkDynamic();
#endif
	}
}
