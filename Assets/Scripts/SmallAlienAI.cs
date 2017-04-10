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
    private Animator anims;

    public float minRestTime = 3f;
    public float maxRestTime = 5f;
    public float minAttackTime = 1.5f;
    public float maxAttackTime = 3f;

    private bool inAttackPosition = false;
    private int attackNumber;
    private float currentRestLength;
    private float startRestTime;
    private bool JumpedForward = false;
    private Vector3 roughAttackPosition;
    private bool attacking;
    public float timeBetweenAttacks = 1f;
    private float AttackTime;
    private float startAttackTime;
    private bool resting;
    private bool canAttack;
    private bool inJumpPos;
    public LayerMask JumpBackMask;

    int RandomAttackNumber()
    {
        attackNumber = Random.Range(0, 2);
        return attackNumber;
    }




    // Use this for initialization
    void Start ()
    {
		currentState = States.Navigating;
        _navMeshInterface = gameObject.GetComponent<AlienNavMeshInterface>();
        _health = gameObject.GetComponent<SmallAlienHealth>();
        anims = gameObject.GetComponent<Animator>();
	}
	
    public void SetState(States state)
    {
        if (state == States.Hit)
        {
            currentState = States.Hit;
        }
        else if(state == States.Navigating)
        {
            _navMeshInterface.attacking = false;
            anims.SetBool("Attacking", false);

            if (_navMeshInterface.usingNavmeshAgent == false)
            {
                currentState = States.Navigating;
                _navMeshInterface.ToggleNavmeshAgent(true);
            }
        }
        else if(state == States.InAir)
        {
            currentState = States.InAir;
            _navMeshInterface.ToggleNavmeshAgent(false);
        }
        else if(state == States.Attacking)
        {
            if(_navMeshInterface.usingNavmeshAgent == false)
            {
                _navMeshInterface.ToggleNavmeshAgent(true);
            }
            anims.applyRootMotion = true;
            currentState = States.Attacking;
            anims.SetBool("Attacking", true);
        }

        if(state != States.Attacking)
        {
            _navMeshInterface.attacking = false;
            anims.SetTrigger("StopAttackCycle");
            anims.ResetTrigger("Hit");
            anims.SetBool("Attacking", false);
            anims.SetBool("Resting", false);
            anims.ResetTrigger("JumpForward");
            anims.ResetTrigger("JumpBack");
            anims.ResetTrigger("Attack01");
            anims.ResetTrigger("Attack02");
            anims.ResetTrigger("Attack03");
            anims.ResetTrigger("StopAttackCycle");



        }



    }


    void TryJumpBack()
    {
        Vector3 jumpPosition = transform.position + (transform.forward * 5);
        Collider[] hitColliders = Physics.OverlapSphere(jumpPosition, 3, JumpBackMask);

        if(hitColliders.Length > 0)
        {
            inJumpPos = false;
        }
        else
        {
            inJumpPos = true;
            anims.SetTrigger("JumpBack");
        }
    }
   
    void TryAttackingAgain()
    {
        if (attacking)
        {
            StartCoroutine(AttackLoop());
        }
    }

    IEnumerator AttackLoop()
    {
        int attackNumber = RandomAttackNumber();

        if(attackNumber == 0)
        {
            anims.SetTrigger("Attack01");
        }
        else if(attackNumber == 1)
        {
            anims.SetTrigger("Attack02");
        }
        else if (attackNumber == 2)
        {
            anims.SetTrigger("Attack03");
        }



        yield return new WaitForSeconds(timeBetweenAttacks);
        TryAttackingAgain();
    }


    void Attack()
    {


        if (resting)
        {
            if (Time.time > (startRestTime + currentRestLength))
            {
                Debug.Log("EndRest");
                anims.SetBool("Resting", false);
                resting = false;
                if (inJumpPos)
                {
                    anims.SetTrigger("JumpForward");
                }

            }
        }

        if (_navMeshInterface.isInAttackPosition)
        {
            if (!attacking && !resting)
            {
                AttackTime = Random.Range(minAttackTime, maxAttackTime);
                attacking = true;
                startAttackTime = Time.time;
                StartCoroutine(AttackLoop());
            }

            if (attacking)
            {
                if (Time.time > startAttackTime + AttackTime)
                {
                    attacking = false;
                    resting = true;
                    anims.SetBool("Resting", true);
                    currentRestLength = Random.Range(minRestTime, maxRestTime);
                    startRestTime = Time.time;
                    TryJumpBack();
                }
            }

        }

    }

	// Update is called once per frame
	void Update ()
    {
		if(currentState == States.Attacking)
        {
            Attack();
        }
        else
        {
            inJumpPos = false;
            resting = false;
            attacking = false;
        }
	}
}
