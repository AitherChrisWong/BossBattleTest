using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Skill1009_line_follow : MonoBehaviour
{
    public Transform casterPosition;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = casterPosition.position;
        targetPos.y = 0.1f;

        lineRenderer.SetPosition(1, targetPos);
    }
}
