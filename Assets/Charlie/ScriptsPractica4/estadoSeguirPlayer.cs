using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class estadoSeguirPlayer : StateMachineBehaviour
{
    //El Game Object al que va a teren por objetivo.
    public GameObject objetivoJugador;
    //Variable tipo NavMeshAgent
    NavMeshAgent sectario;

    public int numeroVidasSectario; 
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //LLamamos al Script Agente Rover desde aquí para que sepa tanto uno como otro a quien ha de seguir
        objetivoJugador = animator.gameObject.GetComponent<SectarioController>().jugadorObjetivo;
        //LLamamos al animator para que sepa que esto es de la Maya de Navegación.
        sectario = animator.GetComponent<NavMeshAgent>();
        //
        numeroVidasSectario= animator.GetComponent<SectarioController>().vidaActualEnemigo;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SeguirPlayer();
        //
        if (Vector3.Distance(sectario.transform.position, objetivoJugador.transform.position) <= 15.0f)
        {
            //
            animator.SetTrigger("aAtacar");
        }

        if (numeroVidasSectario<=2)
        {
            animator.SetTrigger("aPatrulla");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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
    public void SeguirPlayer()
    {
        //Esto hace que siga al objetivo que  haya marcado.
        sectario.destination = objetivoJugador.transform.position;
    }
}
