using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelFollowBehaviour : StateMachineBehaviour
{

    public NavMeshAgent angel;
    public Transform player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel = animator.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

       

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        angel.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if(distance < 15)
        {
            //animator.SetTrigger("TridentAtack");
        }
        else if(distance > 20)
        {
            animator.SetTrigger("GoPatrol");
        }

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel = null;
        player = null;
    }
}
