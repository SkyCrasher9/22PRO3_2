using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelDetectBehaviour : StateMachineBehaviour
{
    public NavMeshAgent angel;


    public GameObject scan;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al iniciar el juego, pilla el componente nav mesh agent del agente.
        angel = animator.gameObject.GetComponent<NavMeshAgent>();

        //cuando detecta un objeto lo guarda en la variable detected del agente.
        scan = animator.gameObject.GetComponent<Angel>().detected;
        Debug.Log("Has Been deetected");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al estar escaneando, el agente se para en su sitio. 
        angel.SetDestination(animator.gameObject.transform.position);

        //El escaneo detecta el collider del objeto para identificar cual es.
        scan = animator.gameObject.GetComponent<Angel>().detected;
        //Dibujamos el color del rayo de un tono distinto para verificar que esta detectando lo que tiene delante
        Debug.DrawRay(angel.transform.position, angel.transform.TransformDirection(Vector3.forward) * 10, Color.green);

        //Cuando registra el Tag de la piedra, el rover con el nombre podra recogerla.
        if (scan.CompareTag("Player"))
        {
            //al pasar de search a scan, le dice a la transicion que no vuelva a search.
            animator.SetTrigger("IsFollowing");
        }
        //Si en caso contrario, no les interesa, pasaran de ello.
        else
        {
            animator.SetTrigger("GoPatrol");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel = null;
        scan = null;
    }
}
