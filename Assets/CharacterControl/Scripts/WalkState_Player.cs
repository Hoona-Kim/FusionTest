using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState_Player : StateMachineBehaviour
{
    private Transform enemyLocation;
    private NavMeshAgent navAgent;
    private float attackRange = 2.7f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyLocation = GameObject.FindGameObjectWithTag("Enemy").transform;
        navAgent = animator.GetComponent<NavMeshAgent>();
        navAgent.SetDestination(enemyLocation.position);

        //change movement speed
        navAgent.speed = 1.8f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Look at the player direction
        animator.transform.LookAt(enemyLocation);

        // Calculate the distance. If the distance is smaller than attackRange, change the state to "AttackReady"
        float distance = Vector3.Distance(enemyLocation.position, animator.transform.position);
        if (distance < attackRange)
        {
            animator.SetBool("isReady", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When exiting the state, stop the character.
        navAgent.SetDestination(navAgent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
