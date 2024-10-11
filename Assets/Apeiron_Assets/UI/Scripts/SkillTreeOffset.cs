using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeOffset : MonoBehaviour
{
    public Transform content;


    ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        float tempOffset = (1 - scrollRect.verticalScrollbar.value) * 48;
        content.transform.localPosition = new Vector2(tempOffset, content.transform.localPosition.y);
    }
}
