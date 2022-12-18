using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelAttackTrindetBehaviour : StateMachineBehaviour
{
    //La variable del Navegador del Angel.
    public Transform player;

    //La variable para guardar al jugador.
    public NavMeshAgent angel;

    

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

        //Cuando la distancia del Angel sea menor a 5m, accede a la programacion del Angel como tal y accedera al componente de Ataque. Pasando despues al comportamiento de 
        if (distance < 5)
        {
            animator.GetComponent<Angel>().Attack();
            animator.SetTrigger("TrindentRespawn");
        }
        //En caso contrario el Angel pasara a seguir al jugador.
        else
        {
            animator.SetTrigger("IsFollowing");
        }
    }
}
