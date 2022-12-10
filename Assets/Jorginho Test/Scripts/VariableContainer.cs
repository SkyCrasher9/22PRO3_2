using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableContainer : MonoBehaviour
{
    public CapsuleCollider colliderSp;

    public Transform[] wayPoints;
    public Transform startPoint;

    public GameObject[] enemyNumber;

    // Start is called before the first frame update
    void Start()
    {
        enemyNumber = GameObject.FindGameObjectsWithTag("Enemy");

        colliderSp = gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Alarm");
            for (int i = 0; i < enemyNumber.Length; i++)
            {
                enemyNumber[i].GetComponent<Animator>().SetTrigger("ToPatrol");
            }
            
        }
    }
}
