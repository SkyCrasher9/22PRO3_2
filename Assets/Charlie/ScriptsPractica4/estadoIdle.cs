using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class estadoIdle : StateMachineBehaviour
{
    //variable flotante para hacer de cronometro
    public float contadorDeTiempo;
    //
    NavMeshAgent sectario;
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
        sectario= animator.GetComponent<NavMeshAgent>();

        //Baja la velocidad y lo para.
        sectario.speed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //valor que nos va a indicar y hace referencia para que lo golpeo
        RaycastHit golpeo;
        //Declaramos el rayo. Este tiene que tener una posición de Inicion, que va a ser la del rover. Y una dirección en la que se va a mover.
        Ray rayo = new Ray(animator.transform.position, animator.transform.forward);

        //Comprobación visual
        Debug.DrawRay(animator.transform.position, animator.transform.forward * 5, Color.green);

        Cronometro();
        if (contadorDeTiempo >= 5.0f)
        {
            //Transicion para pasar de animacón a Patrulla
            animator.SetTrigger("Esperando");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Volvera Inicial
        sectario.speed = 10.0f;
        //Resetee a 0
        contadorDeTiempo = 0.0f;
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
        contadorDeTiempo += Time.deltaTime;
    }


}
