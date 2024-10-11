using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSkillUnlockAnimation : MonoBehaviour
{
    public bool isC2Locked = true;

    public GameObject VFXBottomLeftUnlock;

    public ScrollRect scrollRect;
    bool isForceZoom;

    public bool isNewUnlockC2;

    public RectTransform scrollContent;

    //public Vector3 C2Pos;
    //public Vector3 triggerRange;

    public Vector3 C2ZoomPos;

    public Animator C2baseBgAnim;

    Vector3 refV3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNewUnlockC2)
        {
            if (scrollContent.anchoredPosition.y > 1550)
            {
                //scrollContent.anchoredPosition = C2ZoomPos;
                isNewUnlockC2 = false;

                LockScroll();
                C2baseBgAnim.enabled = true;
                C2baseBgAnim.Play("SkillTreeUnlock");

                print("C2 Zoom");
            }
        }

        if(isForceZoom)
        {
            scrollContent.anchoredPosition = Vector3.SmoothDamp(scrollContent.anchoredPosition, C2ZoomPos, ref refV3, .1f);
        }

    }

    public void ActiveNewSkillTreeHints()
    {
        if(isC2Locked)
        {
            VFXBottomLeftUnlock.SetActive(true);
            isNewUnlockC2 = true;
            isC2Locked = false;
        }

    }

   

    void LockScroll()
    {
        isForceZoom = true;
        VFXBottomLeftUnlock.SetActive(false);

        scrollRect.horizontal = false;
        scrollRect.vertical = false;

    }

    public void UnlockScroll()
    {
        isForceZoom = false;
        scrollRect.horizontal = true;
        scrollRect.vertical = true;
    }
}
