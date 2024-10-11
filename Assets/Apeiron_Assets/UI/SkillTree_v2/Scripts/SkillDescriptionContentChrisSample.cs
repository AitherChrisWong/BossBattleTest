using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillDescriptionContentChrisSample : MonoBehaviour
{
    public TextMeshProUGUI txtSkillName;
    public TextMeshProUGUI txtSkillDescription;
    public TextMeshProUGUI txtSPCost;
    public TextMeshProUGUI txtElementCost;

    public GameObject[] tags;

    public Image elementIcon;

    public Color[] elementTextColor;

    public NewSkillTreeSample sample;

    // Start is called before the first frame update
    void Start()
    {
        sample = transform.parent.root.GetComponent<NewSkillTreeSample>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePopupInfo(string skillName, string skillDescription)
    {
        txtSkillName.text = skillName;
        txtSkillDescription.text = skillDescription;
        
    }

    public void UpdatePopupCost(int spCost, int skillElementId, int elementCost)
    {
        txtSPCost.text = sample.playerCurrentSp + "/" + spCost;
        

        switch(skillElementId)
        {
            case 0:
                txtElementCost.text = sample.MaxElementalDistribution[0] - sample.CurrentElementalDistribution[0] + "/" + elementCost;
                txtElementCost.color = elementTextColor[0];
                elementIcon.color = elementTextColor[0];
                break;

            case 1:
                txtElementCost.text = sample.MaxElementalDistribution[1] - sample.CurrentElementalDistribution[1] + "/" + elementCost;
                txtElementCost.color = elementTextColor[1];
                elementIcon.color = elementTextColor[1];
                break;

            case 2:
                txtElementCost.text = sample.MaxElementalDistribution[2] - sample.CurrentElementalDistribution[2] + "/" + elementCost;
                txtElementCost.color = elementTextColor[2];
                elementIcon.color = elementTextColor[2];
                break;

            case 3:
                txtElementCost.text = sample.MaxElementalDistribution[3] - sample.CurrentElementalDistribution[3] + "/" + elementCost;
                txtElementCost.color = elementTextColor[3];
                elementIcon.color = elementTextColor[3];
                break;
        }
    }

    public void ResetAllTags()
    {
        foreach(GameObject g in tags)
        {
            g.SetActive(false);
        }
    }

    public void AddTag(int id)
    {
        tags[id].SetActive(true);
    }
}
