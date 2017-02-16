using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClip : MonoBehaviour {

    public Transform Clip;
    public float ammoLeft;
    private Animator ClipAnims;

    private Vector3 startPoint;
    private bool flyingToTarget;

    private Transform LoadPoint;
    private Transform ClipPoint;
    private GunShoot Gun;

    public float flySpeed = 12f;
    public float flyDelay = 1f;

	// Use this for initialization
	void Start ()
    {
        ammoLeft = 1f;
        startPoint = transform.position;
        ClipPoint = GameObject.Find("ClipPoint").transform;
        Gun = GameObject.Find("Gun").GetComponent<GunShoot>();
        ClipAnims = gameObject.GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (flyingToTarget)
        {
            float t = (Vector3.Distance(startPoint, LoadPoint.position) - (Vector3.Distance(transform.position, LoadPoint.position)) / Vector3.Distance(startPoint, LoadPoint.position));
            if (Vector3.Distance(transform.position, LoadPoint.position) > 0.1f)
            {
                //aim towards gun
                //Vector3 dir = (LoadPoint.position - transform.position).normalized;

                transform.position = Vector3.Lerp(transform.position, LoadPoint.position, Time.deltaTime * flySpeed * Mathf.Max(0.2f, t));
                transform.rotation = Quaternion.Lerp(transform.rotation, LoadPoint.rotation, Time.deltaTime * flySpeed * Mathf.Max(0.2f, t));
                
            }
            else
            {
                LoadClip();
            }
        }
	}

    public void FlyToGun(Transform Target)
    {
        Invoke("flying", flyDelay);
        LoadPoint = Target;
    }

    void LoadClip()
    {
        transform.position = LoadPoint.position;
        transform.rotation = LoadPoint.rotation;
        transform.SetParent(ClipPoint);
        flyingToTarget = false;
        ClipAnims.enabled = true;
        ClipAnims.SetTrigger("Insert");
        if(Gun == null)
        {
            Gun = GameObject.Find("Gun").GetComponent<GunShoot>();
        }
        Gun.ReloadClip(gameObject);
    }

    void flying()
    {
        flyingToTarget = true;
    }

    public void ResizeClip(float currentAmmo, float maxAmmo)
    {
        ammoLeft = currentAmmo / maxAmmo;
        Clip.localScale = new Vector3(1, ammoLeft, 1);
    }
}
