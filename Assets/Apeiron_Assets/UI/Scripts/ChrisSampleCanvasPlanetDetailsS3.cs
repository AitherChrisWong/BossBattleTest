using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Obsolete("AxPlanetDetailPVPS3")]
public class ChrisSampleCanvasPlanetDetailsS3 : MonoBehaviour
{
    //public int selectedPlanetId = 0;

    public RectTransform planetList;
    public CanvasGroup planetListCanvasGroup;
    public AnimationCurve expendListAnimCurve;
    public float expendListAnimDuration = 1;

    bool isExpendListAnim;
    float expendListAnimTime = 0;

    public bool isPlanetViewExpend = false;

    public Button btnViewMore;

    [Header("Right Content")]
    public CanvasGroup rightContentCanvasGroup;
    public GameObject[] rightContents;

    public GameObject btnRename;
    public GameObject btnBookmark;
    private bool isUpdateRightContentAnim;
    private float updateRightContentAnimTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isExpendListAnim)
        {
            if (expendListAnimTime < 1)
            {
                planetListCanvasGroup.alpha = expendListAnimCurve.Evaluate(expendListAnimTime);
                expendListAnimTime += Time.deltaTime / expendListAnimDuration;
            }
            else
            {
                isExpendListAnim = false;
            }
        }

        if (isUpdateRightContentAnim)
        {
            if (updateRightContentAnimTime < 1)
            {
                rightContentCanvasGroup.alpha = expendListAnimCurve.Evaluate(updateRightContentAnimTime);
                updateRightContentAnimTime += Time.deltaTime / expendListAnimDuration;
            }
            else
            {
                isUpdateRightContentAnim = false;
            }
        }
    }

    public void BtnViewMoreOnPress()
    {

        if (isPlanetViewExpend)
        {
            planetList.sizeDelta = new Vector2(300, 0);
            btnViewMore.transform.Find("group/text_btn").GetComponent<TextMeshProUGUI>().text = "View More";
            isPlanetViewExpend = false;
        }
        else
        {
            planetList.sizeDelta = new Vector2(900, 0);
            btnViewMore.transform.Find("group/text_btn").GetComponent<TextMeshProUGUI>().text = "View Less";
            isPlanetViewExpend = true;
        }

        PlayExpendListAnim();
    }

    public void PlayExpendListAnim()
    {
        expendListAnimTime = 0;
        isExpendListAnim = true;
    }

    public void ShowRightContent(int page)
    {
        foreach(GameObject child in rightContents)
        {
            child.SetActive(false);
        }

        rightContents[page].SetActive(true);

        if(page == 0)
        {
            btnRename.SetActive(true);
            btnBookmark.SetActive(true);
        }else
        {
            btnRename.SetActive(false);
            btnBookmark.SetActive(false);

        }

        //start fade in animation
        updateRightContentAnimTime = 0;
        isUpdateRightContentAnim = true;
    }
}
