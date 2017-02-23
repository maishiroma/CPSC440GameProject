using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour {

    private GunShoot Gun;

    private Animator ammoBoxAnims;
    private bool lifting = false;
    private bool open = false;

    public Transform AmmoSpawnPoint;
    public GameObject AmmoClip;

    public GameObject currentAmmoClip;
    public Transform ClipLoadPoint;

    // Use this for initialization
    void Start ()
    {
        ammoBoxAnims = gameObject.GetComponent<Animator>();
        lifting = false;
        currentAmmoClip = null;
        Gun = GameObject.Find("Gun").GetComponent<GunShoot>();
	}
	
    public void Reload()
    {
        currentAmmoClip.GetComponent<AmmoClip>().FlyToGun(ClipLoadPoint);
        currentAmmoClip = null;
    }


	// Update is called once per frame
	public void Lift ()
    {
		if(lifting == false)
        {
            if (currentAmmoClip == null)
            {
                currentAmmoClip = (GameObject)Instantiate(AmmoClip, AmmoSpawnPoint.position, AmmoSpawnPoint.rotation);
            }
            lifting = true;
            ammoBoxAnims.SetBool("Lift", true);
        }
	}

    public void Open()
    {
        ammoBoxAnims.SetBool("Open", true);
        lifting = false;
        ammoBoxAnims.SetBool("Lift", false);
        open = true;
    }
    public void Close()
    {
        if (lifting)
        {
            lifting = false;
            ammoBoxAnims.SetBool("Lift", false);
        }
        else if (open)
        {
            ammoBoxAnims.SetBool("Open", false);
            open = false;
        }
        
    }

}
