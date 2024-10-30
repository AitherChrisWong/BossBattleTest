using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CanvasBattleUI : MonoBehaviour
{
    public bool isDragCardMode;
    public Transform bottomRightConer;

    public Transform curMousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempMousePos = Input.mousePosition;
        tempMousePos.x -= Screen.width / 2;
        tempMousePos.y -= Screen.height / 2;

        curMousePos.localPosition = tempMousePos;


        if(isDragCardMode)
        {
            bottomRightConer.transform.localScale = Vector3.Lerp(bottomRightConer.transform.localScale, new Vector3(.5f, .5f, .5f), .25f);
        }
        else
        {
            bottomRightConer.transform.localScale = Vector3.Lerp(bottomRightConer.transform.localScale, Vector3.one, .25f);
        }

      
    }
}
