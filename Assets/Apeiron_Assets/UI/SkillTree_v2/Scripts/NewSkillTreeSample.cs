using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewSkillTreeSample : MonoBehaviour
{
    public int playerCurrentSp = 50;
    public TextMeshProUGUI txtSp;

    [Header("Elemental Distribution")]
    public int[] MaxElementalDistribution = new int[4];
    public int[] CurrentElementalDistribution = new int[4];
    public TextMeshProUGUI[] txtElementDistribution;
    public Image[] elementValueBars;
    public Image[] elementValueBarsUsed;
    int tempValue = 100;

    public UiLineRenderer[] elementalDistributionBarBg;
    public UiLineRenderer[] elementalDistributionBar;
    public RectTransform[] elementalDistributionStars;


    [Header("Skill Levels")]
    public int[] SkillCardLevel = new int[4];
    public int[] EarthSkillUpgradeLevel;
    public int[] WaterSkillUpgradeLevel;
    public int[] FireSkillUpgradeLevel;
    public int[] WindSkillUpgradeLevel;
    public int[] PerksLevel = new int[4];

    [Header("Skill Upgrades")]
    public GameObject[] EarthSkillUpgrade;
    public GameObject[] WaterSkillUpgrade;
    public GameObject[] FireSkillUpgrade;
    public GameObject[] WindSkillUpgrade;

    public GameObject[] EarthSkillUpgradeLines;
    public GameObject[] WaterSkillUpgradeLines;
    public GameObject[] FireSkillUpgradeLines;
    public GameObject[] WindSkillUpgradeLines;

    public Color[] skillUpgradeLineColor;

    [Header("All Skills")]
    public SkillCardChrisSample[] C1Skills;
    public SkillCardChrisSample[] C2Skills;

    // Start is called before the first frame update
    void Start()
    {
        CheckSkillUpgradeLine();
        UpdateElementValue();
        UpdateElementDistribution();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockSkillUpgrade(int elementId)
    {
        switch(elementId)
        {
            case 1:
                foreach (GameObject u in EarthSkillUpgrade)
                {
                    u.GetComponent<SkillCardChrisSample>().isLocked = false;
                    u.GetComponent<SkillCardChrisSample>().currentSkillLevel++;
                    u.GetComponent<SkillCardChrisSample>().UpdateCardStatus();
                    print("Earth unlock");
                }
                break;

            case 2:
                foreach (GameObject u in WaterSkillUpgrade)
                {
                    u.GetComponent<SkillCardChrisSample>().isLocked = false;
                    u.GetComponent<SkillCardChrisSample>().currentSkillLevel++;
                    u.GetComponent<SkillCardChrisSample>().UpdateCardStatus();
                }
                break;

            case 3:
                foreach (GameObject u in FireSkillUpgrade)
                {
                    u.GetComponent<SkillCardChrisSample>().isLocked = false;
                    u.GetComponent<SkillCardChrisSample>().currentSkillLevel++;
                    u.GetComponent<SkillCardChrisSample>().UpdateCardStatus();
                }
                break;

            case 4:
                foreach (GameObject u in WindSkillUpgrade)
                {
                    u.GetComponent<SkillCardChrisSample>().isLocked = false;
                    u.GetComponent<SkillCardChrisSample>().currentSkillLevel++;
                    u.GetComponent<SkillCardChrisSample>().UpdateCardStatus();
                }
                break;
        }
    }

    public void CheckSkillUpgradeLine()
    {
        //check all skill line active or not (default = white color)
        foreach (GameObject line in EarthSkillUpgradeLines)
        {
            if (SkillCardLevel[0] > 0)
            {
                line.SetActive(true);
                line.GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[0];
            }
            else
            {
                line.SetActive(false);
            }
        }
        foreach (GameObject line in WaterSkillUpgradeLines)
        {
            if (SkillCardLevel[1] > 0)
            {
                line.SetActive(true);
                line.GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[0];
            }
            else
            {
                line.SetActive(false);
            }
        }
        foreach (GameObject line in FireSkillUpgradeLines)
        {
            if (SkillCardLevel[2] > 0)
            {
                line.SetActive(true);
                line.GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[0];
            }
            else
            {
                line.SetActive(false);
            }
        }
        foreach (GameObject line in WindSkillUpgradeLines)
        {
            if (SkillCardLevel[3] > 0)
            {
                line.SetActive(true);
                line.GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[0];
            }
            else
            {
                line.SetActive(false);
            }
        }



        for (int i = 0; i < 3; i++)
        {
            if (EarthSkillUpgradeLevel[i] > 0)
                EarthSkillUpgradeLines[i].GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[1];

            if (WaterSkillUpgradeLevel[i] > 0)
                WaterSkillUpgradeLines[i].GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[2];

            if (FireSkillUpgradeLevel[i] > 0)
                FireSkillUpgradeLines[i].GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[3];

            if (WindSkillUpgradeLevel[i] > 0)
                WindSkillUpgradeLines[i].GetComponent<UiLineRenderer>().color = skillUpgradeLineColor[4];
        }

    }

    public void UpdateSkillCardLevel(int ElementId, int level)
    {
        SkillCardLevel[ElementId] = level;

        if (level == 0)
        { }
        else if (level <= 2)
            CurrentElementalDistribution[ElementId]++;
        else if (level <= 4)
            CurrentElementalDistribution[ElementId] += 2;
        else if (level <= 6)
            CurrentElementalDistribution[ElementId] += 3;
        else
            CurrentElementalDistribution[ElementId] += 4;


        UpdateElementDistribution();
        UpdateElementDistributionBar(ElementId);

        txtSp.text = playerCurrentSp.ToString();
    }

    public void UpdatePerksLevel(int ElementId, int level)
    {
        PerksLevel[ElementId] = level;

        if (level == 0)
        { }
        else if (level <= 2)
            CurrentElementalDistribution[ElementId]++;
        else if (level <= 4)
            CurrentElementalDistribution[ElementId] += 2;
        else
            CurrentElementalDistribution[ElementId] += 3;


        UpdateElementDistribution();
        UpdateElementDistributionBar(ElementId);

        txtSp.text = playerCurrentSp.ToString();
    }



    public bool CheckSkillUpgradeStatus(SkillCardChrisSample targetSkill)
    {
        txtSp.text = playerCurrentSp.ToString();

        if (targetSkill.currentSkillLevel > 0)
            return true;
        else
            return false;

        
    }

    public void UpdateElementValue()
    {
        tempValue = 100;

        for (int i = 3; i >= 0; i--)
        {
            elementValueBars[i].fillAmount = tempValue / 100f;

            if(i < 3)
            {
                elementValueBarsUsed[i + 1].fillAmount = (tempValue + CurrentElementalDistribution[i+1]) / 100f;
            }
            
            tempValue -= MaxElementalDistribution[i];
        }

        elementValueBarsUsed[0].fillAmount = CurrentElementalDistribution[0]/100f;

    }

    public void UpdateElementDistribution()
    {
        for(int i = 0; i< 4; i++)
        {
            txtElementDistribution[i].text = CurrentElementalDistribution[i] + "/" + MaxElementalDistribution[i] + "(+0)";
        } 
    }

    public void UpdateElementDistributionBar(int ElementId)
    {
        Vector2 endPoint = elementalDistributionBarBg[ElementId].points[1];

        int tempScale = CurrentElementalDistribution[ElementId];
        if(tempScale > 10)
        {
            tempScale = 10;

            GetComponent<NewSkillUnlockAnimation>().ActiveNewSkillTreeHints();
            print("Next Depth unlocked!");
        }

        Vector2 targetPoint = endPoint * tempScale / 10f;

        //elementalDistributionBar[ElementId].points[1] = targetPoint;
        elementalDistributionBar[ElementId].GetComponent<DistributionBarSmoothFollow>().UpdateTargetPosition(targetPoint);
        //elementalDistributionStars[ElementId].localPosition = targetPoint;
        elementalDistributionStars[ElementId].GetComponent<DistributionStarSmoothFollow>().UpdateTargetPosition(targetPoint);

    }


    public void UnlockC2Skills()
    {
        foreach(SkillCardChrisSample card in C2Skills)
        {
            if(card.skillType != SkillType.SkillUpgrade)
            {
                card.isLocked = false;
                card.currentSkillLevel++;
                card.UpdateCardStatus();
            }
            
        }

        CheckAllSkillCanUpgradeOrNot();

    }

    public void CheckAllSkillCanUpgradeOrNot()
    {
        foreach (SkillCardChrisSample card in C1Skills)
        {
            card.CheckCanUpgradeOrNot();

        }

        foreach (SkillCardChrisSample card in C2Skills)
        {
            card.CheckCanUpgradeOrNot();

        }
    }
}
