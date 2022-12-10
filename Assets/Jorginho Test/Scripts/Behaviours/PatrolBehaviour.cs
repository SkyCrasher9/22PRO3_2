using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;

    public Transform[] wayPoints;
    public Transform target;
    public int destPoint = 0;

    public float time;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        wayPoints = animator.GetComponent<VariableContainer>().wayPoints;

        PatrolPointMovement();

        target = wayPoints[destPoint];

        agent.destination = target.transform.position;

        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PatrolPointMovement();
            agent.destination = target.transform.position;
        }

        time += Time.deltaTime;

        if (time > 15)
        {
            time = 0;
            animator.SetTrigger("ToIddle");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    public void PatrolPointMovement()
    {
        // Si no hay ningún punto se hace un return
        if (wayPoints.Length == 0)
            return;

        // El transform de target pasa a ser el valor de dentro del array de patrolPoints y el numero dentro de este array es el valor de destPoint
        target = wayPoints[destPoint];

        agent.destination = target.transform.position;

        destPoint = (destPoint + 1) % wayPoints.Length;
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
