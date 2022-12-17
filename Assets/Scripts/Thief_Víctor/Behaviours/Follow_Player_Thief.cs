using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow_Player_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public Transform Player;
    public float Follow_PlayerWait;
    public static float speed = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        //Thief_Agent.speed = 0f;
        //GoToPlayer();
    }
   
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent.SetDestination(animator.GetBehaviour<Patrol_Thief>().PlayerObjetivo.gameObject.transform.position);
        if (!Thief_Agent.pathPending && Thief_Agent.remainingDistance <= 7.0f)
        {
            //FollowTime(animator);
            animator.SetTrigger("Attack");
           // Debug.Log("Atacando a Player");
           
        }
        
        if(!Thief_Agent.pathPending && Thief_Agent.remainingDistance < 2.5f)
        {  
            animator.SetTrigger("PlayerLost"); 
        }
        
    }
   /* public void FollowTime(Animator animator)
    {
        Follow_PlayerWait += Time.deltaTime;

        if (Follow_PlayerWait >= 2)
        {
            animator.SetTrigger("ThiefCombat"); 
            //TRIGGER QUE NO EXISTE
        }
    }*/
    


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
