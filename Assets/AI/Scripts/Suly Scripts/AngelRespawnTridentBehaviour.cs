using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelRespawnTridentBehaviour : StateMachineBehaviour
{
    public NavMeshAgent angel;
    public Transform player;

    public float counterExposed;
    public float counterReset;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel.speed = 0;
        counterExposed = 0;
        counterReset = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        counterExposed += Time.deltaTime;
        counterReset += Time.deltaTime;

        if (counterExposed >= 3)
        {
            counterReset = 0;
            Debug.Log("Estas Expuesto bro");

            
        }
        if (counterReset >= 5)
        {
           Debug.Log("Sacando un nuevo Tridente");
           animator.SetTrigger("TridentAtack");
        }



    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        angel.speed = 3.5f;
        counterExposed = 0f;
        counterReset = 0f;
    }
}
