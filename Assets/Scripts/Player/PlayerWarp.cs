using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerWarp : MonoBehaviour
{
    public PlayerMovement input;
    private Animator anim;
    public bool isLocked;
    public CinemachineFreeLook aimCam;
    public List<Transform> screenTargets = new List<Transform>();
    public Transform target;
    public GameObject transformTarget;

    [Space]

    [Header("Canvas")]
    public Image aim;
    public Image lockAim;
    public Vector2 uiOffset;

    public GameObject canvas;

    void Start()
    {
        anim = GetComponent<Animator>();

        canvas = GameObject.FindGameObjectWithTag("TargetCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(this.transform.position, target.position);

        if (distance < 10)
        {
            finalTerget();
            canvas.SetActive(true);
            RotateTowards(target);
        }
        else
        {
            canvas.SetActive(false);
        }
    }


    private void UserInterface()
    {

        aim.transform.position = Camera.main.WorldToScreenPoint(target.position + (Vector3)uiOffset);
        Color c = screenTargets.Count < 1 ? Color.clear : Color.white;
        aim.color = c;
    }

    void LockInterface(bool state)
    {
        
        float size = state ? 1 : 2;
        float fade = state ? 1 : 0;
        lockAim.DOFade(fade, .15f);
        lockAim.transform.DOScale(size, .15f).SetEase(Ease.OutBack);
        lockAim.transform.DORotate(Vector3.forward * 180, .15f, RotateMode.FastBeyond360).From();
        aim.transform.DORotate(Vector3.forward * 90, .15f, RotateMode.LocalAxisAdd);
        
    }
    public int targetIndex()
    {
        float[] distances = new float[screenTargets.Count];

        for (int i = 0; i < screenTargets.Count; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(screenTargets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
        }

        float minDistance = Mathf.Min(distances);
        int index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }

        return index;

    }
    public void RotateTowards(Transform t)
    {
        //float lockPos = 0;
        if (isLocked) 
        {
            float inputCameraX = Quaternion.LookRotation(t.position).x;
            float inputCameraZ = Quaternion.LookRotation(t.position).z;

            //transform.rotation = Quaternion.Euler(0f, inputCameraY, inputCameraZ);
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);

            //transform.rotation = Quaternion.LookRotation(t.position - transform.position);
            
           //transform.localRotation = Quaternion.LookRotation(t.position - new Vector3(inputCameraX, 0, 0));

        }
        
        
        //Vector3 rotationLook = new Vector3(transformTarget.transform.position.x, 0, transformTarget.transform.position.z);
        //transform.LookAt(rotationLook);
    }

    public void finalTerget()
    {
        UserInterface();

        if (screenTargets.Count < 1)
            return;

        if (!isLocked)
        {
            target = screenTargets[targetIndex()];
        }

        if (Input.GetMouseButtonDown(1))
        {
            LockInterface(true);
            isLocked = true;

            //aimCam.LookAt = target.transform;

            //Prueba
            //input.blockRotationPlayer = true;
        }

        if (Input.GetMouseButtonUp(1) && isLocked == true)
        {
            LockInterface(false);
            isLocked = false;
            //aimCam.LookAt = transformTarget.transform;

            //Prueba
            //input.blockRotationPlayer = false;
        }

        if (!isLocked)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //input.RotateTowards(target);
            //input.canMove = false;

        }
    }
}
