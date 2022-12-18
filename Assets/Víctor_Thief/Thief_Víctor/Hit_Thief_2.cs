using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Thief_2 : MonoBehaviour
{
    public GameObject Thief; //Declaración de un GameObject
    
    // Start is called before the first frame update
    void Start()
    {
        Thief = GameObject.FindGameObjectWithTag("Thief"); //Busca los GameObjects con el Tag Thief
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) //El if ocurrirá cuando se pulse la tecla espacio
        {
            GolpearLadron();
        }
    }
    public void GolpearLadron() //Este método llama al trigger que hace que el Thief pase al estado Hit
    {
        Thief.GetComponent<Animator>().SetTrigger("BeingHit");
    }
}
