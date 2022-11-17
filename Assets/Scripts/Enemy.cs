using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public GameObject cube;
    public Renderer cubeRenderer;
    private Color newCubeColor;

    private float randomChannelOne, randomChannelTwo, randomChannelThree;

    public GameObject bloodParticle;

    private Rigidbody rb;

    private void Start()
    {
        //cubeRenderer = cube.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            print("AAAAH!");
            ChangeCubeColor();
            DisplayBlood();
            HitJump();
        }
    }

    public void ChangeCubeColor()
    {
        randomChannelOne = Random.Range(0f, 1f);
        randomChannelTwo = Random.Range(0f, 1f);
        randomChannelThree = Random.Range(0f, 1f);

        newCubeColor = new Color(randomChannelOne, randomChannelTwo, randomChannelThree, 1f);

        cubeRenderer.material.SetColor("_BaseColor", newCubeColor);
    }

    private void HitJump()
    {
        cube.GetComponent<Rigidbody>().AddForce(Vector3.up * 70f);
    }

    void DisplayBlood()
    {
        StartCoroutine(BloodDisplay());
    }

    void DisableBlood()
    {
        bloodParticle.SetActive(false);
    }

    IEnumerator BloodDisplay()
    {
        //yield return new WaitForSeconds(1);
        bloodParticle.SetActive(true);
        yield return new WaitForSeconds(1);
        DisableBlood();
    }
}
