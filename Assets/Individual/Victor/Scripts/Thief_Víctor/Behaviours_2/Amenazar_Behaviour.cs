using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Amenazar_Behaviour : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static float speed;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f; //La velocidad se reduce a 0 para que el Thief se detenga al entrar a este estado
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Attack");
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent.speed = speed; //La velocidad será la normal al salir del estado
    }
}
