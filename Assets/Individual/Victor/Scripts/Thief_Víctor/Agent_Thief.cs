using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Thief : MonoBehaviour
{
    public Transform Target; //El objetivo al que se dirigira el Thief

    public NavMeshAgent Thief_Agent; 

    public Transform[] PatrolPoints; //Puntos de patrulla del Thief
    
    public static float speed = 5f; //Velocidad inicial del Thief
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    
}
