using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Anim_Ladrón : MonoBehaviour
{
    public float speed = 1f;

    public new Transform camera;
    public CharacterController player;
    public float gravity = -9.8f;
    private Animator animator;
    private bool canMove = true;
    private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;
        float movementSpeed = 0;

        if (hor != 0 || ver != 0 && canMove == true)
        {
            Vector3 forward = camera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = forward * ver + right * hor;
            movementSpeed = Mathf.Clamp01(direction.magnitude);
            direction.Normalize();

            movement = direction * speed * movementSpeed * Time.deltaTime;
            if (canRotate == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.2f);
            }
        }
        if (canMove == true)
        {
            movement.y += gravity * Time.deltaTime;

            player.Move(movement);
            animator.SetFloat("Speed", movementSpeed);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Idle");
            canMove = true;
            canRotate = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("IdleAlt");

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Correr");

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Amenazar");

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Combatir");

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Alerta");

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("RecibirGolpe");

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Muerte");
            canMove = false;
            canRotate = false;
        }
    }
}
