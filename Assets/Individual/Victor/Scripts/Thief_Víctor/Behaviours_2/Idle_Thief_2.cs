using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle_Thief_2 : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static float speed = 2f;
    public float IdleWait;
    public GameObject PlayerObjetivo; //Variable para el jugador al que debe acercarse al detecarlo

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f; //La velocidad se reduce a 0 para que el Thief se detenga al entrar a este estado
    }
    public void IdleTime(Animator animator) //En el estado Idle se quedará esperando el tiempo indicado
    {
        IdleWait += Time.deltaTime;

        if (IdleWait >= 5) //Cuando termine el tiempo pasará a patrulla
        {
            animator.SetTrigger("StartPatrol");
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        IdleTime(animator);

        Ray ray = new Ray(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward);

        //El rayo emitido tomara la posición del agente y se creara delante de él
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.red);
        RaycastHit hit; //Declaración de la variable hit del Raycast

        if (Physics.Raycast(ray, out hit, 5f)) 
        {
            if (hit.collider.tag == "Player")
            {
                PlayerObjetivo = hit.collider.gameObject;
                animator.SetTrigger("PlayerDetected");
            }

        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent.speed = speed; //La velocidad será la normal al salir del estado
        IdleWait = 0; //El tiempo volverá a 0 al salir de este estado
    }

}
