using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelAttackTrindetBehaviour : StateMachineBehaviour
{
    public Transform player;
    public NavMeshAgent angel;

    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel = animator.gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }



    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < 5)
        {
            animator.GetComponent<Angel>().Attack();
            animator.SetTrigger("TrindentRespawn");
 
        }
        else
        {
            animator.SetTrigger("IsFollowing");
        }


    }
}
