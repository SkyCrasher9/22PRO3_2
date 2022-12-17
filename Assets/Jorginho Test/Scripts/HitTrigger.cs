using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public GameObject[] enemy;
    int random;

    private void Awake()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            HitEnemy();
        }
    }

    public void HitEnemy()
    {
        random= Random.Range(0, enemy.Length);
        enemy[random].GetComponent<Animator>().SetTrigger("ToHit");       
    }

}
