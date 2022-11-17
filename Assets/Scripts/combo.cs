using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combo : MonoBehaviour
{
    Animator animator; 

    public static int cantidad_click; 
    bool puedo_dar_click;
    //public List<Slash> slashes;
    private bool attacking;

    public Slash slash1;
    public Slash slash2;
    public Slash slash3;

    BoxCollider colliderWeapon;
    public GameObject objWeapon;
    

    void Start()
    {
       colliderWeapon = objWeapon.GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        cantidad_click = 0;
        puedo_dar_click = true;
        DisableSlash1();
        DisableSlash2();
        DisableSlash3();


        colliderWeapon.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { Iniciar_combo();}

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("AAAAH!");
        }
    }
    void Iniciar_combo()
    {
        if (puedo_dar_click)
        {
            cantidad_click++;
        }

        if (cantidad_click == 1)
        {
            animator.SetInteger("attack", 1);
            attacking = true;
            //StartCoroutine(SlashAttack1());
        }
    }

    IEnumerator SlashAttack1()
    {
        yield return new WaitForSeconds(slash1.delay);
        slash1.slashObj.SetActive(true);
        yield return new WaitForSeconds(1);
        DisableSlash1();
        attacking = false;
    }
    void DisableSlash1()
    {
        slash1.slashObj.SetActive(false);
    }
    void DisableSlash2()
    {
        slash2.slashObj.SetActive(false);
    }
    void DisableSlash3()
    {
        slash3.slashObj.SetActive(false);
    }

    public void AttackStart()
    {
        colliderWeapon.enabled = true;
    }

    public void AttackEnd()
    {
        colliderWeapon.enabled = false;
    }

    public void Verificar_combo()
    {

        puedo_dar_click = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && cantidad_click == 1)
        {
            animator.SetInteger("attack", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
            
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && cantidad_click >= 2)
        {       
            animator.SetInteger("attack", 2);
            puedo_dar_click = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2") && cantidad_click == 2)
        {       
            animator.SetInteger("attack", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
            //StartCoroutine(SlashAttack2());
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_2") && cantidad_click >= 3)
        {       
            animator.SetInteger("attack", 3);
            puedo_dar_click = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_3"))
        {      
            animator.SetInteger("attack", 0);
            puedo_dar_click = true;
            cantidad_click = 0;
            //StartCoroutine(SlashAttack3());
        }
    }
    IEnumerator SlashAttack2()
    {
        yield return new WaitForSeconds(slash2.delay);
        slash2.slashObj.SetActive(true);
        yield return new WaitForSeconds(1);
        DisableSlash2();
        attacking = false;
    }

    IEnumerator SlashAttack3()
    {
        yield return new WaitForSeconds(slash3.delay);
        slash3.slashObj.SetActive(true);
        yield return new WaitForSeconds(1);
        DisableSlash3();
        attacking = false;
    }
}

[System.Serializable]
public class Slash
{
    public GameObject slashObj;
    public float delay;
}
