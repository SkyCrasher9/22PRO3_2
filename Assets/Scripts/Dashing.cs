using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    MovementInput moveScript;
    public float dashTime;
    public float dashSpeed;

    public float activeTime = 2f;

    [Header("Mesh")]
    public float meshRefreshRate = 0.1f;
    public Transform positionToSpawn;
    public float meshDestroyDelay = 3f;

    [Header("Shader")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.05f;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private void Start()
    {
        moveScript = GetComponent<MovementInput>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && moveScript.Speed !=0)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isTrailActive && moveScript.Speed != 0)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail (float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            for(int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();

                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(gObj, meshDestroyDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.desiredMoveDirection *dashSpeed*Time.deltaTime);

            yield return null; 
        }
    }

    IEnumerator AnimateMaterialFloat(Material mat,float goal, float rate, float refreshRate)
    {
        float ValueToAnimate = mat.GetFloat(shaderVarRef);

        while (ValueToAnimate < goal)
        {
            ValueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, ValueToAnimate);

            yield return new WaitForSeconds(refreshRate);
        }
    }
}