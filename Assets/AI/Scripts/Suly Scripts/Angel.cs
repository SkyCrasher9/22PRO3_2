using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    //Variable para almacenar al objetivo al que se ha de dirigir, en este caso JC.
    public Transform Target;

    //variable donde se almacena a quien a detectado en su rango
    public GameObject detected;

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
        if(Input.GetMouseButtonDown(0))
            this.GetComponent<Animator>().SetTrigger("IsStunned");
        
    }

    public void Attack()
    {
        lifeHits--;
        Debug.Log("hAS SIFO PEGADO");
    }

    //IEnumerator();
}
