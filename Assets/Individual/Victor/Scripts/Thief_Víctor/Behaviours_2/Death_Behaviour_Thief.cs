using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Death_Behaviour_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
   
    public static float speed;
   
    public float DisapearTime;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f;
        DisapearTime = 0f;
        
    }
    public void DisapearTimer (Animator animator)
    {
        DisapearTime += Time.deltaTime; 
        if(DisapearTime >= 1.5f)
        {
            Thief_Agent.gameObject.SetActive(false);   
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DisapearTimer(animator);
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
