using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thief_AIDirector : MonoBehaviour
{
    public NavMeshAgent Thief_Agent;
    public static Thief_AIDirector Instance { get; private set; } //Instancia del director de IA
    public GameObject[] PatrolPoints; //Array para buscar los puntos de patrulla en la escena
    public GameObject[] Thief_Agents; //Array para buscar a los Thief
                                      //Solo hay un Thief en la escena, esta declaración esta pensada para cuando se implementen más Thief en un futuro
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
        PatrolPoints = GameObject.FindGameObjectsWithTag("Thief_Waypoints");// Busca GameObjects con el Tag indicado
        Thief_Agents = GameObject.FindGameObjectsWithTag("Thief");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
