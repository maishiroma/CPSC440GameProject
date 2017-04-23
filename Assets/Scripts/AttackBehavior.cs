using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour {

    private float startAttackTime;
    private float HitTime;
    bool hit = false;
    private SplashDamageScript SplashDamage;
    private AlienNavMeshInterface _nav;
    private PlayerHealthScript playerHealth;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startAttackTime = Time.time;
        HitTime = Time.time + (stateInfo.length / 3f);
        SplashDamage = GameObject.Find("SplashDamage").GetComponent<SplashDamageScript>();
        _nav = animator.gameObject.GetComponent<AlienNavMeshInterface>();
        playerHealth = GameObject.Find("PlayerSpawnLocation").GetComponent<PlayerHealthScript>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    if(Time.time >= HitTime && !hit && (Vector3.Distance(animator.transform.position, _nav.target.transform.position) < _nav.attackDistance))
        {
            hit = true;
            SplashDamage.Hit(animator.gameObject);
            playerHealth.DealDamage(10);
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hit = false;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
