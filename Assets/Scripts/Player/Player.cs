using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    #region Inspector Attributes
    [Header("Settings")]
    [SerializeField] public bool isPlayer;

    [Header("Settings")]
    [SerializeField] protected float[] maxHealth;
    [SerializeField] protected float[] maxStamina;
    [SerializeField] protected float maxSlots;
    [SerializeField] protected float healthRecover;
    [SerializeField] protected float staminaRecover;
    [SerializeField] protected float runStamina;
    [SerializeField] protected float comboDuration;
    [SerializeField] protected float comboSlot;

    [Header("Time")]
    [SerializeField] private AnimationCurve timeLerpCurve;
    [SerializeField] private float timeLerpDuration;
    [SerializeField] private int timeEvent;

    [Header("Damage")]
    [SerializeField] protected int damageEvent;

    [SerializeField] public List<GameObject> enemys;

    /*
    [Header("Audio")]
    [SerializeField] protected AudioSource dieSource;
    */
    /*
    [Header("Ground")]
    [SerializeField] private bool checkGround;
    [SerializeField] private float checkOffset;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask checkMask;
    */


    [Header("References")]
    [SerializeField] protected Animator[] animators;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        enemys = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
