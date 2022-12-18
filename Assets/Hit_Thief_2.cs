using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Thief_2 : MonoBehaviour
{
    public GameObject Thief;
    
   
    // Start is called before the first frame update
    void Start()
    {
        Thief = GameObject.FindGameObjectWithTag("Thief");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GolpearLadron();
        }
    }
    public void GolpearLadron()
    {
        Thief.GetComponent<Animator>().SetTrigger("BeingHit");
    }
}
