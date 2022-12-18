using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelStunBehaviour : StateMachineBehaviour
{
    public NavMeshAgent angel;
    public Transform player;

    public float counterStun;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel.speed = 0;
        counterStun = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        counterStun += Time.deltaTime;
        if (counterStun >= 2)
        {
            animator.SetTrigger("GoPatrol");
        }



    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        angel.speed = 3.5f;
        counterStun = 0f;

    }
}
