using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCardSpawner : MonoBehaviour {


    public GameObject TrapCard;
    public float TrapCardSpacing = 0.1f;
    public GameObject[] Traps;


	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < Traps.Length; i++)
        {
            float width = TrapCard.transform.lossyScale.x + TrapCardSpacing;
            SpawnTrapCard(transform.position + (Vector3.left * i * width), Traps[i].GetComponent<Trap>().icon);
        }
	}
	
	// Update is called once per frame
	void SpawnTrapCard (Vector3 pos, GameObject trap)
    {
        GameObject _trapCard = (GameObject)Instantiate(TrapCard, pos, transform.rotation, transform);
        _trapCard.GetComponent<TrapCard>().LoadTrapInSlot(trap);
	}
}
