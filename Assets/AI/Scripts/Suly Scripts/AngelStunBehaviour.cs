using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelStunBehaviour : StateMachineBehaviour
{
    //La variable del Navegador del Angel.
    public Transform player;

    //La variable para guardar al jugador.
    public NavMeshAgent angel;

    //La variable para el tiempo que estara quieto durante el Stun.
    public float counterStun;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al iniciar el comportamiento, ya que tiene que estar quieto, le bajamos la velocidad de movimiento a 0.
        angel.speed = 0;

        //Dejamos el contador a 0 antes de comenzar el cronometro.
        counterStun = 0;

        //Al ser atacado le resta un golpe de vida.
        animator.GetComponent<Angel>().lifeHits--;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //El cronometro como tal suma el tiempo hasta los 2 segundos
        counterStun += Time.deltaTime;
        //angel.speed = 0;
        //Cuando el cronometro supere los 2 segundos, este pasara de vuelta a su patrulla como tal
        if (counterStun >= 3)
        {
            animator.SetTrigger("GoPatrol");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Devolvemos los valores originales a su posicion normal.
        angel.speed = 3.5f;
        counterStun = 0f;
    }
}
