using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class PlayerMovement : MonoBehaviour
{
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
    bool isGroundeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        IsGroundeedPlayer();
        movementPlayer();

        //TurnRotation();

        
    }

    public void movementPlayer()
    {
        
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

    }

    void PlayerMoveAndRotation()
    {
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

    }

    void InputMagnitude()
    {
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
        }
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
