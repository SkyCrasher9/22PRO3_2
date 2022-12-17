using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class IddleBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    public Transform basePoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        basePoint = animator.GetComponent<PandilleroController>().startPoint;

        //el agente va a la base
        agent.destination = basePoint.position;

        //activa las colisiones del rigidbody
        animator.GetComponent<PandilleroController>().RigidBodyEnable();

        if (agent.destination == basePoint.position )
        {
            agent.speed = 0f;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ejecuta el raycast
        RotatingRayCastDetect(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = 5f;
        //ejecuta el metodo del controlador
        animator.GetComponent<PandilleroController>().RigidBodyDisable();
    }

    public void RotatingRayCastDetect(Animator animator)
    {
        // declaro un quaternion cuyo valor varía con el tiempo
        Quaternion q = Quaternion.AngleAxis(100 * Time.time, Vector3.up);
        // declaro un vector3 cuya direccion varía 
        Vector3 d = animator.transform.forward * 20;

        RaycastHit hit;

        // raycast que gira 360 sin rango limitado
        if (Physics.Raycast(animator.transform.position, q * d, out hit, Mathf.Infinity))
        {
            //comprobacion de que golpea algo
            Debug.DrawRay(animator.transform.position, q * d, Color.blue);
            //si el rayo detecta a un jugador pasa al estado Patrol
            if (hit.collider.CompareTag("Player"))
            {
                animator.SetTrigger("ToPatrol");
            }
        }
        else
        {
            //no golpea nada el raycast
            Debug.DrawRay(animator.transform.position, q * d, Color.red);
        }
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
