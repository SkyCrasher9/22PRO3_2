using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Enemy_Ivan : MonoBehaviour
{
    [Header("------------ EnemyType ------------")]
    [Header("0 = Normal")]
    [Header("1 = Parkour")]
    [Header("2 = Swiming")]
    public int tipoEnemy;

    [Header("Objetivos")]
    public GameObject Target;
    public List<GameObject> SpawnPoints;

    [Header("Hit-s")]
    public GameObject agentHitGameObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
