using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
using Cinemachine;

public class TargetLock : MonoBehaviour
{
    private MovementInput input;
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

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
            aimCam.LookAt = target.transform;
        }

        if (Input.GetMouseButtonUp(1) && isLocked == true)
        {
            LockInterface(false);
            isLocked = false;
            aimCam.LookAt = transformTarget.transform;
        }

        if (!isLocked)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            input.RotateTowards(target);
            //input.canMove = false;
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
}
