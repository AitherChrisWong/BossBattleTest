using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeBaseChrisSample : MonoBehaviour
{
    NewSkillUnlockAnimation newSkillUnlockAnimation;
    NewSkillTreeSample newSkillTreeSample;

    // Start is called before the first frame update
    void Start()
    {
        newSkillUnlockAnimation = transform.parent.root.GetComponent<NewSkillUnlockAnimation>();
        newSkillTreeSample = transform.parent.root.GetComponent<NewSkillTreeSample>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScrollUnlockEvent()
    {
        newSkillUnlockAnimation.UnlockScroll();
    }

    public void UnlockSkillsEvent()
    {
        newSkillTreeSample.UnlockC2Skills();
    }
}
