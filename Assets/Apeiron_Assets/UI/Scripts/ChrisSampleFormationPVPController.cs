using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChrisSampleFormationPVPController : MonoBehaviour
{
    public GameObject[] apsotlesInField;
    public int apostleCount = 0;
    public ChrisSampleFormationPVPCircleApostle[] circleApostles;
    public Transform[] circleApostleSlotPos;

    public int[] apostleStatus;

    public List<GameObject> inFieldApostles = new List<GameObject>();
    public List<GameObject> notInFieldApostles = new List<GameObject>();

    [Header("Formation")]
    public bool isReady;
    public int formationTime = 30;
    public float currentFormationCountdown = 0;
    public RectTransform btnRetreatCountdownBar;
    public RectTransform btnReadyCountdownBar;
    public float btnRetreatDefaultWidth;
    public GameObject[] redAlarm;

    public Button btnReady;
    public Button btnRetreat;

    public GameObject waitForOpponent;


    [Header("Fake squad deck")]
    public ChrisSampleSquadDeck suqadDeckUi;
    public Vector3[] squadDecks;
    public int[] currentSquadDeckInfo;
    public int[] previewSquadDeckInfo;

    [Header("Snap")]
    public int currentSnapCount = 1;
    public TextMeshProUGUI txtSnapCount;
    public TextMeshProUGUI txtNextRoundCount;
    public GameObject labelNextRound;
    public GameObject normalSnap;
    public GameObject activatedSnap;
    public GameObject vfxSnapActive;





    // Start is called before the first frame update
    void Start()
    {
        currentFormationCountdown = formationTime;
        btnRetreatDefaultWidth = btnRetreatCountdownBar.rect.width;

        btnRetreatCountdownBar.anchorMin = new Vector2(0, 0);
        btnRetreatCountdownBar.anchorMax = new Vector2(0, 1);
        btnReadyCountdownBar.anchorMin = new Vector2(0, 0);
        btnReadyCountdownBar.anchorMax = new Vector2(0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if(currentFormationCountdown > 0)
        {
            currentFormationCountdown -= Time.deltaTime;
            float tempWidthX = (currentFormationCountdown / formationTime) * btnRetreatDefaultWidth;
            btnRetreatCountdownBar.sizeDelta = new Vector2(tempWidthX, 1);
            btnReadyCountdownBar.sizeDelta = new Vector2(tempWidthX, 1);


            //last 10 sec button have red alarm effect
            if(currentFormationCountdown < 10)
            {
                redAlarm[0].SetActive(true);
                redAlarm[1].SetActive(true);
            }
        }
        else
        {
            //timesup!
        }
    }

    public void ShowApostle(int id)
    {
        apsotlesInField[id].SetActive(true);
        //circleApostles[id].UpdateTeamColor(id);
        apostleCount++;

    }

    public void HideApostle(int id)
    {
        apsotlesInField[id].SetActive(false);
        apostleCount--;

    }

    public void UpdateCircleApostle(int apostleId, int teamColorId)
    {
        circleApostles[apostleId].UpdateTeamColor(teamColorId);
        apostleStatus[apostleId] = teamColorId;
    }

    public void SortingCircleApostle()
    {
        inFieldApostles.Clear();
        notInFieldApostles.Clear();
        ResetSquadDeckInfo();

        for (int i = 0; i < apostleStatus.Length; i++)
        {
            
            if (apostleStatus[i] > 0)
            {
                //currentSquadDeckInfo[i]++;
                AddSquadDeckInfo(i);
                inFieldApostles.Add(circleApostles[i].gameObject);
                //print(apostleStatus[i]);
            }
            else
            {
                notInFieldApostles.Add(circleApostles[i].gameObject);
            }
        }

        int tempId = 0;
        foreach(GameObject a in inFieldApostles)
        {
            a.GetComponent<ChrisSampleFormationPVPCircleApostle>().UpdateSortingPosition(circleApostleSlotPos[tempId].localPosition);
            tempId++;
        }
        foreach (GameObject b in notInFieldApostles)
        {
            b.GetComponent<ChrisSampleFormationPVPCircleApostle>().UpdateSortingPosition(circleApostleSlotPos[tempId].localPosition);
            tempId++;
        }

        suqadDeckUi.UpdateSquadMana(currentSquadDeckInfo);
    }

    public void ResetSquadDeckInfo()
    {
        //currentSquadDeck = Vector3.zero;
        for(int i = 0; i < currentSquadDeckInfo.Length; i++)
        {
            currentSquadDeckInfo[i] = 0;
        }
    }

    public void AddSquadDeckInfo(int apostleId)
    {
        int tempMana1 = Mathf.RoundToInt(squadDecks[apostleId].x);
        int tempMana2 = Mathf.RoundToInt(squadDecks[apostleId].y);
        int tempMana3 = Mathf.RoundToInt(squadDecks[apostleId].z);
        currentSquadDeckInfo[tempMana1]++;
        currentSquadDeckInfo[tempMana2]++;
        currentSquadDeckInfo[tempMana3]++;

    }

    public void PreviewSquadDeckInfo(int previewApostleId, bool isPreview, bool isInSlot, Color teamColor)
    {
        if(isPreview)
        {
            //previewSquadDeckInfo = currentSquadDeckInfo;
            for (int i = 0; i < previewSquadDeckInfo.Length; i++)
            {
                previewSquadDeckInfo[i] = currentSquadDeckInfo[i];
            }

            if (isInSlot)
            {
                int tempMana1 = Mathf.RoundToInt(squadDecks[previewApostleId].x);
                int tempMana2 = Mathf.RoundToInt(squadDecks[previewApostleId].y);
                int tempMana3 = Mathf.RoundToInt(squadDecks[previewApostleId].z);
                previewSquadDeckInfo[tempMana1]++;
                previewSquadDeckInfo[tempMana2]++;
                previewSquadDeckInfo[tempMana3]++;
                suqadDeckUi.PreviewSquadMana(currentSquadDeckInfo, previewSquadDeckInfo, teamColor);
            }
            else
            {
                int tempMana1 = Mathf.RoundToInt(squadDecks[previewApostleId].x);
                int tempMana2 = Mathf.RoundToInt(squadDecks[previewApostleId].y);
                int tempMana3 = Mathf.RoundToInt(squadDecks[previewApostleId].z);
                previewSquadDeckInfo[tempMana1]--;
                previewSquadDeckInfo[tempMana2]--;
                previewSquadDeckInfo[tempMana3]--;
                suqadDeckUi.PreviewSquadMana(currentSquadDeckInfo, previewSquadDeckInfo, teamColor);
            }
        }
        else
        {
            for (int i = 0; i < previewSquadDeckInfo.Length; i++)
            {
                previewSquadDeckInfo[i] = currentSquadDeckInfo[i];
            }
            suqadDeckUi.UpdateSquadMana(previewSquadDeckInfo);
        }

        
    }


    public void BtnPointerEnter(Button btn)
    {
        if(!isReady)
        {
            btn.GetComponent<Animator>().Play("sharing_btn_hover");
        }
    }

    public void BtnPointerExit(Button btn)
    {
        if (!isReady)
            btn.GetComponent<Animator>().Play("sharing_btn_idle");
    }

    public void BtnReadyOnPress()
    {
        if(!isReady)
        {
            isReady = true;
            btnReady.interactable = false;
            btnRetreat.interactable = false;
            btnReady.GetComponent<Animator>().Play("sharing_btn_disable");
            btnRetreat.GetComponent<Animator>().Play("sharing_btn_disable");

            waitForOpponent.SetActive(true);
        }
    }

    public void BtnRetreatOnPress()
    {
        if (!isReady)
        {
            //isReady = true;
            btnReady.interactable = false;
            btnRetreat.interactable = false;
            btnReady.GetComponent<Animator>().Play("sharing_btn_disable");
            btnRetreat.GetComponent<Animator>().Play("sharing_btn_disable");

        }
    }

    public void BtnSnapHover(Button btn)
    {
        btn.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void BtnSnapIdle(Button btn)
    {
        btn.transform.localScale = new Vector3(1, 1, 1);
    }

    public void BtnSnapOnPress(Button btn)
    {
        //currentSnapCount++;
        labelNextRound.SetActive(true);
        txtNextRoundCount.text = (currentSnapCount + 1).ToString();

        normalSnap.SetActive(false);
        activatedSnap.SetActive(true);
        vfxSnapActive.SetActive(true);
    }
}
