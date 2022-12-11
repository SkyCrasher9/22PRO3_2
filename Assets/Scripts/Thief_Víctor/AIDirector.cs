using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIDirector : MonoBehaviour
{
    public NavMeshAgent Thief_Agent;

    
    public static AIDirector Instance { get; private set; }
    //public GameObject[] PointsOfInterest;
    public GameObject[] PatrolPoints;
    public void Awake()// Declaración del Director de IA en la escena
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PatrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        //PointsOfInterest = GameObject.FindGameObjectsWithTag("PatrolPoint");

        //PatrolPoints = PointsOfInterest;
        //DarPatrulla();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*public GameObject[] DarPatrulla()
    {
        
        for (int i = 0; i < 6; i++)
        {
            int a = UnityEngine.Random.Range(1, PointsOfInterest.Length);
            PatrolPoints[i] = PointsOfInterest[a];
        }
        return PatrolPoints;
        
    }*/
    
}
