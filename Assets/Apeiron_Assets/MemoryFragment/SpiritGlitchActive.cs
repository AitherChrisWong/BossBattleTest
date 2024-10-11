using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritGlitchActive : MonoBehaviour
{
    public Material mtl;

    public bool isStartAnim;

    public AnimationCurve alphaCurve;
    public float speed = 1;
    float curTime;


    // Start is called before the first frame update
    void Start()
    {
        mtl = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStartAnim)
        {
            if(curTime < 1)
            {
                mtl.SetFloat("_OverallGlitchRate", alphaCurve.Evaluate(curTime));
            }else
            {
                isStartAnim = false;
            }

            curTime += Time.deltaTime * speed;

        }
    }

    public void StartGlitch()
    {
        curTime = 0;
        isStartAnim = true;
    }


}
