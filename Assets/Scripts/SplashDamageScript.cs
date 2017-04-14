using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamageScript : MonoBehaviour {

    public float hitDuration;
    public float fadeTime;
    public Color offColor;

    private MeshRenderer SplashDamageRenderer;
    private Material SplashDamageMat;
    private Color startColor;
    private bool hit;
    private float startFadeTime;
    private bool fading;
    private Color targetColor;
    private Color currentColor;

    public float DirectionalSplashDamageAngleThreshold = 60f;
    public Texture DirectionalSplashDamageImage;
    private Texture NormalSplashDamageImage;
    private Transform CameraTransform;
    private Transform Player;

    public void Hit(GameObject damageObject = null)
    {
       
        if (!hit)
        {
            if (damageObject == null)
            {
                //no hit object

                transform.localEulerAngles.Set(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                SplashDamageMat.mainTexture = NormalSplashDamageImage;
                currentColor = offColor;
                SplashDamageRenderer.enabled = true;
                hit = true;
                fading = true;
                targetColor = startColor;
                startFadeTime = Time.time;
            }
            else
            {
                Vector3 hitDirection = damageObject.transform.position - Player.position;
                hitDirection.Set(hitDirection.x, 0, hitDirection.z);
                hitDirection.Normalize();

                Vector3 CameraDirection = CameraTransform.forward;
                CameraDirection.Set(CameraDirection.x, 0, CameraDirection.z);
                CameraDirection.Normalize();

                float angleBetweenCameraAndHit = Vector3.Angle(CameraDirection, hitDirection);
                Vector3 Cross = Vector3.Cross(CameraDirection, hitDirection);
                if(Cross.y > 0)
                {
                    angleBetweenCameraAndHit = -angleBetweenCameraAndHit;
                }

                if(Mathf.Abs(angleBetweenCameraAndHit) > DirectionalSplashDamageAngleThreshold)
                {
                    //indirect hit
                    SplashDamageMat.mainTexture = DirectionalSplashDamageImage;

                    Debug.Log(angleBetweenCameraAndHit);

                    transform.localRotation = Quaternion.Euler (transform.localRotation.eulerAngles.x, 180 - angleBetweenCameraAndHit, transform.localRotation.eulerAngles.z);
                    currentColor = offColor;
                    SplashDamageRenderer.enabled = true;
                    hit = true;
                    fading = true;
                    targetColor = startColor;
                    startFadeTime = Time.time;
                }
                else
                {
                    //direct hit

                    transform.localEulerAngles.Set(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
                    SplashDamageMat.mainTexture = NormalSplashDamageImage;
                    currentColor = offColor;
                    SplashDamageRenderer.enabled = true;
                    hit = true;
                    fading = true;
                    targetColor = startColor;
                    startFadeTime = Time.time;
                }

            }
           
        }
        else
        {
            return;
        }
       
        
    }


	// Use this for initialization
	void Start ()
    {
        SplashDamageRenderer = gameObject.GetComponent<MeshRenderer>();
        SplashDamageMat = SplashDamageRenderer.material;
        startColor = SplashDamageMat.color;
        NormalSplashDamageImage = SplashDamageMat.mainTexture;
        SplashDamageMat.color = offColor;
        currentColor = offColor;
        CameraTransform = transform.parent;
        Player = GameObject.Find("PlayerSpawnLocation").transform;
	}
	

    void FadeOutSplashDamage()
    {
        fading = true;
        startFadeTime = Time.time;
        targetColor = offColor; 
    }

    void FadeSplashDamageColor()
    {
        if(Time.time < startFadeTime + fadeTime)
        {
            Debug.Log("fading");
            SplashDamageMat.SetColor("_Color",Color.Lerp(currentColor, targetColor, ((Time.time - startFadeTime) / fadeTime)));
        }
        else
        {

            SplashDamageMat.color = targetColor;
            currentColor = targetColor;
            fading = false;

            if(targetColor == startColor)
            {
                //finished fading in SplashDamage
                Invoke("FadeOutSplashDamage", hitDuration);
                fading = false;
            }
            else if(targetColor == offColor)
            {
                //finished fading out SplashDamage
                fading = false;
                hit = false;
                SplashDamageRenderer.enabled = false;
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (fading)
        {
            FadeSplashDamageColor();
        }
	}
}
