using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleSkillCard : MonoBehaviour
{
    bool isHover;
    public bool isDrag;
    public bool isDraggedOutside;

    public Transform curParent;
    Transform tempCardGroup;

    CanvasBattleUI canvasBattleUI;
    PVEBattleController pveBattleController;

    Vector2 startDragPos;

    public GameObject cardHoverLightAvatar;
    public GameObject cardHoverLightApostle;

    public float dragOutDistance = 100;
    public float tempDragSlow = 5;



    // Start is called before the first frame update
    void Start()
    {
        curParent = transform.parent;
        canvasBattleUI = GameObject.Find("Canvas_Battle UI").GetComponent<CanvasBattleUI>();
        pveBattleController = GameObject.Find("PVE Battle").GetComponent<PVEBattleController>();
        tempCardGroup = canvasBattleUI.transform.Find("card temp Pos");
    }

    // Update is called once per frame
    void Update()
    {
        CardDrag();

        if(isHover || isDrag)
        {
            cardHoverLightAvatar.SetActive(true);
            cardHoverLightApostle.SetActive(true);
        }
        else
        {
            cardHoverLightAvatar.SetActive(false);
            cardHoverLightApostle.SetActive(false);
        }
    }

    public void CardHover()
    {
        isHover = true;
    }

    public void CardExit()
    {
        isHover = false;
    }

    public void CardOnPress()
    {
        isDrag = true;
        //transform.parent = tempCardGroup;

        startDragPos = Input.mousePosition;
        startDragPos.x -= Screen.width / 2;
        startDragPos.y -= Screen.height / 2;


        
        //print("skill card on press");
    }

    public void CardDrag()
    {
        if(isDrag)
        {
            Vector3 tempMousePos = Input.mousePosition;
            tempMousePos.x -= Screen.width / 2;
            tempMousePos.y -= Screen.height / 2;

            float dragDistance = Vector3.Distance(startDragPos, tempMousePos);

            if(dragDistance < dragOutDistance && !isDraggedOutside)
            {
                transform.parent = curParent;
                transform.localPosition = new Vector3(0, dragDistance/ tempDragSlow, 0);

            }
            else
            {
                isDraggedOutside = true;
                canvasBattleUI.isDragCardMode = true;

                pveBattleController.isSlowMode = true;
            }

            if(isDraggedOutside)
            {
                Vector3 bottomRightConer = new Vector3(Screen.width / 2, -Screen.height / 1.3f, 0);

                float draggedDistance = Vector3.Distance(bottomRightConer, tempMousePos);

                if(draggedDistance < 500)
                {
                    transform.parent = curParent;
                    transform.localPosition = new Vector3(0, dragDistance / tempDragSlow, 0);
                }
                else
                {
                    transform.parent = tempCardGroup;
                    transform.localPosition = Vector3.Lerp(transform.localPosition, canvasBattleUI.curMousePos.localPosition, .25f);
                }

                
            }

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, .1f);
        }
    }

    public void CardRelease()
    {
        isDrag = false;
        isDraggedOutside = false;
        transform.parent = curParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        canvasBattleUI.isDragCardMode = false;
        pveBattleController.isSlowMode = false;

        //print("skill card release");
    }
}