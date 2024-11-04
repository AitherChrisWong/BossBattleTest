using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class PVEBattleController : MonoBehaviour
{
    public bool isSlowMode;
    public Volume slowModePostProcessing;

    public GameObject[] playerTeam;


    [Header("Mana")]
    public TextMeshProUGUI txtMana;
    public Transform manaProgressBar;
    public Transform manaBar;

    public int maxMana = 10;
    public int curMana = 0;

    public float curManaProgress;
    public float manaGenSpeed;

    public GameObject vfxUseMana;
    public Transform[] manaGridPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowMode)
        {
            slowModePostProcessing.weight = Mathf.Lerp(slowModePostProcessing.weight, 1, .2f);

            Time.timeScale = Mathf.Lerp(Time.timeScale, .02f, .05f);
        }
        else
        {
            slowModePostProcessing.weight = Mathf.Lerp(slowModePostProcessing.weight, 0, .2f);

            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, .5f);
        }

        AutoGenMana();
    }

    void AutoGenMana()
    {
        if(curManaProgress < maxMana)
        {
            curManaProgress += manaGenSpeed * Time.deltaTime;
            curMana = (int)Mathf.Floor(curManaProgress);

            txtMana.text = curMana.ToString();
            manaProgressBar.localScale = new Vector3(curManaProgress / maxMana, 1, 1);
            manaBar.localScale = new Vector3(curMana*1f / maxMana, 1, 1);
            
        }
        
    }

    public void UseMana(int value)
    {
        curManaProgress -= value;

        for (int i = 0; i < value; i++)
        {
            if(curMana - i >= 0)
            {
                GameObject tempVfx = Instantiate(vfxUseMana, vfxUseMana.transform.parent);
                tempVfx.SetActive(true);
                tempVfx.transform.SetParent(manaGridPos[curMana - i - 1]);
                tempVfx.transform.localPosition = Vector3.zero;
                tempVfx.transform.localRotation = Quaternion.identity;

                Destroy(tempVfx, 1);
            }
            

        }
    }
}
