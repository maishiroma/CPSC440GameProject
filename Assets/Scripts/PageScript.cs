using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PageScript : MonoBehaviour {

    public string pageName;
    public bool enabled;
    private GameObject pageGroup;

    Animator PageAnims;

    // Use this for initialization
    void Start ()
    {
        pageGroup = transform.FindChild("pageGRP").gameObject;
        enabled = false;
        PageAnims = gameObject.GetComponent<Animator>();
        PageAnims.SetBool("Open", false);
    }

   
    public void Open()
    {
        PageAnims.SetBool("Open", true);
        enabled = true;
        ToggleActive();
    }

    public void Close()
    {
        PageAnims.SetBool("Open", false);
        enabled = false;
        Invoke("ToggleActive", 1f);
    }

    void ToggleActive()
    {
        if (enabled)
        {
            pageGroup.SetActive(true);
        }
        else
        {
            pageGroup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
