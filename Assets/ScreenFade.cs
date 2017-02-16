using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour {

    public float fadeTime = 1f;
    private Material fadeMat;
    private float fadeAmt = 1f;
    public float startFade = 0f;
    public float endFade = 1f;

    private bool isFadeIn;
    private bool fading = false;
    private float fadeStart;
    private float fadeEnd;
    private float fadeStartTime;


	// Use this for initialization
	void Start ()
    {
        fadeMat = gameObject.GetComponent<MeshRenderer>().material;
        Fade(true);
	}

    private void Update()
    {
        if (fading)
        {

            if(Time.time < (fadeStartTime + fadeTime))
            {
                float t = Mathf.Lerp(fadeStart, fadeEnd, (((Time.time+.05f) - fadeStartTime) / (fadeTime)));
                fadeMat.SetFloat("_Cutoff", t);
            }
            else
            {
                fading = false;
                if (isFadeIn)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }


    public void Fade(bool fadeIn = true)
    {
        if (fadeIn)
        {
            fadeStart = startFade;
            fadeEnd = endFade;
            fading = true;
            fadeStartTime = Time.time;
            isFadeIn = true;
        }
        else
        {
            fadeStart = endFade;
            fadeEnd = startFade;
            fading = true;
            fadeStartTime = Time.time;
            isFadeIn = false;
        }

    }
}



