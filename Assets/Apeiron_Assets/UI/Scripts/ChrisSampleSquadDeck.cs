using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChrisSampleSquadDeck : MonoBehaviour
{
    public List<int> manaCounts;
    public List<int> manaPreviews;

    public TextMeshProUGUI[] txtManaCounts;
    public RectTransform[] manaCountBars;
    public RectTransform[] manaCountBarsRed;
    public RectTransform[] manaCountBarsWhite;
    public float barDefaultWidth;
    public float barDefaultHeight;

    // Start is called before the first frame update
    void Start()
    {
        barDefaultWidth = manaCountBars[0].rect.width;
        barDefaultHeight = manaCountBars[0].rect.height;

        UpdateSquadMana(manaCounts.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSquadMana(int[] counts)
    {
        for (int i = 0; i < manaCounts.Count; i++)
        {
            manaCounts[i] = counts[i];
        }

        int highestCount = Mathf.Max(manaCounts.ToArray());

        for (int i = 0; i< manaCounts.Count;i++)
        {
            
            txtManaCounts[i].text = manaCounts[i].ToString();
            txtManaCounts[i].color = Color.white;


            manaCountBars[i].anchorMin = new Vector2(.5f, 0);
            manaCountBars[i].anchorMax = new Vector2(.5f, 0);


            float tempX = barDefaultWidth;
            float tempY = (manaCounts[i] * 1f / highestCount) * barDefaultHeight;

            //just make sure not divisor to 0 
            if (manaCounts[i] ==0 || highestCount ==0)
            {
                tempY = .1f;
            }

            manaCountBars[i].sizeDelta = new Vector2(tempX, tempY);

            //reset white bar scale
            manaCountBarsWhite[i].transform.localScale = new Vector3(1, 1, 1);


            // 0 mana cost bar alpha = .75f
            if (manaCounts[i] == 0 || manaPreviews[i] == 0)
            {
                manaCountBars[i].transform.parent.parent.GetComponent<CanvasGroup>().alpha = .75f;
            }
            else
            {
                manaCountBars[i].transform.parent.parent.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
    }

    public void PreviewSquadMana(int[] counts, int[] previews, Color teamColor)
    {
        for (int i = 0; i < manaPreviews.Count; i++)
        {
            manaPreviews[i] = previews[i];
        }

        int highestCount = Mathf.Max(manaPreviews.ToArray());

        for (int i = 0; i < manaPreviews.Count; i++)
        {
            txtManaCounts[i].text = manaPreviews[i].ToString();


            manaCountBars[i].anchorMin = new Vector2(.5f, 0);
            manaCountBars[i].anchorMax = new Vector2(.5f, 0);


            float tempX = barDefaultWidth;
            float tempY = (manaPreviews[i] * 1f / highestCount) * barDefaultHeight;
            tempY++;
            //manaCountBars[i].sizeDelta = new Vector2(tempX, tempY);

            if (manaPreviews[i] > manaCounts[i])
            {
                txtManaCounts[i].color = Color.white;

                tempY = (manaPreviews[i] * 1f / highestCount) * barDefaultHeight;
                tempY++;
                manaCountBars[i].sizeDelta = new Vector2(tempX, tempY);

                float tempWhiteBarScaleY = 0;
                tempWhiteBarScaleY = (manaCounts[i] * 1f) / manaPreviews[i] + .01f;

                manaCountBarsRed[i].gameObject.SetActive(false);
                manaCountBarsWhite[i].transform.localScale = new Vector3(1, tempWhiteBarScaleY, 1);

            }
            else if(manaPreviews[i] < manaCounts[i]) // try to take out apostle from battle field
            {
                txtManaCounts[i].color = teamColor;
                txtManaCounts[i].text = manaCounts[i].ToString();

                float tempWhiteBarScaleY = 0;
                tempWhiteBarScaleY =  manaPreviews[i] / (manaCounts[i] * 1f);

                //just make sure not divisor to 0 
                if (manaCounts[i] == 0 || manaPreviews[i] == 0)
                {
                    tempWhiteBarScaleY = 0;
                }

                //manaCountBarsRed <- is for show team color
                manaCountBarsRed[i].gameObject.SetActive(true);
                manaCountBarsRed[i].GetComponent<Image>().color = teamColor;
                manaCountBarsWhite[i].transform.localScale = new Vector3(1, tempWhiteBarScaleY, 1);

            }
            else
            {
                txtManaCounts[i].color = Color.white;
                //tempY = (manaPreviews[i] * 1f / highestCount) * barDefaultHeight;
                //manaCountBars[i].sizeDelta = new Vector2(tempX, tempY);

            }

            // 0 mana cost bar alpha = .75f
            if(manaCounts[i] == 0)
            {
                manaCountBars[i].transform.parent.parent.GetComponent<CanvasGroup>().alpha = .75f;
            }else
            {
                manaCountBars[i].transform.parent.parent.GetComponent<CanvasGroup>().alpha = 1;
            }
            
        }
    }
}
