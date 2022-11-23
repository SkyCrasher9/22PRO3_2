using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    TargetLock warp;

    void Start()
    {
        warp = FindObjectOfType<TargetLock>();
    }

    private void OnBecameVisible()
    {
        if (!warp.screenTargets.Contains(transform))
            warp.screenTargets.Add(transform);
    }

    private void OnBecameInvisible()
    {
        if (warp.screenTargets.Contains(transform))
            warp.screenTargets.Remove(transform);
    }
}
