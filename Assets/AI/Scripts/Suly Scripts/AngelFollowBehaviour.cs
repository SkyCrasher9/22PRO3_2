using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelFollowBehaviour : StateMachineBehaviour
{
    //La variable del Navegador del Angel.
    public NavMeshAgent angel;

    //La variable para guardar al jugador.
    public Transform player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al iniciar el juego, pilla el componente nav mesh agent del Angel.
        angel = animator.gameObject.GetComponent<NavMeshAgent>();

        //Al iniciar, busca al objeto con el Tag de jugador, con el cual es el que se juega.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al detectar al juagdor, este se dirige a la posicion en la que esta el jugador.
        angel.SetDestination(player.position);

        //Calculando la distancia que tiene uno con el otro.
        float distance = Vector3.Distance(animator.transform.position, player.position);

        //Cuando la distancia del Angel sea menor a 5m, este pasara al comportamiento de ataque.
        if(distance < 5)
        {
            animator.SetTrigger("TridentAtack");
        }
        //En caso de que la distancia con el jugador sea mayor de 20m, el Angel dejara de seguir al jugador y volvera a su comportamiento de Patrulla.
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
