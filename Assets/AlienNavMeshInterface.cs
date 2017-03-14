using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienNavMeshInterface : MonoBehaviour {

    public GameObject SmallAlienNavmeshAgentPrefab;
    public NavMeshAgent _SmallAlienAgent;
    private Animator anims;
    public bool usingNavmeshAgent;
    private bool trackingTarget = false;
    private GameObject trackedTarget = null;
    private Vector3 lastTargetPos;


    public void ToggleNavmeshAgent (bool useNavMesh)
    {
        if (useNavMesh)
        {
            usingNavmeshAgent = true;
            _SmallAlienAgent.gameObject.SetActive(true);
            _SmallAlienAgent.transform.position = transform.position;
            _SmallAlienAgent.transform.rotation = transform.rotation;
        }
        else if (!useNavMesh)
        {
            usingNavmeshAgent = false;
            _SmallAlienAgent.gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start ()
    {
        GameObject SmallAlienNavmeshAgent = (GameObject)Instantiate(SmallAlienNavmeshAgentPrefab, transform.position, transform.rotation);
        _SmallAlienAgent = SmallAlienNavmeshAgent.GetComponent<NavMeshAgent>();
        anims = gameObject.GetComponent<Animator>();
        usingNavmeshAgent = true;

        //debug target
        GameObject target = GameObject.Find("debugNavmeshtarget");
        SetNavMeshAgentDestination(target.transform.position, true, target);

	}
	
    public void SetNavMeshAgentDestination(Vector3 target, bool track = false, GameObject targetGO = null)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, 2.0f, NavMesh.AllAreas))
        {
            _SmallAlienAgent.SetDestination(hit.position);
            lastTargetPos = hit.position;
        }

        if (track && targetGO != null)
        {
            trackingTarget = true;
            trackedTarget = targetGO;
        }
        else
        {
            StopTrackingTarget();   
        }
    }

    void StopTrackingTarget()
    {
        trackingTarget = false;
        trackedTarget = null;
    }

    void FollowAgent()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_SmallAlienAgent.transform.forward), Time.deltaTime * 20f);
        //agent.transform.rotation *= anims.deltaRotation ;
        transform.position = _SmallAlienAgent.transform.position;
    }

    private void OnAnimatorMove()
    {
        if (usingNavmeshAgent)
        {
            _SmallAlienAgent.speed = (anims.deltaPosition / Time.deltaTime).magnitude;
        }
    }
    
    private void TrackTarget()
    {
        if(Vector3.Distance(trackedTarget.transform.position, lastTargetPos) > 0.25f)
        {
            SetNavMeshAgentDestination(trackedTarget.transform.position, true, trackedTarget);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (usingNavmeshAgent)
        {
            FollowAgent();
        }
        if (trackingTarget && usingNavmeshAgent)
        {
            TrackTarget();
        }
	}
}
