using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isEquipped = false;
    public GameObject Handweapon;
    public GameObject BackWeapon;
    public ParticleSystem DisolveBack;
    public ParticleSystem DisolveHand;
    public MeshRenderer MeshRendererBack;
    public MeshRenderer MeshRendererHand;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        MeshRendererHand.enabled = false; //Mesh Renderer de la katana en la mano.
        MeshRendererBack.enabled = true;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && isEquipped == false) //Si hago Click en el ratón y no está equipada.
        {
            animator.SetBool("HasSword", true);
            DisolveBack.Play();
            MeshRendererBack.enabled = false;
            DisolveHand.Play();
            MeshRendererHand.enabled = true;
            isEquipped = true;
        }

        if (Input.GetMouseButtonDown(3) && isEquipped == true) //Si hago Click en la rueda del ratón y está equipada.
        {
            animator.SetBool("HasSword", false);
            DisolveHand.Play();
            MeshRendererBack.enabled = true;
            DisolveBack.Play();
            MeshRendererHand.enabled = false;
            isEquipped = false;
        }

    }
    void SwapWeapons()
    {

    }
}
