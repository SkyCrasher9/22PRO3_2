using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class estadoPatrullaPlayer : StateMachineBehaviour
{
    //Aqui declaramos un array para tener esta referencia y poder cogerl aen el star y que a su vez los coja de el script del rover
    public Transform[] puntosdeRuta;
    //esto se declara aquí para tener constancia del punto al que le toca ir al agente.
    private int puntoDestino = 0;
    //Declaramos el componenete NaMeshAgent como agentes. 
    private NavMeshAgent sectario;
    //
    public float tiempoPatrulla;
    //valor que nos va a indicar y hace referencia para que lo golpeo
    RaycastHit golpeo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Gracias a esto podemos coger desde el Inicio del juego el componente de NavMeshAgent y le decimos que agentes es un navmesh que va a ser llamado
        //desde el animator y a su vez desde el inspector
        sectario = animator.GetComponent<NavMeshAgent>();
        //Declaramos los puntos de ruta y que a su vez los coja del script Mono de Rover
        //puntosdeRuta = animator.gameObject.GetComponent<AgenteRover>().puntosdeRuta;
        //
        puntosdeRuta = animator.GetComponent<SectarioController>().puntosRuta;
        //
        tiempoPatrulla = 0;
        //
        VeAlSiguientePunto();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //valor que nos va a indicar y hace referencia para que lo golpeo
        RaycastHit golpeo;

        //Declaramos el rayo. Este tiene que tener una posición de Inicion, que va a ser la del rover. Y una dirección en la que se va a mover.
        Ray rayo = new Ray(animator.transform.position, animator.transform.forward);

        //Comprobación visual
        Debug.DrawRay(animator.transform.position, animator.transform.forward * 5, Color.yellow);

        //clase tipo estático para activar las físicas del Raycast y que se pueda golpear y detectar la roca
        //Out Hit parametro para quenos de lo que le golpee desde fuera
        if (Physics.Raycast(animator.transform.position, animator.transform.TransformDirection(Vector3.forward), out golpeo, 10.0f))
        {
            if (golpeo.collider.CompareTag("Player"))
            {
                //Comprobacion visual
                Debug.DrawRay(animator.transform.position + new Vector3(0, 0.3f, 0), animator.transform.forward * 10, Color.blue);
                //Esto es una referencia y un valor que es igual a lo que golpea  para que lo detecte
                //LLama dentro del animator al agenteRover, el script y le dice que al tocar sea igual a golpeo.
                animator.gameObject.GetComponent<SectarioController>().tocarJugador = golpeo;
                //
                animator.SetTrigger("aFollow");

                Debug.Log("He visto al Player");
            }
        }

            //Cronometro
            Cronometro();
        //Esto evita que se quede entre medias de dos puntos y pueda ir puno por punto.
        if (!sectario.pathPending && sectario.remainingDistance < 0.5f)
        {
            VeAlSiguientePunto();
        }

        //O pasa tiempo o corre todos los puntos de la ruta
        if (tiempoPatrulla >= 20.0f || puntoDestino == puntosdeRuta.Length)
        {

            //Transicion para pasar de animación a Patrulla
            animator.SetTrigger("VueltaAlIdle");

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //
        tiempoPatrulla = 0;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Implement code that sets up animation IK (inverse kinematics)
    }

    //Metodo y encargado d definir los puntos de patrulla, su cálculo y movimiento del agente
    void VeAlSiguientePunto()
    {


        //Esto hace que vuelva si no hay ningun punto seteado
        //Además este Length sirve para no tener que poner a mano el punto exacto de Waypoint
        //Si la longitud del array es igual a 0 vuelve punto original.
        if (puntosdeRuta.Length == 0)
            return;

        //Esta linea hace que el agente vaya y toque al destino que nosotros le digamos
        sectario.destination = puntosdeRuta[puntoDestino].transform.position;

        //Esto elige el siguiente punto de ruta dentro del array gracias a una suma a "Punto de destino"
        puntoDestino = (puntoDestino + 1) % puntosdeRuta.Length;
    }

    public void Cronometro()
    {
        tiempoPatrulla += Time.deltaTime;
    }
}
