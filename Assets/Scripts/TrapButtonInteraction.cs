using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButtonInteraction : MonoBehaviour {

    private Animator btnAnims;
    private Material btnMaterial;
    private Color StartColor;
    public Color HighlightColor;
    public Color SelectedColor;
    private Color currentColor;
    private Color targetColor;

    private bool switchingColors;
    private float startSwitchTime;
    public float colorSwitchTime = 0.5f;


    private bool selected = false;
    private bool highlighted = false;




	// Use this for initialization
	void Start ()
    {
        btnAnims = gameObject.GetComponent<Animator>();
        btnMaterial = gameObject.GetComponent<MeshRenderer>().material;
        StartColor = btnMaterial.GetColor("_Emission");
	}
	
    public void Deselect()
    {
        selected = false;

        btnAnims.SetBool("Selected", false);

        if (highlighted)
        {
            targetColor = HighlightColor;
            switchingColors = true;
            startSwitchTime = Time.time;
        }
        else
        {
            targetColor = StartColor;
            switchingColors = true;
            startSwitchTime = Time.time;
        }
        
    }

    public void Select()
    {
        selected = true;

        btnAnims.SetBool("Selected", true);

        targetColor = SelectedColor;
        switchingColors = true;
        startSwitchTime = Time.time;
    }

    public void StartHighlighting()
    {
        btnAnims.SetBool("Highlighted", true);

        highlighted = true;

        if (!selected)
        {
            targetColor = HighlightColor;
            switchingColors = true;
            startSwitchTime = Time.time;
        }
        
    }

    public void EndHighlighting()
    {
        btnAnims.SetBool("Highlighted", false);

        if (selected)
        {
           
        }
        else
        {
            targetColor = StartColor;
            switchingColors = true;
            startSwitchTime = Time.time;
        }
    }


    void FadeColor()
    {
        if(Time.time < (startSwitchTime + colorSwitchTime))
        {
            Color fadeColor = Color.Lerp(currentColor, targetColor, Time.time / (startSwitchTime + colorSwitchTime));
            btnMaterial.SetColor("_Emission", fadeColor);
        }
        else
        {
            switchingColors = false;
            currentColor = targetColor;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (switchingColors)
        {
            FadeColor();
        }	
	}
}
