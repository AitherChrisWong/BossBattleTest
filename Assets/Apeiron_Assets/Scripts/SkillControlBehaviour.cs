using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;

[Serializable]
public class SkillControlBehaviour : PlayableBehaviour
{

    [Serializable]
    public struct SkillState
    {
        
    }

    public bool isSkillCasting = true;
    public bool isSkillMovable = false;
    public bool isSkillDashable = false;
    public bool isSkillForceMove = false;
    public float skillForceMoveSpeed = 0;
    
    
}
