using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandilleroController : MonoBehaviour
{
    Rigidbody rb;

    public Transform[] wayPoints;
    public Transform startPoint;

    public GameObject player;
    public GameObject[] enemyNumber;

    public Color color;
    public Renderer playerRenderer;
    public Renderer enemyRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        enemyNumber = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
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
        DamagePlayer();
    }

    public void ComboAttack2()
    {
        Debug.Log("attack2");
        DamagePlayer();
    }

    public void ComboAttack3()
    {
        Debug.Log("lastattack");
        DamagePlayer();
    }

    public void ChargedAttack()
    {
        Debug.Log("ChargedAttack");
        DamagePlayer();
    }

    public void DamagePlayer()
    {
        color = Color.red;

        playerRenderer.material.SetColor("Rojo", color);
        
        PlayerHitJump();
    }

    public void ReceiveDamage()
    {
        color = Color.red;

        enemyRenderer.material.SetColor("Rojo", color);

        HitJump();
    }


    private void PlayerHitJump()
    {
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * 1f);
    }

    private void HitJump()
    {
        player.GetComponent<Rigidbody>().AddForce(Vector3.up * 1f);
    }
}
