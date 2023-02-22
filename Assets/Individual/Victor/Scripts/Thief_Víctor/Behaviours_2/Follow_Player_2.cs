using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Follow_Player_2 : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public Transform Player;
    public static float speed;
    public GameObject PlayerObjetivo; //Variable para el jugador al que debe acercarse al detecarlo
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 10f;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   // Una vez encuantra al player lo guardará en la variable PlayerObjetivo
       
        //Thief_Agent.SetDestination(animator.GetBehaviour<Patrol_Thief>().PlayerObjetivo.gameObject.transform.position);
        Ray ray = new Ray(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward);

        //El rayo emitido tomara la posición del agente y se creara delante de él
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.red);
        RaycastHit hit; //Declaración de la variable hit del Raycast
        if (Physics.Raycast(ray, out hit, 3f)) //Al acercarse lo suficiente al player pasará al estado Follow_Player
        {
            if (hit.collider.tag == "Player")
            {
                PlayerObjetivo = hit.collider.gameObject;
                animator.SetTrigger("Amenaza");
            }
        }
       /* if (!Thief_Agent.pathPending && Thief_Agent.remainingDistance < 2.5f)// A esta distancia pasará al estado Combat_Thief
        {
            animator.SetTrigger("Amenaza");
        }*/
        else if (!Thief_Agent.pathPending && Thief_Agent.remainingDistance >= 2.5f) //A esta distancia volverá al estado Patrol
        {
            animator.SetTrigger("PlayerLost");
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Amenaza");
        }
    }
    
}
