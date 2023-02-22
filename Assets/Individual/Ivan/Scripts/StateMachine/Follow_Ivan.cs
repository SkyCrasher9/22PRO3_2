using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow_Ivan : StateMachineBehaviour
{
    [Header("Agent")]
    public NavMeshAgent agent;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.destination = animator.gameObject.GetComponent<SC_Enemy_Ivan>().Target.transform.position;

        var distancia = Vector3.Distance(animator.gameObject.GetComponent<SC_Enemy_Ivan>().Target.transform.position, animator.transform.position);

        Debug.DrawRay(animator.gameObject.transform.position + new Vector3(0, 0.3f, 0), animator.transform.forward * distancia, Color.yellow);

        if (distancia >= 15)
        {
            animator.SetTrigger("Unfollow");

            Debug.Log("Est� muy lejos");

            agent.destination = agent.gameObject.GetComponent<SC_Enemy_Ivan>().Target.transform.position;
        }
        
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
