using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Combat_Thief : StateMachineBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static float speed = 5f;
    Player_de_prueba Player;
    public float Combatiendo;
    //public float Combatiendo2;
    //public int Attack
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Player = animator.gameObject.GetComponent<Player_de_prueba>();

        Thief_Agent.speed = 0f;
        
        Atacar(animator);
    }
    public void Atacar (Animator animator)
    {
        Ray ray = new Ray(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward);
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3.0f))
        {
            Debug.Log("Ataque 1");
            //hit.collider.GetComponent<Player_de_prueba>().ChangeCubeColor();

            if(Thief_Agent.remainingDistance <= 3.0f) 
            {
                if (Combatiendo >= 2)
                {
                    Debug.Log("Ataque 2");
                    //hit.collider.GetComponent<Player_de_prueba>().ChangeCubeColor();
                    Combatiendo = 0f;
                }
                        
                if (Thief_Agent.remainingDistance <= 3.0f)
                {
                    if (Combatiendo >= 2)
                    {
                        Debug.Log("Ataque 3");
                        //hit.collider.GetComponent<Player_de_prueba>().ChangeCubeColor();
                    }
                        
                }
                else if (Thief_Agent.remainingDistance > 2.0f)
                {
                    animator.SetTrigger("PerseguirPlayer");
                }
                
            }
            else if(Thief_Agent.remainingDistance > 2.0f)
            {
                animator.SetTrigger("PerseguirPlayer");
            }
            //CombatTimer(animator);
            //PONER UN TIEMPO 2 SEGS entre cada golpe
        }
        else if (Thief_Agent.remainingDistance > 2.0f)
        {
            animator.SetTrigger("PerseguirPlayer");
        }
        else if (Thief_Agent.remainingDistance > 4.0f)
        {
            animator.SetTrigger("StartIdle");
        }
    }
    public void AtaqueCombo()
    {

    }
    public void CombatTimer(Animator animator)
    {
        Combatiendo += Time.deltaTime;
       
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CombatTimer(animator);
        Atacar(animator);
       // Combatiendo2 += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent.speed = speed;
        Combatiendo = 5f;
        
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
