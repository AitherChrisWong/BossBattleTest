using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AvatarSkillController : MonoBehaviour
{
    PVEBattleController battleController;

    public PlayableDirector timeline;
    public AvatarBasicMovement avatarBasicMovement;

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

        foreach (var bind in timeline.playableAsset.outputs)
        {
            if(bind.streamName == "Caster")
            {
                timeline.SetGenericBinding(bind.sourceObject, targetAnim);
            }

            if (bind.streamName == "Skill Control Track")
            {
                timeline.SetGenericBinding(bind.sourceObject, avatarBasicMovement);
            }
        }


        casterPos = avatarBasicMovement.skin.transform;

        startPos = avatarBasicMovement.transform.position;
        startRotation = avatarBasicMovement.skin.rotation;

        tempPos = targetPos.position;
        tempPos.y = 0;
        

        avatarBasicMovement.skillLookPos = tempPos;
        avatarBasicMovement.skillMovePosition = tempPos;
    }

    public void Update()
    {
        transform.position = startPos;
        transform.rotation = startRotation;

        casterGroup.position = casterPos.position;
        casterGroup.rotation = casterPos.rotation;

        targetGroup.position = tempPos;
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
}
