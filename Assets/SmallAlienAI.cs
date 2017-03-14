using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAlienAI : MonoBehaviour {

    public enum States
    {
        Navigating, Hit, InAir, Defensive, Attacking
    }

    public States currentState;
    private AlienNavMeshInterface _navMeshInterface;
    private SmallAlienHealth _health;



	// Use this for initialization
	void Start ()
    {
		currentState = States.Navigating;
        _navMeshInterface = gameObject.GetComponent<AlienNavMeshInterface>();
        _health = gameObject.GetComponent<SmallAlienHealth>();
	}
	
    public void SetState(States state)
    {
        if(state == States.Navigating)
        {
            if(_navMeshInterface.usingNavmeshAgent == false)
            {
                currentState = States.Navigating;
                _navMeshInterface.ToggleNavmeshAgent(true);
            }
        }
        else if(state == States.Hit)
        {
            currentState = States.Hit;
            _navMeshInterface.ToggleNavmeshAgent(false);

        }
        else if(state == States.InAir)
        {

        }
    }


	// Update is called once per frame
	void Update ()
    {
		
	}
}
