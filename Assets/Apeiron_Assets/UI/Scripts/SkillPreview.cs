using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SkillPreview : MonoBehaviour
{
    public float spellRange = 10;
    public Transform targetPosGroup;

    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosGroup)
        {
            /*ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, hit, Mathf.Infinity) 
                {
                transform.position = hit.point;
                }
                //targetPosGroup.position = */
        }
    }
}
