using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCombat : Player
{
    [Header("Timers and tempos Attack")]
    [SerializeField] private float time;
    [SerializeField] private bool onAttack;
    [SerializeField] private float endTime;

    [Header("Combos")]
    [SerializeField] private int numberCombo;
    [SerializeField] private float timeCombo;
    [SerializeField] private float endTimeCombo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        basicAttack();
        
    }

    public void basicAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (onAttack == true)
            {
                for (int i = 0; i < enemys.Count; i++) enemys[i].GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

                time = 0;
                onAttack = false;
                comboAttack();

            }
        }
        timerAttacks(endTime);
        timerCombos(endTime + 0.2f);
    }


    public void comboAttack()
    {

        switch (numberCombo)
        {
            case 3:
                Debug.Log("Este es el 3 ataque");

                timeCombo = 0;
                numberCombo = 0;

                break;
            case 2:
                Debug.Log("Este es el 2 ataque");

                timeCombo = 0;
                numberCombo++;

                break;
            case 1:
                Debug.Log("Este es el 1 ataque");

                timeCombo = 0;
                numberCombo++;

                break;
            default:
                

                break;
        }
    }
    public void timerAttacks(float limitTimer)
    {
        if (time <= limitTimer)
        {
            time += Time.deltaTime;
        }
        else
        {
            onAttack = true;
        }
    }

    public void timerCombos(float limitTimer)
    {
        if (timeCombo <= limitTimer)
        {
            timeCombo += Time.deltaTime;
        }
        else
        {
            numberCombo = 1;
            //onCombo = true;
        }
    }
}
