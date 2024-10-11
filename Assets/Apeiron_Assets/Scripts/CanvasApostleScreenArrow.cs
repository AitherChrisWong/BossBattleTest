using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasApostleScreenArrow : MonoBehaviour
{
    public Transform[] apostles;
    public Transform[] apostleArrows;

    RectTransform canvasRectT;
    public Transform tempCenter;

    float bounds = 100;

    // Start is called before the first frame update
    void Start()
    {
        canvasRectT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < apostles.Length; i++) 
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, apostles[i].position);

            Vector3 tempPos = screenPoint - canvasRectT.sizeDelta / 2f;
            if (tempPos.x < -Screen.width / 2 + bounds)
                tempPos.x = -Screen.width / 2 + bounds;

            if (tempPos.x > Screen.width / 2 - bounds)
                tempPos.x = Screen.width / 2 - bounds;

            if (tempPos.y < -Screen.height / 2 + bounds) 
                tempPos.y = -Screen.height / 2 + bounds;

            if (tempPos.y > Screen.height / 2 - bounds)
                tempPos.y = Screen.height / 2 - bounds;



            apostleArrows[i].localPosition = tempPos;
        }
        

    }
}
