using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisSampleFormationPVPApostleSlot : MonoBehaviour
{
    public bool isInSlot = true;

    public int apostleId;
    public int slotId;
    public GameObject apostleSkin;
    public Color teamColor;
    public Color[] tempColor;

    public GameObject hoverLight;

    public SpriteRenderer slotSprite;
    public Sprite[] slotBgs;

    ChrisSampleFormationPVPController formationPVPController;


    // Start is called before the first frame update
    void Start()
    {
        formationPVPController = GameObject.Find("FormationPVPController")?.GetComponent<ChrisSampleFormationPVPController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        if (formationPVPController == null)
            return;

        if (isInSlot)
        {
            PutApostleInScene();
            //change to mouse hover state
            formationPVPController.PreviewSquadDeckInfo(apostleId, true, false, teamColor);
        }
        else
        {
            ApostleLeaveScene();
        }

        print("apostle slot hover");
    }

    private void OnMouseEnter()
    {
        if (formationPVPController == null)
            return;

        if(isInSlot)
        {
            formationPVPController.PreviewSquadDeckInfo(apostleId, true, true, teamColor);
        }
        else
        {
            formationPVPController.PreviewSquadDeckInfo(apostleId, true, false, teamColor);
        }

        if (hoverLight)
            hoverLight.SetActive(true);
    }

    void OnMouseExit()
    {
        if (formationPVPController == null)
            return;

        formationPVPController.PreviewSquadDeckInfo(0, false, false, teamColor);

        if (hoverLight)
            hoverLight.SetActive(false);
    }

    void PutApostleInScene()
    {
        if (formationPVPController == null ||
            formationPVPController.apsotlesInField == null)
            return;

        if (formationPVPController.apostleCount < 4)
        {
            int targetId = -1;
            for (int i = 0; i < formationPVPController.apsotlesInField.Length; i++)
            {
                if(!formationPVPController.apsotlesInField[i].activeSelf)
                {
                    slotId = i;
                    teamColor = tempColor[i];
                    formationPVPController.ShowApostle(slotId);
                    formationPVPController.UpdateCircleApostle(apostleId, slotId+1);
                    formationPVPController.SortingCircleApostle();


                    apostleSkin.SetActive(false);
                    slotSprite.sprite = slotBgs[i + 1];

                    isInSlot = false;

                    break;
                }
            }
        }
    }

    void ApostleLeaveScene()
    {
        if (formationPVPController == null || apostleSkin == null)
            return;

        apostleSkin.SetActive(true);
        formationPVPController.HideApostle(slotId);
        formationPVPController.UpdateCircleApostle(apostleId, 0);
        formationPVPController.SortingCircleApostle();

        slotId = -1;
        teamColor = new Color(1, 1, 1, .25f);

        slotSprite.sprite = slotBgs[0];
        isInSlot = true;


    }
}
