using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelDetectBehaviour : StateMachineBehaviour
{
    //La variable del Navegador del Angel.
    public NavMeshAgent angel;

    //La variable del Navegador del Angel.
    public GameObject detect;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al iniciar el juego, pilla el componente nav mesh agent del Angel.
        angel = animator.gameObject.GetComponent<NavMeshAgent>();

        //cuando detecta un objeto lo guarda en la variable detected del Angel.
        detect = animator.gameObject.GetComponent<Angel>().detected;
        Debug.Log("Has Been deetected");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al estar escaneando, el agente se para en su sitio. 
        //angel.SetDestination(animator.gameObject.transform.position);

        //El escaneo detecta el collider del objeto para identificar cual es.
        detect = animator.gameObject.GetComponent<Angel>().detected;
        //Dibujamos el color del rayo de un tono distinto para verificar que esta detectando lo que tiene delante
        Debug.DrawRay(angel.transform.position, angel.transform.TransformDirection(Vector3.forward) * 10, Color.green);

        //Cuando registra el Tag del Jugador, el Angel evaluara que hacer, si seguirle o continuar con la patrulla.
        if (detect.CompareTag("Player"))
        {
            //Al detectar al jugador inicia el comportamiento de Follow.
            animator.SetTrigger("IsFollowing");
        }
        //Si en caso contrario, no le interesa, pasaran de ello y seguira Patrullando.
        else
        {
            animator.SetTrigger("GoPatrol");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        angel = null;
        detect = null;
    }
}
