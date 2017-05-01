using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCard : MonoBehaviour {

    public Color InvisibleColor;
    public bool visible;
    public bool equipped;				// Is this trap card equipped?
    public Transform trapIconPos;
    public float fadeTime = 0.1f;
    private List<MeshRenderer> ObjectsToFade = new List<MeshRenderer>();
    private Color StartColor;
    public bool StartVisible;
	public static EquipTrapRadial[] trapRadials;	// Refrence to radial buttons. Used to interact with equipTrapRadial
	public GameObject associatedTrap;	// What Trap prefab is associated to this spot?

	// Use this for initialization
	void Start ()
    {
        ObjectsToFade.Add(gameObject.GetComponent<MeshRenderer>());
        StartColor = ObjectsToFade[0].material.color;
	}

    public void FadeOut()
    {
        if (visible)
        {
            StopAllCoroutines();
            StartCoroutine(FadingOut());
        }
        
    }



    IEnumerator FadingOut()
    {
        float startTime = Time.time;
        while (true)
        {
            if (Time.time <= startTime + fadeTime)
            {
                for (int i = 0; i < ObjectsToFade.Count; i++)
                {
                    ObjectsToFade[i].material.color = Color.Lerp(StartColor, InvisibleColor, (Time.time - startTime) / fadeTime);
                }
            }
            else
            {
                visible = false;
                for (int i = 0; i < ObjectsToFade.Count; i++)
                {
                    ObjectsToFade[i].enabled = false;
                }
                yield break;
            }
            yield return null;
        }
    }

    public void FadeIn()
    {
        if (!visible)
        {
            for(int i = 0; i < ObjectsToFade.Count; i++)
            {
                ObjectsToFade[i].enabled = true;
            }
            StopAllCoroutines();
            StartCoroutine(FadingIn());
        }
    }

    IEnumerator FadingIn()
    {
        visible = true;
        float startTime = Time.time;
        while (true)
        {
            if(Time.time <= startTime + fadeTime)
            {
                for(int i = 0; i < ObjectsToFade.Count; i++)
                {
                    ObjectsToFade[i].material.color = Color.Lerp(InvisibleColor, StartColor, (Time.time - startTime) / fadeTime);
                }
            }
            else
            {
                yield break;
            }
            yield return null;
        }
    }


	// This is only needed to be done once
	void Awake()
	{
		if(trapRadials == null)
			trapRadials = GameObject.FindObjectsOfType<EquipTrapRadial>();
	}
	
    public void LoadTrapInSlot(GameObject trap)
    {
        GameObject _trap = (GameObject)Instantiate(trap, trapIconPos.position, Quaternion.identity, trapIconPos);
        ObjectsToFade.Add(_trap.gameObject.GetComponentInChildren<MeshRenderer>());
        associatedTrap = trap;
    }


		
	// Update is called once per frame
	void Update () {

	}

	// Returns true if this trap name matches one of the names given here.
	bool CheckIfTrapIsThrowable(string name)
	{
		switch(name)
		{
			case "Grenade":
				return true;
		}
		return false;
	}

	// Checks if any of the trapRadials are selected. If so, puts this trap onto there, and sets this trap as equipped.
	public void EquipTrap()
	{
		if(equipped == false)
		{
			EquipTrapRadial[] trapRadials = GameObject.FindObjectsOfType<EquipTrapRadial>();
			for(int i = 0; i < trapRadials.Length; i++)
			{
				if(trapRadials[i].isSelected == true)
				{
					// If there's already a trap in this spot, this current one get's "deequipped"
					if(trapRadials[i].associatedTrapCard != null)
					{
						trapRadials[i].RemoveTrap();
					}
					trapRadials[i].SetTrap(this);
					equipped = true;
					break;
				}
			}
		}
	}
}
