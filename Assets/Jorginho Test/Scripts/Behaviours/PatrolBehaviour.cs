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
        wayPoints = animator.GetComponent<PandilleroController>().wayPoints;

        PatrolControler();

        target = wayPoints[destPoint];

        agent.destination = target.transform.position;

        time = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            PatrolControler();
            agent.destination = target.transform.position;
        }

        time += Time.deltaTime;

        if (time > 45)
        {
            time = 0;
            animator.SetTrigger("ToIddle");
        }

        RayCastDetect(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    public void PatrolControler()
    {
        // Si no hay ningún punto se hace un return
        if (wayPoints.Length == 0)
            return;

        // El transform de target pasa a ser el valor de dentro del array de patrolPoints y el numero dentro de este array es el valor de destPoint
        target = wayPoints[destPoint];

        destPoint = (destPoint + 1) % wayPoints.Length;
    }

    public void RayCastDetect(Animator animator)
    {
        RaycastHit hit;

        if(Physics.Raycast(animator.transform.position, animator.transform.TransformDirection(Vector3.forward), out hit, 5.0f))
        {
            Debug.DrawRay(this.agent.transform.position + new Vector3(0, 0.3f, 0), this.agent.transform.TransformDirection(Vector3.forward) * 5.0f, Color.green);

            if(hit.collider.CompareTag("Player"))
            {
                animator.SetTrigger("ToFollow");
            }
            else if(hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Friend");
            }
        }
        else
        {
            Debug.DrawRay(this.agent.transform.position + new Vector3(0, 0.3f, 0), this.agent.transform.TransformDirection(Vector3.forward) * 5.0f, Color.red);
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
