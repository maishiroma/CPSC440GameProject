using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextualScreenPage : MonoBehaviour {

    public string Name;
    public Color InvisibleColor;
    public MeshRenderer[] ObjectsToFade;
    public Text[] TextToFade;
    private Color[] ObjectStartColors;
    private Color[] TextStartColors;


    public bool active;
    public bool visible;
    ContextualScreenManager ScreenManager;
    private float startFadeTime;
    private float fadeTime;

	// Use this for initialization
	void Start ()
    {
        ScreenManager = transform.parent.GetComponent<ContextualScreenManager>();
        fadeTime = ScreenManager.pageFadeTime;
        ObjectStartColors = new Color[ObjectsToFade.Length];

        if (ObjectsToFade.Length > 0)
        {
            for (int i = 0; i < ObjectsToFade.Length; i++)
            {
                ObjectStartColors[i] = ObjectsToFade[i].material.color;
                ObjectsToFade[i].enabled = false;
            }
        }
        if(TextToFade.Length > 0)
        {
            TextStartColors = new Color[TextToFade.Length];
            for (int i = 0; i < TextToFade.Length; i++)
            {
                TextStartColors[i] = TextToFade[i].color;
                TextToFade[i].enabled = false;
            }
        }

        active = false;
        visible = false;
        
	}
	

    IEnumerator FadingIn()
    {
        while (true)
        {
            if (Time.time <= startFadeTime + fadeTime)
            {
                if (ObjectsToFade.Length > 0)
                {
                    for (int i = 0; i < ObjectsToFade.Length; i++)
                    {
                        ObjectsToFade[i].material.SetColor("_Color", Color.Lerp(InvisibleColor, ObjectStartColors[i], (Time.time - startFadeTime) / fadeTime));
                    }
                }
                if (TextToFade.Length > 0)
                {
                    for (int i = 0; i < TextToFade.Length; i++)
                    {
                        TextToFade[i].color = Color.Lerp(new Color(TextStartColors[i].r, TextStartColors[i].g, TextStartColors[i].b, 0), TextStartColors[i], (Time.time - startFadeTime) / fadeTime);
                    }
                }
            }
            else
            {
                yield break;
            }
            yield return null;
        }
        
    }

    IEnumerator FadingOut()
    {
        while (true)
        {
            if (Time.time <= startFadeTime + fadeTime)
            {
                if (ObjectsToFade.Length > 0)
                {
                    for (int i = 0; i < ObjectsToFade.Length; i++)
                    {
                        ObjectsToFade[i].material.SetColor("_Color", Color.Lerp(ObjectStartColors[i], InvisibleColor, (Time.time - startFadeTime) / ScreenManager.pageFadeTime));
                    }
                }

                if (TextToFade.Length > 0)
                {
                    for (int i = 0; i < TextToFade.Length; i++)
                    {
                        TextToFade[i].color = Color.Lerp(TextStartColors[i], new Color(TextStartColors[i].r, TextStartColors[i].g, TextStartColors[i].b, 0), (Time.time - startFadeTime) / fadeTime);
                    }
                }

            }
            else
            {
                if (ObjectsToFade.Length > 0)
                {
                    for (int i = 0; i < ObjectsToFade.Length; i++)
                    {
                        ObjectsToFade[i].enabled = false;
                    }
                }
                if (TextToFade.Length > 0)
                {
                    for (int i = 0; i < TextToFade.Length; i++)
                    {
                        TextToFade[i].enabled= false;
                    }
                }

                visible = false;
                yield break;
            }

            yield return null;
        }
    }


    public void FadeIn()
    {
        startFadeTime = Time.time;
        active = true;
        visible = true;

        if (ObjectsToFade.Length > 0)
        {
            for (int i = 0; i < ObjectsToFade.Length; i++)
            {
                ObjectsToFade[i].enabled = true;
            }
        }
        if (TextToFade.Length > 0)
        {
            for (int i = 0; i < TextToFade.Length; i++)
            {
                TextToFade[i].enabled = true;
            }
        }

        StartCoroutine(FadingIn());
    }

    public void FadeOut()
    {
        startFadeTime = Time.time;
        active = false;
        StartCoroutine(FadingOut());
    }




	// Update is called once per frame
	void Update ()
    {
		
	}
}
