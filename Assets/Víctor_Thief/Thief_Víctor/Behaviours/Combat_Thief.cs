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
    public int AttackCounter = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent = animator.gameObject.GetComponent<NavMeshAgent>();
        Player = animator.gameObject.GetComponent<Player_de_prueba>();
        Thief_Agent.speed = 0f; //Se frena al Thief para que no se deplace durante su ataque
        Atacar(animator);
    }
    public void Atacar (Animator animator) //Metodo en el que se ejecuta el ataque
    {   //El Raycast detecta si el Player esta en la distancia indicada y lo golpea
        Ray ray = new Ray(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward);
        Debug.DrawRay(Thief_Agent.gameObject.transform.position, Thief_Agent.gameObject.transform.forward, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            Debug.Log("Ataque 1"); //Para comprobar que se ha dado el ataque 
        }
        else if (Thief_Agent.remainingDistance > 2.5f) //A esta distancia vuelve al estado Follow_Player_Thief
        {
            animator.SetTrigger("PerseguirPlayer");
        }
        else if (Thief_Agent.remainingDistance > 3.5f) //A esta distancia vuelve al estado Idle
        {
            animator.SetTrigger("StartIdle");
        }
    } 
    public void CombatTimer(Animator animator) //Para evitar que se quede demasiado en el estado de combatir le he incluido este temporizador
    {
        Combatiendo += Time.deltaTime;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   //Declaracion de los métodos en el Update para que se ejecuten en cualquier momento mientras esta en este estado
        CombatTimer(animator);
        Atacar(animator);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Thief_Agent.speed = speed; //La vaelocidad del Thief vuelve a ser la normal
        Combatiendo = 8f; //Cuando el temporizador llegue a 8 segundos saldrá del estado combatir y voverá al idle
        animator.SetTrigger("StartIdle");
        
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
