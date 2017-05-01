using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanAnimateTexture : MonoBehaviour {

    public Texture[] ScanTextures;
    public float TimeImageShown = 0.3f;
    public GameObject ScanImagePlane;
    private Material ScanImageMaterial;
    private int currentImage = 0;
    private ContextualScreenPage Page;
    private bool pageActive = false;

	// Use this for initialization
	void Start ()
    {
        ScanImageMaterial = ScanImagePlane.GetComponent<MeshRenderer>().material;
        currentImage = 0;
        Page = gameObject.GetComponent<ContextualScreenPage>();
        pageActive = false;
        StartCoroutine(checkIfActive());
	}
	

    IEnumerator AnimatingTexture()
    {
        while (true)
        {
            ScanImageMaterial.mainTexture = ScanTextures[currentImage];
            currentImage++;
            if(currentImage > ScanTextures.Length -1)
            {
                currentImage = 0;
            }
            yield return new WaitForSeconds(TimeImageShown);
        }
    }


	IEnumerator checkIfActive()
    {
        while (true)
        {
            if(!pageActive && Page.active)
            {
                pageActive = true;
                StartCoroutine(AnimatingTexture());
            }
            else if(pageActive && !Page.active)
            {
                StopCoroutine(AnimatingTexture());
                currentImage = 0;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
