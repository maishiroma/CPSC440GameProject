using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunWhileTurning : MonoBehaviour {

    private Camera MainCamera;
    public Color fadeColor;
    public Color otherStartColor;
    private Color startColor;
    public float distanceFromGun = 10f;
    public float radius = 1f;
    public float minThreshold;
    public float maxThreshold;

    public float minRotation = 0.1f;
    public float maxRotation = 3f;
    public float lookXMaxRotation = 55f;

    private Vector3 lastDirection;
    private Quaternion rotation;

    private float lastX;
    private float lastY;

    Animator anims;
    MeshRenderer Reticle;


	// Use this for initialization
	void Start ()
    {
	    rotation = transform.rotation;
        MainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        anims = gameObject.GetComponent<Animator>();
        Reticle = GameObject.Find("GvrReticlePointer").GetComponent<MeshRenderer>();
        Reticle.material.color = otherStartColor;
        startColor = Reticle.material.color;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        

        float deltaX = (MainCamera.transform.rotation.eulerAngles.x - lastX) * Time.deltaTime;

        float deltaY = (MainCamera.transform.rotation.eulerAngles.y - lastY) * Time.deltaTime;

        if(Mathf.Abs(deltaX) > minThreshold*1.5f || Mathf.Abs(deltaY)> minThreshold*1.5f)
        {
            Reticle.material.color = Color.Lerp(Reticle.material.color, fadeColor, Time.deltaTime * 10f);
        }
        else
        {
            Reticle.material.color = Color.Lerp(Reticle.material.color, startColor, Time.deltaTime * 4f);
        }

        if (deltaX > minThreshold)
        {
            anims.SetFloat("X", Mathf.Lerp(anims.GetFloat("X"), -1, Time.deltaTime * 2f));
        }
        else if(deltaX < -minThreshold)
        {
            anims.SetFloat("X", Mathf.Lerp(anims.GetFloat("X"), 1, Time.deltaTime * 2f));
        }
        else
        {
            anims.SetFloat("X", Mathf.Lerp(anims.GetFloat("X"), 0, Time.deltaTime * 4f));
        }

        if (deltaY > minThreshold)
        {
            anims.SetFloat("Y", Mathf.Lerp(anims.GetFloat("Y"),1, Time.deltaTime * 2f));
        }
        else if (deltaY < -minThreshold)
        {
            anims.SetFloat("Y", Mathf.Lerp(anims.GetFloat("Y"), -1, Time.deltaTime * 2f));
        }
        else
        {
            anims.SetFloat("Y", Mathf.Lerp(anims.GetFloat("Y"), 0, Time.deltaTime * 4f));
        }

        if(MainCamera.transform.rotation.eulerAngles.x > 25 && MainCamera.transform.rotation.eulerAngles.x < 90)
        {
            
            anims.SetFloat("Look", Mathf.Lerp(0f, 1f, (MainCamera.transform.rotation.eulerAngles.x - 25) / (55 - 25)));
        }
        else
        {
            anims.SetFloat("Look", 0);
        }

        lastX = MainCamera.transform.rotation.eulerAngles.x;
        lastY = MainCamera.transform.rotation.eulerAngles.y;

        lastDirection = transform.forward;
	}
}
