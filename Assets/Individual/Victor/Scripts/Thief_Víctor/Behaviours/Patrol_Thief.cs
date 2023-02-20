using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;  
    public Transform[] PatrolPoints; //Array de los puntos de patrulla
    public GameObject PlayerObjetivo; //Variable para el jugador al que debe acercarse al detecarlo
    public Transform Target; //Indicará en el inspector cual es el objetivo del Thief
    private int NextPoint = 0;
    public static float speed;
    public float IdleReturn;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Thief_Agent.speed = 2f;
        PatrolPoints = animator.gameObject.GetComponent<Agent_Thief>().PatrolPoints;//Llama a la variable PatrolPoints en el Script Agent_Thief
        GotoNextPoint();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Thief_Agent.remainingDistance < 0.5f) //Cuando la distancia restante al punto de patrulla sea menor a 0.5 se dirigira al siguiente punto de patrulla
            GotoNextPoint();
        IdleTime2(animator); //Declaración del temporizador para volver al idle
        Ray ray = new Ray(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward);

        //El rayo emitido tomara la posición del agente y se creara delante de él
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.red);
        RaycastHit hit; //Declaración de la variable hit del Raycast

        if (Physics.Raycast(ray, out hit, 5f)) //Al acercarse lo suficiente al player pasará al estado Follow_Player
        {
            if (hit.collider.tag == "Player")
            { 
                PlayerObjetivo = hit.collider.gameObject;
                animator.SetTrigger("PlayerDetected");
            } 
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.red);
    }
#endif
    public void GotoNextPoint()
    {    //Manda al agante volver al inicio si no hay más puntos de ruta indicados                   
        if (PatrolPoints.Length == 0)// Si a llegado al último punto del array su detino será el primer punto del array
        {
            return;
        }
        NextPoint = (NextPoint + 1) % PatrolPoints.Length;
        Target = PatrolPoints[NextPoint]; //Con esto se podrá comprobar a que punto se dirige el Thief
        Thief_Agent.destination = PatrolPoints[NextPoint].gameObject.transform.position;
    }
    public void IdleTime2(Animator animator) //Temporizador que manda al Thief de vuelta al Idle
    {
        IdleReturn += Time.deltaTime;

        if (IdleReturn >= 30)
        {
            animator.SetTrigger("StopPatrol");
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        IdleReturn = 0f; //Cuando sale del metodo el valor del float  IdleReturn será 0
    }
    
    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
