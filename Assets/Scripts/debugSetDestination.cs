using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class debugSetDestination : MonoBehaviour
{

    public GameObject target;
    private Vector3 targetpos;
    public NavMeshAgent agent;
    private Animator anims;
    private float lastTurnAmt;
	// Use this for initialization
	void Start ()
    {
        targetpos = Vector3.zero;
        anims = gameObject.GetComponent<Animator>();
        agent.updateRotation = true;

    }

    private void OnAnimatorMove()
    {
        agent.speed = (anims.deltaPosition / Time.deltaTime).magnitude;
        Vector3 agentDir;
        //Vector3 agentDir = agent.velocity - transform.position;
        //agentDir.Normalize();
        //agentDir = (agentDir + transform.forward).normalized;
        float angle = Vector3.Angle(transform.forward, agent.velocity);
        Vector3 cross = Vector3.Cross(transform.forward, agent.velocity);
        if (cross.y < 0) angle = -angle;
        /*
        if (-0.5f < angle && angle < 0.5f)
        {
            float turnAmt = 0;
            turnAmt = Mathf.Lerp(lastTurnAmt, turnAmt, Time.deltaTime * 5);
            anims.SetFloat("Turning", 0);
            lastTurnAmt = turnAmt;
        }
        else if (angle < -0.35f)
        {
            float turnAmt = Mathf.Lerp(-0.25f, -0.5f, (angle) / (-30));
            //turnAmt = Mathf.Lerp(lastTurnAmt, turnAmt, Time.deltaTime * 2);
            anims.SetFloat("Turning", turnAmt);
            lastTurnAmt = turnAmt;
        }
        else if(angle > 0.35f)
        {
            float turnAmt = Mathf.Lerp(0.25f, 0.5f, (angle) / (30));
            //turnAmt = Mathf.Lerp(lastTurnAmt, turnAmt, Time.deltaTime * 2);
            anims.SetFloat("Turning", turnAmt);
            lastTurnAmt = turnAmt;
        }
        */
    }

    // Update is called once per frame
    void Update ()
    {
        transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(agent.transform.forward), Time.deltaTime * 20f);
        //agent.transform.rotation *= anims.deltaRotation ;
        transform.position = agent.transform.position;

		if(targetpos == Vector3.zero)
        {
            targetpos = target.transform.position;
            agent.SetDestination(targetpos);
        }
        else if(Vector3.Distance(targetpos, target.transform.position) >= .5f)
        {
            targetpos = target.transform.position;
            agent.SetDestination(targetpos);
        }
	}
}
