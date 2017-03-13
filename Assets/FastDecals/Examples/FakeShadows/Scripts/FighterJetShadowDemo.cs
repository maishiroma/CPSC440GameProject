using UnityEngine;
using System.Collections;

public class FighterJetShadowDemo : MonoBehaviour {
	public Transform fighterJetPrefab;
	public GUIText infoText;

	private Transform[] mFighterJets;
	private float mPitch = 0.0f;
	private float mRoll = 0.0f;
	private float mHeight = 10.0f;
	private float mDirection = 0.0f;
	
	void Awake() {
		if(infoText) {
			infoText.enabled = false;
		}

		if(fighterJetPrefab) {
			mFighterJets = new Transform[49];
			int i=0;
			for(int x=-150; x<=150; x+=50) {
				for(int z=-150; z<=150; z+=50) {
					mFighterJets[i] = (Transform) Instantiate(fighterJetPrefab, new Vector3(x, mHeight, z), Quaternion.identity);	
					i++;
				}
			}
		}
	}

	void Update() {
		if(mFighterJets != null) {
			for(int i=0; i<mFighterJets.Length; i++) {
				mFighterJets[i].rotation = Quaternion.Euler(mPitch, mDirection, mRoll);	
				mFighterJets[i].position = new Vector3(mFighterJets[i].position.x, mHeight, mFighterJets[i].position.z);
			}
		}
	}

	void OnGUI() {
		GUI.BeginGroup(new Rect(10.0f, 10.0f, 256.0f, 40.0f), "Height");
		mHeight = GUI.HorizontalSlider(new Rect(0, 20, 100, 20.0f), mHeight, 1.0f, 32.0f);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(10.0f, 60.0f, 256.0f, 40.0f), "Roll");
        mRoll = GUI.HorizontalSlider(new Rect(0, 20, 100, 20.0f), mRoll, -180.0f, 180.0f);
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(10.0f, 110.0f, 256.0f, 40.0f), "Pitch");
		mPitch = GUI.HorizontalSlider(new Rect(0, 20, 100, 20.0f), mPitch, -45.0f, 45.0f);
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(10.0f, 160.0f, 256.0f, 40.0f), "Direction");
		mDirection = GUI.HorizontalSlider(new Rect(0, 20, 100, 20.0f), mDirection, -180.0f, 180.0f);
		GUI.EndGroup();
    }
}
