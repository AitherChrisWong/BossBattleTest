using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBackgroupStarOffset : MonoBehaviour
{
    public Material backgroudMat;
    public Transform scrollContent;

    public float scrollSensitive = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 tempOffset = new Vector2(scrollContent.localPosition.x * scrollSensitive, scrollContent.localPosition.y * scrollSensitive);
        backgroudMat.SetVector("_UIScrollOffset", tempOffset);
        backgroudMat.SetVector("_UIScrollOffset_1", tempOffset/5);
    }
}
