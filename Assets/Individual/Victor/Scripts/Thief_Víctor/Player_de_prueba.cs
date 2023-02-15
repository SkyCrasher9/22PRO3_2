using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_de_prueba : MonoBehaviour
{
    private Rigidbody rb;//Declaración del Rigidbody del GameObject
    public float speed;
    private CharacterController controller;   //Referncia al CharacterController del jugador 
    public new Transform camera;  //Referencia al transform de la cámara para que el jugador se mueva en la zona mostrada por la cámara
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Llamada al componente Rigidbody
        controller = GetComponent<CharacterController>(); //Llamada al componente CharacterController
    }
    // Update is called once per frame
    void Update()
    {
        
        float hor = Input.GetAxis("Horizontal"); // Input para que se pueda mover al player en horizontal
        float ver = Input.GetAxis("Vertical"); // Input para que se pueda mover al player en vertical
        Vector3 movement = Vector3.zero; // La variable movement iniciara con valor cero antes de pulsar una tecla
        float movementSpeed; //Velocidad de movimiento
        if (hor != 0 || ver != 0) //Con esto el movimiento solo ocurrira cuando se pulsen las teclas
        {        
            Vector3 forward = camera.forward; //Toma el vector forward de la cámra como referencia para su movimiento

            //Para que el jugador pueda moverse por el escenario y no se mueva unicamente hacia la dirección exacta de la cámara se pone ese valor igual a 0
            forward.y = 0;

            forward.Normalize(); //Para evitar que la cámara pueda dar problemas

            Vector3 right = camera.right; //Toma el vector right de la cámra como referencia para su movimiento

            right.y = 0;//Para que el jugador pueda moverse por el escenario y no se mueva unicamente hacia la dirección exacta de la cámara se pone ese valor igual a 0
            right.Normalize();//Para evitar que la cámara pueda dar problemas

            Vector3 direction = forward * ver + right * hor;

            movementSpeed = Mathf.Clamp01(direction.magnitude);

            direction.Normalize(); //Normaliza la dirección para que se pueda mover pulsando teclas de movimeinto en horizaontal y en vertical al mismo tiempo

            movement = direction * speed * movementSpeed * Time.deltaTime; //El movimiento es igual a la dirección multiplicada por la velocidad
                                                                           //multiplicada por la velocidad de movimiento y multiplicada por el tiempo
                                                                           //Para que el movimiento sea por segundos y se pueda manejar
        }
        controller.Move(movement); //Se llama a la funcion de movimeinto y se le pasa la variable de movimiento

    }

}
