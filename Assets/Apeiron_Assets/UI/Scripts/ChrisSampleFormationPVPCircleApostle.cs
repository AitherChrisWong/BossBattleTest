using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChrisSampleFormationPVPCircleApostle : MonoBehaviour
{
    public int ApostleId = 0;
    public int TeamColorId = 0;
    //public bool isInField = false;

    public Color[] teamColors;

    public TextMeshProUGUI txtApostleId;
    public Image frameImg;
    public GameObject arrow;

    bool isUpdateSorting;
    Vector3 targetPos;
    Vector3 tempV3;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTeamColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        //update circle apostle ui sorting
        if(isUpdateSorting)
        {
            if(Vector3.Distance(transform.localPosition, targetPos) > .1f)
            {
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref tempV3, .05f);
            }
            else
            {
                isUpdateSorting = false;
            }
        }
    }

    public void UpdateTeamColor(int colorId)
    {
        if(colorId > 0)
        {
            frameImg.color = teamColors[colorId];
            arrow.SetActive(true);
            arrow.GetComponent<Image>().color = teamColors[colorId];

            GetComponent<CanvasGroup>().alpha = 1;
            

        }
        else
        {
            frameImg.color = teamColors[0];
            arrow.SetActive(false);
            GetComponent<CanvasGroup>().alpha = .25f;
        }

        TeamColorId = colorId;
        //print("update cirlce: " + colorId);
    }

    public void UpdateSortingPosition(Vector3 tempPos)
    {
        isUpdateSorting = true;
        targetPos = tempPos;
    }
}
