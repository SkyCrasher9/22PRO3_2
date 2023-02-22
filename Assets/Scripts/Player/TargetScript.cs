using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    PlayerWarp warp;

    void Start()
    {
        warp = FindObjectOfType<PlayerWarp>();
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
