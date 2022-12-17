using JesusMayCry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEnemigos : MonoBehaviour
{
    private MovementInput movementInput;

    public LayerMask layerMask;

    [SerializeField] Vector3 inputDirection;
    //[SerializeField] private EnemyScript currentTarget;

    public GameObject cam;
    private void Start()
    {
                movementInput = GetComponentInParent<MovementInput>();
    }

    private void Update()
    {
        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        inputDirection = forward * movementInput.desiredMoveDirection.y + right * movementInput.desiredMoveDirection.x;
        inputDirection = inputDirection.normalized;

        RaycastHit info;

        if (Physics.SphereCast(transform.position, 3f, inputDirection, out info, 20, layerMask))
        {
            Debug.Log("Hola Enemigo");
            /*if (info.collider.transform.GetComponent<EnemyScript>().IsAttackable())
                currentTarget = info.collider.transform.GetComponent<EnemyScript>();*/
        }
    }
}
