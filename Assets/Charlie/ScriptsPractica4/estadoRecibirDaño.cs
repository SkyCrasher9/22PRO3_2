using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class estadoRecibirDaño : StateMachineBehaviour
{
    NavMeshAgent sectario;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
        animator.GetComponent<SectarioController>().RecibirDaño();
        //
        sectario = animator.GetComponent<NavMeshAgent>();
        //
        sectario.speed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

        if (animator.GetComponent<SectarioController>().vidaActualEnemigo <= 0)
        {
           
            animator.SetTrigger("aMorir");
        }
        else
        {
           
            animator.SetTrigger("aPatrulla");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sectario.speed = 10;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
   
}
