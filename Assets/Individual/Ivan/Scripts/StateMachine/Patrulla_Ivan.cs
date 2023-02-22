using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrulla_Ivan : StateMachineBehaviour
{
    [Header("Targets")]
    public GameObject Target;
    public int numeroPoints;

    [Header("Agent")]
    public NavMeshAgent agent;

    [Header("Controladores de tiempos")]
    //Tomporizador.
    public float time;


    //Para mantener la división.
    private float lowSpeed;
    //Para conserver la velocidad base.
    private float baseSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        if (animator.gameObject.GetComponent<SC_Enemy_Ivan>().SpawnPoints.Count == 0)
        {
            IA_Manager.instance.randomSpawnpoints(animator.gameObject.GetComponent<SC_Enemy_Ivan>().SpawnPoints);

            Debug.Log("Se han asignado los puntos");
        }

        //Guardamos el parametro correstpondiente en la variable correspondiente.
        agent = animator.GetComponent<NavMeshAgent>();

        //El punto a seguir.
        Target = animator.gameObject.GetComponent<SC_Enemy_Ivan>().SpawnPoints[numeroPoints];
        animator.gameObject.GetComponent<SC_Enemy_Ivan>().Target = Target;

        //Para iniciar la ruta
        agent.destination = Target.transform.position;

        //Para que no genere bugs al cambiar de carga a normal estando en arena.
        baseSpeed = 3.5f;
        agent.speed = baseSpeed;
        lowSpeed = agent.speed / 2;

        Debug.Log("Se ejecuta el Enter");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        var distancia = Vector3.Distance(Target.transform.position, animator.transform.position);

        //Lo que dispara si esta en el radio a llegar.
        if (distancia < 1)
        {
            siguientePoint();
            //Esta nueva linea meteremos el siguiente objetivo.
            Target = animator.gameObject.GetComponent<SC_Enemy_Ivan>().SpawnPoints[numeroPoints];
            agent.destination = Target.transform.position;
        }
        //Tipo de terreno que transita.
        tipoTerreno();
        //Este es el detector de elementos.
        buscar(animator);
    }


    public void siguientePoint()
    {
        if (agent.GetComponent<SC_Enemy_Ivan>().SpawnPoints.Count == 0)
        {
            return;
        }
        numeroPoints = (numeroPoints + 1) % agent.GetComponent<SC_Enemy_Ivan>().SpawnPoints.Count;
    }
    public void buscar(Animator animator)
    {
        Ray ray = new Ray(animator.gameObject.transform.position + new Vector3(0, 0.3f, 0), animator.transform.forward * 5);

        RaycastHit hit;

        //BORRAR
        Debug.DrawRay(animator.gameObject.transform.position + new Vector3(0, 0.3f, 0), animator.transform.forward * 5, Color.red);

        if (Physics.Raycast(ray, out hit, 5.0f) && hit.collider.gameObject /*!= animator.gameObject.GetComponent<SC_Enemy_Ivan>().agentHitGameObject.gameObject*/)
        {
            //Debug.Log("¡Eh mira, un objeto diferente de basico!");
            Debug.DrawRay(animator.gameObject.transform.position + new Vector3(0, 0.3f, 0), animator.transform.forward * 5, Color.red);

            string nombreObjeto;
            nombreObjeto = hit.collider.tag;

            switch (nombreObjeto)
            {
                case "Player":
                    //Debug.Log("Es hierba");
                    animator.gameObject.GetComponent<SC_Enemy_Ivan>().Target = hit.collider.gameObject;

                    animator.SetBool("follow", true);

                    //Guarda lo ultimo hitteado
                    animator.gameObject.GetComponent<SC_Enemy_Ivan>().agentHitGameObject = hit.collider.gameObject;

                    break;
                default:
                    //Debug.Log("Jaja no, que ha pasado");

                    break;
            }
        }
    }
    public void tipoTerreno()
    {

        int sandMask = 1 << NavMesh.GetAreaFromName("Water");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agent.transform.position, out hit, 1f, sandMask))
        {
            Debug.Log("Se ejecuta al cambio de velocidad");
            agent.speed = lowSpeed;
        }
        else
        {
            agent.speed = baseSpeed;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
