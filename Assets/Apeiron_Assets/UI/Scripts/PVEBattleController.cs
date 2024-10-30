using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PVEBattleController : MonoBehaviour
{
    public bool isSlowMode;
    public Volume slowModePostProcessing;



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
    }
}
