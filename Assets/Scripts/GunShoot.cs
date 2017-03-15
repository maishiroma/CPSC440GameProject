using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour {

    public float fireDelay = .1f;

    Camera mainCam;

    //trigger vars
    public float triggerHoldThreshold = .25f;
    private float triggerDownTime;
    private bool isTriggerDown;
    private bool isTriggerHolding;

    //Ammo Stuff
    public float startAmmo = 100f;
    public float maxAmmo = 100f;
    public float ammoPerShoot = 2f;
    private bool outOfAmmo = false;
    private float currentAmmo;

    public AmmoClip currentClip;
    public Animator currentClipAnims;
	public AudioClip gunFireSound;


    public TrapMenu trapMenu;

    //gun vars
    public Transform shootPoint;
    public GameObject bullet;
    Animator gunAnims;

    public Marker marker;
   

	// Use this for initialization
	void Start ()
    {
        mainCam = Camera.main;
        gunAnims = GetComponent<Animator>();
        currentAmmo = startAmmo;
        //currentClip = GameObject.Find("AmmoClip").GetComponent<AmmoClip>();
        currentClipAnims = currentClip.gameObject.GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update ()
    {
        if (!isTriggerDown)
        {
            /*
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == 1)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        if (!marker.visible)
                        {
                            isTriggerDown = true;
                            triggerDownTime = Time.time;
                        }
                    }
                }
            }
            */

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                //Debug.Log(marker.visible);

                isTriggerDown = true;
                triggerDownTime = Time.time;
            }
        }

        if (marker.visible)
        {
            if ((Input.GetKeyUp(KeyCode.Mouse0) || (Input.GetKeyUp(KeyCode.Joystick1Button1) && isTriggerDown)))
            {
                isTriggerDown = false;
                if (!isTriggerHolding)
                {
                    Shoot();
                }
                else if (isTriggerHolding)
                {
                    trapMenu.toggleRadialMenu();
                    isTriggerHolding = false;
                }
            }
            else if (isTriggerDown)
            {
                float triggerTotalTime = Time.time - triggerDownTime;

                if (triggerTotalTime > triggerHoldThreshold)
                {
                    if (!isTriggerHolding)
                    {
                        marker.holdingTrigger = true;
                        isTriggerHolding = true;
                        trapMenu.toggleRadialMenu();
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                Shoot();
                isTriggerDown = false;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) || (Input.GetKeyUp(KeyCode.Joystick1Button1) && isTriggerDown))
            {
                isTriggerDown = false;
                if (isTriggerHolding)
                {
                    trapMenu.toggleRadialMenu();
                    isTriggerHolding = false;
                }
            }
        }

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }



	}

    Vector3 getShootDirection()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, Mathf.Infinity))
        {
            return (hit.point - shootPoint.position).normalized;
        }
        else
        {
            return mainCam.transform.forward;
        }
        
    }

    void Shoot()
    {
        if (!outOfAmmo)
        {
            Vector3 shootDir = getShootDirection();
            GameObject _bullet = (GameObject)Instantiate(bullet, shootPoint.position, Quaternion.LookRotation(shootDir), GameObject.Find("AllBullets").transform);
            
			// Here, sound is played when firing
			//AudioSource playSound = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<AudioSource>();
			//playSound.PlayOneShot(gunFireSound,.15f);

			gunAnims.SetTrigger("Shoot");
            currentAmmo -= ammoPerShoot;
            currentClip.ResizeClip(currentAmmo, maxAmmo);
            if(currentAmmo <= 0 && currentClip != null && !outOfAmmo)
            {
                outOfAmmo = true;
                DropAmmoClip();
            }
        }
        else
        {
            //tries to shoot without ammo
            //misfire
            gunAnims.SetTrigger("Misfire");
        }

    }

    public void ReloadClip(GameObject clip)
    {
        currentClip = clip.GetComponent<AmmoClip>();
        currentClipAnims = currentClip.GetComponent<Animator>();
        currentAmmo = startAmmo;
        outOfAmmo = false;
    }

    public void DropAmmoClip()
    {
        if (!outOfAmmo)
        {
            outOfAmmo = true;
        }

        if (currentClip != null)
        {
            currentClipAnims.SetTrigger("Remove");
            Invoke("Drop", .4f);
        }
    }

    void Drop()
    {
        currentClip.gameObject.transform.parent = null;
        currentClip.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        currentClipAnims.enabled = false;
        currentClip = null;
    }

}
