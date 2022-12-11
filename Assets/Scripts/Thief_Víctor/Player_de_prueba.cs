using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_de_prueba : MonoBehaviour
{
    public NavMeshAgent PlayerTest;
    public GameObject cube;
    public Renderer cubeRenderer;
    private Color newCubeColor;

    private Rigidbody rb;

    private float randomChannelOne, randomChannelTwo, randomChannelThree;

    public Vector3 desiredMoveDirection;
    
    public float speed;
    public float movementX;
    public float movementY;

    private CharacterController controller;
    
    public new Transform camera;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print("Golpe RECIBIDO");
        ChangeCubeColor();
    }
    public void ChangeCubeColor()
    {
        randomChannelOne = Random.Range(0f, 1f);
        randomChannelTwo = Random.Range(0f, 1f);
        randomChannelThree = Random.Range(0f, 1f);

        newCubeColor = new Color(randomChannelOne, randomChannelTwo, randomChannelThree, 1f);

        cubeRenderer.material.SetColor("_BaseColor", newCubeColor);
        print("Golpe RECIBIDO");
    }
    public void LadronGolpeado(Animator animator)
    {
        animator.SetTrigger("BeigHit");
    }
    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(PlayerTest.gameObject.transform.position, PlayerTest.gameObject.transform.forward);
        Debug.DrawRay(PlayerTest.gameObject.transform.position, PlayerTest.gameObject.transform.forward, Color.red);
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.collider.tag == "Thief")
            {
                //animator.SetTrigger("PlayerDetected");
                //PlayerObjetivo = hit.collider.gameObject;
                Debug.Log("RAYCAST JUGADOR");
               // LadronGolpeado(animator);
                //Animator.SetTrigger("BeingHit");
            }
        }

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.zero;
        float movementSpeed;

        if (hor != 0 || ver != 0)
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

        }
        controller.Move(movement);

    }

}
