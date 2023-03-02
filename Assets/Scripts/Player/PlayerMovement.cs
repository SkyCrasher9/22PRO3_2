using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class PlayerMovement : MonoBehaviour
{
    public float velocitySpeed = 9;

    public float InputX;
    public float InputZ;

    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;

    public float Speed;
    public float allowPlayerRotation = 0.1f;
    
    public Camera cam;
    public CharacterController controller;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGroundeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Animaciones")]
    public Animator anim;

    /*
    public CharacterController controller;

    public float speed = 12f;
    public float Speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Turn")]
    //private float forwardAmount;
    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    
    //Borrar
    public float InputX; //Registro de acciones
    public float InputZ;

    Vector3 velocity;
    */



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGroundeedPlayer();
        movementPlayer();
        anim.SetFloat("Forward", Speed);

        //TurnRotation();

        
    }

    public void movementPlayer()
    {
        InputMagnitude();

        //Jump
        if (Input.GetButtonDown("Jump") && isGroundeed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        /*
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        var forward = cam.transform.forward; //La dirección "forward" será la de la cámara y no la del personaje.
        var right = cam.transform.right;


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGroundeed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        //Rotate2
        InputMagnitude();
        */
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;

        if (blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * velocitySpeed);
        }

        /*
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = cam.transform.forward; //La dirección "forward" será la de la cámara y no la del personaje.
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        desiredMoveDirection = forward * InputZ + right * InputX;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        controller.Move(desiredMoveDirection * Time.deltaTime * speed);
        */
    }

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        if (Speed > allowPlayerRotation)
        {
            //anim.SetFloat ("InputMagnitude", Speed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            //anim.SetFloat ("InputMagnitude", Speed, StopAnimTime, Time.deltaTime);
        }

        /*
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            PlayerMoveAndRotation(); //Rotamos al personaje
        }
        else if (Speed < allowPlayerRotation)
        {
            //No rotará el personaje
        }*/

    }

    
    public void IsGroundeedPlayer()
    {
        isGroundeed = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGroundeed && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
    



}
