using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkillControlMixerBehaviour : PlayableBehaviour
{
    private AvatarBasicMovement avatarBasicMovement;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        avatarBasicMovement = (AvatarBasicMovement)playerData;

        if (avatarBasicMovement == null)
        {
            Debug.Log("avatar not found");
            return;
        }
            

        int inputCount = playable.GetInputCount();

        bool tempIsSkillCasting = false;
        bool tempIsSkillMovable = false;
        bool tempIsSkillDashable = false;
        bool tempIsSkillForveMove = false;
        float tempSkillForceMoveSpeed = 0;

        float totalWeight = 0;

        for(int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<SkillControlBehaviour> inputPlayable = (ScriptPlayable<SkillControlBehaviour>)playable.GetInput(i);
            SkillControlBehaviour behaviour  = inputPlayable.GetBehaviour();

            if(inputWeight >= 1)
            {
                tempIsSkillCasting = behaviour.isSkillCasting;
                tempIsSkillMovable = behaviour.isSkillMovable;
                tempIsSkillDashable = behaviour.isSkillDashable;
                tempIsSkillForveMove = behaviour.isSkillForceMove;
                tempSkillForceMoveSpeed = behaviour.skillForceMoveSpeed;
            }


            totalWeight += inputWeight;
        }

        if(avatarBasicMovement.isCastingSkill)
        {
            avatarBasicMovement.isCastingSkill = tempIsSkillCasting;
            avatarBasicMovement.isSkillMovable = tempIsSkillMovable;
            avatarBasicMovement.isSkillDashable = tempIsSkillDashable;
            avatarBasicMovement.isSkillForceMove = tempIsSkillForveMove;
            avatarBasicMovement.skillMoveSpeed = tempSkillForceMoveSpeed;
        }
        


        //Debug.Log("isCastingSkill:" + tempIsSkillCasting);
        //Debug.Log("isSkillMovable:" + tempIsSkillMovable);
        //Debug.Log("isSkillForceMove:" + tempIsSkillForveMove);

        //base.ProcessFrame(playable, info, playerData);


    }
}
