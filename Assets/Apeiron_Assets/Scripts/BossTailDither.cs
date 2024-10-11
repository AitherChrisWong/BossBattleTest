using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTailDither : MonoBehaviour
{
    public Transform tailPos;
    Transform cameraPos;

    public float tailDistance;
    float currentAlpha = 1;
    float targetAlpha = 1;

    public SkinnedMeshRenderer[] tailMeshes;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        tailDistance = Vector3.Distance(tailPos.position, cameraPos.position);

        if(tailDistance < 8)            targetAlpha = -.99f;
        else if(tailDistance < 13)      targetAlpha = -.7f;
        else if (tailDistance < 18)     targetAlpha = -.3f;
        else                            targetAlpha = 1f;

        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, .05f);
        UpdateTailDitherAlpha(currentAlpha);

    }

    void UpdateTailDitherAlpha(float value)
    {
        foreach(SkinnedMeshRenderer mesh in tailMeshes)
        {
            mesh.GetComponent<SkinnedMeshRenderer>().material.SetFloat("_alpha", value);
        }
    }
}
