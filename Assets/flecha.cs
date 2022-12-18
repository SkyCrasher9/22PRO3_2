using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flecha : MonoBehaviour
{
    public float velocidad = 10;

    public float tiempo;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        tiempo+=Time.deltaTime;
        //
        this.transform.position += Vector3.back * velocidad * Time.deltaTime;

        if (tiempo > 3)
        {
            Destroy(this.gameObject);
        }
        
    }
}
