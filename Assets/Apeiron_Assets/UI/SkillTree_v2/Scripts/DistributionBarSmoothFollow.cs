using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributionBarSmoothFollow : MonoBehaviour
{
    public Vector2 targetPos;
    UiLineRenderer uiLineRenderer;

    Vector2 refV;

    // Start is called before the first frame update
    void Start()
    {
        uiLineRenderer = GetComponent<UiLineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(uiLineRenderer.points[1] != targetPos)
        {
            uiLineRenderer.points[1] = Vector2.SmoothDamp(uiLineRenderer.points[1], targetPos, ref refV, .2f);
        }
    }

    public void UpdateTargetPosition(Vector3 pos)
    {
        targetPos = pos;
    }
}
