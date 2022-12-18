using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AngelPatrolBehaviour : StateMachineBehaviour
{
    //La variable que dice en que punto de se encuetra.
    public int destPoint = 0;

    //La variable del Navegador del Angel.
    public NavMeshAgent angel;

    //La variable para guardar al jugador.
    public Transform target;
    
    //Variable para la velocidad del Angel.
    public float currentSpeed = 5f;

    //El Raycast para detectar las cosas durante su patrulla.
    public RaycastHit raycastHit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Al iniciar el juego, pilla el componente nav mesh agent del Angel.
        angel = animator.GetComponent<NavMeshAgent>();

        // Deshabilita el auto-braking que permite continuar el movimiento entre los puntos
        angel.autoBraking = false;

        
        if(target != null)
            angel.destination = target.position;
    }

    void GotoNextPoint(Animator animator)
    {
        // Vuelve al punto inicial si no hay mas puntos
        if (animator.gameObject.GetComponent<Angel>().PatrolPoints.Length == 0)
            return;

        // Le dice al Angel a que Waypoint se tiene que dirigir
        target = animator.gameObject.GetComponent<Angel>().PatrolPoints[destPoint];
        angel.destination = target.position;

        // Escoge el siguiente punto dentro del array como destino, ciclandolo al principio cuando se acaba la lista
        destPoint = (destPoint + 1) % animator.gameObject.GetComponent<Angel>().PatrolPoints.Length;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        //Creamos el rayo con el que navegamos por el mapa.
        Ray ray = new Ray(angel.transform.position + new Vector3(0f, 0.2f, 0f), angel.transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(angel.transform.position + new Vector3(0f, 0.2f, 0f), angel.transform.TransformDirection(Vector3.forward) * 10, Color.magenta);

        //Llamamos al rayo
        if (Physics.Raycast(ray, out raycastHit, 10.0f))
        {
            //Cuando se cruza con una roca guarda la variable en el Angel.
            if (raycastHit.transform.CompareTag("Player") && raycastHit.transform.gameObject != animator.gameObject.GetComponent<Angel>().detected)
            {
                //Cuando choca con el collider lo detecta.
                animator.gameObject.GetComponent<Angel>().detected = raycastHit.collider.gameObject;

                //Pasa al siguiente estado, que es el de deteccion
                animator.SetTrigger("IsDetecting");
                Debug.DrawRay(angel.transform.position, angel.transform.TransformDirection(Vector3.forward) * 10, Color.green);
            }
        }


        // Elige el siguiente Waypoint cuando ya ha llegado al previo 
        if (!angel.pathPending && angel.remainingDistance < 0.5f)
        {
            GotoNextPoint(animator);
        }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
