using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPageManager : MonoBehaviour
{

    public float pageXOffset = 6f;
    public GameObject[] pages;

    public bool switchingpages = false;
    public float switchSpeed = 5f;
    private float currentPageOffset = 0;
    public int currentPage = 1;
    private GameObject currentPageGO;

	// Use this for initialization
	void Start ()
    {
        currentPageGO = pages[0];
        currentPageGO.GetComponent<PageScript>().Open();
	}
	
    public void SwitchToPage(int pageNum)
    {
        if(pageNum <= pages.Length && pageNum != currentPage)
        {
            pages[currentPage - 1].GetComponent<PageScript>().Close();


            currentPageOffset = (pageNum-1) * pageXOffset;
            currentPageGO = pages[pageNum - 1];
            currentPage = pageNum;

            switchingpages = true;

            pages[pageNum - 1].GetComponent<PageScript>().Open();
        }
    }


	// Update is called once per frame
	void Update ()
    {
        if (switchingpages)
        {
            if (Mathf.Abs((-transform.localPosition.x) - currentPageOffset) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(currentPageOffset, 0, 0), Time.deltaTime * switchSpeed);
            }
            else
            {
                transform.localPosition = new Vector3(currentPageOffset, 0, 0);
                switchingpages = false;
            }
        }
	}
}
