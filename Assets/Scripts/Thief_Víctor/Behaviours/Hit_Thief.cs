using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hit_Thief : StateMachineBehaviour
{
    
    public NavMeshAgent Thief_Agent;
    public NavMeshAgent PlayerTest;

    public static float speed;
    public float NowStunned;
    public int PuntosdeDaño;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f;
        Debug.Log("Ladrón Aturdido");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StunnedTime(animator);
        {
            if (PuntosdeDaño >= 3)
            {
                Debug.Log("Ladrón Muerto");
                animator.SetTrigger("IsDead");

            }
            else if (PuntosdeDaño < 3)
            {
                Debug.Log("Ladrón No Muerto");
                animator.SetTrigger("ReturnCombat");
            }
        }
    }
    public void StunnedTime(Animator animator)
    {
        NowStunned += Time.deltaTime;
        if (NowStunned >= 10)
        {
            animator.SetTrigger("SeguirPatrulla");
        }
    }
    public void RecibirDaño()
    {
        PuntosdeDaño++;
        Debug.Log("Se suma un golpe");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NowStunned = 10f;
        Thief_Agent.speed = speed;
        Debug.Log("Vuelve a buscar");
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
