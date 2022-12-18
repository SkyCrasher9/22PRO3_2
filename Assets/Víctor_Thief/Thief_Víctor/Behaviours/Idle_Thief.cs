using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static float speed = 5f;
    public float IdleWait; 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f; //La velocidad se reduce a 0 para que el Thief se detenga al entrar a este estado
    }
    public void IdleTime(Animator animator) //En el estado Idle se quedará esperando el tiempo indicado
    {
        IdleWait += Time.deltaTime;

        if (IdleWait >= 1) //Cuando termine el tiempo pasará a patrulla
        {
            animator.SetTrigger("StartPatrol");
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        IdleTime(animator);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent.speed = speed; //La velocidad será la normal al salir del estado
        IdleWait = 0; //El tiempo volverá a 0 al salir de este estado
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
