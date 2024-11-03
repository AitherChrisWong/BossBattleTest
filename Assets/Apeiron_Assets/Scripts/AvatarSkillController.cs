using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public enum vfxType { vfx, buff, outerProjectile };


public class AvatarSkillController : MonoBehaviour
{
    public vfxType _vfxType;

    PVEBattleController battleController;

    public PlayableDirector timeline;
    public AvatarBasicMovement avatarBasicMovement;

    public GameObject additionTimeline;

    public float moveSpeed;

    [HideInInspector]
    public Transform targetPos;

    public Transform casterGroup;
    public Transform targetGroup;
    Vector3 tempPos;

    Vector3 startPos;
    Transform casterPos;
    Quaternion startRotation;

    // Start is called before the first frame update
    private void Start()
    {
        battleController = GameObject.Find("PVE Battle").GetComponent<PVEBattleController>();
        Animator targetAnim = battleController.playerTeam[0].transform.Find("skin").GetChild(0).GetComponent<Animator>();
        Animator skinAnim = battleController.playerTeam[0].transform.Find("skin").GetComponent<Animator>();
        avatarBasicMovement = battleController.playerTeam[0].GetComponent<AvatarBasicMovement>();

        foreach (var bind in timeline.playableAsset.outputs)
        {
            if(bind.streamName == "Caster")
            {
                timeline.SetGenericBinding(bind.sourceObject, targetAnim);
            }

            if (bind.streamName == "Caster Skin")
            {
                timeline.SetGenericBinding(bind.sourceObject, skinAnim);
            }

            if (bind.streamName == "Skill Control Track")
            {
                timeline.SetGenericBinding(bind.sourceObject, avatarBasicMovement);
            }
        }


        casterPos = avatarBasicMovement.skin.transform;


        switch(_vfxType)
        {
            case vfxType.vfx:
                startPos = avatarBasicMovement.transform.position;
                startRotation = avatarBasicMovement.skin.rotation;
                break;

            case vfxType.buff:
                startPos = avatarBasicMovement.transform.position;
                startRotation = avatarBasicMovement.skin.rotation;
                break;

            case vfxType.outerProjectile:
                startPos = transform.position;
                startRotation = avatarBasicMovement.skin.rotation;
                break;
        }

        if(targetGroup)
        {
            tempPos = targetPos.position;
            tempPos.y = 0;
        }
        
        

        avatarBasicMovement.skillLookPos = tempPos;
        avatarBasicMovement.skillMovePosition = tempPos;
    }

    public void FixedUpdate()
    {


        switch (_vfxType)
        {
            case vfxType.vfx:
                transform.position = startPos;
                transform.rotation = startRotation;
                break;

            case vfxType.buff:
                transform.position = startPos;
                transform.rotation = startRotation;
                break;

            case vfxType.outerProjectile:
                transform.position = Vector3.Lerp(transform.position, targetPos.position, moveSpeed);
                break;
        }



        if(casterGroup != null)
        {
            casterGroup.position = casterPos.position;
            casterGroup.rotation = casterPos.rotation;
        }
        
        if(targetGroup != null)
        {
            targetGroup.position = tempPos;
        }



    }


    public void SignalAvatarSkillEnd()
    {
        gameObject.SetActive(false);
        Destroy(this.gameObject, 1);
    }

    public void SignalCameraShakeSmall()
    {
        CameraSmoothFollow cam = GameObject.Find("Camera Pos").GetComponent<CameraSmoothFollow>();

        cam.camShakeAnim.Play("cameraShakeSmall", -1, 0);
    }

    public void SignalCameraShakeLarge()
    {
        CameraSmoothFollow cam = GameObject.Find("Camera Pos").GetComponent<CameraSmoothFollow>();

        cam.camShakeAnim.Play("cameraShakeLarge", -1, 0);
    }

    public void SignalCreateBuff()
    {
        GameObject tempVFX = Instantiate(additionTimeline, battleController.playerTeam[0].transform.Find("skin"));
        tempVFX.transform.localPosition = Vector3.zero;
        tempVFX.transform.localRotation = Quaternion.identity;
        tempVFX.transform.localScale = Vector3.one;
    }

    public void SignalCreateProjectile()
    {
        GameObject tempVFX = Instantiate(additionTimeline);

        //3024 force follow player
        tempVFX.GetComponent<AvatarSkillController>().targetPos = battleController.playerTeam[0].transform;

        tempVFX.transform.position = targetGroup.position;
        tempVFX.transform.localRotation = Quaternion.identity;
        tempVFX.transform.localScale = Vector3.one;
    }
}
