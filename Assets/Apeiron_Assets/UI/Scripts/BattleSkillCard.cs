using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Caster { Avatar,Apostle1,Apostle2, Apostle3, Apostle4}

public class BattleSkillCard : MonoBehaviour
{
    public Caster caster;
    public int manaCost;

    bool isHover;
    public bool isDrag;
    public bool isDraggedOutside;

    public Transform curParent;
    Transform tempCardGroup;

    CanvasBattleUI canvasBattleUI;
    PVEBattleController pveBattleController;

    Vector2 startDragPos;

    [Header("UI Assets")]
    public GameObject cardHoverLightAvatar;
    public GameObject cardHoverLightApostle;

    public TextMeshProUGUI txtManaCostAvatar;
    public TextMeshProUGUI txtManaCostApostle;

    public float dragOutDistance = 100;
    public float tempDragSlow = 5;


    [Header("Skill status")]

    public GameObject skillPreview;
    public GameObject skillGroup;


    [Header("Card Drag Rotation")]
    Vector3 oldPos = Vector3.zero;
    Vector3 newPos = Vector3.zero;
    public Vector3 cardDragDirection = Vector3.zero;
    public float rotationPower = 1;
    public float rotationSpeed = 1;


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
        newPos = transform.localPosition;

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

        if(isDraggedOutside)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isDraggedOutside = false;
                CardRelease();
            }

            //
            cardDragDirection = oldPos - newPos;
            cardDragDirection = new Vector3(-cardDragDirection.y, cardDragDirection.x, 0);
            Quaternion targetRotation = Quaternion.Euler(cardDragDirection * rotationPower);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed);

            
        }

        oldPos = newPos;
    }

    public void CardHover()
    {
        isHover = true;
        UpdateTargetOutline(true);
    }

    public void CardExit()
    {
        isHover = false;
        UpdateTargetOutline(false);
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

                skillPreview.SetActive(true);
                UpdateTargetLayer("No Post");

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

                UpdateTargetOutline(true);
            }

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, .1f);
        }
    }

    public void CardRelease()
    {
        isDrag = false;
        transform.parent = curParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        canvasBattleUI.isDragCardMode = false;
        pveBattleController.isSlowMode = false;


        if (isDraggedOutside)
        {
            isDraggedOutside = false;

            //cast skill
            pveBattleController.curManaProgress -= manaCost;
            print("used mana: " + manaCost);

            //skillGroup.SetActive(true);
            GameObject tempVFX = Instantiate(skillGroup);
            tempVFX.transform.SetParent(pveBattleController.playerTeam[0].transform.Find("skin"));

            tempVFX.SetActive(true);
            tempVFX.transform.localPosition = Vector3.zero;
            tempVFX.transform.localRotation = Quaternion.identity;
            tempVFX.transform.localScale = Vector3.one;


            tempVFX.GetComponent<AvatarSkillController>().avatarBasicMovement = pveBattleController.playerTeam[0].GetComponent<AvatarBasicMovement>();
            tempVFX.GetComponent<AvatarSkillController>().avatarBasicMovement.isCastingSkill = true;
            tempVFX.GetComponent<AvatarSkillController>().targetPos = skillPreview.GetComponent<SkillPreview>().targetPosGroup;


            
        }

        //disable preview
        skillPreview.SetActive(false);
        UpdateTargetLayer("Default");

    }

    void UpdateTargetLayer(string layerName)
    {
        Transform skinMesh = pveBattleController.playerTeam[0].transform.Find("skin").GetChild(0);
        skinMesh.GetComponent<CharacterChildMeshBatchUpdate>().MeshChangeLayer(layerName);
    }

    void UpdateTargetOutline(bool active)
    {
        Transform skinMesh = pveBattleController.playerTeam[0].transform.Find("skin").GetChild(0);
        skinMesh.GetComponent<CharacterChildMeshBatchUpdate>().MeshOutlineLight(active);
    }




}
