using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageControl : MonoBehaviour
{

    public MenuPageManager mm;
    public int pageNumber;
    // Use this for initialization
    public void SwitchToPage()
    {
        mm.SwitchToPage(pageNumber);
    }
}
