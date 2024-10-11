using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SkillType
{
    Skillcard, SkillUpgrade, Perks
}

public enum SkillElement
{
    None, Earth, Water, Fire, Wind
}

public enum SkillUpgradeId
{
    U1, U2, U3
}

public class SkillCardChrisSample : MonoBehaviour
{
    NewSkillTreeSample newSkillTreeSample;

    public SkillType skillType;
    public SkillElement skillElement;
    public SkillUpgradeId skillUpgradeId;

    public bool isBirthSkill;
    public bool isLocked;
    public int maxSkillLevel;
    public int currentSkillLevel;
    

    public Color[] elementDimColors;

    Vector3 refV3 = Vector3.zero;

    [Header("Skill Card Assets")]
    public Image cardBase;
    public GameObject birthLight;
    public GameObject rainbowHalo;
    public GameObject dim;
    public TextMeshProUGUI txtLevel;
    public GameObject upgradeGroup;
    public GameObject skillCardActiveLight;

    public int skillIconWidth;
    public GameObject skillDescription;

    [Header("Skill Upgrade Assets")]
    public GameObject ringLight;
    public GameObject iconCanUpgrade;




    // Start is called before the first frame update
    void Start()
    {
        newSkillTreeSample = transform.root.GetComponent<NewSkillTreeSample>();

        if (isBirthSkill) isLocked = false;


        UpdateCardStatus();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckSkillCardStatus()
    {
        birthLight.SetActive(isBirthSkill);
        birthLight.GetComponent<RawImage>().color = elementDimColors[0];
        rainbowHalo.SetActive(isBirthSkill);
 

        if (isLocked)
        {
            currentSkillLevel = -1;
        }


        if(currentSkillLevel < 0) //set dim alpha: -1 = locked, 0 = not learn, 1 > learned
        {
            dim.SetActive(true);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .95f);
            txtLevel.text = "Locked";
            txtLevel.color = new Color(.5f, .5f, .5f);

            cardBase.color = elementDimColors[(int)skillElement] / 2;

            iconCanUpgrade.SetActive(false);
        }
        else if( currentSkillLevel == 0)
        {
            dim.SetActive(true);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .75f);

            cardBase.color = elementDimColors[(int)skillElement];

            //update level text & color
            string tempElementColor = ColorUtility.ToHtmlStringRGB(elementDimColors[(int)skillElement]);
            tempElementColor = "<#" + tempElementColor + ">";

            txtLevel.text = tempElementColor + currentSkillLevel + "</color>" + "/7"; 
            txtLevel.color = new Color(1, 1, 1, .5f);
        }
        else
        {
            dim.SetActive(false);

            //update level text & color
            string tempElementColor = ColorUtility.ToHtmlStringRGB(elementDimColors[(int)skillElement]);
            tempElementColor = "<#" + tempElementColor + ">";

            txtLevel.text = tempElementColor + currentSkillLevel + "</color>" + "/7";
            txtLevel.color = Color.white;

            //update edge light when learned skill
            birthLight.SetActive(true);
            birthLight.GetComponent<RawImage>().color = elementDimColors[(int)skillElement];
        }
    }

    public void CheckSkillUpgradeStatus()
    {
        //check auto disable or not
        int targetSkillLevel = newSkillTreeSample.SkillCardLevel[(int)skillElement - 1];
        if (targetSkillLevel <= 0)
            isLocked = true;
        else
            isLocked = false;
            

        if (isLocked)
        {
            currentSkillLevel = -1;
        }


        if (currentSkillLevel < 0) //locked
        {
            dim.SetActive(true);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .95f);

            ringLight.SetActive(false);
            iconCanUpgrade.SetActive(false);
        }
        else if (currentSkillLevel == 0) //not learn
        {
            dim.SetActive(true);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .75f);

            ringLight.SetActive(true);
            ringLight.GetComponent<Image>().color = elementDimColors[0];
        }
        else //learned
        {
            dim.SetActive(false);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .95f);

            ringLight.SetActive(true);
            ringLight.GetComponent<Image>().color = elementDimColors[(int)skillElement];
        }
    }

    public void CheckPerksStatus()
    {
        birthLight.SetActive(isBirthSkill);
        birthLight.GetComponent<RawImage>().color = elementDimColors[0];
        rainbowHalo.SetActive(isBirthSkill);


        if (isLocked)
        {
            currentSkillLevel = -1;
        }


        if (currentSkillLevel < 0) //set dim alpha: -1 = locked, 0 = not learn, 1 > learned
        {
            dim.SetActive(true);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .95f);

            txtLevel.text = "Locked";
            txtLevel.color = new Color(.5f, .5f, .5f);

            ringLight.SetActive(false);
            iconCanUpgrade.SetActive(false);

        }
        else if (currentSkillLevel == 0)
        {
            dim.SetActive(true);
            dim.GetComponent<Image>().color = new Color(0, 0, 0, .75f);

            //update level text & color
            string tempElementColor = ColorUtility.ToHtmlStringRGB(elementDimColors[(int)skillElement]);
            tempElementColor = "<#" + tempElementColor + ">";

            txtLevel.text = tempElementColor + currentSkillLevel + "</color>" + "/5";
            txtLevel.color = new Color(1, 1, 1, .5f);

            ringLight.SetActive(false);
            //ringLight.GetComponent<Image>().color = elementDimColors[0];
        }
        else
        {
            dim.SetActive(false);

            //update level text & color
            string tempElementColor = ColorUtility.ToHtmlStringRGB(elementDimColors[(int)skillElement]);
            tempElementColor = "<#" + tempElementColor + ">";

            txtLevel.text = tempElementColor + currentSkillLevel + "</color>" + "/7";
            txtLevel.color = Color.white;

            //update edge light when learned skill
            birthLight.GetComponent<RawImage>().color = elementDimColors[(int)skillElement];

            ringLight.SetActive(true);
            ringLight.GetComponent<Image>().color = elementDimColors[(int)skillElement];
        }
    }


    public void ButtonHoverAnimation()
    {
        Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f);
        //transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref refV3, .1f);
        transform.localScale = targetScale;

        ShowDescription();
    }

    public void ShowDescription()
    {
        skillDescription.SetActive(true);
        RectTransform rect = skillDescription.GetComponent<RectTransform>();

        rect.pivot = new Vector2(0, .5f);
        rect.localPosition = Camera.main.WorldToScreenPoint(GetComponent<RectTransform>().position);
        rect.localPosition -= new Vector3(Screen.width / 2, Screen.height / 2);
        rect.localPosition -= new Vector3(0, 0, 100);
        rect.localPosition += new Vector3(skillIconWidth, 0, 0);


        //check out of screen
        if(rect.localPosition.x > 540)
        {
            //flip X
            rect.pivot = new Vector2(1, .5f);
            rect.localPosition = Camera.main.WorldToScreenPoint(GetComponent<RectTransform>().position);
            rect.localPosition -= new Vector3(Screen.width / 2, Screen.height / 2);
            rect.localPosition -= new Vector3(0, 0, 100);
            rect.localPosition -= new Vector3(skillIconWidth, 0, 0);
        }

        if (rect.localPosition.y > 180)
        {
            //fix Y
            rect.localPosition = new Vector2(rect.localPosition.x, 180);
        }

        if (rect.localPosition.y < -190)
        {
            //fix Y
            rect.localPosition = new Vector2(rect.localPosition.x, -190);
        }

        //update description contents
        SkillDescriptionContentChrisSample content = skillDescription.GetComponent<SkillDescriptionContentChrisSample>();
        content.ResetAllTags();

        switch(skillElement)
        {
            case SkillElement.Earth:
                content.AddTag(0); break;

            case SkillElement.Water:
                content.AddTag(1); break;

            case SkillElement.Fire:
                content.AddTag(2); break;

            case SkillElement.Wind:
                content.AddTag(3); break;
        }

        switch (skillType)
        {
            case SkillType.Skillcard:
                content.AddTag(4); break;

            case SkillType.SkillUpgrade:
                content.AddTag(5); break;

            case SkillType.Perks:
                content.AddTag(6); break;
        }

        content.UpdatePopupCost(1, (int)skillElement-1, 1);
    }

    public void ButtonIdleAnimation()
    {
        Vector3 targetScale = new Vector3(1f, 1f, 1f);
        transform.localScale = targetScale;

        skillDescription.SetActive(false);
    }

    public void SkillCardUpgrade()
    {
        if(!isLocked)
        {
            if(newSkillTreeSample.playerCurrentSp > 1)
            {
                if (currentSkillLevel < maxSkillLevel)
                {
                    currentSkillLevel++;
                    newSkillTreeSample.playerCurrentSp--;
                    upgradeGroup.SetActive(true);
                    upgradeGroup.GetComponent<Animator>().Play("SkillCardUpgrade", -1, 0);

                    UpdateCardStatus();
                    if (skillType == SkillType.Skillcard && currentSkillLevel == 1)
                    {
                        newSkillTreeSample.UnlockSkillUpgrade((int)skillElement);
                        skillCardActiveLight.SetActive(true);
                    }

                    newSkillTreeSample.CheckAllSkillCanUpgradeOrNot();
                    newSkillTreeSample.UpdateElementValue();

                    ShowDescription();
                }
            }
            else
            {
                //not enough cost
            }
            
            

        }
    }

    public void UpdateCardStatus()
    {
        switch (skillType)
        {
            case SkillType.Skillcard:
                newSkillTreeSample.UpdateSkillCardLevel((int)skillElement - 1, currentSkillLevel);


                CheckSkillCardStatus();
                newSkillTreeSample.CheckSkillUpgradeLine();
                break;

            case SkillType.SkillUpgrade:
                switch(skillElement)
                {
                    case SkillElement.Earth:
                        newSkillTreeSample.EarthSkillUpgradeLevel[(int)skillUpgradeId] = currentSkillLevel;
                        break;

                    case SkillElement.Water:
                        newSkillTreeSample.WaterSkillUpgradeLevel[(int)skillUpgradeId] = currentSkillLevel;
                        break;

                    case SkillElement.Fire:
                        newSkillTreeSample.FireSkillUpgradeLevel[(int)skillUpgradeId] = currentSkillLevel;
                        break;

                    case SkillElement.Wind:
                        newSkillTreeSample.WindSkillUpgradeLevel[(int)skillUpgradeId] = currentSkillLevel;
                        break;

                }

                CheckSkillUpgradeStatus();
                newSkillTreeSample.CheckSkillUpgradeLine();
                break;

            case SkillType.Perks:
                newSkillTreeSample.UpdatePerksLevel((int)skillElement - 1, currentSkillLevel);
                CheckPerksStatus();
                break;

        }
    }

    public void CheckCanUpgradeOrNot()
    {
        iconCanUpgrade.SetActive(false);

        if (!isLocked && currentSkillLevel < maxSkillLevel)
        {
            switch (skillElement)
            {
                case SkillElement.Earth:
                    if (newSkillTreeSample.CurrentElementalDistribution[0] < newSkillTreeSample.MaxElementalDistribution[0])
                        iconCanUpgrade.SetActive(true);
                    break;

                case SkillElement.Water:
                    if (newSkillTreeSample.CurrentElementalDistribution[1] < newSkillTreeSample.MaxElementalDistribution[1])
                        iconCanUpgrade.SetActive(true);
                    break;

                case SkillElement.Fire:
                    if (newSkillTreeSample.CurrentElementalDistribution[2] < newSkillTreeSample.MaxElementalDistribution[2])
                        iconCanUpgrade.SetActive(true);
                    break;

                case SkillElement.Wind:
                    if (newSkillTreeSample.CurrentElementalDistribution[3] < newSkillTreeSample.MaxElementalDistribution[3])
                        iconCanUpgrade.SetActive(true);
                    break;
            }
        }
    }
}
