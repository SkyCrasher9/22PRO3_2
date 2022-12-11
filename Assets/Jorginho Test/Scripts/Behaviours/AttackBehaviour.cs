using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehaviour : StateMachineBehaviour
{
    public GameObject player;
    NavMeshAgent agent;

    int attackSelector;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PandilleroController>().player;
        agent = animator.GetComponent<NavMeshAgent>();

        attackSelector = Random.Range(1, 2);

        if(attackSelector == 1)
        {
            ComboAttack();
        }
        else if (attackSelector == 2) 
        {
            ChargedAttack();
        }
        agent.destination = player.transform.position;
    }

    public void ComboAttack()
    {
        Debug.Log("attack1");
        if (agent.remainingDistance < 1)
        {
            Debug.Log("attack2");
            if (agent.remainingDistance < 1)
            {
                Debug.Log("lastattack");
            }
        }
    }

    public void ChargedAttack() 
    {
        Debug.Log("ChargedAttack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance < 1)
        {
            attackSelector = Random.Range(1, 2);

            if (attackSelector == 1)
            {
                ComboAttack();
            }
            else if (attackSelector == 2)
            {
                ChargedAttack();
            }
        }
        else if (agent.remainingDistance <= 3)
        {
            animator.SetTrigger("ToFollow");
        }
        else if(agent.remainingDistance >= 10)
        {
            animator.SetTrigger("ToIddle");
        }
        else
        {
            animator.SetTrigger("ToPatrol");
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
