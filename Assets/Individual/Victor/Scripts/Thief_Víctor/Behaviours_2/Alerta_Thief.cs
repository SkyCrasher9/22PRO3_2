using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alerta_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public Transform Player;
    public static float speed;
    public float AlertaWait;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f;
       
    }
    public void AlertTimer(Animator animator)
    {
        AlertaWait += Time.deltaTime;

        if (AlertaWait >= 1)
        {
            animator.SetTrigger("PerseguirPlayer"); 
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AlertTimer(animator);
    }
    
   
}
