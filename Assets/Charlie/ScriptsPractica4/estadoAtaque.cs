using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class estadoAtaque : StateMachineBehaviour
{
    //
    NavMeshAgent sectario;

    public float distanciaDeParado;
    //
    public GameObject flechaDeSectario;
    //
    public float tiempo;
    //El Game Object al que va a teren por objetivo.
    public GameObject objetivoJugador;
    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
        objetivoJugador=animator.GetComponent<SectarioController>().jugadorObjetivo;
        //
       sectario = animator.GetComponent<NavMeshAgent>();
        //
        sectario.speed = 0;
        //
        tiempo = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
        sectario.destination = objetivoJugador.transform.position;
        Cronometro();

        //Mirar al jugador Objetivo
        sectario.transform.LookAt(animator.GetComponent<SectarioController>().jugadorObjetivo.transform.position);
       
        if (sectario.remainingDistance < 10)
        {
            if (tiempo >= 5.0f)
            {
                //
                animator.GetComponent<SectarioController>().AlAtaquer();
                tiempo = 0;
            }
        }
        else if(sectario.remainingDistance>=10&&sectario.remainingDistance<20)
        {
            animator.SetTrigger("aFollow");
        }
        else
        {
            animator.SetTrigger("aPatrulla");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
        animator.ResetTrigger("aAtacar");
        //
        sectario.speed = 10.0f;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }

    public void Cronometro()
    {
        tiempo += Time.deltaTime;
    }
}
