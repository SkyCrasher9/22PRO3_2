using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thief_AIDirector : MonoBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static Thief_AIDirector Instance { get; private set; }
    public GameObject[] PatrolPoints;
    public GameObject[] Thief_Agents;
    public float Combatiendo2;
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
        PatrolPoints = GameObject.FindGameObjectsWithTag("Thief_Waypoints");
        Thief_Agents = GameObject.FindGameObjectsWithTag("Thief");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
