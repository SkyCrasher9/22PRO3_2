using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
   
    public Transform[] PatrolPoints;

    public GameObject PlayerObjetivo;

    public Transform Target;

    private int NextPoint = 0;

    public static float speed = 5f;

    public float IdleReturn;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();

        PatrolPoints = animator.gameObject.GetComponent<Agent_Thief>().PatrolPoints;
       
        GotoNextPoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Thief_Agent.destination = PatrolPoints[NextPoint].gameObject.transform.position;
        if (Thief_Agent.remainingDistance < 0.5f)
            GotoNextPoint();
        IdleTime2(animator);

        Ray ray = new Ray(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward);

        //El rayo emitido tomara la posición del agente y se creara delante de él
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.red);

        RaycastHit hit; //Declaración de la variable hit del Raycast

        if (Physics.Raycast(ray, out hit, 5.0f))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log("PLAYER DETECTADO");
                PlayerObjetivo = hit.collider.gameObject;
                animator.SetTrigger("PlayerDetected");

                Debug.Log("Ir al jugador");
            }
            
           
        }
    }
    public void GotoNextPoint()
    {    //Manda al agante volver al inicio si no hay más puntos de ruta indicados                   
        if (PatrolPoints.Length == 0)
        {
            return;
        }
        NextPoint = (NextPoint + 1) % PatrolPoints.Length;
        Target = PatrolPoints[NextPoint];
        Thief_Agent.destination = PatrolPoints[NextPoint].gameObject.transform.position;

    }
    public void IdleTime2(Animator animator)
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
        IdleReturn = 0f;
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
