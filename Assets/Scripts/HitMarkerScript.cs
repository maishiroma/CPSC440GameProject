using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMarkerScript : MonoBehaviour {

    public Color defaultColor;
    GameObject HitMarkerIcon;
    Material HitMarkerMat;
    MeshRenderer HitMarkerRenderer;
    public float hitFadeTime = 0.1f;
    public float deathHoldTime = .3f;
    public float deathFadeTime = 0.6f;
    public Color deathColor;

    private Color startColor;
    private Color currentColor;
    private Color targetColor;
    private bool fadingColor;
    private bool showingHitMarker = false;
    private bool death;
    private bool hit;
    private bool holdingDeath;
    private float currentTransitionTime;
    private float startTransitionTime;

	// Use this for initialization
	void Start ()
    {
        HitMarkerIcon = transform.FindChild("HitMarkerIcon").gameObject;
        HitMarkerRenderer = HitMarkerIcon.GetComponent<MeshRenderer>();
        startColor = HitMarkerIcon.GetComponent<MeshRenderer>().material.color;
        HitMarkerMat = HitMarkerIcon.GetComponent<MeshRenderer>().material;
        HitMarkerMat.color = defaultColor;
        currentColor = defaultColor;
        showingHitMarker = false;
    }

    public void Death()
    {
        if (!showingHitMarker)
        {
            showingHitMarker = true;
            HitMarkerRenderer.enabled = true;
        }
        death = true;
        currentColor = HitMarkerMat.color;
        targetColor = deathColor;
        startTransitionTime = Time.time;
        currentTransitionTime = hitFadeTime;
        fadingColor = true;
    }

    public void Hit()
    {
        if(!showingHitMarker)
        {
            showingHitMarker = true;
            HitMarkerRenderer.enabled = true;
        }
        if (!death)
        {
            currentColor = HitMarkerMat.color;
            targetColor = startColor;
            fadingColor = true;
            startTransitionTime = Time.time;
            currentTransitionTime = hitFadeTime;
        } 
    }

    public void StopDeath()
    {
        death = false;
        fadingColor = true;
        targetColor = defaultColor;
        startTransitionTime = Time.time;
        currentTransitionTime = deathFadeTime;
        holdingDeath = false;
        Debug.Log("StopDeath");
    }

	// Update is called once per frame
	void Update ()
    {
        if (fadingColor)
        {
            if (!death)
            {
                if (targetColor == startColor)
                {
                    if (Time.time < (startTransitionTime + currentTransitionTime))
                    {
                        float t = (Time.time - startTransitionTime) / (currentTransitionTime);
                        HitMarkerMat.SetColor("_Color", Color.Lerp(currentColor, targetColor, t));
                    }
                    else
                    {
                        targetColor = defaultColor;
                        currentColor = startColor;
                        startTransitionTime = Time.time;
                    }

                }
                else if (targetColor == defaultColor)
                {
                    if (Time.time < (startTransitionTime + currentTransitionTime))
                    {
                        float t = (Time.time - startTransitionTime) / (currentTransitionTime);
                        HitMarkerMat.SetColor("_Color", Color.Lerp(currentColor, targetColor, t));
                    }
                    else
                    {
                        fadingColor = false;
                        currentColor = defaultColor;
                        showingHitMarker = false;
                        HitMarkerRenderer.enabled = false;

                    }
                }
            } 
            else if(death)
            {
                if(targetColor == deathColor && !holdingDeath)
                {
                    if (Time.time < (startTransitionTime + currentTransitionTime))
                    {
                        
                        float t = (Time.time - startTransitionTime) / (currentTransitionTime);
                        HitMarkerMat.SetColor("_Color", Color.Lerp(currentColor, targetColor, t));
                    }
                    else
                    {
                        currentColor = deathColor;
                        Invoke("StopDeath", deathHoldTime);
                        holdingDeath = true;
                    }
                }
            }
        }
	}
}
