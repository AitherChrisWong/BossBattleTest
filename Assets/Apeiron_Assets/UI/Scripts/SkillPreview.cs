using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SkillPreview : MonoBehaviour
{
    public Vector3 spellOffset = Vector3.zero;

    public float spellRange = 6;

    public Transform spellRangeGroup;
    public Transform targetPosGroup;

    public LayerMask layer;
    RaycastHit hit;
    Vector3 ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        spellRangeGroup.localScale = new Vector3(spellRange, spellRange, spellRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosGroup)
        {
            //ray = );
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer))
                {
                Vector3 targetPos = hit.point;
                targetPos += spellOffset;

                targetPosGroup.position = targetPos;

                //clamp max distance
                float tempDis = Vector3.Distance(transform.position, targetPos);

                if(tempDis <= spellRange)
                {
                    //targetPosGroup.position = targetPos;
                }else
                {
                    float tempProportion = spellRange/ tempDis;
                    targetPosGroup.localPosition *= tempProportion;

                }

                }



        }
    }
}
