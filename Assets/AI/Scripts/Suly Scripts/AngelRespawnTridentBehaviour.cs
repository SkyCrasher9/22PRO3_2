using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AngelRespawnTridentBehaviour : StateMachineBehaviour
{
    //La variable del Navegador del Angel.
    public Transform player;

    //La variable para guardar al jugador.
    public NavMeshAgent angel;

    //La variable para el tiempo que estara quieto despues del Ataque.
    public float counterExposed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al iniciar, como esta expuesto, se ralentiza
        angel.speed = 0;

        //Dejamos el contador a 0 antes de comenzar el cronometro.
        counterExposed = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //El cronometro como tal suma el tiempo hasta los 7 segundos
        counterExposed += Time.deltaTime;

        //Cuando el cronometro supere los 7 segundos, este pasara de vuelta al ataque.
        if (counterExposed >= 7)
        {
            Debug.Log("Estas Expuesto bro");
            animator.SetTrigger("TridentAtack");
        }

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Devolvemos los valores originales a su posicion normal.
        angel.speed = 3.5f;
        counterExposed = 0f;
    }
}
