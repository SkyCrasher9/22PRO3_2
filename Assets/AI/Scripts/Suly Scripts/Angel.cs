using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    //Variable para almacenar al objetivo al que se ha de dirigir, en este caso JC.
    public Transform Target;

    //variable donde se almacena a quien a detectado en su rango
    public GameObject detected;

    //Array donde se guardan todos los puntos de patrulla
    public Transform[] PatrolPoints;

    //Variable de los golpes de vida del Angel.
    public int lifeHits = 7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Al presionar el boton izquierdo del raton se activa el comportamientod de Stun.
        if(Input.GetMouseButtonDown(0))
            this.GetComponent<Animator>().SetTrigger("IsStunned");
    }

    //Componente de ataque
    public void Attack()
    {
        Debug.Log("Lo estas pegando");
    }

    /*
    IEnumerator Angel.ResetTrident()
    {
        counterExposed += Time.deltaTime;
        

        if (counterExposed >= 3)
        {
            counterReset = 0;
            Debug.Log("Estas Expuesto bro");
        }
        if (counterReset >= 5)
        {
            counterReset += Time.deltaTime;
            Debug.Log("Sacando un nuevo Tridente");
        }
    }
    */
        
}
