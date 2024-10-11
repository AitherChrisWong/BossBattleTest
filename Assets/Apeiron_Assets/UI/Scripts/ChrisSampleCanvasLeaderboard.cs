using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChrisSampleCanvasLeaderboard : MonoBehaviour
{
    //public int selectedPlanetId = 0;

    public AnimationCurve rightContentAlphaAnimCurve;
    public float rightContentAlphaAnimDuration = 1;

    [Header("Right Content")]
    public CanvasGroup rightContentCanvasGroup;
    public GameObject[] rightContents;

    private bool isUpdateRightContentAnim;
    private float updateRightContentAnimTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isUpdateRightContentAnim)
        {
            if (updateRightContentAnimTime < 1)
            {
                rightContentCanvasGroup.alpha = rightContentAlphaAnimCurve.Evaluate(updateRightContentAnimTime);
                updateRightContentAnimTime += Time.deltaTime / rightContentAlphaAnimDuration;
            }
            else
            {
                isUpdateRightContentAnim = false;
            }
        }
    }


    public void ShowRightContent(int page)
    {
        foreach(GameObject child in rightContents)
        {
            child.SetActive(false);
        }

        rightContents[page].SetActive(true);


        //start fade in animation
        updateRightContentAnimTime = 0;
        isUpdateRightContentAnim = true;
    }
}
