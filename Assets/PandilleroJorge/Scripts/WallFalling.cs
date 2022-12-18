using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFalling : MonoBehaviour
{
    public float tiempo1;
    public float tiempo2;

    // Start is called before the first frame update
    void Start()
    {
        tiempo1 = Random.Range(15, 40);
    }

    // Update is called once per frame
    void Update()
    {
        tiempo2 += Time.deltaTime;

        WallFall();
    }

    public void WallFall()
    {
        if (tiempo1 <= tiempo2)
        {
            Debug.Log("Se callo la pared");
            this.gameObject.SetActive(false);
        }
    }
        

}
