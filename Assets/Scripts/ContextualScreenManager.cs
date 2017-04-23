using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextualScreenManager : MonoBehaviour {

    public ContextualScreenPage[] ContextualPages;
    public float pageFadeTime = 0.1f;
    private ContextualScreenPage currentPage;
    private ContextualScreenPage targetPage;
    private bool switching;
    public ContextualScreenPage defaultPage;

	// Use this for initialization
	void Start ()
    {
        Invoke("debugLoadHealthPage", .5f);	
        
	}
	


    IEnumerator SwitchingToPage()
    {
        while (true)
        {
            if (currentPage != null && currentPage.visible)
            {
                //do nothing;
            }
            else if(currentPage == null || !currentPage.visible)
            {
                targetPage.FadeIn();
                switching = false;
                currentPage = targetPage;
                yield break;

            }
            yield return null;
        }
    }


    public void SwitchToDefaultPage(ContextualScreenPage ContextPage = null)
    {
        if (ContextPage == null)
        {
            if (currentPage != null)
            {
                currentPage.FadeOut();
            }
            targetPage = defaultPage;
            StartCoroutine(SwitchingToPage());
        }
        else
        {
            if(ContextPage == currentPage)
            {
                currentPage.FadeOut();
                targetPage = defaultPage;
                StartCoroutine(SwitchingToPage());
            }
            else
            {
                return;
            }
        }
    }

    public void SwitchToPage(ContextualScreenPage Page)
    {
        if(currentPage == Page || switching)
        {
            return;
        }
        else
        {
            if(currentPage!= null)
            {
                currentPage.FadeOut();
            }

            foreach (ContextualScreenPage page in ContextualPages)
            {
                if (page == Page)
                {
                    targetPage = page;
                    switching = true;
                    StartCoroutine(SwitchingToPage());
                    break;
                }
            }
        }
    }

    void debugLoadHealthPage()
    {
        SwitchToDefaultPage();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
