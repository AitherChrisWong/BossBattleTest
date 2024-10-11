using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineRenderer : MonoBehaviour
{
    public bool isActive;

    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public LineRenderer linerenderer;
    float vertexCount = 48;
    public float Point2Ypositio = 2;

    public Material lineMat;
    public AnimationCurve lineAlphaCurve;

    float alphaTimer;

    Vector3 offset = new Vector3(-.1f, 0f, 0);
    Vector3 targetOffset = new Vector3(-.1f, 2f, 0);

    Transform currentTrans;
    Transform targetTrans;

    public AnimationCurve activeArrowCurve;
    float tempActiveArrowCurveTime;

    public bool isKeepShowTargetLine;
    float arrowAlpha = 5;

    // Start is called before the first frame update
    void Start()
    {
        lineMat = transform.Find("Line").GetComponent<LineRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && Point3 != null)
        {
            if (Point3 != null)
            {
                if (linerenderer.positionCount <= 0)
                {
                    ActiveArrow();
                }

                UpdateCurve();

                UpdateActiveArrowAnimation();

                if (Input.GetKey("0"))
                {
                    ActiveArrow();
                    print("active arrow");
                }
            }

            if (alphaTimer <= 1)
            {
                //lineMat.SetColor("_TintColor", new Color(1, 1, 1, lineAlphaCurve.Evaluate(alphaTimer)));
                alphaTimer += Time.deltaTime;
            }
        }else
        {
            isActive = false;
            linerenderer.positionCount = 0;
        }
    }

    private void UpdateCurve()
    {
        targetTrans = Point1;
        currentTrans = Point3;

        Point2Ypositio = 8 + Vector3.Distance(currentTrans.position + targetOffset, targetTrans.position) / 5;

        //Point2.transform.position = new Vector3((Point1.transform.position.x + Point3.transform.position.x), Point2Ypositio, (Point1.transform.position.z + Point3.transform.position.z) / 2);
        Point2.position = new Vector3((currentTrans.transform.position.x + offset.x + targetTrans.transform.position.x) / 2, Point2Ypositio, (currentTrans.transform.position.z + offset.z + targetTrans.transform.position.z) / 2);

        if (Point2.position.y < 3)
            Point2.position = new Vector3(Point2.position.x, 3, Point2.position.z);

        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Slerp(currentTrans.position + targetOffset, Point2.position, ratio);
            var tangent2 = Vector3.Lerp(Point2.position, targetTrans.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        linerenderer.positionCount = pointList.Count;
        linerenderer.SetPositions(pointList.ToArray());
    }

    public void showTargetLine()
    {
        //lineMat.SetColor("_TintColor", new Color(1, 1, 1, .1f));
        alphaTimer = 0;
    }

    public void ActiveArrow()
    {
        tempActiveArrowCurveTime = 0;

        if(isKeepShowTargetLine)
        {
            arrowAlpha = 99999;
        }else
        {
            arrowAlpha = 5;
        }
        
        //yield return null;
    }

    private void UpdateActiveArrowAnimation()
    {
        float tempCount = activeArrowCurve.Evaluate(tempActiveArrowCurveTime);

        for (int i = 0; i < linerenderer.positionCount * tempCount; i++)
        {
            linerenderer.SetPosition(i, linerenderer.GetPosition((int)(linerenderer.positionCount * tempCount)));
        }

        if (tempActiveArrowCurveTime < 1)
        {
            tempActiveArrowCurveTime += Time.deltaTime*2;
            linerenderer.material.SetFloat("_alpha", 1);
        }
        else if (arrowAlpha > 0)
        {
            arrowAlpha -= Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime;
            
            if(arrowAlpha <=1)
                linerenderer.material.SetFloat("_alpha", arrowAlpha);
            else
                linerenderer.material.SetFloat("_alpha", 1);

        }
        else
        {
            linerenderer.material.SetFloat("_alpha", 0);
        }
    }
}
