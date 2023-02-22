using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Manager : MonoBehaviour
{

    [Header("SpawnPoints")]
    public List<GameObject> SpawnPoints = new List<GameObject>();
    [Header("Rovers")]
    public GameObject[] Enemy;

    public static IA_Manager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("wayPoint"));

        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void randomSpawnpoints(List<GameObject> puntos)
    {
        //List<GameObject> SpawnPoints_temporal_002 = SpawnPoints;
        List<GameObject> spawnPoints_temporal = new List<GameObject>();
        int numero;

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            numero = Random.Range(0, SpawnPoints.Count);
            //spawnPoints_temporal.Add(SpawnPoints[numero]);
            if (SpawnPoints[numero] != spawnPoints_temporal.Contains(SpawnPoints[numero]))
            {
                spawnPoints_temporal.Add(SpawnPoints[numero]);
            }
            else
            {
                i--;
            }
        }
        puntos.AddRange(spawnPoints_temporal);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
