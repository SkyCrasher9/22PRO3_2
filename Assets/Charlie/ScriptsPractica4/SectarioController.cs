using System.Collections;
using System.Collections.Generic;

using UnityEngine.AI;
using UnityEngine;

public class SectarioController : MonoBehaviour
{
    //Contador de golpes
    public int contadorDeGolpes;
    //WayPoints
    public Transform[] puntosRuta;
    //Ayuda de contador de tiempo
    public float tiempoDeEspera;
    // variable De golpeo del Rayo para poder detectar que golpean al agente.
    public RaycastHit tocarJugador;
    //
    public GameObject jugadorObjetivo;
    //
    public int vidaBaseEnemigo = 20;
    //Puntos de vida
    public int vidaActualEnemigo;
    //Maximo numero de vidas
    public int numeroMaximoDeVidas = 20;
    //
    public int dañoRecibido;
    //
    public GameObject flechaDeSectario;
    //
    public Transform inicioFlecha;
    
    public void Start()
    {
        vidaActualEnemigo = numeroMaximoDeVidas;
       
    }
    public void Update()
    {
        if (vidaActualEnemigo <= 0)
        {
            this.GetComponent<Animator>().SetTrigger("aMuerte");
        }
    }
    public void AlAtaquer()
    {
        //
        Debug.Log("Chiquito: Al ataquer");
       //
        Instantiate(flechaDeSectario,inicioFlecha.transform.position,Quaternion.Euler(0.0f,180.0f,0.0f));
    }
    //
    public void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Cubo"))
        {
            this.GetComponent<Animator>().SetTrigger("aRecibidoDaño");
           
        }
    }
    public void RecibirDaño()
    {
        vidaActualEnemigo = vidaActualEnemigo - dañoRecibido;
    }

}
