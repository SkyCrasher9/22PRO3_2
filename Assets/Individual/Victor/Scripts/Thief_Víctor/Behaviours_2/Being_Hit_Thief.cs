using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Being_Hit_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static float speed;
    public float NowStunned; //Float utilizado en el temporizador
    public int PuntosdeDaño; //Int utilziado para los golpes recibidos

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 0f; //La velocidad del Thief se reduce a 0 al entar a este estado
        NowStunned = 0f; //El temporizador empezará siendo 0
        Debug.Log("Ladrón Aturdido");
        RecibirDaño(); //Decalaración del método RecibirDaño
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PuntosdeDaño >= 3) //Si el Thief recibe 3 o más puntos de daño el GameObject se desactiva
        {
            Debug.Log("Ladrón Muerto");
            animator.SetTrigger("IsReallyDead"); //Trigger qyue conduce al exit en la máquina de estados
            
        }
        else if (PuntosdeDaño < 3) //Si el Thief tiene menos de 3 golpes acumulados aún se mantiene en la escena
        {                          //Se quedará aturdido el tiempo indicado en el temporizador
            Debug.Log("Ladrón No Muerto");
            StunnedTime(animator);
        }
    }
    public void StunnedTime(Animator animator)
    {
        NowStunned += Time.deltaTime;
        if (NowStunned >= 1.5f)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void RecibirDaño() //Para mostrar como el Thief sufre daño he utilizado un contador que se activa cada vez que entra a este estado
    {
        PuntosdeDaño++;
        Debug.Log("Se suma un golpe");
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // NowStunned = 10f;
       // Debug.Log("Vuelve a buscar");
    }

}
