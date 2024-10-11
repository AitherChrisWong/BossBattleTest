using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CradleBubbleGround : MonoBehaviour
{
    public Material cracleBubbleGround;
    Transform playerTrans;

    public bool isStartWiggle = false;
    public float power = 2;
    public AnimationCurve wiggleCurve;
    float wiggleCurveTime = 0;
    public float wiggleSpeed = 1;

    float currentWigglePower = 0;

    public string materialVectorName = "_PlayerPosition";
    public string materialVectorName2 = "_WigglePower";

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        cracleBubbleGround.SetVector(materialVectorName, playerTrans.position);



        if(isStartWiggle)
        {

            if(wiggleCurveTime >= 1)
            {
                wiggleCurveTime = 0;
                
            }

            currentWigglePower = (wiggleCurve.Evaluate(wiggleCurveTime) - 0.5f) * -power;
            wiggleCurveTime += Time.deltaTime * wiggleSpeed;


            if (wiggleCurveTime >= 1)
            {
                isStartWiggle = false;

            }

            cracleBubbleGround.SetFloat(materialVectorName2, currentWigglePower);
        }
    }

    public void SetIsWiggle(bool isTrue)
    {

        isStartWiggle = isTrue;
        wiggleCurveTime = 0;
    }
}
