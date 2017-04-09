using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour {

    public GameObject Alien;
    public GameObject EnemyIconMovement;
    public float maxDistFromPlayerIcon = .33f;
    public Texture deadTexture;
    public Color deadColor;
    public float lastAngleFromForward;

    private Transform PlayerLocation;
    private bool dead = false;

    // Use this for initialization
    void Start ()
    {
        EnemyIconMovement = transform.FindChild("EnemyIcon_MovementToPlayer").gameObject;
        PlayerLocation = GameObject.Find("PlayerSpawnLocation").transform;
    }
	
    public void UpdateAlienIconPosition()
    {
        if(Alien != null && PlayerLocation != null)
        {
            float t = Vector3.Distance(Alien.transform.position, PlayerLocation.position) / Alien.GetComponent<AlienNavMeshInterface>().startDistanceFromPlayer;
            float distance = Mathf.Lerp(0f, maxDistFromPlayerIcon, t);
            Debug.Log(distance);
            EnemyIconMovement.transform.localPosition = new Vector3(EnemyIconMovement.transform.localPosition.x, distance, EnemyIconMovement.transform.localPosition.z);
        }
       
    }

	// Update is called once per frame
	void Update ()
    {
        if(Alien != null)
        {
            if (Alien.GetComponent<SmallAlienHealth>().dead && !dead)
            {
                dead = true;
                EnemyIconMovement.GetComponent<MeshRenderer>().material.mainTexture = deadTexture;
                EnemyIconMovement.GetComponent<MeshRenderer>().material.color = deadColor;

            }
        }
        
	}
}
