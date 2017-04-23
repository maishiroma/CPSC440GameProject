using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {


    Material HealthBarMat;

	// Use this for initialization
	void Start ()
    {
        HealthBarMat = gameObject.GetComponent<MeshRenderer>().material;	
	}
	
    public void SetHealthBar(float health)
    {
        HealthBarMat.SetTextureOffset("_MainTex", new Vector2(0, (1- health)/2));
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
