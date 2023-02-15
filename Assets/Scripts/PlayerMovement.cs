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
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    [Header("Settings")]
    [SerializeField] private bool applyMotion;

    [Header("Turn")]
    /*
    // Turn
    private float turnAmount;                   // Curren turn amount
    private float turnSpeed;                    // Current turn speed
    [SerializeField] private bool canTurn;

    [SerializeField] private bool cameraTurn;
    [SerializeField] private float cameraTurnSpeed;
    [SerializeField] private float staticTurnSpeed;
    [SerializeField] private float dynamicTurnSpeed;
    */
    //private float forwardAmount;
    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed = 0.1f;
    //public float allowPlayerRotation = 0.1f;
    public Camera cam;

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
    private void TurnRotation()
    {
        /*
        // Calculate turn speed based on direction
        turnSpeed = Mathf.Lerp(staticTurnSpeed, dynamicTurnSpeed, Mathf.Abs(speed));

        if (cameraTurn)
        {
            // Apply rotation to transform based on current camera forward direction
            if (applyMotion && canTurn) this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(new Vector3(0f, Camera.main.transform.rotation.eulerAngles.y, 0f)), Time.deltaTime * cameraTurnSpeed);
        }
        else
        {
            // Apply rotation to transform
            if (applyMotion && canTurn) this.transform.Rotate(0f, turnAmount * turnSpeed * Time.deltaTime, 0f);
        }*/
    }
    public void movementPlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
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
        this.transform.Rotate(Vector3.up * x);

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
