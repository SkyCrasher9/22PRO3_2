using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandilleroController : MonoBehaviour
{
    public GameObject player;

    Rigidbody rb;

    public Transform[] wayPoints;
    public Transform startPoint;

    public GameObject[] enemyNumber;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        enemyNumber = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Alarm");
            for (int i = 0; i < enemyNumber.Length; i++)
            {
                enemyNumber[i].GetComponent<Animator>().SetTrigger("ToPatrol");
            }           
        }
    }

    public void RigidBodyEnable()
    {
        rb.detectCollisions = true;
        Debug.Log("on");
    }

    public void RigidBodyDinable()
    {
        rb.detectCollisions = false;
        Debug.Log("off");
    }

    public void ComboAttack1()
    {
        Debug.Log("attack1");
    }

    public void ComboAttack2()
    {
        Debug.Log("attack2");
    }

    public void ComboAttack3()
    {
        Debug.Log("lastattack");
    }

    public void ChargedAttack()
    {
        Debug.Log("ChargedAttack");
    }

}
