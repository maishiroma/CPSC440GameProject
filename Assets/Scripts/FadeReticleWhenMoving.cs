using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeReticleWhenMoving : MonoBehaviour {

    MeshRenderer mr;
    private Material mat;
    Camera MainCamera;
    public Color fadeColor;
    private Color startColor;
    public float focusFreq = 0.1f;
    public float fadeTime = .5f;
    public float minFadeTime = 0.5f;
    public float fadeAngleDelta = 15f;
    private float startFadeTime;

    private bool visible;

    private bool fadingIn;
    private bool fadingOut;
    private Vector3 lastDirection;
    private bool fadeDelay = false;
	// Use this for initialization
	void Start ()
    {
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }


	// Update is called once per frame
	void Update ()
    {
        Debug.Log(Mathf.Abs(Vector3.Angle(lastDirection, MainCamera.transform.forward)) * Time.deltaTime);


        lastDirection = MainCamera.transform.forward;
    }
}
