using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PandilleroController : MonoBehaviour
{
    Rigidbody rb;

    public Transform[] wayPoints;
    public Transform startPoint;

    public GameObject player;
    public GameObject[] enemyNumber;

    public int hitCounter;

    float timeMakeDamage;
    float timeBeDamaged;

    bool canReceiveDmg;
    bool canMakeDmg;

    private void Awake()
    {
        //todas las variables se resetean
        hitCounter = 0;
        timeBeDamaged= 0;
        timeMakeDamage= 0;
        canMakeDmg= false;
        canReceiveDmg= false;
    }


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
        timeBeDamaged += Time.deltaTime;
        timeMakeDamage += Time.deltaTime;
        if(timeMakeDamage >= 0.2f)
        {
            canReceiveDmg = true;
        }
        if (timeMakeDamage >= 0.2f)
        {
            canMakeDmg = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetTrigger("ToHit");
        }
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
        Debug.Log("on");
        rb.detectCollisions = true;

    }

    public void RigidBodyDisable()
    {
        Debug.Log("off");
        rb.detectCollisions = false;
    }

    public void CallForHelp()
    {
        for (int i = 0; i < enemyNumber.Length; i++)
        {
            Debug.Log("Ayuda");
            enemyNumber[i].GetComponent<Animator>().SetTrigger("ToFollow");
        }
    }

    public void Combo()
    {
        ComboAttack1();

        if (canMakeDmg == true)
        {
            ComboAttack2();

            if (canMakeDmg == true)
            {
                ComboAttack3();
            }
        }
    }

    public void ComboAttack1()
    {
        Debug.Log("attack1");
        DamagePlayer();
        canMakeDmg = false;
        timeMakeDamage = 0;
    }

    public void ComboAttack2()
    {
        Debug.Log("attack2");
        DamagePlayer();
        canMakeDmg = false;
        timeBeDamaged = 0;
    }

    public void ComboAttack3()
    {
        Debug.Log("final attack");
        DamagePlayer();
        canMakeDmg = false;
        timeMakeDamage = 0;
    }

    public void ChargedAttack()
    {
        if (canMakeDmg == true)
        {
            Debug.Log("ChargedAttack");
            DamagePlayer();
            canMakeDmg = false;
            timeMakeDamage = 0;
        }
    }

    public void DamagePlayer()
    {
        Debug.Log("daño al jugador");
        
       
    }

    public void ReceiveDamage()
    {
        if(canReceiveDmg == true)
        {
            hitCounter++;
            Debug.Log("daño recibido");
            canReceiveDmg= false;
            timeBeDamaged = 0;
        }          
    }
}
