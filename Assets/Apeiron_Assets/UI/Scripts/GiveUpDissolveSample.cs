using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GiveUpDissolveSample : MonoBehaviour
{
    public Image dissolveSprite;

    public AnimationCurve dissolveCurve;
    public float durationTime;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime < durationTime)
        {
            dissolveSprite.material.SetFloat("_Disslove", dissolveCurve.Evaluate(currentTime));
            currentTime += Time.deltaTime/ durationTime;
        }
    }


}
